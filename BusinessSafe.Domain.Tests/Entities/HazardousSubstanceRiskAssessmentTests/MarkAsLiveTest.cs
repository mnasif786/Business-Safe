using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class MarkAsLiveTest
    {
        [Test]
        public void Given_riskassessment_mark_as_live_Then_should_set_correct_properties()
        {
            //Given
            var riskAssessment = Domain.Entities.HazardousSubstanceRiskAssessment.Create("", "", default(long), null, null);
            var user = new UserForAuditing();


            //When
            riskAssessment.MarkAsLive(user);

            //Then
            Assert.That(riskAssessment.Status, Is.EqualTo(RiskAssessmentStatus.Live));
            Assert.That(riskAssessment.LastModifiedBy, Is.EqualTo(user));
            Assert.That(riskAssessment.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }
    }
}