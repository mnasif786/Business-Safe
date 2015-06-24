using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class RiskAssessmentMarkAsArchivedTest
    {
        [Test]
        public void Given_riskassessment_mark_as_draft_Then_should_set_correct_properties()
        {
            //Given
            var riskAssessment = HazardousSubstanceRiskAssessment.Create("", "", default(long), null, null);
            var user = new UserForAuditing();

            //When
            riskAssessment.MarkAsArchived(user);

            //Then
            Assert.That(riskAssessment.Status, Is.EqualTo(RiskAssessmentStatus.Archived));
            Assert.That(riskAssessment.LastModifiedBy, Is.EqualTo(user));
            Assert.That(riskAssessment.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }
    }
}