using System;
using System.Linq;

using BusinessSafe.Domain.Entities;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceRiskAssessmentFurtherActionTasksTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetPreviousHistoryTests
    {
        [Test]
        public void Given_task_with_no_history_When_get_task_history_Then_returns_correct_history()
        {
            //Given
            var task = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask { TaskReoccurringType = TaskReoccurringType.None };

            //When
            var result = task.GetPreviousHistory();


            //Then
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Given_task_with_one_history_record_When_get_task_history_Then_returns_correct_history()
        {
            //Given
            var historytask = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask { TaskAssignedTo = new Employee() { Forename = "Vince", Surname = "Lee" }, TaskReoccurringType = TaskReoccurringType.None, TaskCompletedDate = DateTime.Today, TaskCompletionDueDate = DateTime.Today.AddDays(-1) };
            var task = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask { TaskAssignedTo = new Employee() { Forename = "Vince", Surname = "Lee" }, TaskReoccurringType = TaskReoccurringType.None, PrecedingTask = historytask };

            //When
            var result = task.GetPreviousHistory();


            //Then
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().DueDate.ToShortDateString(), Is.EqualTo(historytask.TaskCompletionDueDate.Value.ToShortDateString()));
            Assert.That(result.First().CompletedDate, Is.EqualTo(historytask.TaskCompletedDate.Value.DateTime.ToShortDateString()));
        }

        [Test]
        public void Given_task_with_two_history_record_When_get_task_history_Then_returns_correct_history()
        {
            //Given
            var historytask2 = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask { TaskAssignedTo = new Employee() { Forename = "Vince", Surname = "Lee" }, TaskReoccurringType = TaskReoccurringType.None, TaskCompletedDate = DateTime.Today.AddDays(-9), TaskCompletionDueDate = DateTime.Today.AddDays(-10) };
            var historytask1 = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask { TaskAssignedTo = new Employee() { Forename = "Vince", Surname = "Lee" }, TaskReoccurringType = TaskReoccurringType.None, TaskCompletedDate = DateTime.Today, TaskCompletionDueDate = DateTime.Today.AddDays(-1), PrecedingTask = historytask2 };
            var task = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask { TaskReoccurringType = TaskReoccurringType.None, PrecedingTask = historytask1 };

            //When
            var result = task.GetPreviousHistory();


            //Then
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.Skip(1).Take(1).First().DueDate.ToShortDateString(), Is.EqualTo(historytask1.TaskCompletionDueDate.Value.ToShortDateString()));
            Assert.That(result.Skip(1).Take(1).First().CompletedDate, Is.EqualTo(historytask1.TaskCompletedDate.Value.DateTime.ToShortDateString()));
            Assert.That(result.First().DueDate.ToShortDateString(), Is.EqualTo(historytask2.TaskCompletionDueDate.Value.ToShortDateString()));
            Assert.That(result.First().CompletedDate, Is.EqualTo(historytask2.TaskCompletedDate.Value.DateTime.ToShortDateString()));
        }

        [Test]
        public void Given_task_with_twenty_history_record_When_get_task_history_Then_returns_correct_history_only_ten()
        {
            //Given
            var today = DateTime.Today;
            HazardousSubstanceRiskAssessmentFurtherControlMeasureTask previousTask = null;
            for (int i = 20; i > 0; i--)
            {
                var historytask = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask
                                      {
                                          TaskAssignedTo = new Employee() { Forename = "Vince", Surname = "Lee" }, 
                                          PrecedingTask = previousTask,
                                          TaskReoccurringType = TaskReoccurringType.Weekly,
                                          TaskCompletedDate = today.AddDays(-i),
                                          TaskCompletionDueDate = today.AddDays(-i)
                                      };
                previousTask = historytask;
            }
            var task = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask { TaskReoccurringType = TaskReoccurringType.Weekly, PrecedingTask = previousTask };

            //When
            var result = task.GetPreviousHistory();


            //Then
            Assert.That(result.Count(), Is.EqualTo(10));
            for (int j = 0; j < result.Count(); j++)
            {
                var expectedDate = today.AddDays(-j - 1);
                Assert.That(result.ElementAt(result.Count() - 1 - j).DueDate.ToShortDateString(), Is.EqualTo(expectedDate.ToShortDateString()));
            }
        }



        [Test]
        public void Given_a_recurring_task_with_a_preceding_task_completed_When_TaskPreviousHistory_requested_Then_returned_TaskHistoryRecord_has_corresponding_values_set()
        {
            // Given
            var taskCompletedDate = new DateTime(1980, 6, 23);
            var taskDueDate = new DateTime(1989, 12, 20);
            var completedBy = "Kim Howard";
            var completedByParts = completedBy.Split();

            var mockFcmA = new Mock<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>();
            mockFcmA.Setup(x => x.TaskCompletedDate).Returns(taskCompletedDate);
            mockFcmA.Setup(x => x.TaskCompletionDueDate).Returns(taskDueDate);
            mockFcmA.Setup(x => x.TaskAssignedTo).Returns(new Employee() { Forename = completedByParts[0], Surname = completedByParts[1] });
            var fcmA = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask()
                           {
                               PrecedingTask = mockFcmA.Object
                           };

            // When
            var result = fcmA.GetPreviousHistory();
            
            // Then
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().CompletedDate, Is.EqualTo(taskCompletedDate.ToShortDateString()));
            Assert.That(result.First().DueDate.ToShortDateString(), Is.EqualTo(taskDueDate.ToShortDateString()));
            Assert.That(result.First().CompletedBy, Is.EqualTo(completedBy));
        }

        private Mock<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask> BuildRecurringFCMTask(Mock<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask> precedingTask)
        {
            var mockNewFcmTask = new Mock<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>();

            if (precedingTask != null)
                mockNewFcmTask.Setup(x => x.PrecedingTask).Returns(precedingTask.Object);

            mockNewFcmTask.Setup(x => x.TaskCompletedDate).Returns(new DateTime());
            mockNewFcmTask.Setup(x => x.TaskCompletionDueDate).Returns(new DateTime());
            mockNewFcmTask.Setup(x => x.TaskAssignedTo).Returns(new Employee() { Forename = "Vince", Surname = "Lee" });

            return mockNewFcmTask;
        }
    }
}