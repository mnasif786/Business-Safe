using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;
using System.Linq;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class UpdateControlMeasureForRiskAssessmentHazardTest
    {
        [Test]
        public void Given_controlmeasure_Then_should_update_correctly()
        {
            //Given
            string target = "Test";
            var user = new UserForAuditing();
            var riskAssessment = GeneralRiskAssessment.Create("", "", default(long), null);
            var hazard = new Hazard();

            var riskAssessmentHazard = MultiHazardRiskAssessmentHazard.Create(riskAssessment, hazard, user);
            riskAssessmentHazard.Id = 1;

            var controlMeasure = MultiHazardRiskAssessmentControlMeasure.Create("", riskAssessmentHazard, user);
            controlMeasure.Id = 1;

            riskAssessmentHazard.ControlMeasures.Add(controlMeasure);
            riskAssessment.Hazards.Add(riskAssessmentHazard);

            //When
            riskAssessment.UpdateControlMeasureForRiskAssessmentHazard(1, 1, target, user);

            //Then
            Assert.That(riskAssessmentHazard.ControlMeasures.Count(c => c.ControlMeasure == target), Is.EqualTo(1));
            Assert.That(riskAssessmentHazard.LastModifiedBy, Is.EqualTo(user));
        }
    }
}