using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Domain.Services;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.DomainServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class ControlSystemCalculationServiceTests
    {
        private Mock<IControlSystemRepository> _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IControlSystemRepository>();

            _repository.Setup(x => x.GetById(0)).Returns(new ControlSystem {Id = 0, Description = "None"});
            _repository.Setup(x => x.GetById(1)).Returns(new ControlSystem {Id = 1, Description = "General"});
            _repository.Setup(x => x.GetById(2)).Returns(new ControlSystem
                                                             {Id = 2, Description = "Engineering Controls"});
            _repository.Setup(x => x.GetById(3)).Returns(new ControlSystem {Id = 3, Description = "Containment"});
            _repository.Setup(x => x.GetById(4)).Returns(new ControlSystem {Id = 4, Description = "Special"});
        }

        [TestCase(null, MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Low, "None")]
        [TestCase("A", null, null, null, "None")]
        [TestCase("A", MatterState.Liquid, null, null, "None")]
        [TestCase("A", MatterState.Liquid, Quantity.Small, null, "None")]
        [TestCase("A", null, Quantity.Small, null, "None")]
        [TestCase("A", MatterState.Liquid, null, DustinessOrVolatility.Low, "None")]
        [TestCase("A", null, Quantity.Small, DustinessOrVolatility.Low, "None")]
        [TestCase(null, null, Quantity.Small, DustinessOrVolatility.Low, "None")]
        [TestCase(null, null, null, DustinessOrVolatility.Low, "None")]
        [TestCase(null, null, Quantity.Small, null, "None")]
        [TestCase(null, MatterState.Liquid, Quantity.Small, null, "None")]
        [TestCase("A", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Low, "General")]
        [TestCase("A", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Low, "General")]
        [TestCase("A", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Low, "General")]
        [TestCase("A", MatterState.Solid, Quantity.Small, DustinessOrVolatility.Low, "General")]
        [TestCase("A", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Medium, "General")]
        [TestCase("A", MatterState.Solid, Quantity.Small, DustinessOrVolatility.Medium, "General")]
        [TestCase("A", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.High, "General")]
        [TestCase("A", MatterState.Solid, Quantity.Small, DustinessOrVolatility.High, "General")]
        [TestCase("A", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.Low, "General")]
        [TestCase("A", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.Low, "General")]
        [TestCase("A", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.Medium, "General")]
        [TestCase("A", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.Medium, "General")]
        [TestCase("A", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.High, "Engineering Controls")]
        [TestCase("A", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.High, "Engineering Controls")]
        [TestCase("A", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.Low, "General")]
        [TestCase("A", MatterState.Solid, Quantity.Large, DustinessOrVolatility.Low, "General")]
        [TestCase("A", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.Medium, "General")]
        [TestCase("A", MatterState.Solid, Quantity.Large, DustinessOrVolatility.Medium, "Engineering Controls")]
        [TestCase("A", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.High, "Engineering Controls")]
        [TestCase("A", MatterState.Solid, Quantity.Large, DustinessOrVolatility.High, "Engineering Controls")]
        [TestCase("B", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Low, "General")]
        [TestCase("B", MatterState.Solid, Quantity.Small, DustinessOrVolatility.Low, "General")]
        [TestCase("B", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Medium, "General")]
        [TestCase("B", MatterState.Solid, Quantity.Small, DustinessOrVolatility.Medium, "General")]
        [TestCase("B", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.High, "General")]
        [TestCase("B", MatterState.Solid, Quantity.Small, DustinessOrVolatility.High, "General")]
        [TestCase("B", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.Low, "General")]
        [TestCase("B", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.Low, "General")]
        [TestCase("B", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.Medium, "Engineering Controls")]
        [TestCase("B", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.Medium, "Engineering Controls")]
        [TestCase("B", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.High, "Engineering Controls")]
        [TestCase("B", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.High, "Engineering Controls")]
        [TestCase("B", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.Low, "General")]
        [TestCase("B", MatterState.Solid, Quantity.Large, DustinessOrVolatility.Low, "General")]
        [TestCase("B", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.Medium, "Engineering Controls")]
        [TestCase("B", MatterState.Solid, Quantity.Large, DustinessOrVolatility.Medium, "Containment")]
        [TestCase("B", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.High, "Containment")]
        [TestCase("B", MatterState.Solid, Quantity.Large, DustinessOrVolatility.High, "Containment")]
        [TestCase("C", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Low, "General")]
        [TestCase("C", MatterState.Solid, Quantity.Small, DustinessOrVolatility.Low, "General")]
        [TestCase("C", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Medium, "Engineering Controls")]
        [TestCase("C", MatterState.Solid, Quantity.Small, DustinessOrVolatility.Medium, "General")]
        [TestCase("C", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.High, "Engineering Controls")]
        [TestCase("C", MatterState.Solid, Quantity.Small, DustinessOrVolatility.High, "Engineering Controls")]
        [TestCase("C", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.Low, "Engineering Controls")]
        [TestCase("C", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.Low, "Engineering Controls")]
        [TestCase("C", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.Medium, "Containment")]
        [TestCase("C", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.Medium, "Containment")]
        [TestCase("C", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.High, "Containment")]
        [TestCase("C", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.High, "Containment")]
        [TestCase("C", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.Low, "Engineering Controls")]
        [TestCase("C", MatterState.Solid, Quantity.Large, DustinessOrVolatility.Low, "Engineering Controls")]
        [TestCase("C", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.Medium, "Special")]
        [TestCase("C", MatterState.Solid, Quantity.Large, DustinessOrVolatility.Medium, "Special")]
        [TestCase("C", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.High, "Special")]
        [TestCase("C", MatterState.Solid, Quantity.Large, DustinessOrVolatility.High, "Special")]
        [TestCase("D", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Low, "Engineering Controls")]
        [TestCase("D", MatterState.Solid, Quantity.Small, DustinessOrVolatility.Low, "Engineering Controls")]
        [TestCase("D", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Medium, "Containment")]
        [TestCase("D", MatterState.Solid, Quantity.Small, DustinessOrVolatility.Medium, "Containment")]
        [TestCase("D", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.High, "Containment")]
        [TestCase("D", MatterState.Solid, Quantity.Small, DustinessOrVolatility.High, "Containment")]
        [TestCase("D", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.Low, "Containment")]
        [TestCase("D", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.Low, "Containment")]
        [TestCase("D", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.Medium, "Special")]
        [TestCase("D", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.Medium, "Special")]
        [TestCase("D", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.High, "Special")]
        [TestCase("D", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.High, "Special")]
        [TestCase("D", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.Low, "Containment")]
        [TestCase("D", MatterState.Solid, Quantity.Large, DustinessOrVolatility.Low, "Containment")]
        [TestCase("D", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.Medium, "Special")]
        [TestCase("D", MatterState.Solid, Quantity.Large, DustinessOrVolatility.Medium, "Special")]
        [TestCase("D", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.High, "Special")]
        [TestCase("D", MatterState.Solid, Quantity.Large, DustinessOrVolatility.High, "Special")]
        [TestCase("E", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Low, "Special")]
        [TestCase("E", MatterState.Solid, Quantity.Small, DustinessOrVolatility.Low, "Special")]
        [TestCase("E", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Medium, "Special")]
        [TestCase("E", MatterState.Solid, Quantity.Small, DustinessOrVolatility.Medium, "Special")]
        [TestCase("E", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.High, "Special")]
        [TestCase("E", MatterState.Solid, Quantity.Small, DustinessOrVolatility.High, "Special")]
        [TestCase("E", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.Low, "Special")]
        [TestCase("E", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.Low, "Special")]
        [TestCase("E", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.Medium, "Special")]
        [TestCase("E", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.Medium, "Special")]
        [TestCase("E", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.High, "Special")]
        [TestCase("E", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.High, "Special")]
        [TestCase("E", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.Low, "Special")]
        [TestCase("E", MatterState.Solid, Quantity.Large, DustinessOrVolatility.Low, "Special")]
        [TestCase("E", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.Medium, "Special")]
        [TestCase("E", MatterState.Solid, Quantity.Large, DustinessOrVolatility.Medium, "Special")]
        [TestCase("E", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.High, "Special")]
        [TestCase("E", MatterState.Solid, Quantity.Large, DustinessOrVolatility.High, "Special")]
        public void Given_various_parameters_When_calculate_called_Then_returns_correct_control_system(
            string hazardGroupCode, 
            MatterState? matterState, 
            Quantity? quantity, 
            DustinessOrVolatility? dustinessOrVolatility,
            string expectedDescription)
        {
            //When
            var controlSystemCalculationService = new ControlSystemCalculationService(_repository.Object);
            var controlSystem = controlSystemCalculationService.Calculate(hazardGroupCode, matterState, quantity,
                                                                          dustinessOrVolatility);

            //Then
            Assert.AreEqual(expectedDescription, controlSystem.Description);
        }

        [Test]
        public void Given_none_existing_group_When_Calculate_Then_should_throw_correct_exception()
        {
            // Given
            var controlSystemCalculationService = new ControlSystemCalculationService(_repository.Object);

            //When
            //Then
            Assert.Throws<NoneMatchingControlSystemRuleException>(() => controlSystemCalculationService.Calculate("ZZZZZZZZZ", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.High));
            
        }
    }
}