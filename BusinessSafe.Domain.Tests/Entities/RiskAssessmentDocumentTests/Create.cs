using System;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentDocumentTests
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
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask();

            var createParams = new CreateDocumentParameters()
            {
                ClientId = 1234,
                Description = "Woo hoo",
                DocumentLibraryId = 1,
                DocumentType = new DocumentType(),
                Extension = ".biz",
                Filename = "FooBar",
                FilesizeByte = 2000,
                CreatedBy = user,
                CreatedOn = _now
            };


            //When
            var result = RiskAssessmentDocument.Create(createParams);

            //Then
            Assert.That(result.ClientId, Is.EqualTo(createParams.ClientId));
            Assert.That(result.Title, Is.EqualTo(createParams.Filename));
            Assert.That(result.Description, Is.EqualTo(createParams.Description));
            Assert.That(result.DocumentLibraryId, Is.EqualTo(createParams.DocumentLibraryId));
            Assert.That(result.DocumentType, Is.EqualTo(createParams.DocumentType));
            Assert.That(result.Extension, Is.EqualTo(createParams.Extension));
            Assert.That(result.Filename, Is.EqualTo(createParams.Filename));
            Assert.That(result.FilesizeByte, Is.EqualTo(createParams.FilesizeByte));
            Assert.That(result.CreatedBy, Is.EqualTo(user));
            Assert.That(result.CreatedOn, Is.EqualTo(_now));
        }

    }
}
