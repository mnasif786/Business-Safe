using System.Web.Mvc;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;
using BusinessSafe.WebSite.Helpers;
using FluentValidation;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers
{
    [HazardousSubstanceRiskAssessmentCurrentTabActionFilter(HazardousSubstancesTabs.Assessment)]
    [HazardousSubstanceRiskAssessmentContextFilter]
    public class AssessmentController: BaseController 
    {
        private readonly IAssessmentViewModelFactory _assessmentViewModelFactory;
        private readonly IHazardousSubstanceRiskAssessmentService _hazardousSubstanceRiskAssessmentService;

        public AssessmentController(
            IAssessmentViewModelFactory assessmentViewModelFactory, 
            IHazardousSubstanceRiskAssessmentService hazardousSubstanceRiskAssessmentService)
        {
            _assessmentViewModelFactory = assessmentViewModelFactory;
            _hazardousSubstanceRiskAssessmentService = hazardousSubstanceRiskAssessmentService;
        }

        [PermissionFilter(Permissions.ViewGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult View(long riskAssessmentId, long companyId)
        {
            IsReadOnly = true;

            return Index(riskAssessmentId, companyId);
        }

        [PermissionFilter(Permissions.ViewGeneralandHazardousSubstancesRiskAssessments)] 
        public ActionResult Index(long riskAssessmentId, long companyId)
        {
            var viewModel = _assessmentViewModelFactory
                .WithCompanyId(companyId)
                .WithRiskAssessmentId(riskAssessmentId)
                .GetViewModel();

            return View("Index", viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult Save(AssessmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var invalidViewModel = _assessmentViewModelFactory
                    .WithCompanyId(viewModel.CompanyId)
                    .WithRiskAssessmentId(viewModel.RiskAssessmentId)
                    .GetViewModel();

                return View("Index", invalidViewModel);
            }

            UpdateRiskAssessment(viewModel);
            TempData["Notice"] = "Assessment Information Successfully Saved";

            return RedirectToAction("Index", new { riskAssessmentId = viewModel.RiskAssessmentId, companyId = viewModel.CompanyId });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult SaveAndNext(AssessmentViewModel viewModel)
        {
            try
            {
                UpdateRiskAssessment(viewModel);

                return Json(new { Success = true });
            }
            catch (ValidationException validationException)
            {
                ModelState.AddValidationErrors(validationException);

                return ModelStateErrorsAsJson();
            }
        }

        private void UpdateRiskAssessment(AssessmentViewModel viewModel)
        {
            _hazardousSubstanceRiskAssessmentService.UpdateHazardousSubstanceRiskAssessmentAssessmentDetails(new UpdateHazardousSubstanceRiskAssessmentAssessmentDetailsRequest
            {
                RiskAssessmentId = viewModel.RiskAssessmentId,
                CompanyId = viewModel.CompanyId,
                Quantity = viewModel.Quantity,
                MatterState = viewModel.MatterState,
                DustinessOrVolatility = viewModel.DustinessOrVolatility,
                CurrentUserId = CurrentUser.UserId,
                HealthSurveillanceRequired = viewModel.HealthSurveillanceRequired.GetValueOrDefault(),
                ControlSystemId = viewModel.ControlSystemId
            });
        }
    }
}