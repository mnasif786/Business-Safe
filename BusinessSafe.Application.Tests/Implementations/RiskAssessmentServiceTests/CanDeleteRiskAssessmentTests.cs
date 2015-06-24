using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.Implementations;
using BusinessSafe.Application.Implementations.GeneralRiskAssessments;
using BusinessSafe.Application.Implementations.RiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class CanDeleteRiskAssessmentTests
    {
        private Mock<IRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<ITaskService> _taskService;
        private Mock<IPeninsulaLog> _log;
        private const long CompanyId = 1;
        private const long RiskAssessmentId = 2;
        
        [SetUp]
        public void Setup()
        {
            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _taskService = new Mock<ITaskService>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_request_When_can_delete_risk_assessment_Then_calls_the_correct_methods()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();

          
            var mockRiskAssessment = new Mock<GeneralRiskAssessment>();
            _riskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(RiskAssessmentId, CompanyId))
                .Returns(mockRiskAssessment.Object);

            //When
            riskAssessmentService.HasUndeletedTasks(CompanyId, RiskAssessmentId);

            //Then
            mockRiskAssessment.Verify(x => x.HasUndeletedTasks());
        }

        private RiskAssessmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new RiskAssessmentService(_log.Object, _riskAssessmentRepository.Object, _userRepository.Object, _taskService.Object);
            return riskAssessmentService;
        }
    }
}