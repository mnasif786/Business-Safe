using System.Web.Mvc;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using FluentValidation;
using EditSummaryViewModel = BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels.EditSummaryViewModel;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers
{
    [HazardousSubstanceRiskAssessmentCurrentTabActionFilter(HazardousSubstancesTabs.Summary)]
    [HazardousSubstanceRiskAssessmentContextFilter]
    public class SummaryController : BaseController
    {
        private readonly IEditHazardousSubstanceRiskAssessmentSummaryViewModelFactory _viewModelFactory;
        private readonly IHazardousSubstanceRiskAssessmentService _riskAssessmentService;

        public SummaryController(IHazardousSubstanceRiskAssessmentService riskAssessmentService, IEditHazardousSubstanceRiskAssessmentSummaryViewModelFactory viewModelFactory)
        {
            _riskAssessmentService = riskAssessmentService;
            _viewModelFactory = viewModelFactory;
        }

        [PermissionFilter(Permissions.ViewGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult View(long riskAssessmentId, long companyId)
        {
            IsReadOnly = true;
            return Index(riskAssessmentId, companyId);
        }

        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult Index(long riskAssessmentId, long companyId)
        {
            TempData["Notice"] = null;
            var model = _viewModelFactory
                .WithRiskAssessmentId(riskAssessmentId)
                .WithCompanyId(CurrentUser.CompanyId)
                .WithAllowableSiteIds(CurrentUser.GetSitesFilter())
                .GetViewModel();
            return View("Index", model);
        }

        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        [HttpPost]
        public ActionResult Save(EditSummaryViewModel viewModelParam)
        {
            Validate(viewModelParam);

            if (ModelState.IsValid)
            {
                try
                {
                    UpdateRiskAssessmentSummary(viewModelParam);
                    TempData["Notice"] = "Risk Assessment Summary Successfully Updated";
                    RedirectToAction("Index", new { hazardousSubstanceRiskAssessmentId = viewModelParam, companyId = CurrentUser.CompanyId });
                }
                catch (ValidationException validationException)
                {
                    ModelState.AddValidationErrors(validationException);
                }
            }

            return View("Index", _viewModelFactory
                                        .WithCompanyId(viewModelParam.CompanyId)
                                        .WithAllowableSiteIds(CurrentUser.GetSitesFilter())
                                        .AttachDropDownData(viewModelParam));
        }

        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult SaveAndNext(EditSummaryViewModel model)
        {
            if(model.HazardousSubstanceId == 0)
            {
                return Json(new { Success = false, Errors = new[] { "The Hazardous Substance is required" } });
            }
            try
            {
                if (ModelState.IsValid)
                {
                    UpdateRiskAssessmentSummary(model);
                    return Json(new { Success = true });
                }
                else
                {
                    return ModelStateErrorsAsJson();
                }
            }
            catch (ValidationException validationException)
            {
                ModelState.AddValidationErrors(validationException);
                return ModelStateErrorsAsJson();
            }
        }
        
        private void Validate(EditSummaryViewModel viewModel)
        {
            if (viewModel.RiskAssessorId == null)
            {
                ModelState.AddModelError("RiskAssessorId", "Please select a Risk Assessor");
            }
            if (viewModel.SiteId == null)
            {
                ModelState.AddModelError("SiteId", "Please select a Site");
            }
        }

        private void UpdateRiskAssessmentSummary(EditSummaryViewModel model)
        {
            _riskAssessmentService.UpdateRiskAssessmentSummary(new SaveHazardousSubstanceRiskAssessmentRequest
            {
                Id = model.RiskAssessmentId,
                CompanyId = model.CompanyId,
                Title = model.Title,
                Reference = model.Reference,
                UserId = CurrentUser.UserId,
                AssessmentDate = model.DateOfAssessment,
                RiskAssessorId = model.RiskAssessorId,
                HazardousSubstanceId = model.HazardousSubstanceId,
                SiteId = model.SiteId
            });
        }
    }
}
