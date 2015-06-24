using System.Web.Mvc;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using FluentValidation;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers
{
    [HazardousSubstanceRiskAssessmentCurrentTabActionFilter(HazardousSubstancesTabs.Description)]
    [HazardousSubstanceRiskAssessmentContextFilter]
    public class DescriptionController : BaseController
    {
        private readonly IHazardousSubstanceRiskAssessmentService _hazardousSubstanceRiskAssessmentService;
        private readonly IHazardousSubstanceDescriptionViewModelFactory _viewModelFactory;

        public DescriptionController(IHazardousSubstanceRiskAssessmentService hazardousSubstanceRiskAssessmentService, IHazardousSubstanceDescriptionViewModelFactory viewModelFactory)
        {
            _hazardousSubstanceRiskAssessmentService = hazardousSubstanceRiskAssessmentService;
            _viewModelFactory = viewModelFactory;
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
            var viewModel = _viewModelFactory
                            .WithRiskAssessmentId(riskAssessmentId)
                            .WithCompanyId(companyId)
                            .GetViewModel();
           
            return View("Index", viewModel);
        }

        
        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult Save(DescriptionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", viewModel);
            }

            try
            {
                UpdateRiskAssessment(viewModel);
            }
            catch (ValidationException validationException)
            {
                ModelState.AddValidationErrors(validationException);
                return View("Index", viewModel);
            }
            
            TempData["Notice"] = "Description Information Successfully Saved";
            return RedirectToAction("Index", new { riskAssessmentId = viewModel.RiskAssessmentId, companyId = viewModel.CompanyId });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult SaveAndNext(DescriptionViewModel model)
        {
            try
            {
                UpdateRiskAssessment(model);

                return Json(new { Success = true });
            }
            catch (ValidationException validationException)
            {
                ModelState.AddValidationErrors(validationException);

                return ModelStateErrorsAsJson();
            }
        }

        private void UpdateRiskAssessment(DescriptionViewModel viewModel)
        {
            _hazardousSubstanceRiskAssessmentService.UpdateRiskAssessmentDescription(new SaveHazardousSubstanceRiskAssessmentRequest
                                                                              {
                                                                                  CompanyId = viewModel.CompanyId,
                                                                                  Id = viewModel.RiskAssessmentId,
                                                                                  UserId = CurrentUser.UserId,
                                                                                  IsInhalationRouteOfEntry = viewModel.IsInhalationRouteOfEntry,
                                                                                  IsIngestionRouteOfEntry = viewModel.IsIngestionRouteOfEntry,
                                                                                  IsAbsorptionRouteOfEntry = viewModel.IsAbsorptionRouteOfEntry,
                                                                                  WorkspaceExposureLimits = viewModel.WorkspaceExposureLimits,
                                                                                  HazardousSubstanceId = viewModel.HazardousSubstanceId
                                                                              });
        }
    }
}