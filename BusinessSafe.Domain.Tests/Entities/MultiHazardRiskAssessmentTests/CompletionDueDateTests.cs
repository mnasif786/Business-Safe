using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;
using Moq;

namespace BusinessSafe.Domain.Tests.Entities.MultiHazardRiskAssessmentTests
{
    [TestFixture]
    public class CompletionDueDateTests
    {

        [Test]
        public void Given_no_further_control_measure_tasks_and_no_review_tasks_then_CompletionDueDate_is_null()
        {
            //given
            var riskAss = new Mock<MultiHazardRiskAssessment>() { CallBase = true };
            var hazards = new List<MultiHazardRiskAssessmentHazard>() {new MultiHazardRiskAssessmentHazard() {Id = 124124}};

            riskAss.Setup(x => x.Hazards)
                .Returns(() => new List<MultiHazardRiskAssessmentHazard>());


            Assert.IsNull(riskAss.Object.CompletionDueDate);


        }

        [Test]
        public void Given_further_control_measure_tasks_and_no_review_tasks_then_CompletionDueDate_is_next_FCM_CompletionDueDate()
        {
            //given
            var expectedCompletionDueDate = new DateTime(2013, 4, 1);
            var riskAss = new Mock<MultiHazardRiskAssessment>() { CallBase = true };
            var hazard = new MultiHazardRiskAssessmentHazard() {Id = 124124};
            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask() { Id = 12312, Deleted = false, TaskCompletionDueDate = DateTime.Now }, null);
            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask() { Id = 1231234, Deleted = false, TaskCompletionDueDate = expectedCompletionDueDate }, null);

            var hazards = new List<MultiHazardRiskAssessmentHazard>() { hazard };

            riskAss.Setup(x => x.Hazards)
                .Returns(() => hazards);

            //THEN
            Assert.AreEqual(expectedCompletionDueDate, riskAss.Object.CompletionDueDate.Value);

        }

        [Test]
        public void Given_further_control_measure_task_has_been_deleted_and_no_review_tasks_then_CompletionDueDate_is_null()
        {
            //given
            var riskAss = new Mock<MultiHazardRiskAssessment>() { CallBase = true };
            var hazard = new MultiHazardRiskAssessmentHazard() { Id = 124124 };
            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask() { Id = 12312, Deleted = true, TaskCompletionDueDate = DateTime.Now}, null);

            var hazards = new List<MultiHazardRiskAssessmentHazard>() { hazard };

            riskAss.Setup(x => x.Hazards)
                .Returns(() => hazards);

            //THEN
            Assert.IsNull(riskAss.Object.CompletionDueDate);

        }

        [Test]
        public void Given_hazard_has_been_deleted_and_no_review_tasks_then_CompletionDueDate_is_null()
        {
            //given
            var riskAss = new Mock<MultiHazardRiskAssessment>() { CallBase = true };
            var hazard = new MultiHazardRiskAssessmentHazard() { Id = 124124 };
            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask() { Id = 12312, Deleted = false, TaskCompletionDueDate = DateTime.Now }, null);
            hazard.Deleted = true;

            var hazards = new List<MultiHazardRiskAssessmentHazard>() { hazard };

            riskAss.Setup(x => x.Hazards)
                .Returns(() => hazards);

            //THEN
            Assert.IsNull(riskAss.Object.CompletionDueDate);
        }

        [Test]
        public void Given_further_control_measure_task_has_been_completed_and_no_review_tasks_then_CompletionDueDate_is_null()
        {
            //given
            var riskAss = new Mock<MultiHazardRiskAssessment>() { CallBase = true };
            var hazard = new MultiHazardRiskAssessmentHazard() { Id = 124124 };
            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask() { Id = 12312, Deleted = false, TaskCompletionDueDate = DateTime.Now, TaskStatus = TaskStatus.Completed}, null);

            var hazards = new List<MultiHazardRiskAssessmentHazard>() { hazard };

            riskAss.Setup(x => x.Hazards)
                .Returns(() => hazards);

            //THEN
            Assert.IsNull(riskAss.Object.CompletionDueDate);

        }

        [Test]
        public void Given_further_control_measure_task_has_been_marked_as_not_required_and_no_review_tasks_then_CompletionDueDate_is_null()
        {
            //given
            var riskAss = new Mock<MultiHazardRiskAssessment>() { CallBase = true };
            var hazard = new MultiHazardRiskAssessmentHazard() { Id = 124124 };
            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask() { Id = 12312, Deleted = false, TaskCompletionDueDate = DateTime.Now, TaskStatus = TaskStatus.NoLongerRequired }, null);

            var hazards = new List<MultiHazardRiskAssessmentHazard>() { hazard };

            riskAss.Setup(x => x.Hazards)
                .Returns(() => hazards);

            //THEN
            Assert.IsNull(riskAss.Object.CompletionDueDate);

        }


        [Test]
        public void Given_a_review_task_and_no_further_control_measure_tasks_then_CompletionDueDate_is_next_review_date()
        {
            //given
            var expectedCompletionDueDate = new DateTime(2013, 7, 1);
            var riskAss = new Mock<MultiHazardRiskAssessment>() {CallBase = true};
            riskAss.Object.AddReview(new RiskAssessmentReview() { Id = 1, CompletionDueDate = DateTime.Now.AddDays(123) });
            riskAss.Object.AddReview(new RiskAssessmentReview() {Id = 2, CompletionDueDate = expectedCompletionDueDate});
            riskAss.Object.AddReview(new RiskAssessmentReview() {Id = 3, CompletionDueDate = DateTime.Now});

            //THEN
            Assert.AreEqual(expectedCompletionDueDate, riskAss.Object.CompletionDueDate.Value);

        }


        [Test]
        public void Given_a_review_task_has_been_deleted_and_no_further_control_measure_tasks_then_CompletionDueDate_is_null()
        {
            //given
          var riskAss = new Mock<MultiHazardRiskAssessment>() { CallBase = true };
                    riskAss.Object.Reviews = new List<RiskAssessmentReview>()
                                                 {

                                                     new RiskAssessmentReview() {Id = 2, CompletionDueDate = DateTime.Now ,Deleted = true}
                                                 };

            //THEN
            Assert.IsNull(riskAss.Object.CompletionDueDate);

        }

        [Test]
        public void Given_a_review_task_has_a_completion_due_date_before_the_next_further_control_measure_tasks_then_CompletionDueDate_is_the_next_review_date()
        {
            //given
            var nextFCMCompletionDueDate = new DateTime(2013, 4, 1);
            var nextReviewDate = new DateTime(2013, 3, 4);
            var riskAss = new Mock<MultiHazardRiskAssessment>() { CallBase = true };
            var hazard = new MultiHazardRiskAssessmentHazard() { Id = 124124 };
            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask() { Id = 12312, Deleted = false, TaskCompletionDueDate = DateTime.Now }, null);
            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask() { Id = 1231234, Deleted = false, TaskCompletionDueDate = nextFCMCompletionDueDate }, null);
            riskAss.Object.AddReview(new RiskAssessmentReview() {Id = 2, CompletionDueDate = nextReviewDate});

            var hazards = new List<MultiHazardRiskAssessmentHazard>() { hazard };

            riskAss.Setup(x => x.Hazards)
                .Returns(() => hazards);

            //THEN
            Assert.AreEqual(nextReviewDate, riskAss.Object.CompletionDueDate.Value);

        }

        [Test]
        public void Given_a_review_task_has_a_completion_due_date_after_the_next_further_control_measure_tasks_then_CompletionDueDate_is_the_next_FCM_completion_due_date()
        {
            //given
            var nextFCMCompletionDueDate = new DateTime(2013, 2, 16);
            var nextReviewDate = new DateTime(2013, 3, 4);
            var riskAss = new Mock<MultiHazardRiskAssessment>() { CallBase = true };
            var hazard = new MultiHazardRiskAssessmentHazard() { Id = 124124 };
            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask() { Id = 12312, Deleted = false, TaskCompletionDueDate = DateTime.Now }, null);
            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask() { Id = 1231234, Deleted = false, TaskCompletionDueDate = nextFCMCompletionDueDate }, null);
            riskAss.Object.Reviews = new List<RiskAssessmentReview>()
                                                 {

                                                     new RiskAssessmentReview() {Id = 2, CompletionDueDate = nextReviewDate}
                                                 };


            var hazards = new List<MultiHazardRiskAssessmentHazard>() { hazard };

            riskAss.Setup(x => x.Hazards)
                .Returns(() => hazards);

            //THEN
            Assert.AreEqual(nextFCMCompletionDueDate, riskAss.Object.CompletionDueDate.Value);

        }
    }

    
}
