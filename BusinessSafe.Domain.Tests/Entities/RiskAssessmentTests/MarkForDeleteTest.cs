using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class MarkForDeleteTest
    {
        [Test]
        public void Given_riskassessment_mark_for_delete_Then_should_set_correct_properties()
        {
            //Given
            var riskAssessment = GeneralRiskAssessment.Create("", "", default(long), null);
            var user = new UserForAuditing();

            //When
            riskAssessment.MarkForDelete(user);

            //Then
            Assert.True(riskAssessment.Deleted);
            Assert.That(riskAssessment.LastModifiedBy, Is.EqualTo(user));
            Assert.That(riskAssessment.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }
    }
}