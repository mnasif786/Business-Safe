using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Contracts.MultiHazardRiskAssessment;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers
{
    [FireRiskAssessmentCurrentTabActionFilter(FireRiskAssessmentTabs.Hazards)]
    [FireRiskAssessmentContextFilter]
    public class HazardsController : BaseController
    {
        private readonly ICompanyDefaultService _companyDefaultService;
        private readonly IFireRiskAssessmentService _fireRiskAssessmentService;
        private readonly IFireRiskAssessmentAttachmentService _fireRiskAssessmentAttachmentService;

        public HazardsController(ICompanyDefaultService companyDefaultService, IFireRiskAssessmentService fireRiskAssessmentService, IFireRiskAssessmentAttachmentService fireRiskAssessmentAttachmentService)
        {
            _companyDefaultService = companyDefaultService;
            _fireRiskAssessmentService = fireRiskAssessmentService;
            _fireRiskAssessmentAttachmentService = fireRiskAssessmentAttachmentService;
        }

        [PermissionFilter(Permissions.ViewFireRiskAssessments)]
        public ActionResult View(long companyId, long riskAssessmentId)
        {
            IsReadOnly = true;
            return Index(companyId, riskAssessmentId);
        }

        [PermissionFilter(Permissions.ViewFireRiskAssessments)]
        public ActionResult Index(long companyId, long riskAssessmentId)
        {
            var allPeopleAtRiskForCompany = _companyDefaultService.GetAllPeopleAtRiskForRiskAssessments(companyId, riskAssessmentId);
            var allFireSafetyControlMeasuresForCompany = _companyDefaultService.GetAllFireSafetyControlMeasuresForRiskAssessments(companyId, riskAssessmentId);
            var allSourceOfIgnitionForCompany = _companyDefaultService.GetAllSourceOfIgnitionForRiskAssessment(companyId, riskAssessmentId);
            var allSourceOfFuelForCompany = _companyDefaultService.GetAllSourceOfFuelForRiskAssessment(companyId, riskAssessmentId);

            var riskAssessmentDto = _fireRiskAssessmentService.GetRiskAssessmentWithFireSafetyControlMeasuresAndPeopleAtRisk(riskAssessmentId, companyId);

            return View("Index", new HazardsViewModel()
                                     {
                                         CompanyId = companyId,
                                         RiskAssessmentId = riskAssessmentId,

                                         PeopleAtRisk = allPeopleAtRiskForCompany.Select(x => new LookupDto() { Id = x.Id, Name = x.Name }),
                                         SelectedPeopleAtRisk = riskAssessmentDto.PeopleAtRisk.Select(x => new LookupDto() { Id = x.Id, Name = x.Name }),

                                         FireSafetyControlMeasures = allFireSafetyControlMeasuresForCompany.Select(x => new LookupDto() { Id = x.Id, Name = x.Name }),
                                         SelectedFireSafetyControlMeasures = riskAssessmentDto.FireSafetyControlMeasures.Select(x => new LookupDto() { Id = x.ControlMeasure.Id, Name = x.ControlMeasure.Name }),

                                         SourceOfIgnitions = allSourceOfIgnitionForCompany.Select(x => new LookupDto() { Id = x.Id, Name = x.Name }),
                                         SelectedSourceOfIgnitions = riskAssessmentDto.FireRiskAssessmentSourcesOfIgnition.Select(x => new LookupDto() { Id = x.SourceOfIgnition.Id, Name = x.SourceOfIgnition.Name }),

                                         SourceOfFuels = allSourceOfFuelForCompany.Select(x => new LookupDto() { Id = x.Id, Name = x.Name }),
                                         SelectedSourceOfFuels = riskAssessmentDto.FireRiskAssessmentSourcesOfFuel.Select(x => new LookupDto() { Id = x.SourceOfFuel.Id, Name = x.SourceOfFuel.Name })
                                     });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditFireRiskAssessments)]
        public JsonResult SaveAndNext(SaveHazardsViewModel viewModel)
        {
            UpdateRiskAssessmentFireSafetyControlMeasuresAndPeopleAtRisk(viewModel);
            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditFireRiskAssessments)]
        public ActionResult Save(SaveHazardsViewModel viewModel)
        {
            UpdateRiskAssessmentFireSafetyControlMeasuresAndPeopleAtRisk(viewModel);

            TempData["Notice"] = "Hazards have been saved.";

            return RedirectToAction("Index", new { companyId = viewModel.CompanyId, riskAssessmentId = viewModel.RiskAssessmentId });
        }

        private void UpdateRiskAssessmentFireSafetyControlMeasuresAndPeopleAtRisk(SaveHazardsViewModel viewModel)
        {
            _fireRiskAssessmentAttachmentService.AttachPeopleAtRiskToRiskAssessment(new AttachPeopleAtRiskToRiskAssessmentRequest
            {
                CompanyId = viewModel.CompanyId,
                UserId = CurrentUser.UserId,
                RiskAssessmentId =
                    viewModel.RiskAssessmentId,
                PeopleAtRiskIds =
                    viewModel.PeopleAtRiskIds
            });

            _fireRiskAssessmentAttachmentService.AttachFireSafetyControlMeasuresToRiskAssessment(new AttachFireSafetyControlMeasuresToRiskAssessmentRequest()
            {
                CompanyId = viewModel.CompanyId,
                UserId = CurrentUser.UserId,
                RiskAssessmentId =
                    viewModel.RiskAssessmentId,
                FireSafetyControlMeasureIds = 
                    viewModel.FireSafetyControlMeasureIds
            });

            _fireRiskAssessmentAttachmentService.AttachSourcesOfFuelToRiskAssessment(new AttachSourceOfFuelToRiskAssessmentRequest()
            {
                CompanyId = viewModel.CompanyId,
                UserId = CurrentUser.UserId,
                RiskAssessmentId =
                    viewModel.RiskAssessmentId,
                SourceOfFuelIds = 
                    viewModel.SourceOfFuelsIds
            });

            _fireRiskAssessmentAttachmentService.AttachSourcesOfIgnitionToRiskAssessment(new AttachSourceOfIgnitionToRiskAssessmentRequest()
            {
                CompanyId = viewModel.CompanyId,
                UserId = CurrentUser.UserId,
                RiskAssessmentId =
                    viewModel.RiskAssessmentId,
                SourceOfIgnitionIds = 
                    viewModel.SourceOfIgnitionIds
            });
        }
    }
}