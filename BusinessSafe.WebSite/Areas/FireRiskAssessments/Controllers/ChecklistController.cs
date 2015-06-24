using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Request.FireRiskAssessments;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;

using FluentValidation.Results;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers
{
    [FireRiskAssessmentCurrentTabActionFilter(FireRiskAssessmentTabs.FireChecklist)]
    [FireRiskAssessmentContextFilter]
    public class ChecklistController : BaseController
    {
        private readonly IFireRiskAssessmentChecklistViewModelFactory _viewModelFactory;
        private readonly IFireRiskAssessmentChecklistService _fireRiskAssessmentChecklistService;
        private readonly IFireRiskAssessmentService _fireRiskAssessmentService;
        private readonly IFireRiskAssessmentFurtherControlMeasureTaskService _furtherControlMeasureTaskService;

        public ChecklistController(
            IFireRiskAssessmentChecklistViewModelFactory viewModelFactory,
            IFireRiskAssessmentChecklistService fireRiskAssessmentChecklistService,
            IFireRiskAssessmentService fireRiskAssessmentService,
            IFireRiskAssessmentFurtherControlMeasureTaskService furtherControlMeasureTaskService
            )
        {
            _viewModelFactory = viewModelFactory;
            _fireRiskAssessmentChecklistService = fireRiskAssessmentChecklistService;
            _fireRiskAssessmentService = fireRiskAssessmentService;
            _furtherControlMeasureTaskService = furtherControlMeasureTaskService;
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
            var viewModel = _viewModelFactory
                                    .WithRiskAssessmentId(riskAssessmentId)
                                    .WithCompanyId(companyId)
                                    .GetViewModel();
            
            return View("Index", viewModel);
        }

        
        [HttpPost]
        [PermissionFilter(Permissions.EditFireRiskAssessments)]
        public ActionResult SaveChecklistOnlyForAuditing(FireRiskAssessmentChecklistViewModel viewModel)
        {
            var request = viewModel.CreateSaveRequest(CurrentUser);

            _fireRiskAssessmentChecklistService.Save(request);

            return Json(new { success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditFireRiskAssessments)]
        public ActionResult SaveAndNext(FireRiskAssessmentChecklistViewModel viewModel)
        {
            var request = viewModel.CreateSaveRequest(CurrentUser);

            _fireRiskAssessmentService.SaveFireRiskAssessmentChecklist(request);

            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditFireRiskAssessments)]
        public ActionResult Save(FireRiskAssessmentChecklistViewModel viewModel)
        {
            var request = viewModel.CreateSaveRequest(CurrentUser);

            _fireRiskAssessmentService.SaveFireRiskAssessmentChecklist(request);

            TempData["Notice"] = "Checklist has been saved.";
            
            return RedirectToAction("Index", new { companyId = viewModel.CompanyId, riskAssessmentId = viewModel.RiskAssessmentId });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditFireRiskAssessments)]
        public ActionResult Complete(FireRiskAssessmentChecklistViewModel viewModel)
        {
            // Validate 
            var allNonNotApplicableFireAnswerQuestionIds = viewModel.GetAllNonNotApplicableFireAnswerQuestionIds();
            var validationResult = GetValidationResult(viewModel.FireRiskAssessmentChecklistId, allNonNotApplicableFireAnswerQuestionIds);
            if (!validationResult.IsValid)
            {
                _fireRiskAssessmentChecklistService.MarkChecklistWithCompleteFailureAttempt(GetMarkCompleteAsFailureRequest(viewModel.FireRiskAssessmentChecklistId));

                var viewModelWithValidationErrors = _viewModelFactory.GetViewModel(viewModel, validationResult.Errors);
                return View("Index", viewModelWithValidationErrors);
            }

            // Complete
            var request = viewModel.CreateCompleteRequest(CurrentUser);

            _fireRiskAssessmentService.CompleteFireRiskAssessmentChecklist(request);

            TempData["Notice"] = "Checklist has been completed.";
            return RedirectToAction("Index", new { viewModel.RiskAssessmentId, viewModel.CompanyId });
        }

        [PermissionFilter(Permissions.EditFireRiskAssessments)]
        public JsonResult CheckFireAnswerCanBeChanged(DoesAnswerHaveFurtherControlMeasureTasksViewModel viewModel)
        {
            var response = _furtherControlMeasureTaskService
                                        .GetFurtherControlMeasureTaskCountsForAnswer(new GetFurtherControlMeasureTaskCountsForAnswerRequest
                                                                                                                            {
                                                                                                                                FireChecklistId = viewModel.FireChecklistId,
                                                                                                                                FireQuestionId = viewModel.FireQuestionId
                                                                                                                            });
            return Json(
                new
                    {
                        CanBeChanged = response.TotalFurtherControlMeasureTaskCount == 0,
                        CanBeDeleted = response.TotalCompletedFurtherControlMeasureTaskCount == 0
                    }, JsonRequestBehavior.AllowGet);
        }

        [PermissionFilter(Permissions.EditFireRiskAssessments)]
        public JsonResult Validate(ValidateCompleteFireRiskAssessmentChecklistViewModel viewModel)
        {
            var validationResult = GetValidationResult(viewModel.ChecklistId, viewModel.AllNoAnswerQuestionIds);
            if (!validationResult.IsValid)
            {
                _fireRiskAssessmentChecklistService.MarkChecklistWithCompleteFailureAttempt(GetMarkCompleteAsFailureRequest(viewModel.ChecklistId));
            }
            var errors = validationResult.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }).ToList();

            return Json(new { Success = validationResult.IsValid, Errors = errors }, JsonRequestBehavior.AllowGet);
        }

        private ValidationResult GetValidationResult(long checklistId, IEnumerable<long> questionIds)
        {
            if (questionIds == null)
            {
                questionIds = new List<long>();
            }

            return _fireRiskAssessmentChecklistService
                                            .ValidateFireRiskAssessmentChecklist(new ValidateCompleteFireRiskAssessmentChecklistRequest
                                            {
                                                ChecklistId = checklistId,
                                                QuestionIds = questionIds
                                            });
        }

        private MarkChecklistWithCompleteFailureAttemptRequest GetMarkCompleteAsFailureRequest(long id)
        {
            return new MarkChecklistWithCompleteFailureAttemptRequest()
            {
                ChecklistId = id,
                CompanyId = CurrentUser.CompanyId,
                UserId = CurrentUser.UserId
            };
        }
    }
}
