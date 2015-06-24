using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Request.FireRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;
using FluentValidation.Results;

namespace BusinessSafe.Application.Implementations.FireRiskAssessments
{
    public class FireRiskAssessmentChecklistService : IFireRiskAssessmentChecklistService
    {
        private readonly IFireRiskAssessmentChecklistRepository _fireRiskAssessmentChecklistRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IPeninsulaLog _log;
        private readonly IFireAnswerRepository _fireAnswerRepository;

        public FireRiskAssessmentChecklistService(
            IFireRiskAssessmentChecklistRepository fireRiskAssessmentChecklistRepository,
            IUserForAuditingRepository userForAuditingRepository,
            IQuestionRepository questionRepository,
            IPeninsulaLog log,
            IFireAnswerRepository fireAnswerRepository)
        {
            _fireRiskAssessmentChecklistRepository = fireRiskAssessmentChecklistRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _questionRepository = questionRepository;
            _log = log;
            _fireAnswerRepository = fireAnswerRepository;
        }

        public void Save(SaveFireRiskAssessmentChecklistRequest request)
        {
            _log.Add(request);

            try
            {
                var fireRiskAssessmentChecklist = _fireRiskAssessmentChecklistRepository.GetById(request.FireRiskAssessmentChecklistId);
                var currentUser = _userForAuditingRepository.GetByIdAndCompanyId(request.CurrentUserId, request.CompanyId);
                var submitAnswerParameterClasses = new List<SubmitFireAnswerParameters>();

                foreach (var submitAnswerRequest in request.Answers)
                {
                    var question = _questionRepository.GetById(submitAnswerRequest.QuestionId);

                    submitAnswerParameterClasses.Add(new SubmitFireAnswerParameters
                    {
                        AdditionalInfo = submitAnswerRequest.AdditionalInfo,
                        YesNoNotApplicableResponse = submitAnswerRequest.YesNoNotApplicableResponse,
                        Question = question
                    });
                }

                fireRiskAssessmentChecklist.Save(submitAnswerParameterClasses, currentUser);
                _fireRiskAssessmentChecklistRepository.Save(fireRiskAssessmentChecklist);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public ValidationResult ValidateFireRiskAssessmentChecklist(ValidateCompleteFireRiskAssessmentChecklistRequest request)
        {
            var result = new ValidationResult();
            var correspondingAnswers = _fireAnswerRepository.GetByChecklistIdAndQuestionIds(request.ChecklistId, request.QuestionIds);

            foreach (var questionId in request.QuestionIds)
            {
                var answer = correspondingAnswers.SingleOrDefault(x => x.Question != null && x.Question.Id == questionId);
                
                if(answer == null)
                {
                    result.Errors.Add(new ValidationFailure(questionId.ToString(), "Please select a response"));
                }else if(!answer.IsValidateForCompleteChecklist())
                {
                    string errorMessage = string.Empty;
                    switch(answer.YesNoNotApplicableResponse)
                    {
                        case YesNoNotApplicableEnum.No:
                            errorMessage = "Please add a Further Control Measure Task";
                            break;
                        case YesNoNotApplicableEnum.Yes:
                            errorMessage = "Please enter a comment";
                            break;
                    }
                    result.Errors.Add(new ValidationFailure(questionId.ToString(), errorMessage));
                }
            }
            
            return result;
        }

        public void MarkChecklistWithCompleteFailureAttempt(MarkChecklistWithCompleteFailureAttemptRequest request)
        {
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var fireRiskAssessmentChecklist = _fireRiskAssessmentChecklistRepository.GetById(request.ChecklistId);
            
            fireRiskAssessmentChecklist.MarkChecklistWithCompleteFailureAttempt(user);

            _fireRiskAssessmentChecklistRepository.SaveOrUpdate(fireRiskAssessmentChecklist);
        }
    }
}
