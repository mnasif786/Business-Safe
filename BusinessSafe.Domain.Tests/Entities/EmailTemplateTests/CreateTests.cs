using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.EmailTemplateTests
{
    [TestFixture]
    [Category("Unit")]
    public class CreateTests
    {
        [Test]
        public void Given_all_required_felds_are_available_Then_create_EmailTemplate_method_creates_an_object()
        {
            //Given
            const string name = "CompanySummeryChangeNotification";
            const string subject = "Change in Company Details";
            const string body = "Body";
            
            //When
            var result = EmailTemplate.Create(name, subject, body);

            //Then
            Assert.That(result.Name, Is.Not.Null);
            Assert.That(result.Subject, Is.Not.Null);
            Assert.That(result.Body, Is.Not.Null);
        }
    }
}
