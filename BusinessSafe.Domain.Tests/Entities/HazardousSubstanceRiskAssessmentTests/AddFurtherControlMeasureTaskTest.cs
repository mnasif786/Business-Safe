using System;

using BusinessSafe.Domain.Entities;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class AddFurtherControlMeasureTaskTest
    {
        [Test]
        public void Given_Add_FurtherControlMeasureTask_Then_FurtherControlMeasureTask_Is_Added_To_TaskList()
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = Domain.Entities.HazardousSubstanceRiskAssessment.Create("title", "reference", 200, user, null);
            var furtherControlMeasureTask = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask();

            //When
            riskAssessment.AddFurtherControlMeasureTask(furtherControlMeasureTask, user);

            //Then
            Assert.That(riskAssessment.FurtherControlMeasureTasks.Count, Is.EqualTo(1));
            Assert.That(riskAssessment.LastModifiedBy, Is.EqualTo(user));
            Assert.That(riskAssessment.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }
    }
}
