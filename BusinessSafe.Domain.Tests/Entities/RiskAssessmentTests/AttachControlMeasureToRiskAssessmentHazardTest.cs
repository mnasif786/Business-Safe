using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class AttachControlMeasureToRiskAssessmentHazardTest
    {
        [Test]
        public void Given_controlmeasure_Then_should_add_correctly()
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = GeneralRiskAssessment.Create("", "", default(long), null);
            var hazard = new Hazard();
            var riskAssessmentHazard = MultiHazardRiskAssessmentHazard.Create(riskAssessment, hazard, user);

            riskAssessmentHazard.Id = 1;
            
            var controlMeasure = MultiHazardRiskAssessmentControlMeasure.Create("", riskAssessmentHazard, user);

            riskAssessment.Hazards.Add(riskAssessmentHazard);

            //When
            riskAssessmentHazard.AddControlMeasure(controlMeasure, user);

            //Then
            Assert.That(riskAssessmentHazard.ControlMeasures.Count, Is.EqualTo(1));
            Assert.That(riskAssessmentHazard.LastModifiedBy, Is.EqualTo(user));
        }
    }
}