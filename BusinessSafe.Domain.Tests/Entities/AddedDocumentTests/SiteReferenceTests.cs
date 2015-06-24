using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.AddedDocumentTests
{
    [TestFixture]
    [Category("Unit")]
    public class SiteReferenceTests
    {
        [Test]
        public void When_SiteReference_Then_should_return_correct_result()
        {
            //Given
            //When
            var result = new AddedDocument()
                             {
                                 Site = Site.Create(1,null,1,"Site Name", "Site Reference", "", new UserForAuditing())                                          
                             }.SiteReference;

            //Then
            Assert.That(result, Is.EqualTo("Site Name"));

        }
    }
}