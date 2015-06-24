using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    public class CompletionDueDateTests
    {
        [Test]
        public void Given_no_further_control_measure_tasks_and_no_review_tasks_then_CompletionDueDate_is_null()
        {
            //given
            var riskAss = new HazardousSubstanceRiskAssessment();
            riskAss.FurtherControlMeasureTasks.Clear();

            Assert.IsNull(riskAss.CompletionDueDate);
        }

        [Test]
        public void Given_further_control_measure_tasks_and_no_review_tasks_then_CompletionDueDate_is_next_FCM_CompletionDueDate()
        {
            //given
            var expectedCompletionDueDate = new DateTime(2013, 4, 1);
            var riskAss = new HazardousSubstanceRiskAssessment();
            riskAss.AddFurtherControlMeasureTask(new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask() { Id = 12312, Deleted = false, TaskCompletionDueDate = DateTime.Now }, null);
            riskAss.AddFurtherControlMeasureTask(new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask() { Id = 1231234, Deleted = false, TaskCompletionDueDate = expectedCompletionDueDate }, null);
          
            //THEN
            Assert.AreEqual(expectedCompletionDueDate, riskAss.CompletionDueDate.Value);
        }

        [Test]
        public void Given_further_control_measure_task_has_been_deleted_and_no_review_tasks_then_CompletionDueDate_is_null()
        {
            //given
            var riskAss = new HazardousSubstanceRiskAssessment();
            riskAss.AddFurtherControlMeasureTask(new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask() { Id = 12312, Deleted = true, TaskCompletionDueDate = DateTime.Now }, null);

            //THEN
            Assert.IsNull(riskAss.CompletionDueDate);
        }

        [Test]
        public void Given_further_control_measure_task_has_been_completed_and_no_review_tasks_then_CompletionDueDate_is_null()
        {
            //given
            var riskAss = new HazardousSubstanceRiskAssessment();
            riskAss.AddFurtherControlMeasureTask(new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask() { Id = 12312, Deleted = false, TaskCompletionDueDate = DateTime.Now, TaskStatus = TaskStatus.Completed }, null);

            //THEN
            Assert.IsNull(riskAss.CompletionDueDate);
        }

        [Test]
        public void Given_further_control_measure_task_has_been_marked_as_not_required_and_no_review_tasks_then_CompletionDueDate_is_null()
        {
            //given
            var riskAss = new HazardousSubstanceRiskAssessment();
            riskAss.AddFurtherControlMeasureTask(new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask() { Id = 12312, Deleted = false, TaskCompletionDueDate = DateTime.Now, TaskStatus = TaskStatus.NoLongerRequired }, null);

            //THEN
            Assert.IsNull(riskAss.CompletionDueDate);
        }


        [Test]
        public void Given_a_review_task_and_no_further_control_measure_tasks_then_CompletionDueDate_is_next_review_date()
        {
            //given
            var expectedCompletionDueDate = new DateTime(2013, 7, 1);
            var riskAss = new HazardousSubstanceRiskAssessment();
            riskAss.AddReview(new RiskAssessmentReview() {Id = 1, CompletionDueDate = DateTime.Now.AddDays(123)});
            riskAss.AddReview(new RiskAssessmentReview() {Id = 2, CompletionDueDate = expectedCompletionDueDate});
            riskAss.AddReview(new RiskAssessmentReview() {Id = 3, CompletionDueDate = DateTime.Now});

            //THEN
            Assert.AreEqual(expectedCompletionDueDate, riskAss.CompletionDueDate.Value);
        }

        [Test]
        public void Given_a_review_task_has_been_deleted_and_no_further_control_measure_tasks_then_CompletionDueDate_is_null()
        {
            //given
            var riskAss = new HazardousSubstanceRiskAssessment();
            riskAss.AddReview(new RiskAssessmentReview() {Id = 2, CompletionDueDate = DateTime.Now, Deleted = true});
        
            //THEN
            Assert.IsNull(riskAss.CompletionDueDate);
        }

        [Test]
        public void Given_a_review_task_has_a_completion_due_date_before_the_next_further_control_measure_tasks_then_CompletionDueDate_is_the_next_review_date()
        {
            //given
            var nextFCMCompletionDueDate = new DateTime(2013, 4, 1);
            var nextReviewDate = new DateTime(2013, 3, 4);
            var riskAss = new HazardousSubstanceRiskAssessment();
            riskAss.AddFurtherControlMeasureTask(new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask() { Id = 12312, Deleted = false, TaskCompletionDueDate = DateTime.Now }, null);
            riskAss.AddFurtherControlMeasureTask(new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask() { Id = 1231234, Deleted = false, TaskCompletionDueDate = nextFCMCompletionDueDate }, null);
            riskAss.AddReview(new RiskAssessmentReview() {Id = 2, CompletionDueDate = nextReviewDate});


            //THEN
            Assert.AreEqual(nextReviewDate, riskAss.CompletionDueDate.Value);
        }

        [Test]
        public void Given_a_review_task_has_a_completion_due_date_after_the_next_further_control_measure_tasks_then_CompletionDueDate_is_the_next_FCM_completion_due_date()
        {
            //given
            var nextFCMCompletionDueDate = new DateTime(2013, 2, 16);
            var nextReviewDate = new DateTime(2013, 3, 4);
            var riskAss = new HazardousSubstanceRiskAssessment();
            var hazard = new MultiHazardRiskAssessmentHazard() { Id = 124124 };
            riskAss.AddFurtherControlMeasureTask(new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask() { Id = 12312, Deleted = false, TaskCompletionDueDate = DateTime.Now }, null);
            riskAss.AddFurtherControlMeasureTask(new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask() { Id = 1231234, Deleted = false, TaskCompletionDueDate = nextFCMCompletionDueDate }, null);
            riskAss.Reviews = new List<RiskAssessmentReview>()
                                                 {

                                                     new RiskAssessmentReview() {Id = 2, CompletionDueDate = nextReviewDate}
                                                 };


            //THEN
            Assert.AreEqual(nextFCMCompletionDueDate, riskAss.CompletionDueDate.Value);
        }
    }
}
