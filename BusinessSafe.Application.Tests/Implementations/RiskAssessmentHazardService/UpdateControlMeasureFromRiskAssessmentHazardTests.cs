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
    public class UpdateControlMeasureFromRiskAssessmentHazardTests
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
        public void Given_valid_request_When_update_control_measure_from_risk_assessment_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateRiskAssessmentHazardService();
            var request = new UpdateControlMeasureRequest();

            var user = new UserForAuditing();
            _userRepo
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(user);

            var mockRiskAssessment = new Mock<GeneralRiskAssessment>();
            _riskAssessmentRepo
                .Setup(x => x.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId))
                .Returns(mockRiskAssessment.Object);

          
            //When
            target.UpdateControlMeasureForRiskAssessmentHazard(request);

            //Then
            _userRepo.VerifyAll();
            _riskAssessmentRepo.Verify(x => x.SaveOrUpdate(mockRiskAssessment.Object));
            mockRiskAssessment.Verify(x => x.UpdateControlMeasureForRiskAssessmentHazard(request.RiskAssessmentHazardId,request.ControlMeasureId, request.ControlMeasure,user));
        }


        private Application.Implementations.RiskAssessments.RiskAssessmentHazardService CreateRiskAssessmentHazardService()
        {
            var target = new Application.Implementations.RiskAssessments.RiskAssessmentHazardService(_riskAssessmentHazardRepository.Object, _riskAssessmentRepo.Object, _userRepo.Object, _log.Object);
            return target;
        }
    }
}