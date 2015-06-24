using System.Web.Mvc;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using FluentValidation;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers
{
    [FireRiskAssessmentCurrentTabActionFilter(FireRiskAssessmentTabs.Summary)]
    [FireRiskAssessmentContextFilter]
    public class SummaryController : BaseController
    {
        private readonly IEditFireRiskAssessmentSummaryViewModelFactory _viewModelFactory;
        private readonly IFireRiskAssessmentService _riskAssessmentService;

        public SummaryController(IEditFireRiskAssessmentSummaryViewModelFactory viewModelFactory, IFireRiskAssessmentService riskAssessmentService)
        {
            _viewModelFactory = viewModelFactory;
            _riskAssessmentService = riskAssessmentService;
        }

        [PermissionFilter(Permissions.ViewFireRiskAssessments)]
        public ActionResult View(long riskAssessmentId, long companyId)
        {
            IsReadOnly = true;
            return Index(riskAssessmentId, companyId);
        }

        [PermissionFilter(Permissions.EditFireRiskAssessments)]
        public ActionResult Index(long riskAssessmentId, long companyId)
        {
            var model = _viewModelFactory
                .WithRiskAssessmentId(riskAssessmentId)
                .WithCompanyId(companyId)
                .WithAllowableSiteIds(CurrentUser.GetSitesFilter())
                .GetViewModel();
            return View("Index", model);
        }

        [PermissionFilter(Permissions.EditFireRiskAssessments)]
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
                PersonAppointed = model.PersonAppointed,
                SiteId = model.SiteId
            });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditFireRiskAssessments)]
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
    }
}