using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.FireRiskAssessmenrChecklistTests
{
    [TestFixture]
    public class MarkChecklistWithCompleteFailureAttemptTests
    {
        [Test]
        public void When_MarkChecklistWithCompleteFailureAttemptTests_Then_should_set_properties_correctly()
        {
            // Given
            var fireRiskAssessmentChecklist = new FireRiskAssessmentChecklist();
            var user = new UserForAuditing();

            // When
            fireRiskAssessmentChecklist.MarkChecklistWithCompleteFailureAttempt(user);

            // Then
            Assert.That(fireRiskAssessmentChecklist.HasCompleteFailureAttempt,Is.True);
            Assert.That(fireRiskAssessmentChecklist.LastModifiedBy, Is.EqualTo(user));
            Assert.That(fireRiskAssessmentChecklist.LastModifiedOn, Is.Not.EqualTo(default(DateTime)));
        }
    }
}