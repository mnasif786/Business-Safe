using System;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentDocumentTests
{
    [TestFixture]
    [Category("Unit")]
    public class CloneForRiskAssessmentTemplatingTests
    {
        [Test]
        public void When_clone_for_risk_assessment_templating_Then_should_have_correct_result()
        {
            //Given
            var user = new UserForAuditing();
            var parameters = new CreateDocumentParameters()
                                 {
                                     ClientId = 1234,
                                     CreatedBy = user,
                                     Description = "Testing Description",
                                     DocumentLibraryId = 1,
                                     DocumentType = new DocumentType(),
                                     Filename = "Test File Name",
                                     Extension = "bb bb bb",
                                     FilesizeByte = 1024
                                 };
            var riskAssessmentDocument = RiskAssessmentDocument.Create(parameters);

            //When
            var result = riskAssessmentDocument.CloneForRiskAssessmentTemplating(user);

            //Then
            Assert.That(result.ClientId, Is.EqualTo(riskAssessmentDocument.ClientId));
            Assert.That(result.DocumentLibraryId, Is.EqualTo(riskAssessmentDocument.DocumentLibraryId));
            Assert.That(result.Filename, Is.EqualTo(riskAssessmentDocument.Filename));
            Assert.That(result.Extension, Is.EqualTo(riskAssessmentDocument.Extension));
            Assert.That(result.FilesizeByte, Is.EqualTo(riskAssessmentDocument.FilesizeByte));
            Assert.That(result.Description, Is.EqualTo(riskAssessmentDocument.Description));
            Assert.That(result.DocumentType, Is.EqualTo(riskAssessmentDocument.DocumentType));
            Assert.That(result.CreatedBy, Is.EqualTo(user));
            Assert.That(result.CreatedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }
    }
}
