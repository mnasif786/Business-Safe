using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.FireRiskAssessmentTests
{
    [TestFixture]
    public class UpdateSummary
    {
        [Test]
        public void Given_all_required_felds_are_available_When_UpdatePremisesInformation_Then_Values_Updated()
        {
            //Given
            const string title = "RA Test";
            const string personAppointed = "Person Appointed";
            const string reference = "RA 001";
            const long clientId = 100;
            var user = new UserForAuditing();
            var riskAssessor = new RiskAssessor();
            var dateOfAssessment = DateTime.Now;
            var site = new Site();
            var result = FireRiskAssessment.Create(title, reference, clientId, new Checklist(), user);
            
            //When
            result.UpdateSummary("new title", "new ref", personAppointed, dateOfAssessment, riskAssessor, site, user);

            //Then
            Assert.That(result.Title, Is.EqualTo("new title"));
            Assert.That(result.PersonAppointed, Is.EqualTo(personAppointed));
            Assert.That(result.Reference, Is.EqualTo("new ref"));
            Assert.That(result.AssessmentDate, Is.EqualTo(dateOfAssessment));
            Assert.That(result.RiskAssessor, Is.EqualTo(riskAssessor));
            Assert.That(result.RiskAssessmentSite, Is.EqualTo(site));
            Assert.That(result.LastModifiedBy, Is.EqualTo(user));
            Assert.That(result.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
        }

        [Test]
        public void Given_the_risk_assessor_is_different_when_Update_RA_summary_then_further_control_measure_completed_email_notification_value_is_updated()
        {
            //Given
            var riskAss = FireRiskAssessment.Create("", "", 123, new Checklist(), new UserForAuditing());
            riskAss.RiskAssessor = new RiskAssessor() { Id = 1, DoNotSendTaskCompletedNotifications = true };
            //Add significant finding without any further control measure tasks
            riskAss.LatestFireRiskAssessmentChecklist.Answers.Add(new FireAnswer()
            {
                SignificantFinding = new SignificantFinding() { FurtherControlMeasureTasks = null }
            });


            //create mocked tasks
            var furtherControlMeasureTasks1 = new List<Mock<FireRiskAssessmentFurtherControlMeasureTask>>()
                                                  {
                                                      new Mock<FireRiskAssessmentFurtherControlMeasureTask>() {CallBase = true},
                                                      new Mock<FireRiskAssessmentFurtherControlMeasureTask>() {CallBase = true}
                                                  };
          
            var furtherControlMeasureTasks2 = new List<Mock<FireRiskAssessmentFurtherControlMeasureTask>>()
                                                  {
                                                      new Mock<FireRiskAssessmentFurtherControlMeasureTask>() {CallBase = true},
                                                      new Mock<FireRiskAssessmentFurtherControlMeasureTask>() {CallBase = true}
                                                  };

            //Add mocked tasks to risk assessement
            riskAss.LatestFireRiskAssessmentChecklist.Answers.Add(new FireAnswer()
                                                                      {
                                                                          SignificantFinding =
                                                                              new SignificantFinding() {FurtherControlMeasureTasks = furtherControlMeasureTasks1.Select(t => t.Object).ToList()}
                                                                      });

            riskAss.LatestFireRiskAssessmentChecklist.Answers.Add(new FireAnswer()
                                                                      {
                                                                          SignificantFinding =
                                                                              new SignificantFinding() {FurtherControlMeasureTasks = furtherControlMeasureTasks2.Select(t => t.Object).ToList()}
                                                                      });

            //when
            riskAss.UpdateSummary("new title", "new ref", "person appointed", new DateTime(), new RiskAssessor() {Id = 2}, new Site(), new UserForAuditing());

            //then
            furtherControlMeasureTasks1.ForEach(task => task.VerifySet(x => x.SendTaskCompletedNotification, Times.Once()));
            furtherControlMeasureTasks2.ForEach(task => task.VerifySet(x => x.SendTaskCompletedNotification, Times.Once()));

            Assert.IsTrue(furtherControlMeasureTasks1.All(task => task.Object.SendTaskCompletedNotification.Value));
            Assert.IsTrue(furtherControlMeasureTasks2.All(task => task.Object.SendTaskCompletedNotification.Value));
        }

        [Test]
        public void Given_the_risk_assessor_is_NOT_different_when_Update_RA_summary_then_further_control_measure_completed_email_notification_value_is_NOT_updated()
        {
            //Given
            var riskAss = FireRiskAssessment.Create("", "", 123, new Checklist(), new UserForAuditing());
            riskAss.RiskAssessor = new RiskAssessor() { Id = 1, DoNotSendTaskCompletedNotifications = true };

            //create mocked tasks
            var furtherControlMeasureTasks1 = new List<Mock<FireRiskAssessmentFurtherControlMeasureTask>>()
                                                  {
                                                      new Mock<FireRiskAssessmentFurtherControlMeasureTask>() {CallBase = true},
                                                      new Mock<FireRiskAssessmentFurtherControlMeasureTask>() {CallBase = true}
                                                  };

            var furtherControlMeasureTasks2 = new List<Mock<FireRiskAssessmentFurtherControlMeasureTask>>()
                                                  {
                                                      new Mock<FireRiskAssessmentFurtherControlMeasureTask>() {CallBase = true},
                                                      new Mock<FireRiskAssessmentFurtherControlMeasureTask>() {CallBase = true}
                                                  };

            //Add mocked tasks to risk assessement
            riskAss.LatestFireRiskAssessmentChecklist.Answers.Add(new FireAnswer()
            {
                SignificantFinding =
                    new SignificantFinding() { FurtherControlMeasureTasks = furtherControlMeasureTasks1.Select(t => t.Object).ToList() }
            });

            riskAss.LatestFireRiskAssessmentChecklist.Answers.Add(new FireAnswer()
            {
                SignificantFinding =
                    new SignificantFinding() { FurtherControlMeasureTasks = furtherControlMeasureTasks2.Select(t => t.Object).ToList() }
            });

            //when
            riskAss.UpdateSummary("new title", "new ref", "person",new DateTime(), new RiskAssessor() { Id = 1, DoNotSendTaskCompletedNotifications = true }, new Site(), new UserForAuditing());

            //then
            furtherControlMeasureTasks1.ForEach(task => task.VerifySet(x => x.SendTaskCompletedNotification, Times.Never()));
            furtherControlMeasureTasks2.ForEach(task => task.VerifySet(x => x.SendTaskCompletedNotification, Times.Never()));

        }

        [Test]
        public void Given_the_risk_assessor_is_different_when_Update_RA_summary_then_further_control_measure_overdue_email_notification_value_is_updated()
        {
            //Given
            var riskAss = FireRiskAssessment.Create("", "", 123, new Checklist(), new UserForAuditing());
            riskAss.RiskAssessor = new RiskAssessor() { Id = 1, DoNotSendTaskCompletedNotifications = true };

            //create mocked tasks
            var furtherControlMeasureTasks1 = new List<Mock<FireRiskAssessmentFurtherControlMeasureTask>>()
                                                  {
                                                      new Mock<FireRiskAssessmentFurtherControlMeasureTask>() {CallBase = true},
                                                      new Mock<FireRiskAssessmentFurtherControlMeasureTask>() {CallBase = true}
                                                  };

            var furtherControlMeasureTasks2 = new List<Mock<FireRiskAssessmentFurtherControlMeasureTask>>()
                                                  {
                                                      new Mock<FireRiskAssessmentFurtherControlMeasureTask>() {CallBase = true},
                                                      new Mock<FireRiskAssessmentFurtherControlMeasureTask>() {CallBase = true}
                                                  };

            //Add mocked tasks to risk assessement
            riskAss.LatestFireRiskAssessmentChecklist.Answers.Add(new FireAnswer()
            {
                SignificantFinding =
                    new SignificantFinding() { FurtherControlMeasureTasks = furtherControlMeasureTasks1.Select(t => t.Object).ToList() }
            });

            riskAss.LatestFireRiskAssessmentChecklist.Answers.Add(new FireAnswer()
            {
                SignificantFinding =
                    new SignificantFinding() { FurtherControlMeasureTasks = furtherControlMeasureTasks2.Select(t => t.Object).ToList() }
            });

            //when
            riskAss.UpdateSummary("new title", "new ref","person", new DateTime(), new RiskAssessor() { Id = 2 }, new Site(), new UserForAuditing());

            //then
            furtherControlMeasureTasks1.ForEach(task => task.VerifySet(x => x.SendTaskOverdueNotification, Times.Once()));
            furtherControlMeasureTasks2.ForEach(task => task.VerifySet(x => x.SendTaskOverdueNotification, Times.Once()));

            Assert.IsTrue(furtherControlMeasureTasks1.All(task => task.Object.SendTaskOverdueNotification.Value));
            Assert.IsTrue(furtherControlMeasureTasks2.All(task => task.Object.SendTaskOverdueNotification.Value));
        }

        [Test]
        public void Given_the_risk_assessor_is_NOT_different_when_Update_RA_summary_then_further_control_measure_overdue_email_notification_value_is_NOT_updated()
        {
            //Given
            var riskAss = FireRiskAssessment.Create("", "", 123, new Checklist(), new UserForAuditing());
            riskAss.RiskAssessor = new RiskAssessor() { Id = 1, DoNotSendTaskCompletedNotifications = true };

            //create mocked tasks
            var furtherControlMeasureTasks1 = new List<Mock<FireRiskAssessmentFurtherControlMeasureTask>>()
                                                  {
                                                      new Mock<FireRiskAssessmentFurtherControlMeasureTask>() {CallBase = true},
                                                      new Mock<FireRiskAssessmentFurtherControlMeasureTask>() {CallBase = true}
                                                  };

            var furtherControlMeasureTasks2 = new List<Mock<FireRiskAssessmentFurtherControlMeasureTask>>()
                                                  {
                                                      new Mock<FireRiskAssessmentFurtherControlMeasureTask>() {CallBase = true},
                                                      new Mock<FireRiskAssessmentFurtherControlMeasureTask>() {CallBase = true}
                                                  };
            //Add mocked tasks to risk assessement
            riskAss.LatestFireRiskAssessmentChecklist.Answers.Add(new FireAnswer()
            {
                SignificantFinding =
                    new SignificantFinding() { FurtherControlMeasureTasks = furtherControlMeasureTasks1.Select(t => t.Object).ToList() }
            });

            riskAss.LatestFireRiskAssessmentChecklist.Answers.Add(new FireAnswer()
            {
                SignificantFinding =
                    new SignificantFinding() { FurtherControlMeasureTasks = furtherControlMeasureTasks2.Select(t => t.Object).ToList() }
            });

            //when
            riskAss.UpdateSummary("new title", "new ref","person", new DateTime(), new RiskAssessor() { Id = 1, DoNotSendTaskOverdueNotifications = true }, new Site(), new UserForAuditing());

            //then
            furtherControlMeasureTasks1.ForEach(task => task.VerifySet(x => x.SendTaskOverdueNotification, Times.Never()));
            furtherControlMeasureTasks2.ForEach(task => task.VerifySet(x => x.SendTaskOverdueNotification, Times.Never()));

        }

        [Test]
        public void Given_the_risk_assessor_is_different_and_no_further_control_measures_when_Update_RA_summary_then_nulls_do_not_throw_exception()
        {
            //Given
            var riskAss = FireRiskAssessment.Create("", "", 123, new Checklist(), new UserForAuditing());
            //Add significant finding without any further control measure tasks
            riskAss.LatestFireRiskAssessmentChecklist.Answers.Add(new FireAnswer()
                                                                      {
                                                                          SignificantFinding = new SignificantFinding() {FurtherControlMeasureTasks = null}
                                                                      });


            var sigFinding = riskAss.LatestFireRiskAssessmentChecklist.SignificantFindings.First();

            riskAss.LatestFireRiskAssessmentChecklist.SignificantFindings.First().FurtherControlMeasureTasks = null;
            riskAss.RiskAssessor = new RiskAssessor() {Id = 1, DoNotSendTaskCompletedNotifications = true};

            //when
            riskAss.UpdateSummary("new title", "new ref", "person", new DateTime(), new RiskAssessor() {Id = 2}, new Site(), new UserForAuditing());
        }

    }
}