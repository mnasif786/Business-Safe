using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentDocumentTests
{
    [TestFixture]
    [Category("Unit")]
    public class DocumentReferenceTests
    {
        [Test]
        public void Given_document_without_RiskAssessment_When_GetDocumentReference_Then_should_throw_correct_exception()
        {
            //Given
            var riskAssessmentDocument = new RiskAssessmentDocument {RiskAssessment = null};

            //When
            //Then
            Assert.Throws<ApplicationException>(() => riskAssessmentDocument.DocumentReference.ToLower());
        }

        [Test]
        public void When_GetDocumentReference_Then_should_return_correct_result()
        {
            //Given
            var riskAssessmentDocument = new RiskAssessmentDocument();
            riskAssessmentDocument.RiskAssessment = GeneralRiskAssessment.Create("", "Reference", 1, null);
            
            //When
            var result = riskAssessmentDocument.DocumentReference;

            //Then
            Assert.That(result, Is.EqualTo("GRA : Reference"));
            
        }
    }
}