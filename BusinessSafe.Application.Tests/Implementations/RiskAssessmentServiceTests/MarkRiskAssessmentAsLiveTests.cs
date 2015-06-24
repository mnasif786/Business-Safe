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
    public class MarkRiskAssessmentAsLiveTests
    {
        private Mock<IRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<ITaskService> _taskService;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _taskService = new Mock<ITaskService>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_request_When_mark_for_live_Then_calls_the_correct_methods()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();

            var request = new MarkRiskAssessmentAsLiveRequest()
                              {
                                  CompanyId = 100,
                                  UserId = Guid.NewGuid(),
                                  RiskAssessmentId = 200
                              };

            var user = new UserForAuditing();
            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(user);

            var mockRiskAssessment = new Mock<BusinessSafe.Domain.Entities.HazardousSubstanceRiskAssessment>();
            _riskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId))
                .Returns(mockRiskAssessment.Object);


            //When
            riskAssessmentService.MarkRiskAssessmentAsLive(request);

            //Then
            _riskAssessmentRepository.Verify(x => x.SaveOrUpdate(mockRiskAssessment.Object));
            mockRiskAssessment.Verify(x => x.MarkAsLive(user));
        }

        private RiskAssessmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new RiskAssessmentService(_log.Object, _riskAssessmentRepository.Object, _userRepository.Object, _taskService.Object);
            return riskAssessmentService;
        }
    }
}