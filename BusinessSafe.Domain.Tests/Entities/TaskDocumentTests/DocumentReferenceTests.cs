using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.TaskDocumentTests
{
    [TestFixture]
    [Category("Unit")]
    public class DocumentReferenceTests
    {
        [Test]
        public void Given_document_without_Task_When_GetDocumentReference_Then_should_throw_correct_exception()
        {
            //Given
            var taskDocument = new TaskDocument {Task = null};

            //When
            //Then
            Assert.Throws<ApplicationException>(()=> taskDocument.DocumentReference.ToLower() );
        }

        [Test]
        public void When_GetDocumentReference_Then_should_return_correct_result()
        {
            //Given
            var riskAssessmentDocument = new TaskDocument();
            
            var riskAssessment = GeneralRiskAssessment.Create("", "Reference", 1, null);
            riskAssessmentDocument.Task = new MultiHazardRiskAssessmentFurtherControlMeasureTask()
                                              {
                                                  MultiHazardRiskAssessmentHazard = MultiHazardRiskAssessmentHazard.Create(riskAssessment,null, null)
                                              };
            //When
            var result = riskAssessmentDocument.DocumentReference;

            //Then
            Assert.That(result, Is.EqualTo("GRA : Reference"));
            
        }
    }
}