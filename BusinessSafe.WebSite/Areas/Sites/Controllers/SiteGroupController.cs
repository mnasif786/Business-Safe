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
using FluentValidation;

namespace BusinessSafe.WebSite.Areas.Sites.Controllers
{
    public class SiteGroupController : BaseController
    {
        private readonly ISiteGroupViewModelFactory _siteGroupViewModelFactory;
        private readonly ISiteStructureViewModelFactory _siteStructureViewModelFactory;
        private readonly ISiteGroupService _siteGroupService;
        public const string UpdateSiteGroupIdKey = "UpdateSiteGroupIdKey";
        public const string DeleteSiteGroupValidationMessageKey = "DeleteSiteGroupExceptionMessage";
        public const string MessageKey = "Message";

        public SiteGroupController(ISiteGroupViewModelFactory siteGroupViewModelFactory, ISiteGroupService siteGroupService, ISiteStructureViewModelFactory siteStructureViewModelFactory)
        {
            _siteGroupViewModelFactory = siteGroupViewModelFactory;
            _siteGroupService = siteGroupService;
            _siteStructureViewModelFactory = siteStructureViewModelFactory;
        }

        [PermissionFilter(Permissions.ViewSiteDetails)]
        public PartialViewResult GetSiteGroup(long companyId, long siteGroupId)
        {
            var viewModel = _siteGroupViewModelFactory
                .WithSiteGroupId(siteGroupId)
                .WithClientId(companyId)
                .GetViewModel();


            return PartialView("_SiteGroup", viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddSiteDetails)]
        public ActionResult CreateGroup(SiteGroupDetailsViewModel siteGroupDetailsViewModel)
        {
            return SaveSiteGroup(siteGroupDetailsViewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditSiteDetails)]
        public ActionResult UpdateGroup(SiteGroupDetailsViewModel siteGroupDetailsViewModel)
        {
            return SaveSiteGroup(siteGroupDetailsViewModel);
        }

        private ActionResult SaveSiteGroup(SiteGroupDetailsViewModel siteGroupDetailsViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var siteGroup = new SiteGroupRequestMapper().Map(siteGroupDetailsViewModel, CurrentUser.UserId);
                    _siteGroupService.CreateUpdate(siteGroup);
                    TempData[UpdateSiteGroupIdKey] = siteGroupDetailsViewModel.GroupId;
                    SetSavedSiteGroupSuccessMessage(siteGroupDetailsViewModel);
                }
                catch (ValidationException e)
                {
                    ModelState.Update(e);
                    var result = SiteStructureViewModelForUpdateGroup(siteGroupDetailsViewModel);
                    return View("Index", result);
                }
                return RedirectToAction("Index", "SitesStructure");
            }

            var viewModel = SiteStructureViewModelForUpdateGroup(siteGroupDetailsViewModel);

            return View("Index", viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.DeleteSiteDetails)]
        public ActionResult DeleteSiteGroup(DeleteSiteGroupViewModel deleteSiteGroupViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData[DeleteSiteGroupValidationMessageKey] = "Unable to delete site group, request is not valid";
            }

            _siteGroupService.Delete(new DeleteSiteGroupRequest()
            {
                CompanyId = deleteSiteGroupViewModel.ClientId,
                GroupId = deleteSiteGroupViewModel.GroupId,
                UserId = CurrentUser.UserId
            });

            TempData[MessageKey] = "Your site group has been successfully deleted";
            return RedirectToAction("Index", "SitesStructure");
        }

        private SiteStructureViewModel SiteStructureViewModelForUpdateGroup(SiteGroupDetailsViewModel siteGroupDetailsViewModel)
        {
            var viewModel = _siteStructureViewModelFactory
                .WithClientId(CurrentUser.CompanyId)
                .WithGroupDetailsViewModel(siteGroupDetailsViewModel)
                .HideSiteDetails()
                .DisplaySiteGroups()
                .WithValidationError(true)
                .GetViewModel();
            return viewModel;
        }


        private void SetSavedSiteGroupSuccessMessage(SiteGroupDetailsViewModel siteGroupDetailsViewModel)
        {
            TempData[MessageKey] = siteGroupDetailsViewModel.GroupId == 0
                                       ? "Your new Site Group has been successfully created"
                                       : "Your changes have been successfully saved";
        }
    }
}