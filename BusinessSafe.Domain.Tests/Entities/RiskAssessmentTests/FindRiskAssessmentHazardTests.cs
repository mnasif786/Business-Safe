using System.Linq;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class FindRiskAssessmentHazardTests
    {
        [Test]
        public void Given_riskassessmenthazardid_that_exists_When_Find_Risk_Assessment_Hazard_Then_should_return_hazard()
        {
            //Given
            var target = GeneralRiskAssessment.Create("", "", default(long), null);
            
            var user = new UserForAuditing();
            var hazard = new Hazard();
            target.AttachHazardsToRiskAssessment(new[] { hazard }, user);


            var hazardId = target.Hazards.First().Id;

            //When
            var result = target.FindRiskAssessmentHazard(hazardId);

            //Then
            Assert.That(result, Is.Not.Null);
            
        }

        [Test]
        public void Given_riskassessmenthazardid_does_not_exists_When_Find_Risk_Assessment_Hazard_Then_should_throw_correct_exception()
        {
            //Given
            var target = GeneralRiskAssessment.Create("", "", default(long), null);
            var hazardId = 1;
            
            //When
            //Then
            Assert.Throws<HazardDoesNotExistInRiskAssessmentException>(() => target.FindRiskAssessmentHazard(hazardId));
            
        }
    }
}