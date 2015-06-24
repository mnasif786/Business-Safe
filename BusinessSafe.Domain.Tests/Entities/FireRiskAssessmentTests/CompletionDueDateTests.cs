using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.FireRiskAssessmentTests
{
    [TestFixture]
    public class CompletionDueDateTests
    {

        [Test]
        public void Given_no_further_control_measure_tasks_and_no_review_tasks_then_CompletionDueDate_is_null()
        {
            //given
            var riskAss = new FireRiskAssessment();
   
            Assert.IsNull(riskAss.CompletionDueDate);
        }

        [Test]
        public void Given_a_further_control_measure_tasks_and_no_review_tasks_then_CompletionDueDate_is_next_FCM_completion_due_date()
        {
            //given
            var nextFCMCompletionDueDate = new DateTime(2013, 5, 7);
            var riskAss = new FireRiskAssessment(); 
            
            var finding = new SignificantFinding();
            finding.AddFurtherControlMeasureTask(new FireRiskAssessmentFurtherControlMeasureTask() { TaskCompletionDueDate = nextFCMCompletionDueDate }, null);
            finding.AddFurtherControlMeasureTask(new FireRiskAssessmentFurtherControlMeasureTask() { TaskCompletionDueDate = DateTime.Now }, null);

            var findingList = new List<SignificantFinding>();
            findingList.Add((finding));

            var fireChecklist = new Mock<FireRiskAssessmentChecklist>() { CallBase = true };
            fireChecklist.Setup(x => x.SignificantFindings).Returns(findingList);
            
            riskAss.FireRiskAssessmentChecklists.Add(fireChecklist.Object);
         
            Assert.AreEqual( nextFCMCompletionDueDate, riskAss.CompletionDueDate.Value);
        }

        [Test]
        public void Given_no_further_control_measure_tasks_and_1_review_tasks_then_CompletionDueDate_is_next_review_date()
        {
            var riskAss = new FireRiskAssessment();
            var reviewTask = new RiskAssessmentReview() {CompletionDueDate = new DateTime(2013, 5, 7)};
            
            riskAss.AddReview(reviewTask);

            Assert.AreEqual(reviewTask.CompletionDueDate, riskAss.CompletionDueDate.Value);
        }

        [Test]
        public void Given_a_further_control_measure_task_is_deleted_and_no_review_tasks_then_CompletionDueDate_is_null()
        {
            //given
            var nextFCMCompletionDueDate = new DateTime(2013, 5, 7);
            var riskAss = new FireRiskAssessment();

            var finding = new SignificantFinding();
            finding.AddFurtherControlMeasureTask(new FireRiskAssessmentFurtherControlMeasureTask() { TaskCompletionDueDate = nextFCMCompletionDueDate ,Deleted = true}, null);
            finding.AddFurtherControlMeasureTask(new FireRiskAssessmentFurtherControlMeasureTask() { TaskCompletionDueDate = DateTime.Now, Deleted = true }, null);

            var findingList = new List<SignificantFinding>();
            findingList.Add((finding));

            var fireChecklist = new Mock<FireRiskAssessmentChecklist>() { CallBase = true };
            fireChecklist.Setup(x => x.SignificantFindings).Returns(findingList);

            riskAss.FireRiskAssessmentChecklists.Add(fireChecklist.Object);

            Assert.IsNull(riskAss.CompletionDueDate);
        }

        [Test]
        public void Given_a_further_control_measure_task_is_completed_and_no_review_tasks_then_CompletionDueDate_is_null()
        {
            //given
            var nextFCMCompletionDueDate = new DateTime(2013, 5, 7);
            var riskAss = new FireRiskAssessment();

            var finding = new SignificantFinding();
            finding.AddFurtherControlMeasureTask(new FireRiskAssessmentFurtherControlMeasureTask() { TaskCompletionDueDate = nextFCMCompletionDueDate, TaskStatus = TaskStatus.Completed}, null);

            var findingList = new List<SignificantFinding>();
            findingList.Add((finding));

            var fireChecklist = new Mock<FireRiskAssessmentChecklist>() { CallBase = true };
            fireChecklist.Setup(x => x.SignificantFindings).Returns(findingList);

            riskAss.FireRiskAssessmentChecklists.Add(fireChecklist.Object);

            Assert.IsNull(riskAss.CompletionDueDate);
        }

        [Test]
        public void Given_a_further_control_measure_task_is_not_required_and_no_review_tasks_then_CompletionDueDate_is_null()
        {
            //given
            var nextFCMCompletionDueDate = new DateTime(2013, 5, 7);
            var riskAss = new FireRiskAssessment();

            var finding = new SignificantFinding();
            finding.AddFurtherControlMeasureTask(new FireRiskAssessmentFurtherControlMeasureTask() { TaskCompletionDueDate = nextFCMCompletionDueDate, TaskStatus = TaskStatus.NoLongerRequired }, null);

            var findingList = new List<SignificantFinding>();
            findingList.Add((finding));

            var fireChecklist = new Mock<FireRiskAssessmentChecklist>() { CallBase = true };
            fireChecklist.Setup(x => x.SignificantFindings).Returns(findingList);

            riskAss.FireRiskAssessmentChecklists.Add(fireChecklist.Object);

            Assert.IsNull(riskAss.CompletionDueDate);
        }

        [Test]
        public void Given_a_significant_finding_but_no_further_control_measure_task_is_not_required_and_no_review_tasks_then_CompletionDueDate_is_null()
        {
            //given
            var riskAss = new FireRiskAssessment();
            var findingList = new List<SignificantFinding> {new SignificantFinding()};

            var fireChecklist = new Mock<FireRiskAssessmentChecklist>() { CallBase = true };
            fireChecklist.Setup(x => x.SignificantFindings).Returns(findingList);

            riskAss.FireRiskAssessmentChecklists.Add(fireChecklist.Object);

            Assert.IsNull(riskAss.CompletionDueDate);
        }

        [Test]
        public void Given_a_further_control_measure_task_is_attached_to_a_deleted_significant_finding_and_no_review_tasks_then_CompletionDueDate_is_null()
        {
            //given
            var riskAss = new FireRiskAssessment();
            var finding = new SignificantFinding();
            finding.Deleted = true;
            finding.AddFurtherControlMeasureTask(new FireRiskAssessmentFurtherControlMeasureTask() { TaskCompletionDueDate = DateTime.Now}, null);

            var findingList = new List<SignificantFinding> {(finding)};

            var fireChecklist = new Mock<FireRiskAssessmentChecklist>() { CallBase = true };
            fireChecklist.Setup(x => x.SignificantFindings).Returns(findingList);

            riskAss.FireRiskAssessmentChecklists.Add(fireChecklist.Object);

            Assert.IsNull(riskAss.CompletionDueDate);
        }
    }
}
