using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentControlMeasureTests
{
    [TestFixture]
    [Category("Unit")]
    public class CloneForRiskAssessmentTemplatingTests
    {
        [Test]
        public void When_clone_for_risk_assessment_templating_Then_should_have_correct_result()
        {
            //Given
            var riskAssessmentHazard = new MultiHazardRiskAssessmentHazard();
            var user = new UserForAuditing();
            var controlMeasure = MultiHazardRiskAssessmentControlMeasure.Create("Control Measure Description", riskAssessmentHazard, user);

            var clonedRiskAssessmentHazard = new MultiHazardRiskAssessmentHazard();

            //When
            var result = controlMeasure.CloneForRiskAssessmentTemplating(clonedRiskAssessmentHazard, user);

            //Then
            Assert.That(result.ControlMeasure, Is.EqualTo(controlMeasure.ControlMeasure));
            Assert.That(result.MultiHazardRiskAssessmentHazard, Is.EqualTo(clonedRiskAssessmentHazard));
            Assert.That(result.CreatedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
            Assert.That(result.CreatedBy, Is.EqualTo(user));

        }
    }
}