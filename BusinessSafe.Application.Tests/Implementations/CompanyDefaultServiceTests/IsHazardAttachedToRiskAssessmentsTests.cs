using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.CompanyDefaultServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class IsHazardAttachedToRiskAssessmentsTests
    {
        private Mock<IMultiHazardRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<IPeninsulaLog> _log;
        private int _hazardId;
        private int _companyId;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentRepository = new Mock<IMultiHazardRiskAssessmentRepository>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void When_IsHazardAttachedToRiskAssessments_Then_should_call_correct_methods()
        {
            //Given
            var companyDefaultService = CreateCompanyDefaultService();

            _hazardId = 1;
            _companyId = 2;
            
            _riskAssessmentRepository
                .Setup(rr => rr.IsHazardAttachedToRiskAssessments(_hazardId, _companyId))
                .Returns(true);

            //When
            companyDefaultService.CanDeleteHazard(_hazardId, _companyId);

            //Then
            _riskAssessmentRepository.VerifyAll();
        }

        [Test]
        public void Given_hazard_not_attached_When_IsHazardAttachedToRiskAssessments_Then_should_return_correct_result()
        {
            //Given
            var companyDefaultService = CreateCompanyDefaultService();

            _hazardId = 1;
            _companyId = 2;

            _riskAssessmentRepository
                .Setup(rr => rr.IsHazardAttachedToRiskAssessments(_hazardId, _companyId))
                .Returns(false);

            //When
            var result = companyDefaultService.CanDeleteHazard(_hazardId, _companyId);

            //Then
            Assert.True(result);
        }

        [Test]
        public void Given_hazard_is_attached_When_IsHazardAttachedToRiskAssessments_Then_should_return_correct_result()
        {
            //Given
            var companyDefaultService = CreateCompanyDefaultService();

            _hazardId = 1;
            _companyId = 2;

            _riskAssessmentRepository
                .Setup(rr => rr.IsHazardAttachedToRiskAssessments(_hazardId, _companyId))
                .Returns(true);

            //When
            var result = companyDefaultService.CanDeleteHazard(_hazardId, _companyId);

            //Then
            Assert.False(result);
        }


        private CompanyDefaultService CreateCompanyDefaultService()
        {
            var target = new CompanyDefaultService(null, null, null, null, _riskAssessmentRepository.Object, null, null, null, null, null, _log.Object);
            return target;
        }
    }
}
