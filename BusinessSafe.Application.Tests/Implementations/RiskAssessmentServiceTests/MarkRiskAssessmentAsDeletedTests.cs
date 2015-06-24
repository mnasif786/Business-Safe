using System;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.Implementations.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class MarkRiskAssessmentAsDeletedTests
    {
        private Mock<IRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<ITaskService> _taskService;
        private Mock<IPeninsulaLog> _log;
        private const long CompanyId = 1;
        private const long RiskAssessmentId = 2;
        private readonly Guid _userId = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _taskService = new Mock<ITaskService>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_request_When_mark_for_delete_Then_calls_the_correct_methods()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();

            var user = new UserForAuditing();
            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(_userId, CompanyId))
                .Returns(user);

            var mockRiskAssessment = new Mock<RiskAssessment>();
            _riskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(RiskAssessmentId, CompanyId))
                .Returns(mockRiskAssessment.Object);

            //When
            riskAssessmentService.MarkRiskAssessmentAsDeleted(new MarkRiskAssessmentAsDeletedRequest()
                                                                  {
                                                                      CompanyId = CompanyId,
                                                                      RiskAssessmentId = RiskAssessmentId,
                                                                      UserId = _userId
                                                                  });

            //Then
            _riskAssessmentRepository.Verify(x => x.SaveOrUpdate(mockRiskAssessment.Object));
            mockRiskAssessment.Verify(x => x.MarkForDelete(user));
        }

        private RiskAssessmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new RiskAssessmentService(_log.Object, _riskAssessmentRepository.Object, _userRepository.Object, _taskService.Object);
            return riskAssessmentService;
        }
    }
}