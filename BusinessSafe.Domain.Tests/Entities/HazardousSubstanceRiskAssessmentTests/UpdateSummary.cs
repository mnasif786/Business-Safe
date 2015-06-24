using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    public class UpdateSummary
    {
        private HazardousSubstanceRiskAssessment _riskAss;
        private List<Mock<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>> _furtherControlMeasureTasks;

        [SetUp]
        public void SetUp()
        {
            //create mocked tasks
            _furtherControlMeasureTasks = new List<Mock<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>>()
                                                 {
                                                     new Mock<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>() {CallBase = true},
                                                     new Mock<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>() {CallBase = true}
                                                 };



            _riskAss = HazardousSubstanceRiskAssessment.Create("", "", 123, new UserForAuditing(), new HazardousSubstance());
            _riskAss.RiskAssessor = new RiskAssessor() { Id = 1, DoNotSendTaskCompletedNotifications = true };

            //Add mocked tasks to risk assessment
            _furtherControlMeasureTasks.ForEach(x => _riskAss.FurtherControlMeasureTasks.Add(x.Object));

        }

        [Test]
        public void Given_all_required_felds_are_available_When_UpdatePremisesInformation_Then_Values_Updated()
        {
            //Given
            const string title = "RA Test";
            const string reference = "RA 001";
            const long clientId = 100;
            var user = new UserForAuditing();
            var riskAssessor = new RiskAssessor();
            var hazsub = new HazardousSubstance() {Id = 1};
            var newHazsub = new HazardousSubstance() {Id = 2};
            var dateOfAssessment = DateTime.Now;
            var site = new Site();
            var result = HazardousSubstanceRiskAssessment.Create(title, reference, clientId, user, hazsub);

            //When
            result.UpdateSummary("new title", "new ref", dateOfAssessment, riskAssessor, newHazsub, site, user);

            //Then
            Assert.That(result.Title, Is.EqualTo("new title"));
            Assert.That(result.Reference, Is.EqualTo("new ref"));
            Assert.That(result.AssessmentDate, Is.EqualTo(dateOfAssessment));
            Assert.That(result.RiskAssessor, Is.EqualTo(riskAssessor));
            Assert.That(result.RiskAssessmentSite, Is.EqualTo(site));
            Assert.That(result.LastModifiedBy, Is.EqualTo(user));
            Assert.That(result.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(result.HazardousSubstance, Is.EqualTo(newHazsub));
        }

        [Test]
        public void Given_the_risk_assessor_is_different_when_Update_RA_summary_then_further_control_measure_completed_email_notification_value_is_updated()
        {
            //Given

            //when
            _riskAss.UpdateSummary("new title", "new ref", new DateTime(), new RiskAssessor() { Id = 2 }, new HazardousSubstance(), new Site(), new UserForAuditing());

            //then
            _furtherControlMeasureTasks.ForEach(task => task.VerifySet(x => x.SendTaskCompletedNotification, Times.Once()));

            Assert.IsTrue(_furtherControlMeasureTasks.All(task => task.Object.SendTaskCompletedNotification.Value));

        }

        [Test]
        public void Given_the_risk_assessor_is_NOT_different_when_Update_RA_summary_then_further_control_measure_completed_email_notification_value_is_NOT_updated()
        {
            //Given

            //when
            _riskAss.UpdateSummary("new title", "new ref", new DateTime(), new RiskAssessor() {Id = 1, DoNotSendTaskCompletedNotifications = true}, new HazardousSubstance(), new Site(), new UserForAuditing());

            //then
            _furtherControlMeasureTasks.ForEach(task => task.VerifySet(x => x.SendTaskCompletedNotification, Times.Never()));

        }

        [Test]
        public void Given_the_risk_assessor_is_different_when_Update_RA_summary_then_further_control_measure_overdue_email_notification_value_is_updated()
        {
            //Given

            //when
            _riskAss.UpdateSummary("new title", "new ref", new DateTime(), new RiskAssessor() { Id = 2 }, new HazardousSubstance(), new Site(), new UserForAuditing());

            //then
            _furtherControlMeasureTasks.ForEach(task => task.VerifySet(x => x.SendTaskOverdueNotification, Times.Once()));
            Assert.IsTrue(_furtherControlMeasureTasks.All(task => task.Object.SendTaskOverdueNotification.Value));
        }

        [Test]
        public void Given_the_risk_assessor_is_NOT_different_when_Update_RA_summary_then_further_control_measure_overdue_email_notification_value_is_NOT_updated()
        {
            //Given

            //when
            _riskAss.UpdateSummary("new title", "new ref", new DateTime(), new RiskAssessor() { Id = 1, DoNotSendTaskOverdueNotifications = true }, new HazardousSubstance(), new Site(), new UserForAuditing());

            //then
            _furtherControlMeasureTasks.ForEach(task => task.VerifySet(x => x.SendTaskOverdueNotification, Times.Never()));

        }

     
    }
}