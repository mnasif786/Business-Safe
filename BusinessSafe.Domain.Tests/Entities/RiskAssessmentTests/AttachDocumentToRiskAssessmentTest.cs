using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class AttachDocumentToRiskAssessmentTest
    {
        [Test]
        public void Given_valid_document_to_attach_Then_should_set_correct_properties()
        {
            //Given
            var riskAssessment = GeneralRiskAssessment.Create("", "", default(long), null);
            var user = new UserForAuditing();

            var riskAssessmentDocument = new RiskAssessmentDocument()
                                             {
                                                 Id = 1
                                             };
            var documents = new List<RiskAssessmentDocument>(){ riskAssessmentDocument};
            
            //When
            riskAssessment.AttachDocumentToRiskAssessment(documents , user);

            //Then
            Assert.That(riskAssessment.Documents.Count, Is.EqualTo(1));
            Assert.That(riskAssessment.Documents.Count(x => x.Id == riskAssessmentDocument.Id), Is.EqualTo(1));
            Assert.That(riskAssessment.LastModifiedBy, Is.EqualTo(user));
            Assert.That(riskAssessment.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void Given_invalid_document_to_attach_already_attached_Then_should_throw_correct_exceptions()
        {
            //Given
            var riskAssessment = GeneralRiskAssessment.Create("", "", default(long), null);
            var user = new UserForAuditing();

            var riskAssessmentDocument = new RiskAssessmentDocument()
            {
                DocumentLibraryId = 1
            };
            var documents = new List<RiskAssessmentDocument>() { riskAssessmentDocument };
            riskAssessment.AttachDocumentToRiskAssessment(documents, user);

            //When
            //Then
            Assert.Throws<DocumentAlreadyAttachedToRiskAssessmentException>(() => riskAssessment.AttachDocumentToRiskAssessment(documents, user));
            
        }
    }
}