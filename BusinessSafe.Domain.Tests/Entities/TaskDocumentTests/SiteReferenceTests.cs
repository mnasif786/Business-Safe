using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.TaskDocumentTests
{
    [TestFixture]
    [Category("Unit")]
    public class SiteReferenceTests
    {
        [Test]
        public void When_SiteReference_Then_should_return_correct_result()
        {
            //Given
            var riskAssessmentDocument = new TaskDocument();
            const int riskAssessmentId = 200;

            var riskAssessment = new GeneralRiskAssessment()
            {
                Id = riskAssessmentId,
                RiskAssessmentSite = Site.Create(1,null,1,"Site Name","", "", new UserForAuditing())
            };
            riskAssessmentDocument.Task = new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                MultiHazardRiskAssessmentHazard = MultiHazardRiskAssessmentHazard.Create(riskAssessment, null, null)
            };
            //When
            var result = riskAssessmentDocument.SiteReference;

            //Then
            Assert.That(result, Is.EqualTo("Site Name"));

        }
    }
}