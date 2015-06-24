using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.AddedDocumentTests
{


    [TestFixture]
    [Category("Unit")]
    public class Create
    {

        private DateTime _now;

        [SetUp]
        public void Setup()
        {
            _now = DateTime.Now;
        }

        [Test]
        public void When_requesting_Create_values_are_set_correctly()
        {
            //Given
            var user = new UserForAuditing();

            var createParams = new CreateAddedDocumentParameters()
            {
                ClientId = 1234,
                Title = "wibbly bibbly",
                Description = "Woo hoo",
                DocumentLibraryId = 1,
                DocumentType = new DocumentType(),
                Extension = ".biz",
                Filename = "FooBar",
                FilesizeByte = 2000,
                CreatedBy = user,
                CreatedOn = _now,
                Site = new SiteGroup()
            };


            //When
            var result = AddedDocument.Create(createParams);

            //Then
            Assert.That(result.ClientId, Is.EqualTo(createParams.ClientId));
            Assert.That(result.Title, Is.EqualTo(createParams.Title));
            Assert.That(result.Description, Is.EqualTo(createParams.Description));
            Assert.That(result.DocumentLibraryId, Is.EqualTo(createParams.DocumentLibraryId));
            Assert.That(result.DocumentType, Is.EqualTo(createParams.DocumentType));
            Assert.That(result.Extension, Is.EqualTo(createParams.Extension));
            Assert.That(result.Filename, Is.EqualTo(createParams.Filename));
            Assert.That(result.FilesizeByte, Is.EqualTo(createParams.FilesizeByte));
            Assert.That(result.CreatedBy, Is.EqualTo(user));
            Assert.That(result.CreatedOn, Is.EqualTo(_now));
            Assert.That(result.Site, Is.EqualTo(createParams.Site));
        }

    }
}
