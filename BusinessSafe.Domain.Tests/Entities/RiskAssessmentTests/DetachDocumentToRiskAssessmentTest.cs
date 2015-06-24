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
    public class DetachDocumentToRiskAssessmentTest
    {
        [Test]
        public void Given_valid_document_to_detach_Then_should_set_correct_properties()
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
            riskAssessment.DetachDocumentFromRiskAssessment(new[]{ riskAssessmentDocument.DocumentLibraryId}, user);

            //Then
            Assert.That(riskAssessment.Documents.Count(x => !x.Deleted), Is.EqualTo(0));
            Assert.That(riskAssessment.Documents.Count(x => x.Id == riskAssessmentDocument.Id && !x.Deleted), Is.EqualTo(0));
            Assert.That(riskAssessment.LastModifiedBy, Is.EqualTo(user));
            Assert.That(riskAssessment.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void Given_invalid_document_to_detach_document_not_attached_Then_should_throw_correct_exceptions()
        {
            //Given
            var riskAssessment = GeneralRiskAssessment.Create("", "", default(long), null);
            var user = new UserForAuditing();

            //When
            //Then
            Assert.Throws<RiskAssessmentDocumentDoesNotExistInRiskAssessmentException>(() => riskAssessment.DetachDocumentFromRiskAssessment(new long[] { 1 }, user));

        }
    }
}