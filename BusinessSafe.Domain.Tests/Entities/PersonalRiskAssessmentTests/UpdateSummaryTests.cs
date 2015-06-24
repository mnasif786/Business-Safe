using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.PersonalRiskAssessmentTests
{
    [TestFixture]
    public class UpdateSummaryTests
    {
        [Test]
        public void Given_the_risk_assessor_is_different_when_Update_RA_summary_then_further_control_measure_completed_email_notification_value_is_updated()
        {
            //Given
             var hazard = new MultiHazardRiskAssessmentHazard();

            //create mocked tasks
            var furtherControlMeasureTasks = new List<Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>>()
                                                 {
                                                     new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>() {CallBase = true},
                                                     new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>() {CallBase = true}
                                                 };

            //Add mocked tasks to hazard
            furtherControlMeasureTasks.ForEach(x => hazard.FurtherControlMeasureTasks.Add(x.Object));

            var riskAss = PersonalRiskAssessment.Create("", "", 123, new UserForAuditing());
            riskAss.RiskAssessor = new RiskAssessor() { Id = 1, DoNotSendTaskCompletedNotifications = true };
            riskAss.Hazards.Add(hazard);

            //when
            riskAss.UpdateSummary("new title", "new ref", new DateTime(), new RiskAssessor() { Id = 2 },true, new Site(), new UserForAuditing());

            //then
            furtherControlMeasureTasks.ForEach(task => task.VerifySet(x => x.SendTaskCompletedNotification, Times.Once()));

            Assert.IsTrue(furtherControlMeasureTasks.All(task => task.Object.SendTaskCompletedNotification.Value));

        }

        [Test]
        public void Given_the_risk_assessor_is_NOT_different_when_Update_RA_summary_then_further_control_measure_completed_email_notification_value_is_NOT_updated()
        {
            //Given
            var hazard = new MultiHazardRiskAssessmentHazard();

            //create mocked tasks
            var furtherControlMeasureTasks = new List<Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>>()
                                                 {
                                                     new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>() {CallBase = true},
                                                     new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>() {CallBase = true}
                                                 };

            //Add mocked tasks to hazard
            furtherControlMeasureTasks.ForEach(x => hazard.FurtherControlMeasureTasks.Add(x.Object));

            var riskAss = PersonalRiskAssessment.Create("", "", 123, new UserForAuditing());
            riskAss.RiskAssessor = new RiskAssessor() { Id = 1, DoNotSendTaskCompletedNotifications = true };
            riskAss.Hazards.Add(hazard);

            //when
            riskAss.UpdateSummary("new title", "new ref", new DateTime(), new RiskAssessor() { Id = 1, DoNotSendTaskCompletedNotifications = true },true, new Site(), new UserForAuditing());

            //then
            furtherControlMeasureTasks.ForEach(task => task.VerifySet(x => x.SendTaskCompletedNotification, Times.Never()));

        }

        [Test]
        public void Given_the_risk_assessor_is_different_when_Update_RA_summary_then_further_control_measure_overdue_email_notification_value_is_updated()
        {
            //Given
            var hazard = new MultiHazardRiskAssessmentHazard();

            //create mocked tasks
            var furtherControlMeasureTasks = new List<Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>>()
                                                 {
                                                     new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>() {CallBase = true},
                                                     new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>() {CallBase = true}
                                                 };

            //Add mocked tasks to hazard assessement
            furtherControlMeasureTasks.ForEach(x => hazard.FurtherControlMeasureTasks.Add(x.Object));
            var riskAss = PersonalRiskAssessment.Create("", "", 123, new UserForAuditing());
            riskAss.RiskAssessor = new RiskAssessor() { Id = 1, DoNotSendTaskCompletedNotifications = true };
            riskAss.Hazards.Add(hazard);

            //when
            riskAss.UpdateSummary("new title", "new ref",  new DateTime(), new RiskAssessor() { Id = 2 }, true,new Site(), new UserForAuditing());

            //then
            furtherControlMeasureTasks.ForEach(task => task.VerifySet(x => x.SendTaskOverdueNotification, Times.Once()));
            Assert.IsTrue(furtherControlMeasureTasks.All(task => task.Object.SendTaskOverdueNotification.Value));
        }

        [Test]
        public void Given_the_risk_assessor_is_NOT_different_when_Update_RA_summary_then_further_control_measure_overdue_email_notification_value_is_NOT_updated()
        {
            //Given
            var hazard = new MultiHazardRiskAssessmentHazard();

            //create mocked tasks
            var furtherControlMeasureTasks = new List<Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>>()
                                                 {
                                                     new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>() {CallBase = true},
                                                     new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>() {CallBase = true}
                                                 };

            //Add mocked tasks to hazard
            furtherControlMeasureTasks.ForEach(x => hazard.FurtherControlMeasureTasks.Add(x.Object));
            var riskAss = PersonalRiskAssessment.Create("", "", 123, new UserForAuditing());
            riskAss.RiskAssessor = new RiskAssessor() { Id = 1, DoNotSendTaskCompletedNotifications = true };
            riskAss.Hazards.Add(hazard);

            //when
            riskAss.UpdateSummary("new title", "new ref",  new DateTime(), new RiskAssessor() { Id = 1, DoNotSendTaskOverdueNotifications = true }, true,new Site(), new UserForAuditing());

            //then
            furtherControlMeasureTasks.ForEach(task => task.VerifySet(x => x.SendTaskOverdueNotification, Times.Never()));

        }
    }
}
