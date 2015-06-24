using System.Web.Mvc;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers
{
    [HazardousSubstanceRiskAssessmentCurrentTabActionFilter(HazardousSubstancesTabs.ControlMeasures)]
    [HazardousSubstanceRiskAssessmentContextFilter]
    public class ControlMeasuresController : BaseController
    {
        private readonly IControlMeasuresViewModelFactory _viewModelFactory;
        private readonly IHazardousSubstanceRiskAssessmentService _riskAssessmentService;

        public ControlMeasuresController(IControlMeasuresViewModelFactory viewModelFactory, IHazardousSubstanceRiskAssessmentService riskAssessmentService)
        {
            _viewModelFactory = viewModelFactory;
            _riskAssessmentService = riskAssessmentService;
        }

        [PermissionFilter(Permissions.ViewGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult View(long riskAssessmentId, long companyId)
        {
            IsReadOnly = true;

            return Index(companyId, riskAssessmentId);
        }

        [PermissionFilter(Permissions.ViewGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult Index(long companyId, long riskAssessmentId)
        {
            var viewModel = _viewModelFactory
                                    .WithCompanyId(companyId)
                                    .WithHazardousSubstanceRiskAssessmentId(riskAssessmentId)
                                    .GetViewModel();

            return View("Index", viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult AddControlMeasureToRiskAssessment(SaveControlMeasureViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelStateErrorsAsJson();
            }
            var Id = _riskAssessmentService.AddControlMeasureToRiskAssessment(new AddControlMeasureRequest
            {
                CompanyId = model.CompanyId,
                RiskAssessmentId = model.RiskAssessmentId,
                ControlMeasure = model.ControlMeasure,
                UserId = CurrentUser.UserId

            });

            return Json(new { Success = true, Id });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult UpdateControlMeasure(SaveControlMeasureViewModel viewModel)
        {
            _riskAssessmentService.UpdateControlMeasure(new UpdateControlMeasureRequest
            {
                CompanyId = viewModel.CompanyId,
                RiskAssessmentId = viewModel.RiskAssessmentId,
                ControlMeasureId = viewModel.ControlMeasureId,
                ControlMeasure = viewModel.ControlMeasure,
                UserId = CurrentUser.UserId
            });

            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult RemoveControlMeasureFromRiskAssessment(long riskAssessmentId, long controlMeasureId, long companyId)
        {
            _riskAssessmentService.RemoveControlMeasureFromRiskAssessment(new RemoveControlMeasureRequest
            {
                CompanyId = companyId,
                RiskAssessmentId = riskAssessmentId,
                ControlMeasureId = controlMeasureId,
                UserId = CurrentUser.UserId
            });

            return Json(new { Success = true });
        }
    }
}