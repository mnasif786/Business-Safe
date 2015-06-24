using System;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentHazardService
{
    [TestFixture]
    [Category("Unit")]
    public class RemoveControlMeasureFromRiskAssessmentHazardTests
    {
        private Mock<IMultiHazardRiskAssessmentHazardRepository> _riskAssessmentHazardRepository;
        private Mock<IUserForAuditingRepository> _userRepo;
        private Mock<IMultiHazardRiskAssessmentRepository> _riskAssessmentRepo;
        private Mock<IPeninsulaLog> _log;
        
        [SetUp]
        public void SetUp()
        {
            _riskAssessmentHazardRepository = new Mock<IMultiHazardRiskAssessmentHazardRepository>();
            _userRepo = new Mock<IUserForAuditingRepository>();
            _riskAssessmentRepo = new Mock<IMultiHazardRiskAssessmentRepository>();
            _log = new Mock<IPeninsulaLog>();
        }

       
        [Test]
        public void Given_valid_request_When_remove_control_measure_from_risk_assessment_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateRiskAssessmentHazardService();
            var request = new RemoveControlMeasureRequest()
                              {
                                  CompanyId = 1000,
                                  RiskAssessmentId = 2000,
                                  UserId = Guid.NewGuid(),
                                  ControlMeasureId = 1,
                                  RiskAssessmentHazardId = 2
                              };

            var user = new UserForAuditing();
            _userRepo
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(user);

            var stubRiskAssessment = new Mock<GeneralRiskAssessment>();
            _riskAssessmentRepo
                .Setup(x => x.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId))
                .Returns(stubRiskAssessment.Object);

            var mockHazard = new Mock<MultiHazardRiskAssessmentHazard>();
            stubRiskAssessment
                .Setup(x => x.FindRiskAssessmentHazard(request.RiskAssessmentHazardId))
                .Returns(mockHazard.Object);


            //When
            target.RemoveControlMeasureFromRiskAssessmentHazard(request);

            //Then
            _userRepo.VerifyAll();
            _riskAssessmentRepo.VerifyAll();
            mockHazard.Verify(x => x.RemoveControlMeasure(request.ControlMeasureId, user));
        }

    
        private Application.Implementations.RiskAssessments.RiskAssessmentHazardService CreateRiskAssessmentHazardService()
        {
            var target = new Application.Implementations.RiskAssessments.RiskAssessmentHazardService(_riskAssessmentHazardRepository.Object, _riskAssessmentRepo.Object, _userRepo.Object, _log.Object);
            return target;
        }
    }
}