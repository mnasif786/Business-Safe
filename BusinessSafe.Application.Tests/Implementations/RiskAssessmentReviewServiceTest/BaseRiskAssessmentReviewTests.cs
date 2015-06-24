using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Implementations.RiskAssessments;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentReviewServiceTest
{
    public class BaseRiskAssessmentReviewTests
    {
        protected Mock<IUserForAuditingRepository> _userForAuditingRepo;
        protected Mock<IUserRepository> _userRepo;
        protected Mock<IEmployeeRepository> _employeeRepo;
        protected Mock<IRiskAssessmentRepository> _riskAssessmentRepo;
        protected Mock<IRiskAssessmentReviewRepository> _riskAssessmentReviewRepo;
        protected Mock<ITaskCategoryRepository> _responsibilityTaskCategoryRepository;
        protected Mock<IDocumentParameterHelper> _documentParameterHelper;
        protected Mock<IPeninsulaLog> _log;
        
        [SetUp]
        public void SetUp()
        {
            _userForAuditingRepo = new Mock<IUserForAuditingRepository>();
            _employeeRepo = new Mock<IEmployeeRepository>();
            _riskAssessmentRepo = new Mock<IRiskAssessmentRepository>();
            _riskAssessmentReviewRepo = new Mock<IRiskAssessmentReviewRepository>();
            _responsibilityTaskCategoryRepository = new Mock<ITaskCategoryRepository>();
            _log = new Mock<IPeninsulaLog>();
            _documentParameterHelper = new Mock<IDocumentParameterHelper>();
            _userRepo = new Mock<IUserRepository>();
        }

        protected RiskAssessmentReviewService CreateRiskAssessmentReviewService()
        {
            var target = new RiskAssessmentReviewService(
                _riskAssessmentRepo.Object,
                _riskAssessmentReviewRepo.Object,
                _userForAuditingRepo.Object,
                _employeeRepo.Object,
                _responsibilityTaskCategoryRepository.Object,
                _log.Object,
                _documentParameterHelper.Object,
                _userRepo.Object
                );
            return target;
        }
    }
}
