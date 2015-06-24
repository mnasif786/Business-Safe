using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Request.FireRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.AuthenticationService;
using FluentValidation.Results;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels
{
    public static class FireRiskAssessmentChecklistViewModelExtension
    {
        public static IEnumerable<long> GetAllNonNotApplicableFireAnswerQuestionIds(this FireRiskAssessmentChecklistViewModel viewModel)
        {
            return viewModel.Sections
                .SelectMany(x => x.Questions)
                .Where(x => x.Answer != null && x.Answer.YesNoNotApplicableResponse != YesNoNotApplicableEnum.NotApplicable)
                .Select(x => x.Id)
                .ToList();
        }

        public static IList<SubmitFireAnswerRequest> CreateFireAnswerRequests(this FireRiskAssessmentChecklistViewModel viewModel)
        {
            return viewModel.Sections.SelectMany(x => x.Questions)
                .Select(a => new SubmitFireAnswerRequest
                {
                    AdditionalInfo = a.Answer.AdditionalInfo,
                    YesNoNotApplicableResponse = a.Answer.YesNoNotApplicableResponse,
                    QuestionId = a.Id
                }).ToList();
        }

        public static QuestionViewModel GetQuestionViewModel(this FireRiskAssessmentChecklistViewModel viewModel, long questionId)
        {
            var allQuestions = viewModel.Sections.SelectMany(x => x.Questions);
            var question = allQuestions.SingleOrDefault(x => x.Id == questionId);
            return question;
        }

        public static SectionViewModel GetSectionViewModel(this FireRiskAssessmentChecklistViewModel viewModel, long questionId)
        {
            foreach (var sectionViewModel in viewModel.Sections)
            {
                if (sectionViewModel.Questions.Any(question => question.Id == questionId))
                {
                    return sectionViewModel;
                }
            }

            throw new SystemException("Section does not exist for the supplied question id");
        }

        public static SaveFireRiskAssessmentChecklistRequest CreateSaveRequest(this FireRiskAssessmentChecklistViewModel viewModel, CustomPrincipal user)
        {
            var request = new SaveFireRiskAssessmentChecklistRequest
            {
                FireRiskAssessmentId = viewModel.RiskAssessmentId,
                FireRiskAssessmentChecklistId = viewModel.FireRiskAssessmentChecklistId,
                CompanyId = viewModel.CompanyId,
                Answers = viewModel.CreateFireAnswerRequests(),
                CurrentUserId = user.UserId
            };
            return request;
        }

        public static CompleteFireRiskAssessmentChecklistRequest CreateCompleteRequest(this FireRiskAssessmentChecklistViewModel viewModel, CustomPrincipal user)
        {
            var request = new CompleteFireRiskAssessmentChecklistRequest
                              {
                                  FireRiskAssessmentId = viewModel.RiskAssessmentId,
                                  CompanyId = viewModel.CompanyId,
                                  Answers = viewModel.CreateFireAnswerRequests(),
                                  CurrentUserId = user.UserId
                              };
            return request;
        }

        public  static long GetQuestionId(this ValidationFailure error)
        {
            var questionIdWithValidationError = error.PropertyName;
            var questionId = long.Parse(questionIdWithValidationError);
            return questionId;
        }
    }
}