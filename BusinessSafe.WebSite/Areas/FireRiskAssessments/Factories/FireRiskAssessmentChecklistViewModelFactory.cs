using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request.FireRiskAssessments;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using FluentValidation.Results;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories
{
    public class FireRiskAssessmentChecklistViewModelFactory : IFireRiskAssessmentChecklistViewModelFactory
    {
        private readonly IFireRiskAssessmentService _fireRiskAssessmentService;
        private readonly IFireRiskAssessmentChecklistService _fireRiskAssessmentChecklistService;
        private long _companyId;
        private long _riskAssessmentId;
        
        public FireRiskAssessmentChecklistViewModelFactory(IFireRiskAssessmentService fireRiskAssessmentService, IFireRiskAssessmentChecklistService fireRiskAssessmentChecklistService)
        {
            _fireRiskAssessmentService = fireRiskAssessmentService;
            _fireRiskAssessmentChecklistService = fireRiskAssessmentChecklistService;
        }

        public IFireRiskAssessmentChecklistViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IFireRiskAssessmentChecklistViewModelFactory WithRiskAssessmentId(long riskAssessmentId)
        {
            _riskAssessmentId = riskAssessmentId;
            return this;
        }
        
        public FireRiskAssessmentChecklistViewModel GetViewModel()
        {
            
            var fireRiskAssessment = _fireRiskAssessmentService.GetWithLatestFireRiskAssessmentChecklist(_riskAssessmentId, _companyId);

            var result = new FireRiskAssessmentChecklistViewModel
                         {
                             RiskAssessmentId = _riskAssessmentId,
                             CompanyId = _companyId,
                             Sections = new List<SectionViewModel>()
                         };

            var latestFireRiskAssessmentChecklist = fireRiskAssessment.LatestFireRiskAssessmentChecklist;
            if (latestFireRiskAssessmentChecklist != null)
            {
                result.FireRiskAssessmentChecklistId = latestFireRiskAssessmentChecklist.Id;

                foreach (var section in latestFireRiskAssessmentChecklist.Checklist.Sections.OrderBy(s => s.ListOrder))
                {
                    var sectionViewModel = new SectionViewModel
                                           {
                                               ControlId = "Section_" + section.Id.ToString(CultureInfo.InvariantCulture),
                                               Title = section.ShortTitle,
                                               Questions = new List<QuestionViewModel>()
                                           };

                    foreach (var question in section.Questions.OrderBy(q => q.ListOrder))
                    {
                        var questionViewModel = new QuestionViewModel
                                                {
                                                    Id = question.Id,
                                                    ListOrder = question.ListOrder,
                                                    Text = question.Text,
                                                    Information = question.Information,
                                                    Answer =
                                                        CreateAnswerViewModelForQuestion(question.Id,
                                                            latestFireRiskAssessmentChecklist.Answers)
                                                };
                        sectionViewModel.Questions.Add(questionViewModel);
                    }

                    result.Sections.Add(sectionViewModel);
                }

                if (result.Sections.Any())
                {
                    result.Sections.First().Active = true;
                }

            }

            if (HasLatestFireChecklistGotCompleteFailureAttempt(latestFireRiskAssessmentChecklist))
            {
                return AddCompleteValidationErrorsToViewModel(result);
            }

            return result;
        }

        public FireRiskAssessmentChecklistViewModel GetViewModel(FireRiskAssessmentChecklistViewModel viewModel, IList<ValidationFailure> errors)
        {
            if (errors.Any())
            {
                viewModel.MarkAsInvalid();

                foreach (var error in errors)
                {
                    var questionId = error.GetQuestionId();
                    var sectionViewModel = viewModel.GetSectionViewModel(questionId);
                    sectionViewModel.MarkAsInvalid();

                    var questionViewModel = viewModel.GetQuestionViewModel(questionId);
                    questionViewModel.MarkAsInvalid();

                }
            }
            return viewModel;
        }

        private static bool HasLatestFireChecklistGotCompleteFailureAttempt(FireRiskAssessmentChecklistDto latestFireRiskAssessmentChecklist)
        {
            return latestFireRiskAssessmentChecklist != null && latestFireRiskAssessmentChecklist.HasCompleteFailureAttempt.GetValueOrDefault();
        }

        private FireRiskAssessmentChecklistViewModel AddCompleteValidationErrorsToViewModel(FireRiskAssessmentChecklistViewModel viewModel)
        {
            var nonNotApplicableAnswerQuestionIds = viewModel.GetAllNonNotApplicableFireAnswerQuestionIds();
            var validationResult = _fireRiskAssessmentChecklistService.ValidateFireRiskAssessmentChecklist(new ValidateCompleteFireRiskAssessmentChecklistRequest
                                                         {
                                                             ChecklistId = viewModel.FireRiskAssessmentChecklistId,
                                                             QuestionIds = nonNotApplicableAnswerQuestionIds
                                                         });
            if (!validationResult.IsValid)
            {
                viewModel = GetViewModel(viewModel, validationResult.Errors);
            }
            return viewModel;
        }

        private FireAnswerViewModel CreateAnswerViewModelForQuestion(long questionId, IEnumerable<FireAnswerDto> answers)
        {
            return
                answers
                .Where(x => x.Question.Id == questionId)
                .Select(x =>
                    new FireAnswerViewModel()
                    {
                        Id = x.Id,
                        YesNoNotApplicableResponse = x.YesNoNotApplicableResponse,
                        AdditionalInfo = x.AdditionalInfo
                    })
                .FirstOrDefault();
        }

   
    }
}

