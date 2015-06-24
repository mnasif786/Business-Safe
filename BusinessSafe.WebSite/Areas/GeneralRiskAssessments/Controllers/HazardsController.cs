using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Contracts.MultiHazardRiskAssessment;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Implementations.GeneralRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.ViewModels;
using System;

namespace BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Controllers
{
    [GeneralRiskAssessmentCurrentTabActionFilter(GeneralRiskAssessmentTabs.Hazards)]
    [GeneralRiskAssessmentContextFilter]
    public class HazardsController : BaseController
    {
        private readonly ICompanyDefaultService _companyDefaultService;
        private readonly IGeneralRiskAssessmentAttachmentService _riskAssessmentAttachmentService;
        private readonly IGeneralRiskAssessmentService _riskAssessmentService;
        private readonly IMultiHazardRiskAssessmentAttachmentService _multiHazardRiskAssessmentAttachmentService;
        private readonly IMultiHazardRiskAssessmentService _multiHazardRiskAssessmentService;

        public HazardsController(IGeneralRiskAssessmentService riskAssessmentService, IGeneralRiskAssessmentAttachmentService riskAssessmentAttachmentService, ICompanyDefaultService companyDefaultService, IMultiHazardRiskAssessmentAttachmentService multiHazardRiskAssessmentAttachmentService, IMultiHazardRiskAssessmentService multiHazardRiskAssessmentService)
        {
            _companyDefaultService = companyDefaultService;
            _multiHazardRiskAssessmentAttachmentService = multiHazardRiskAssessmentAttachmentService;
            _multiHazardRiskAssessmentService = multiHazardRiskAssessmentService;
            _riskAssessmentAttachmentService = riskAssessmentAttachmentService;
            _riskAssessmentService = riskAssessmentService;
        }

        [PermissionFilter(Permissions.ViewGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult View(long companyId, long riskAssessmentId)
        {
            IsReadOnly = true;
            return Index(companyId, riskAssessmentId);
        }

        [PermissionFilter(Permissions.ViewGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult Index(long companyId, long riskAssessmentId)
        {
            var riskAssessmentDto = _riskAssessmentService.GetRiskAssessmentWithHazardsAndPeopleAtRisk(riskAssessmentId, companyId);
            var allHazardsForCompany = _companyDefaultService.GetAllMultiHazardRiskAssessmentHazardsForCompany(companyId, HazardTypeEnum.General, riskAssessmentId);
            var allPeopleAtRiskForCompany = _companyDefaultService.GetAllPeopleAtRiskForRiskAssessments(companyId, riskAssessmentId);
            var allSelectedHazardsIds = riskAssessmentDto.Hazards.ToList().Select(hazard => hazard.Id).ToArray();
            var availableHazards = allHazardsForCompany
                .Where(x => !allSelectedHazardsIds.Contains(x.Id));

            var viewModel = new HazardsViewModel()
                                {
                                    RiskAssessmentId = riskAssessmentId,
                                    CompanyId = companyId,
                                    DisplayWarningMessageForPeopleAtRisk = new HtmlString(PeopleAtRiskWarningDisplayMessages.Json(companyId)),
                                    Hazards = availableHazards.Select(x => new LookupDto() { Id = x.Id, Name = x.Name }),
                                    SelectedHazards = riskAssessmentDto.Hazards.Select(x => new LookupDto(){Id = x.Id, Name = x.Name}).ToList(),
                                    PeopleAtRisk = allPeopleAtRiskForCompany.Select(x => new LookupDto(){Id=x.Id, Name = x.Name}).ToList(),
                                    SelectedPeopleAtRisk = riskAssessmentDto.PeopleAtRisk.Select(x => new LookupDto() { Id = x.Id, Name = x.Name })
                                };

            return View("Index", viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments, Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult SaveAndNext(SaveHazardsViewModel viewModel)
        {
            UpdateRiskAssessmentHazardsAndPeopleAtRisk(viewModel);
            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddGeneralandHazardousSubstancesRiskAssessments, Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult Save(SaveHazardsViewModel viewModel)
        {
            UpdateRiskAssessmentHazardsAndPeopleAtRisk(viewModel);
            
            return RedirectToAction("Index", new { companyId = viewModel.CompanyId, riskAssessmentId = viewModel.RiskAssessmentId });
        }

        private void UpdateRiskAssessmentHazardsAndPeopleAtRisk(SaveHazardsViewModel viewModel)
        {
            var attachHazardsToRiskAssessmentHazardDetail = viewModel.HazardIds != null ? viewModel.HazardIds.Select((hazardId, index) => new AttachHazardsToRiskAssessmentHazardDetail() {Id = hazardId, OrderNumber = index + 1}).ToList() : new List<AttachHazardsToRiskAssessmentHazardDetail>();

            _multiHazardRiskAssessmentAttachmentService.AttachHazardsToRiskAssessment(new AttachHazardsToRiskAssessmentRequest
                                                                               {
                                                                                   CompanyId = viewModel.CompanyId,
                                                                                   UserId = CurrentUser.UserId,
                                                                                   RiskAssessmentId = viewModel.RiskAssessmentId,
                                                                                   Hazards = attachHazardsToRiskAssessmentHazardDetail
                                                                               });


            _riskAssessmentAttachmentService.AttachPeopleAtRiskToRiskAssessment(new AttachPeopleAtRiskToRiskAssessmentRequest
                                                                                    {
                                                                                        CompanyId = viewModel.CompanyId,
                                                                                        UserId = CurrentUser.UserId,
                                                                                        RiskAssessmentId =
                                                                                            viewModel.RiskAssessmentId,
                                                                                        PeopleAtRiskIds =
                                                                                            viewModel.PeopleAtRiskIds
                                                                                    });
        }


        [PermissionFilter(Permissions.ViewRiskAssessmentTasks)]
        public JsonResult CheckRiskAssessmentHazardCanBeRemoved(long companyId, long riskAssessmentId,
                                                                long riskAssessmentHazardId)
        {
            bool canBeRemoved = false;

            if (CurrentUser.IsInRole(Permissions.EditGeneralandHazardousSubstancesRiskAssessments.ToString())
                || CurrentUser.IsInRole(Permissions.EditFireRiskAssessments.ToString())
                || CurrentUser.IsInRole(Permissions.EditPersonalRiskAssessments.ToString()))
            {
                canBeRemoved = _multiHazardRiskAssessmentService.CanRemoveRiskAssessmentHazard(companyId,
                                                                                               riskAssessmentId,
                                                                                               riskAssessmentHazardId);
            }
            return Json(new {CanBeRemoved = canBeRemoved}, JsonRequestBehavior.AllowGet);
        }
    }
}