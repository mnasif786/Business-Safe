using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Implementations.RiskAssessments;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.FireRiskAssessments;
using BusinessSafe.Application.Validators;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;
using FluentValidation;


namespace BusinessSafe.Application.Implementations.FireRiskAssessments
{
    public class FireRiskAssessmentService : IFireRiskAssessmentService
    {
        private readonly IRiskAssessmentRepository _riskAssessmentRepository;
        private readonly IFireRiskAssessmentRepository _fireRiskAssessmentRepository;
        private readonly IUserForAuditingRepository _auditedUserRepository;
        private readonly IPeninsulaLog _log;
        private readonly IChecklistRepository _checklistRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IDocumentParameterHelper _documentParameterHelper;
        private readonly IUserRepository _userForAuditingRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly IRiskAssessorRepository _riskAssessorRepository;

        public FireRiskAssessmentService(IFireRiskAssessmentRepository fireRiskAssessmentRepository
            , IUserForAuditingRepository auditedUserRepository
            , IChecklistRepository checklistRepository
            , IQuestionRepository questionRepository
            , IDocumentParameterHelper documentParameterHelper
            , IPeninsulaLog log
            , IRiskAssessmentRepository riskAssessmentRepository
            , IUserRepository userForAuditingRepository
            , ISiteRepository siteRepository
            , IRiskAssessorRepository riskAssessorRepository)
        {
            _auditedUserRepository = auditedUserRepository;
            _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
            _log = log;
            _checklistRepository = checklistRepository;
            _questionRepository = questionRepository;
            _documentParameterHelper = documentParameterHelper;
            _riskAssessmentRepository = riskAssessmentRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _siteRepository = siteRepository;
            _riskAssessorRepository = riskAssessorRepository;
        }

        public long CreateRiskAssessment(CreateRiskAssessmentRequest request)
        {
            _log.Add(request);

            var user = _auditedUserRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var checklist = _checklistRepository.GetFireRiskAssessmentChecklist();

            new CreateRiskAssessmentValidator<FireRiskAssessment>(_riskAssessmentRepository).ValidateAndThrow(request);

            var riskAssessment = FireRiskAssessment.Create(request.Title,
                                                           request.Reference,
                                                           request.CompanyId,
                                                           checklist,
                                                           user);


            
            _fireRiskAssessmentRepository.Save(riskAssessment);

            return riskAssessment.Id;

        }

        public void UpdateRiskAssessmentSummary(SaveRiskAssessmentSummaryRequest request)
        {
            _log.Add(request);
            var user = _auditedUserRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var riskAssessor = request.RiskAssessorId.HasValue ? _riskAssessorRepository.GetByIdAndCompanyId(request.RiskAssessorId.Value, request.CompanyId) : null;
            var site = request.SiteId.HasValue ? _siteRepository.GetByIdAndCompanyId(request.SiteId.Value, request.CompanyId) : null;
            var riskAssessment = _fireRiskAssessmentRepository.GetByIdAndCompanyId(request.Id, request.CompanyId);
            new UpdateRiskAssessmentValidator<FireRiskAssessment>(_riskAssessmentRepository).ValidateAndThrow(request);
            riskAssessment.UpdateSummary(request.Title, request.Reference, request.PersonAppointed, request.AssessmentDate, riskAssessor, site, user);
            _fireRiskAssessmentRepository.SaveOrUpdate(riskAssessment);
        }

        public FireRiskAssessmentDto GetRiskAssessment(long riskAssessmentId, long companyId)
        {
            _log.Add(new object[] {riskAssessmentId, companyId});

            var riskAssessment = _fireRiskAssessmentRepository.GetByIdAndCompanyId(riskAssessmentId, companyId);
            return new FireRiskAssessmentDtoMapper().MapWithEverything(riskAssessment);
        }

        public FireRiskAssessmentDto GetRiskAssessmentWithFireSafetyControlMeasuresAndPeopleAtRisk(long riskAssessmentId, long companyId)
        {
            _log.Add(new object[] { riskAssessmentId, companyId });

            var riskAssessment = _fireRiskAssessmentRepository.GetByIdAndCompanyId(riskAssessmentId, companyId);
            return new FireRiskAssessmentDtoMapper().MapWithFireSafetyControlMeasuresAndPeopleAtRiskAndSourcesOfFuelAndSourcesOfIgnition(riskAssessment);
        }

        public virtual IEnumerable<FireRiskAssessmentDto> Search(SearchRiskAssessmentsRequest request)
        {
            _log.Add(new object[] { request.CompanyId });

            var riskAssessments = _fireRiskAssessmentRepository.Search(
                request.Title,
                request.CompanyId,
                request.AllowedSiteIds,
                request.CreatedFrom,
                request.CreatedTo,
                request.SiteGroupId,
                request.SiteId,
                request.CurrentUserId,
                request.ShowDeleted,
                request.ShowArchived,
                request.Page,
                request.PageSize,
                request.OrderBy,
                request.OrderByDirection);

            var riskAssessmentDtos = new RiskAssessmentDtoMapper().MapWithSiteAndRiskAssessor(riskAssessments);
            var fireRiskAssessmentDtos = riskAssessmentDtos.Select(riskAssessmentDto => riskAssessmentDto as FireRiskAssessmentDto);
            return fireRiskAssessmentDtos;

        }

        public virtual int Count(SearchRiskAssessmentsRequest request)
        {
            var count = _fireRiskAssessmentRepository.Count(
                request.Title,
                request.CompanyId,
                request.AllowedSiteIds,
                request.CreatedFrom,
                request.CreatedTo,
                request.SiteGroupId,
                request.SiteId,
                request.CurrentUserId,
                request.ShowDeleted,
                request.ShowArchived
                );

            return count;
        }

        public void UpdatePremisesInformation(UpdateFireRiskAssessmentPremisesInformationRequest request)
        {
            _log.Add(request);

            var fireRiskAssessment = _fireRiskAssessmentRepository.GetByIdAndCompanyId(request.FireRiskAssessmentId, request.CompanyId);
            var currentUser = _auditedUserRepository.GetById(request.CurrentUserId);

            fireRiskAssessment.UpdatePremisesInformation(
                request.PremisesProvidesSleepingAccommodation,
                request.PremisesProvidesSleepingAccommodationConfirmed,
                request.Location,
                request.BuildingUse,
                request.NumberOfFloors,
                request.NumberOfPeople,
                new EmergencyShutOffParameters()
                    {
                        ElectricityEmergencyShutOff = request.ElectricityEmergencyShutOff,
                        WaterEmergencyShutOff = request.WaterEmergencyShutOff,
                        GasEmergencyShutOff = request.GasEmergencyShutOff,
                        OtherEmergencyShutOff = request.OtherEmergencyShutOff
                    }
                , currentUser);

            _fireRiskAssessmentRepository.Update(fireRiskAssessment);

        }

        public FireRiskAssessmentDto GetWithLatestFireRiskAssessmentChecklist(long riskAssessmentId, long companyId)
        {
            _log.Add(new object[] { riskAssessmentId, companyId });

            var riskAssessment = _fireRiskAssessmentRepository.GetByIdAndCompanyId(riskAssessmentId, companyId);
            return new FireRiskAssessmentDtoMapper().MapWithLatestFireRiskAssessmentChecklist(riskAssessment);

        }

        public void SaveFireRiskAssessmentChecklist(SaveFireRiskAssessmentChecklistRequest request)
        {
            _log.Add(request);

            var fireRiskAssessment = _fireRiskAssessmentRepository.GetById(request.FireRiskAssessmentId);
            var currentUser = _auditedUserRepository.GetByIdAndCompanyId(request.CurrentUserId, request.CompanyId);

            var submitAnswerParameterClasses = CreateSubmitAnswerParameters(request.Answers);

            fireRiskAssessment.SaveFireRiskAssessmentChecklist(submitAnswerParameterClasses, currentUser);
            _fireRiskAssessmentRepository.Save(fireRiskAssessment);

        }

        

        public void CompleteFireRiskAssessmentChecklist(CompleteFireRiskAssessmentChecklistRequest request)
        {
            _log.Add(request);

            var fireRiskAssessment = _fireRiskAssessmentRepository.GetById(request.FireRiskAssessmentId);
            var currentUser = _auditedUserRepository.GetByIdAndCompanyId(request.CurrentUserId, request.CompanyId);

            var submitAnswerParameterClasses = CreateSubmitAnswerParameters(request.Answers);

            fireRiskAssessment.CompleteFireRiskAssessmentChecklist(submitAnswerParameterClasses, currentUser);
            _fireRiskAssessmentRepository.Save(fireRiskAssessment);
        }

        public FireRiskAssessmentDto GetRiskAssessmentWithSignificantFindings(long riskAssessmentId, long companyId)
        {
            var riskAssessment = _fireRiskAssessmentRepository.GetByIdAndCompanyId(riskAssessmentId, companyId);
            return new FireRiskAssessmentDtoMapper().MapWithSignificantFindings(riskAssessment);
        }

        private List<SubmitFireAnswerParameters> CreateSubmitAnswerParameters(IList<SubmitFireAnswerRequest> answers)
        {
            var result = new List<SubmitFireAnswerParameters>();

            var allQuestions = GetAllFireRiskAssessmentQuestions(answers);

            foreach (var submitAnswerRequest in answers)
            {
                var question = GetQuestionForAnswer(submitAnswerRequest, allQuestions);
                result.Add(CreateSubmitFireAnswerParamter(submitAnswerRequest, question));
            }
            return result;
        }

        private static Question GetQuestionForAnswer(SubmitFireAnswerRequest submitAnswerRequest, IEnumerable<Question> allQuestions)
        {
            var question = allQuestions.Single(x => x.Id == submitAnswerRequest.QuestionId);
            return question;
        }

        private IEnumerable<Question> GetAllFireRiskAssessmentQuestions(IEnumerable<SubmitFireAnswerRequest> answers)
        {
            var allQuestionIds = answers.Select(x => x.QuestionId).ToList();
            var allQuestions = _questionRepository.GetByIds(allQuestionIds);
            return allQuestions;
        }

        private static SubmitFireAnswerParameters CreateSubmitFireAnswerParamter(SubmitFireAnswerRequest submitAnswerRequest, Question question)
        {
            return new SubmitFireAnswerParameters
                       {
                           AdditionalInfo = submitAnswerRequest.AdditionalInfo,
                           YesNoNotApplicableResponse = submitAnswerRequest.YesNoNotApplicableResponse,
                           Question = question
                       };
        }

        public long CopyRiskAssessment(CopyRiskAssessmentRequest copyRiskAssessmentRequest)
        {
            new CopyRiskAssessmentRequestValidator<FireRiskAssessment>(_riskAssessmentRepository).ValidateAndThrow(copyRiskAssessmentRequest);

            var riskAssessment = _fireRiskAssessmentRepository.GetByIdAndCompanyId(copyRiskAssessmentRequest.RiskAssessmentToCopyId, copyRiskAssessmentRequest.CompanyId);
            var user = _auditedUserRepository.GetByIdAndCompanyId(copyRiskAssessmentRequest.UserId, copyRiskAssessmentRequest.CompanyId);
            var copiedRiskAssessment = riskAssessment.Copy(copyRiskAssessmentRequest.Title, copyRiskAssessmentRequest.Reference, user) as FireRiskAssessment;
            _fireRiskAssessmentRepository.SaveOrUpdate(copiedRiskAssessment);
            return copiedRiskAssessment.Id;
        }

        public void CompleteFireRiskAssessementReview(CompleteRiskAssessmentReviewRequest request)
        {
            var reviewingUser = _auditedUserRepository.GetByIdAndCompanyId(request.ReviewingUserId, request.ClientId);

            var completingUser = _userForAuditingRepository.GetByIdAndCompanyId(request.ReviewingUserId,request.ClientId);

            var createDocumentParameterObjects = _documentParameterHelper.GetCreateDocumentParameters(request.CreateDocumentRequests, request.ClientId).ToList();

            var fireRiskAss = _fireRiskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.ClientId);
            fireRiskAss.Reviews
                .First(x => x.Id == request.RiskAssessmentReviewId)
                .Complete(request.CompletedComments,
                          reviewingUser,
                          request.NextReviewDate,
                          request.Archive,
                          createDocumentParameterObjects,
                          completingUser);

            var replacementChecklist = fireRiskAss.LatestFireRiskAssessmentChecklist.CopyWithYesAnswers(reviewingUser);

            fireRiskAss.FireRiskAssessmentChecklists.Add(replacementChecklist);

            _fireRiskAssessmentRepository.SaveOrUpdate(fireRiskAss);

        }

        public void CopyForMultipleSites(CopyRiskAssessmentForMultipleSitesRequest request)
        {
            _log.Add(request);
            var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentToCopyId, request.CompanyId);
            var user = _auditedUserRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var sites = _siteRepository.GetByIds(request.SiteIds);
            var copiedRiskAssessments = riskAssessment.CopyForMultipleSites(request.Title, sites, user);
            copiedRiskAssessments.ForEach(_riskAssessmentRepository.SaveOrUpdate);
        }
    }
}
