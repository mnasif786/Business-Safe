using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class CreateTests
    {
        [Test]
        public void Given_all_required_felds_are_available_Then_create_RiskAssessment_method_creates_an_object()
        {
            //Given
            const string title = "RA Test";
            const string reference = "RA 001";
            const long clientId = 100;
            var user = new UserForAuditing();

            //When
            var result = GeneralRiskAssessment.Create(title, reference, clientId, user);

            //Then
            Assert.That(result.Title, Is.EqualTo(title));
            Assert.That(result.Reference, Is.EqualTo(reference));
            Assert.That(result.CompanyId, Is.EqualTo(clientId));
            Assert.That(result.CreatedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(result.CreatedBy, Is.EqualTo(user));
            Assert.That(result.Status, Is.EqualTo(RiskAssessmentStatus.Draft));
        }
    }
}
