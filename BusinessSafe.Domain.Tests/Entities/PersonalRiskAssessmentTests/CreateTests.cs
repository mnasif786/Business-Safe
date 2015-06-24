
using System;

using BusinessSafe.Domain.Entities;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.PersonalRiskAssessmentTests
{
    [TestFixture]
    public class CreateTests
    {
        [Test]
        public void When_Create_Then_Populate_And_Return_PersonalRiskAssessment()
        {
            // Given
            const string title = "Title";
            const string reference = "Ref";
            const int clientId = 100;
            var user = new UserForAuditing();

            // When
            var result = PersonalRiskAssessment.Create(title, reference, clientId, user);

            // Then
            Assert.That(result, Is.InstanceOf<PersonalRiskAssessment>());
            Assert.That(result.Title, Is.EqualTo(title));
            Assert.That(result.Reference, Is.EqualTo(reference));
            Assert.That(result.CompanyId, Is.EqualTo(clientId));
            Assert.That(result.CreatedBy, Is.EqualTo(user));

            var createdDate = (DateTime)result.CreatedOn;
            Assert.That(createdDate.Date, Is.EqualTo(DateTime.Today));
            Assert.That(result.Status, Is.EqualTo(RiskAssessmentStatus.Draft));
        }
    }
}
