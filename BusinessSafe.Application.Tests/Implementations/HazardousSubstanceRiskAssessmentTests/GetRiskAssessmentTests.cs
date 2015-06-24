using BusinessSafe.Application.Implementations.HazardousSubstanceRiskAssessments;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetRiskAssessmentTests
    {
        private Mock<IHazardousSubstanceRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<IPeninsulaLog> _log;
        private int _hazardousSubstanceAssessmentId;
        private int _companyId;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentRepository = new Mock<IHazardousSubstanceRiskAssessmentRepository>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_request_When_GetRiskAssessment_Then_should_call_correct_methods()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();

            _hazardousSubstanceAssessmentId = 1;
            _companyId = 2;
            _riskAssessmentRepository.Setup(rr => rr.GetByIdAndCompanyId(_hazardousSubstanceAssessmentId, _companyId)).Returns(new Domain.Entities.HazardousSubstanceRiskAssessment());

            //When
            riskAssessmentService.GetRiskAssessment(_hazardousSubstanceAssessmentId, _companyId);

            //Then
            _riskAssessmentRepository.VerifyAll();
        }


        private HazardousSubstanceRiskAssessmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new HazardousSubstanceRiskAssessmentService(_riskAssessmentRepository.Object, null, null, _log.Object,  null, null, null, null);
            return riskAssessmentService;
        }
    }
}
