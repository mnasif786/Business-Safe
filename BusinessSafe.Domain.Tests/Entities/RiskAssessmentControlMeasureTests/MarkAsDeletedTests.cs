using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentControlMeasureTests
{
    [TestFixture]
    [Category("Unit")]
    public class MarkAsDeletedTests
    {
        [Test]
        public void When_MarkAsDeleted_Then_set_last_modified()
        {
            //Given
            var riskAssessmentHazard = new MultiHazardRiskAssessmentHazard();
            var user = new UserForAuditing();
            var controlMeasure = MultiHazardRiskAssessmentControlMeasure.Create("Control Measure Description", riskAssessmentHazard, user);

            //When
            controlMeasure.MarkAsDeleted(user);

            //Then
            Assert.That(controlMeasure.LastModifiedBy, Is.EqualTo(user));
            Assert.That(controlMeasure.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
        }
        [Test]
        public void When_MarkAsDeleted_Then_set_deleted_to_true()
        {
            //Given
            var riskAssessmentHazard = new MultiHazardRiskAssessmentHazard();
            var user = new UserForAuditing();
            var controlMeasure = MultiHazardRiskAssessmentControlMeasure.Create("Control Measure Description", riskAssessmentHazard, user);

            //When
            controlMeasure.MarkAsDeleted(user);

            //Then
            Assert.That(controlMeasure.Deleted, Is.True);
        }
    }
}