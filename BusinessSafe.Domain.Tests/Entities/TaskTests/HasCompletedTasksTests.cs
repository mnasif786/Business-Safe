using NUnit.Framework;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.Tests.Entities.TaskTests
{
    [TestFixture]
    [Category("Unit")]
    public class HasCompletedTasksTests
    {
        [Test]
        public void Given_task_is_recurring_and_has_completed_tasks_When_HasCompletedTasks_is_called_Then_returns_true()
        {
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask
                           {
                               TaskStatus = TaskStatus.Outstanding,
                               TaskReoccurringType = TaskReoccurringType.Weekly,
                               PrecedingTask = new MultiHazardRiskAssessmentFurtherControlMeasureTask
                                                   {
                                                       TaskStatus = TaskStatus.Completed,
                                                       TaskReoccurringType = TaskReoccurringType.Weekly,
                                                       PrecedingTask = new MultiHazardRiskAssessmentFurtherControlMeasureTask
                                                                           {
                                                                               TaskStatus = TaskStatus.Completed,
                                                                               TaskReoccurringType =
                                                                                   TaskReoccurringType.Weekly,
                                                                           }
                                                   }
                           };

            Assert.That(task.HasCompletedTasks());
        }

        [Test]
        public void Given_task_is_not_recurring_When_HasCompletedTasks_is_called_Then_returns_false()
        {
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask
                           {
                               TaskReoccurringType = TaskReoccurringType.None
                           };

            Assert.That(task.HasCompletedTasks(), Is.False);
        }

        [Test]
        public void Given_task_is_recurring_and_does_not_have_completed_tasks_When_HasCompletedTasks_is_called_Then_returns_false()
        {
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask
            {
                TaskStatus = TaskStatus.Outstanding,
                TaskReoccurringType = TaskReoccurringType.Weekly,
                PrecedingTask = new MultiHazardRiskAssessmentFurtherControlMeasureTask
                {
                    TaskStatus = TaskStatus.Outstanding,
                    TaskReoccurringType = TaskReoccurringType.Weekly,
                    PrecedingTask = new MultiHazardRiskAssessmentFurtherControlMeasureTask
                    {
                        TaskStatus = TaskStatus.Outstanding,
                        TaskReoccurringType =
                            TaskReoccurringType.Weekly,
                    }
                }
            };

            Assert.That(task.HasCompletedTasks(), Is.False);
        }

        [Test]
        public void Given_task_is_recurring_and_has_completed_tasks_but_not_most_recent_one_When_HasCompletedTasks_is_called_Then_returns_true()
        {
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask
            {
                TaskStatus = TaskStatus.Outstanding,
                TaskReoccurringType = TaskReoccurringType.Weekly,
                PrecedingTask = new MultiHazardRiskAssessmentFurtherControlMeasureTask
                {
                    TaskStatus = TaskStatus.Outstanding,
                    TaskReoccurringType = TaskReoccurringType.Weekly,
                    PrecedingTask = new MultiHazardRiskAssessmentFurtherControlMeasureTask
                    {
                        TaskStatus = TaskStatus.Completed,
                        TaskReoccurringType =
                            TaskReoccurringType.Weekly,
                    }
                }
            };

            Assert.That(task.HasCompletedTasks());
        }

        [Test]
        public void Given_task_is_recurring_and_has_no_preceeding_tasks_When_HasCompletedTasks_is_called_Then_returns_false()
        {
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask
            {
                TaskStatus = TaskStatus.Outstanding,
                TaskReoccurringType = TaskReoccurringType.Weekly
            };

            Assert.That(task.HasCompletedTasks(), Is.False);
        }
    }
}
