using BusinessSafe.Application.Implementations.HazardousSubstanceRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.Services;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class ControlSystemCalculateTests
    {
        private Mock<IControlSystemCalculationService> _calculationService;
        private Mock<IPeninsulaLog> _log;
        private const string groupCode = "a group code";
        private readonly MatterState? matterState = MatterState.Solid;
        private readonly Quantity? quantity = Quantity.Small;
        private readonly DustinessOrVolatility? dustinessOrVolatility = DustinessOrVolatility.Low;

        [SetUp]
        public void Setup()
        {
            _calculationService = new Mock<IControlSystemCalculationService>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_request_When_Calculate_Then_should_call_correct_methods()
        {
            //Given
            var controlSystemService = CreateControlSystemService();

            var controlSystem = new ControlSystem();
            _calculationService
                .Setup(x => x.Calculate(groupCode, matterState, quantity, dustinessOrVolatility))
                .Returns(controlSystem);

            //When
            controlSystemService.Calculate(groupCode, matterState, quantity, dustinessOrVolatility);

            //Then
            _calculationService.VerifyAll();
        }

        [Test]
        public void Given_valid_request_When_Calculate_Then_should_return_correct_results()
        {
            //Given
            var controlSystemService = CreateControlSystemService();

            var controlSystem = new ControlSystem()
                                    {
                                        Id = 250,
                                        Description = "This Description",
                                        DocumentLibraryId = 1021L
                                    };
            _calculationService
                .Setup(x => x.Calculate(groupCode, matterState, quantity, dustinessOrVolatility))
                .Returns(controlSystem);

            //When
            var result = controlSystemService.Calculate(groupCode, matterState, quantity, dustinessOrVolatility);

            //Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(controlSystem.Id));
            Assert.That(result.Description, Is.EqualTo(controlSystem.Description));
            Assert.That(result.DocumentLibraryId, Is.EqualTo(controlSystem.DocumentLibraryId));
        }


        private ControlSystemService CreateControlSystemService()
        {
            var riskAssessmentService = new ControlSystemService(_calculationService.Object);
            return riskAssessmentService;
        }
    }
}