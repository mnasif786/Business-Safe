using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Contracts;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Controllers.AutoMappers;
using BusinessSafe.WebSite.Controllers.Helpers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.ViewModels;
using FluentValidation;
using BusinessSafe.WebSite.ServiceCompositionGateways;
using FluentValidation.Results;
using NHibernate.Linq;

namespace BusinessSafe.WebSite.Areas.Sites.Controllers
{
    public class SitesController : BaseController
    {
        private readonly ISiteStructureViewModelFactory _siteStructureViewModelFactory;
        private readonly ISiteDetailsViewModelFactory _siteDetailsViewModelFactory;
        private readonly ISiteService _siteService;
        private readonly ISiteUpdateCompositionGateway _siteUpdateCompositionGateway;
        private readonly ISiteGroupService _siteGroupService;
        public const string UpdateSiteIdKey = "UpdatedSiteId";
        public const string MessageKey = "Message";
        public const string DelinkSiteAddressValidationMessageKey = "DelinkSiteExceptionMessage";

        public SitesController(
            ISiteStructureViewModelFactory siteStructureViewModelFactory,
            ISiteDetailsViewModelFactory siteDetailsViewModelFactory,
            ISiteService siteService,
            ISiteUpdateCompositionGateway siteUpdateCompositionGateway, ISiteGroupService siteGroupService)
        {
            _siteUpdateCompositionGateway = siteUpdateCompositionGateway;
            _siteGroupService = siteGroupService;
            _siteStructureViewModelFactory = siteStructureViewModelFactory;
            _siteDetailsViewModelFactory = siteDetailsViewModelFactory;
            _siteService = siteService;
        }

        [PermissionFilter(Permissions.ViewSiteDetails)]
        public PartialViewResult GetLinkedSiteDetails(long companyId, long bsoSiteId)
        {
            var viewModel = _siteDetailsViewModelFactory
                                        .WithId(bsoSiteId)
                                        .WithClientId(companyId)
                                        .GetViewModel();

            return PartialView("_SiteDetails", viewModel);
        }

        [PermissionFilter(Permissions.ViewSiteDetails)]
        public PartialViewResult GetUnlinkedSiteDetails(long companyId, long peninsulaSiteId)
        {
            var viewModel = _siteDetailsViewModelFactory
                                        .WithSiteId(peninsulaSiteId)
                                        .WithClientId(companyId)
                                        .GetViewModel();

            return PartialView("_SiteDetails", viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddSiteDetails)]
        public ActionResult CresteSite(SiteDetailsViewModel siteDetailsViewModel)
        {
            return SaveSite(siteDetailsViewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditSiteDetails)]
        public ActionResult UpdateSite(SiteDetailsViewModel siteDetailsViewModel)
        {
            return SaveSite(siteDetailsViewModel);
        }

        private ActionResult SaveSite(SiteDetailsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var structureViewModel = GetSiteStructureViewModel(viewModel);

                return View("Index", structureViewModel);
            }

            var request = new SiteAddressRequestMapper().Map(viewModel, CurrentUser.UserId);

            try
            {
                if (viewModel.SiteStructureId != default(long))
                {
                    var siteBeforeUpdate = _siteService.GetByIdAndCompanyId(viewModel.SiteStructureId,
                                                                            viewModel.ClientId);
                    viewModel.NameBeforeUpdate = siteBeforeUpdate.Name;
                }

                var validationMessages = _siteService.ValidateSiteCloseRequest(request);

                if (!validationMessages.Any())
                {
                    bool isSiteOpenRequest , isSiteClosedRequest;
                    //Site can be opened, closed or remains with no change
                    _siteService.GetSiteOpenClosedRequest(request, out isSiteOpenRequest, out isSiteClosedRequest);
                    request.IsSiteOpenRequest = isSiteOpenRequest;
                    request.IsSiteClosedRequest = isSiteClosedRequest;
                    if (isSiteOpenRequest)
                    {
                        viewModel.SiteStatusUpdated = SiteStatus.Open;
                    }
                    else 
                        if(isSiteClosedRequest)
                        {
                            viewModel.SiteStatusUpdated = SiteStatus.Close;
                        }
                        else
                        {
                            viewModel.SiteStatusUpdated = SiteStatus.NoChange;
                        }

                    _siteService.CreateUpdate(request);
                    TempData[UpdateSiteIdKey] = viewModel.SiteId;

                    viewModel.ActioningUserName = CurrentUser.FullName;
                    
                    var addressInformationChange = _siteUpdateCompositionGateway.SendEmailIfRequired(viewModel);

                    SetSavedSiteSuccessMessage(viewModel, addressInformationChange);
                }
                else
                {

                    var faultMessages = new List<ValidationFailure>();
                    foreach (var validationMessage in validationMessages)
                    {
                        faultMessages.Add(new ValidationFailure("SiteClosed", validationMessage));
                    }

                    throw new ValidationException(faultMessages);
                }
            }
            catch (ValidationException e)
            {
                ModelState.Update(e);
                var structureViewModel = GetSiteStructureViewModel(viewModel);
                return View("Index", structureViewModel);
            }
            return RedirectToAction("Index", "SitesStructure");
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditSiteDetails)]
        public ActionResult DelinkSite(DelinkSiteViewModel delinkSiteViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData[DelinkSiteAddressValidationMessageKey] = "Unable to delink site, request is not valid";
            }

            var delinkSiteRequest = new DelinkSiteRequestMapper().Map(delinkSiteViewModel);
            _siteService.DelinkSite(delinkSiteRequest);

            TempData[MessageKey] = "Delinked Site Successfully";
            return RedirectToAction("Index", "SitesStructure");
        }

        [HttpGet]
        [PermissionFilter(Permissions.ViewSiteDetails)]
        public JsonResult GetChildSitesBySiteId(long companyId, long siteId)
        {
            var sites = _siteService.GetByCompanyIdNotIncluding(companyId, siteId);
            var sitesJson = sites.Select(s => new { name = s.Name, value = s.Id });

            return Json(sitesJson, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSites(string filter, long companyId, int pageLimit)
        {
            var employees = _siteService.Search(new SearchSitesRequest
            {
                CompanyId = companyId,
                NameStartsWith = filter,
                AllowedSiteIds = CurrentUser.GetSitesFilter(),
                PageLimit = pageLimit
            });

            var result = employees.Select(AutoCompleteViewModel.ForSite).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSitesAndSiteGroups(long companyId)
        {
            var sites = _siteService.Search(new SearchSitesRequest
            {
                CompanyId = companyId,
                AllowedSiteIds = CurrentUser.GetSitesFilter(),
            });

            var siteGroup = _siteGroupService.GetByCompanyId(CurrentUser.CompanyId);

            var result = sites.Select(x => new SiteAndSiteGroupViewModel() {Id = x.Id, Name = x.Name, IsSiteGroup = false})
                .OrderBy(x=> x.Name)
                .ToList();

            result.AddRange(siteGroup.Select(x => new SiteAndSiteGroupViewModel() {Id = x.Id, Name = x.Name, IsSiteGroup = true})
                .OrderBy(x => x.Name)
                .ToList() );

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private SiteStructureViewModel GetSiteStructureViewModel(SiteDetailsViewModel siteDetailsViewModel)
        {
            var viewModel = _siteStructureViewModelFactory
                .WithClientId(CurrentUser.CompanyId)
                .WithSiteDetailsViewModel(siteDetailsViewModel)
                .WithValidationError(true)
                .DisplaySiteDetails()
                .GetViewModel();
            return viewModel;
        }

        private void SetSavedSiteSuccessMessage(SiteDetailsViewModel siteDetailsViewModel, bool onlyAddressDetailInformationIsChanged)
        {
            const string ifOnlyDetailInfoChangedMessage = "A member of Client Services has been notified and will be in contact in due course";
            const string ifAddressDetailInfoChangedMessage = "Your changes have been successfully saved";

            TempData[MessageKey] = siteDetailsViewModel.SiteStructureId == 0
                                       ? "Your new Site has been successfully created"
                                       : (onlyAddressDetailInformationIsChanged ? ifOnlyDetailInfoChangedMessage : ifAddressDetailInfoChangedMessage);
        }
    }

    public class SiteAndSiteGroupViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsSiteGroup { get; set; }
        
    }
}
