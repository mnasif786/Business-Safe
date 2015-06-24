using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.SignificantFindingTests
{
    [TestFixture]
    public class AddFurtherControlMeasureTaskTests
    {
        [Test]
        public void When_adding_Then_should_set_correct_properties()
        {
            // Given
            var finding = new SignificantFinding();
            var user = new UserForAuditing();

            // When
            var task = new FireRiskAssessmentFurtherControlMeasureTask();
            finding.AddFurtherControlMeasureTask(task, user);

            // Then
            Assert.That(finding.FurtherControlMeasureTasks.Count, Is.EqualTo(1));
            Assert.That(finding.LastModifiedBy, Is.EqualTo(user));
            Assert.That(finding.LastModifiedOn.Value.ToShortDateString(), Is.EqualTo(DateTime.Now.ToShortDateString()));
        }
    }
}
