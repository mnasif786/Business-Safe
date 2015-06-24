using System;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class UpdateControlMeasureToRiskAssessmentTest
    {
        [Test]
        public void Given_controlmeasure_not_attached_to_risk_assessment_Then_should_throw_correct_exception()
        {
            //Given
            var user = new UserForAuditing();
            long controlMeasureId = 500;

            var riskAssessment = Domain.Entities.HazardousSubstanceRiskAssessment.Create("title", "reference", 200, user, null);
            
            //When
            //Then
            Assert.Throws<ControlMeasureDoesNotExistOnRiskAssessmentException>(() => riskAssessment.UpdateControlMeasure(controlMeasureId, "Updated Control Measure Description", user));
        }

        [Test]
        public void Given_update_controlmeasure_on_risk_assessment_Then_should_call_correct_methods()
        {
            //Given
            var user = new UserForAuditing();
            long controlMeasureId = 500;
            var riskAssessment = Domain.Entities.HazardousSubstanceRiskAssessment.Create("title", "reference", 200, user, null);

            var mockControlMeasure = new Mock<HazardousSubstanceRiskAssessmentControlMeasure>();
            mockControlMeasure.SetupGet(x => x.Id).Returns(controlMeasureId);

            riskAssessment.AddControlMeasure(mockControlMeasure.Object, user);

            //When
            riskAssessment.UpdateControlMeasure(controlMeasureId, "Updated Control Measure Description", user);
            
            //Then
            mockControlMeasure.Verify(x => x.UpdateControlMeasure("Updated Control Measure Description", user));
        }

        [Test]
        public void Given_update_controlmeasure_on_risk_assessment_Then_should_set_correct_properties()
        {
            //Given
            var user = new UserForAuditing();
            long controlMeasureId = 500;
            var riskAssessment = Domain.Entities.HazardousSubstanceRiskAssessment.Create("title", "reference", 200, user, null);

            var mockControlMeasure = new Mock<HazardousSubstanceRiskAssessmentControlMeasure>();
            mockControlMeasure.SetupGet(x => x.Id).Returns(controlMeasureId);

            riskAssessment.AddControlMeasure(mockControlMeasure.Object, user);

            //When
            riskAssessment.UpdateControlMeasure(controlMeasureId, "Updated Control Measure Description", user);

            //Then
            Assert.That(riskAssessment.LastModifiedBy, Is.EqualTo(user));
            Assert.That(riskAssessment.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }
    }
}