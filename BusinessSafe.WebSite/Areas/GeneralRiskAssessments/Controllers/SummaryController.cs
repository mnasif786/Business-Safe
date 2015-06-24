using System.Web.Mvc;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using FluentValidation;

namespace BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Controllers
{
    [GeneralRiskAssessmentCurrentTabActionFilter(GeneralRiskAssessmentTabs.Summary)]
    [GeneralRiskAssessmentContextFilter]
    public class SummaryController : BaseController
    {
        private readonly IEditGeneralRiskAssessmentSummaryViewModelFactory _viewModelFactory;
        private readonly IGeneralRiskAssessmentService _riskAssessmentService;
        
        public SummaryController(IGeneralRiskAssessmentService riskAssessmentService, IEditGeneralRiskAssessmentSummaryViewModelFactory viewModelFactory)
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
            var model = _viewModelFactory
                .WithRiskAssessmentId(riskAssessmentId)
                .WithCompanyId(companyId)
                .WithAllowableSiteIds(CurrentUser.GetSitesFilter())
                .GetViewModel();
            return View("Index", model);
        }

        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        [HttpPost]
        public ActionResult Save(EditSummaryViewModel viewModel)
        {
            Validate(viewModel);

            if (ModelState.IsValid) 
            {
                try
                {
                    UpdateRiskAssessmentSummary(viewModel);
                    TempData["Notice"] = "Risk Assessment Summary Successfully Updated";
                    return RedirectToAction("Index", new { riskAssessmentId = viewModel.RiskAssessmentId, companyId = CurrentUser.CompanyId });
                }
                catch (ValidationException validationException)
                {
                    ModelState.AddValidationErrors(validationException);
                }
            }

            var viewModelReturned = _viewModelFactory
                .WithRiskAssessmentId(viewModel.RiskAssessmentId)
                .WithCompanyId(viewModel.CompanyId)
                .WithAllowableSiteIds(CurrentUser.GetSitesFilter())
                .GetViewModel();
            return View("Index", viewModelReturned);
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

        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult SaveAndNext(EditSummaryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UpdateRiskAssessmentSummary(model);
                    return Json(new {Success = true});
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

        private void UpdateRiskAssessmentSummary(EditSummaryViewModel model)
        {
            _riskAssessmentService.UpdateRiskAssessmentSummary(new SaveRiskAssessmentSummaryRequest
            {
                Id = model.RiskAssessmentId,
                CompanyId = model.CompanyId,
                Title = model.Title,
                Reference = model.Reference,
                UserId = CurrentUser.UserId,
                AssessmentDate = model.DateOfAssessment,
                RiskAssessorId = model.RiskAssessorId,
                SiteId = model.SiteId
            });
        }
    }
}