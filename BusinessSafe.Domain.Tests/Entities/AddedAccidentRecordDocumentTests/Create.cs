using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.AddedAccidentRecordDocumentTests
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
            var user = new UserForAuditing()
                           {
                               Id = Guid.NewGuid()
                           };


            var createAccidentRecordParams = new AccidentRecord()
                                                 {
                                                     CompanyId = 1L,
                                                     Title = "title",
                                                     Reference = "reference",
                                                     CreatedBy = user,
                                                     CreatedOn = _now,
                                                     Deleted = false
                                                 };

            DocumentType docType = new DocumentType() {Name = "Accident Record Doc", Id = 17};

            //When
            var result = AccidentRecordDocument.Create("description", "Filename.ext", 1L, docType, createAccidentRecordParams, user);

            //Then
            Assert.That(result.ClientId, Is.EqualTo(1L));
            Assert.That(result.DocumentLibraryId, Is.EqualTo(1L));
            Assert.That(result.Description, Is.EqualTo("description"));
            Assert.That(result.Title, Is.EqualTo("Filename.ext"));
            Assert.That(result.Filename, Is.EqualTo("Filename.ext"));
            Assert.That(result.CreatedBy, Is.EqualTo(createAccidentRecordParams.CreatedBy));
        }
    }
}
