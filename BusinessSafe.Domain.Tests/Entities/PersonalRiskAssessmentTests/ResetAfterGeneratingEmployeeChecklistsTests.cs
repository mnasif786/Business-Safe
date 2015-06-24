using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.PersonalRiskAssessmentTests
{
    [TestFixture]
    public class ResetAfterGeneratingEmployeeChecklistsTests
    {
        [Test]
        public void When_ResetAfterGeneratingEmployeeChecklists_Then_Resets()
        {
            // Given
            const string title = "Title";
            const string reference = "Ref";
            const int clientId = 100;
            var user = new UserForAuditing();

            var result = PersonalRiskAssessment.Create(title, reference, clientId, user);
            result.Checklists = new List<PersonalRiskAssessmentChecklist>();
            result.ChecklistGeneratorEmployees = new List<ChecklistGeneratorEmployee>();

            // When
            result.ResetAfterGeneratingEmployeeChecklists(user);

            // Then
            Assert.That(result.HasMultipleChecklistRecipients, Is.Null);
            Assert.That(result.SendCompletedChecklistNotificationEmail, Is.Null);
            Assert.That(result.CompletionDueDateForChecklists, Is.Null);
            Assert.That(result.CompletionNotificationEmailAddress, Is.Null);
            Assert.That(result.ChecklistGeneratorMessage, Is.Null);
            Assert.That(result.ChecklistGeneratorEmployees.Count(x => !x.Deleted), Is.EqualTo(0));
            Assert.That(result.Checklists.Count(x => !x.Deleted), Is.EqualTo(0));
        }
    }
}