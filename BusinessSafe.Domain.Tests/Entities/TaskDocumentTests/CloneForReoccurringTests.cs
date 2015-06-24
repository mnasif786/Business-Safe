using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.TaskDocumentTests
{
    [TestFixture]
    [Category("Unit")]
    public class CloneForReoccurringTests
    {
        [Test]
        public void When_reassign_Then_should_set_properties_correctly()
        {
            //Given
            var document = new TaskDocument()
                               {
                                   Description = "Woo hoo",
                                   DocumentLibraryId = 1,
                                   DocumentType = new DocumentType(),
                                   Extension = ".biz",
                                   Filename = "FooBar",
                                   FilesizeByte = 2000
                               };

            var user = new UserForAuditing();
            

            //When
            var result = document.CloneForReoccurring(user);

            //Then
            Assert.That(result.DocumentLibraryId, Is.EqualTo(document.DocumentLibraryId));
            Assert.That(result.Filename, Is.EqualTo(document.Filename));
            Assert.That(result.Extension, Is.EqualTo(document.Extension));
            Assert.That(result.FilesizeByte, Is.EqualTo(document.FilesizeByte));
            Assert.That(result.Description, Is.EqualTo(document.Description));
            Assert.That(result.CreatedBy, Is.EqualTo(user));
            Assert.That(result.DocumentType, Is.EqualTo(document.DocumentType));
            Assert.That(result.CreatedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }

        
    }
}