using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentHazardTests
{
    [TestFixture]
    [Category("Unit")]
    public class CloneForRiskAssessmentTemplatingTests
    {
        [Test]
        public void When_clone_for_risk_assessment_templating_Then_should_have_correct_result()
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = new GeneralRiskAssessment();
            var hazard = new Hazard();

            var riskAssessmentHazard = MultiHazardRiskAssessmentHazard.Create(riskAssessment, hazard, user);
            riskAssessmentHazard.UpdateDescription("Test Description", user);

            var controlMeasure1 = new MultiHazardRiskAssessmentControlMeasure();
            var controlMeasure2 = new MultiHazardRiskAssessmentControlMeasure();
            riskAssessmentHazard.AddControlMeasure(controlMeasure1, user);
            riskAssessmentHazard.AddControlMeasure(controlMeasure2, user);


            //When
            var result = riskAssessmentHazard.CloneForRiskAssessmentTemplating(user, riskAssessment);

            //Then
            Assert.That(result.Description, Is.EqualTo(riskAssessmentHazard.Description));
            Assert.That(result.MultiHazardRiskAssessment, Is.EqualTo(riskAssessment));
            Assert.That(result.Hazard, Is.EqualTo(hazard));
            Assert.That(result.CreatedBy, Is.EqualTo(user));
            Assert.That(result.CreatedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
            Assert.That(result.ControlMeasures.Count, Is.EqualTo(riskAssessmentHazard.ControlMeasures.Count));
        }
    }
}