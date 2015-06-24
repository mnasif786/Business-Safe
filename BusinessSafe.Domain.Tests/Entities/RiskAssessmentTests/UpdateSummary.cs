using System;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;


namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentTests
{
    [TestFixture]
    public class UpdateSummary
    {
        [Test]
        public void Given_all_required_felds_are_available_When_UpdatePremisesInformation_Then_Values_Updated()
        {
            //Given
            const string title = "RA Test";
            const string reference = "RA 001";
            const long clientId = 100;
            var user = new UserForAuditing();
            var riskAssessor = new RiskAssessor();
            var site = new Site();
            var dateOfAssessment = DateTime.Now;
            var result = GeneralRiskAssessment.Create(title, reference, clientId, user, null);
            
            //When
            result.UpdateSummary("new title", "new ref", dateOfAssessment, riskAssessor, site, user);

            //Then
            Assert.That(result.Title, Is.EqualTo("new title"));
            Assert.That(result.Reference, Is.EqualTo("new ref"));
            Assert.That(result.AssessmentDate, Is.EqualTo(dateOfAssessment));
            Assert.That(result.RiskAssessor, Is.EqualTo(riskAssessor));
            Assert.That(result.RiskAssessmentSite,Is.EqualTo(site));
            Assert.That(result.LastModifiedBy, Is.EqualTo(user));
            Assert.That(result.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
        }

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

            //Add mocked tasks to risk assessement
            furtherControlMeasureTasks.ForEach(x => hazard.FurtherControlMeasureTasks.Add(x.Object));

            var riskAss = GeneralRiskAssessment.Create("", "", 123, new UserForAuditing());
            riskAss.Hazards.Add(hazard);
            //add a hazard without any tasks
            riskAss.Hazards.Add(new MultiHazardRiskAssessmentHazard() {});

            riskAss.RiskAssessor = new RiskAssessor() {Id = 1, DoNotSendTaskCompletedNotifications = true};

            //when
            riskAss.UpdateSummary("new title", "new ref", new DateTime(), new RiskAssessor() {Id = 2}, new Site(), new UserForAuditing());

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

            //Add mocked tasks to risk assessement
            furtherControlMeasureTasks.ForEach(x => hazard.FurtherControlMeasureTasks.Add(x.Object));

            var riskAss = GeneralRiskAssessment.Create("", "", 123, new UserForAuditing());
            riskAss.Hazards.Add(hazard);
            riskAss.RiskAssessor = new RiskAssessor() { Id = 1, DoNotSendTaskCompletedNotifications = true };

            //when
            riskAss.UpdateSummary("new title", "new ref", new DateTime(), new RiskAssessor() { Id = 1, DoNotSendTaskCompletedNotifications = true }, new Site(), new UserForAuditing());

            //then
            furtherControlMeasureTasks.ForEach(task => task.VerifySet(x => x.SendTaskCompletedNotification,Times.Never()));

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

            //Add mocked tasks to risk assessement
            furtherControlMeasureTasks.ForEach(x => hazard.FurtherControlMeasureTasks.Add(x.Object));

            var riskAss = GeneralRiskAssessment.Create("", "", 123, new UserForAuditing());
            riskAss.Hazards.Add(hazard);
            riskAss.RiskAssessor = new RiskAssessor() { Id = 1, DoNotSendTaskOverdueNotifications = true };

            //when
            riskAss.UpdateSummary("new title", "new ref", new DateTime(), new RiskAssessor() { Id = 2 }, new Site(), new UserForAuditing());

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

            //Add mocked tasks to risk assessement
            furtherControlMeasureTasks.ForEach(x => hazard.FurtherControlMeasureTasks.Add(x.Object));

            var riskAss = GeneralRiskAssessment.Create("", "", 123, new UserForAuditing());
            riskAss.Hazards.Add(hazard);
            riskAss.RiskAssessor = new RiskAssessor() { Id = 1,DoNotSendTaskOverdueNotifications = true };

            //when
            riskAss.UpdateSummary("new title", "new ref", new DateTime(), new RiskAssessor() { Id = 1, DoNotSendTaskOverdueNotifications = true }, new Site(), new UserForAuditing());

            //then
            furtherControlMeasureTasks.ForEach(task => task.VerifySet(x => x.SendTaskOverdueNotification, Times.Never()));

        }

        
    }
}