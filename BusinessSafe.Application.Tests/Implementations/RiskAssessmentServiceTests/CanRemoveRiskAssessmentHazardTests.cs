using BusinessSafe.Application.Implementations.MultiHazardRiskAssessment;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class CanRemoveRiskAssessmentHazardTests
    {
        private Mock<IMultiHazardRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<IMultiHazardRiskAssessmentHazardRepository> _riskAssessmentHazardRepository;
        private Mock<IPeninsulaLog> _log;
        private const long HazardId = 3;
        private const long CompanyId = 1;
        private const long RiskAssessmentId = 2;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentRepository = new Mock<IMultiHazardRiskAssessmentRepository>();
            _riskAssessmentHazardRepository = new Mock<IMultiHazardRiskAssessmentHazardRepository>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_request_When_can_delete_risk_assessment_Then_calls_the_correct_methods()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();


            var stubRiskAssessment = new Mock<GeneralRiskAssessment>();
            _riskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(RiskAssessmentId, CompanyId))
                .Returns(stubRiskAssessment.Object);

            var mockHazard = new Mock<MultiHazardRiskAssessmentHazard>();

            stubRiskAssessment
                .Setup(x => x.FindRiskAssessmentHazardByHazardId(HazardId))
                .Returns(mockHazard.Object);

            _riskAssessmentHazardRepository
                .Setup(
                    x =>
                    x.GetByRiskAssessmentIdAndHazardIdAndCompanyId(RiskAssessmentId, HazardId, CompanyId))
                .Returns(mockHazard.Object);

            //When
            riskAssessmentService.CanRemoveRiskAssessmentHazard(CompanyId, RiskAssessmentId, HazardId);

            //Then
            mockHazard.Verify(x => x.CanDeleteHazard());
        }

        private MultiHazardRiskAssessmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new MultiHazardRiskAssessmentService(
                _riskAssessmentRepository.Object, 
                _riskAssessmentHazardRepository.Object, 
                null, 
                _log.Object);

            return riskAssessmentService;
        }
    }
}