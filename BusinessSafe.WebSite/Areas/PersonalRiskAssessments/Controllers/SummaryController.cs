using System.Web.Mvc;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using FluentValidation;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers
{
    [PersonalRiskAssessmentCurrentTabActionFilter(PersonalRiskAssessmentTabs.Summary)]
    [PersonalRiskAssessmentContextFilter]
    public class SummaryController : BaseController
    {
        private readonly IEditPersonalRiskAssessmentSummaryViewModelFactory _viewModelFactory;
        private readonly IPersonalRiskAssessmentService _personalRiskAssessmentService;
        
        public SummaryController(IPersonalRiskAssessmentService personalRiskAssessmentService, IEditPersonalRiskAssessmentSummaryViewModelFactory viewModelFactory)
        {
            _personalRiskAssessmentService = personalRiskAssessmentService;
            _viewModelFactory = viewModelFactory;
        }

        [PermissionFilter(Permissions.ViewPersonalRiskAssessments)]
        public ActionResult View(long riskAssessmentId, long companyId)
        {
            IsReadOnly = true;
            return Index(riskAssessmentId, companyId);
        }

        [PermissionFilter(Permissions.EditPersonalRiskAssessments)]
        public ActionResult Index(long riskAssessmentId, long companyId)
        {
            var model = _viewModelFactory
                .WithRiskAssessmentId(riskAssessmentId)
                .WithCompanyId(companyId)
                .WithCurrentUserId(CurrentUser.UserId)
                .WithAllowableSiteIds(CurrentUser.GetSitesFilter())
                .GetViewModel();
            return View("Index", model);
        }

        [PermissionFilter(Permissions.EditPersonalRiskAssessments)]
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
                    return RedirectToAction("Index", new { riskAssessmentId = viewModelParam.RiskAssessmentId, companyId = CurrentUser.CompanyId });
                }
                catch (ValidationException validationException)
                {
                    ModelState.AddValidationErrors(validationException);
                }
            }

            var viewModelReturned = _viewModelFactory
                .WithRiskAssessmentId(viewModelParam.RiskAssessmentId)
                .WithCompanyId(viewModelParam.CompanyId)
                .WithCurrentUserId(CurrentUser.UserId)
                .WithAllowableSiteIds(CurrentUser.GetSitesFilter())
                .GetViewModel();
            return View("Index", viewModelReturned);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditPersonalRiskAssessments)]
        public JsonResult SaveAndNext(EditSummaryViewModel model)
        {
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
            _personalRiskAssessmentService.UpdateRiskAssessmentSummary(new UpdatePersonalRiskAssessmentSummaryRequest()
            {
                Id = model.RiskAssessmentId,
                CompanyId = model.CompanyId,
                Title = model.Title,
                Reference = model.Reference,
                UserId = CurrentUser.UserId,
                AssessmentDate = model.DateOfAssessment,
                RiskAssessorId = model.RiskAssessorId,
                Sensitive = model.Sensitive,
                SiteId = model.SiteId
            });
        }
    }
}