using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Domain.Factories;

namespace BusinessSafe.Application.Implementations.RiskAssessments
{
    public class RiskAssessmentReviewService : IRiskAssessmentReviewService
    {
        private readonly IRiskAssessmentRepository _riskAssessmentRepository;
        private readonly IRiskAssessmentReviewRepository _riskAssessmentReviewRepository;
        private readonly IUserForAuditingRepository _auditedUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITaskCategoryRepository _responsibilityTaskCategoryRepository;
        private readonly IPeninsulaLog _log;
        private readonly IDocumentParameterHelper _documentParameterHelper;


        public RiskAssessmentReviewService(
            IRiskAssessmentRepository riskAssessmentRepository,
            IRiskAssessmentReviewRepository riskAssessmentReviewRepository,
            IUserForAuditingRepository auditedUserRepository,
            IEmployeeRepository employeeRepository,
            ITaskCategoryRepository responsibilityTaskCategoryRepository,
            IPeninsulaLog log,
            IDocumentParameterHelper documentParameterHelper, 
            IUserRepository userRepository)
        {
            _riskAssessmentRepository = riskAssessmentRepository;
            _riskAssessmentReviewRepository = riskAssessmentReviewRepository;
            _auditedUserRepository = auditedUserRepository;
            _employeeRepository = employeeRepository;
            _responsibilityTaskCategoryRepository = responsibilityTaskCategoryRepository;
            _log = log;
            _documentParameterHelper = documentParameterHelper;
            _userRepository = userRepository;
        }

        public void Add(AddRiskAssessmentReviewRequest request)
        {
            _log.Add(request);

            var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);
            var user = _auditedUserRepository.GetByIdAndCompanyId(request.AssigningUserId, request.CompanyId);
            var assignedToEmployee = _employeeRepository.GetByIdAndCompanyId(request.ReviewingEmployeeId, request.CompanyId);

            var riskAssessmentReview = RiskAssessmentReviewFactory.Create(
                riskAssessment,
                user,
                assignedToEmployee,
                request.ReviewDate,
                _responsibilityTaskCategoryRepository,
                request.SendTaskNotification,
                request.SendTaskCompletedNotification,
                request.SendTaskOverdueNotification,
                request.SendTaskDueTomorrowNotification,
                request.TaskGuid);

            _riskAssessmentReviewRepository.Save(riskAssessmentReview);

            _riskAssessmentReviewRepository.Flush();

        }

        public IEnumerable<RiskAssessmentReviewDto> Search(long riskAssessmentId)
        {
            _log.Add(riskAssessmentId);

            var riskAssessmentReviews = _riskAssessmentReviewRepository.Search(riskAssessmentId);
            var riskAssessmentReviewDtos = new RiskAssessmentReviewDtoMapper().Map(riskAssessmentReviews);
            return riskAssessmentReviewDtos;

        }

        public void CompleteRiskAssessementReview(CompleteRiskAssessmentReviewRequest request)
        {
            _log.Add(request);

            var riskAssessmentReview =
                _riskAssessmentReviewRepository.GetByIdAndCompanyId(request.RiskAssessmentReviewId, request.ClientId);

            var reviewingUser = _auditedUserRepository.GetByIdAndCompanyId(request.ReviewingUserId, request.ClientId);

            var completingUser = _userRepository.GetByIdAndCompanyId(request.ReviewingUserId, request.ClientId);

            var createDocumentParameterObjects =
                _documentParameterHelper.GetCreateDocumentParameters(request.CreateDocumentRequests, request.ClientId).ToList();

            riskAssessmentReview.Complete(
                request.CompletedComments,
                reviewingUser,
                request.NextReviewDate,
                request.Archive,
                createDocumentParameterObjects,
                completingUser);

            _riskAssessmentReviewRepository.SaveOrUpdate(riskAssessmentReview);
        }

        public RiskAssessmentReviewDto GetByIdAndCompanyId(long id, long companyId)
        {
            _log.Add(new object[] { id, companyId });

            var riskAssessmentReview = _riskAssessmentReviewRepository.GetByIdAndCompanyId(id, companyId);
            var riskAssessmentReviewDto = new RiskAssessmentReviewDtoMapper().Map(riskAssessmentReview);
            return riskAssessmentReviewDto;

        }

        public void Edit(EditRiskAssessmentReviewRequest request)
        {
            _log.Add(request);

            var riskAssessmentReview = _riskAssessmentReviewRepository.GetByIdAndCompanyId(request.RiskAssessmentReviewId, request.CompanyId);
            var user = _auditedUserRepository.GetByIdAndCompanyId(request.AssigningUserId, request.CompanyId);
            var assignedToEmployee = _employeeRepository.GetByIdAndCompanyId(request.ReviewingEmployeeId, request.CompanyId);
            riskAssessmentReview.Edit(user, assignedToEmployee, request.ReviewDate);
            _riskAssessmentReviewRepository.SaveOrUpdate(riskAssessmentReview);

        }
    }
}