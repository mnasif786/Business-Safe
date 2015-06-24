using System;
using System.Linq;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentHazardTests
{
    [TestFixture]
    [Category("Unit")]
    public class AddFurtherActionTaskTests
    {
        [Test]
        public void When_addfurtheractiontasks_Then_should_add_correctly()
        {
            //Given
            var user = new UserForAuditing();
            var hazard = new Hazard();
            var riskAssessment = new GeneralRiskAssessment();
            
            var target = MultiHazardRiskAssessmentHazard.Create(riskAssessment, hazard, user);

            var furtherActionTask = new MultiHazardRiskAssessmentFurtherControlMeasureTask();
            
            //When
            target.AddFurtherActionTask(furtherActionTask, user);

            //Then
            Assert.That(target.FurtherControlMeasureTasks.Count, Is.EqualTo(1));
            Assert.That(target.FurtherControlMeasureTasks.First().MultiHazardRiskAssessmentHazard, Is.EqualTo(target));
            Assert.That(target.LastModifiedBy, Is.EqualTo(user));
            Assert.That(target.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today.Date));
            
        }
    }
}