using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;
using Action = BusinessSafe.Domain.Entities.Action;

namespace BusinessSafe.Domain.Tests.Entities.ActionTests
{
    [TestFixture]
    public class GetStatusFromActionsTests
    {
        [Test]
        public void given_no_tasks_when_get_status_from_tasks_then_return_none()
        {
            // Given
            var plan = new ActionPlan();

            // When
            var result = plan.GetStatusFromActions();

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.None));
        }

        [Test]
        public void given_only_deleted_tasks_when_get_status_from_tasks_then_return_none()
        {
            // Given
            var plan = new ActionPlan();
            plan.Actions = new List<Action>();

            var actionTask = new ActionTask()
                                 {
                                     Deleted = true,
                                     TaskStatus =
                                         TaskStatus.Outstanding,
                                     TaskCompletionDueDate =
                                         DateTime.Now
                                 };

            plan.Actions.Add(new Action{ActionTasks = new List<ActionTask>{actionTask}});


            // When
            var result = plan.GetStatusFromActions();

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.None));
        }

        [Test]
        public void given_outstanding_tasks_when_get_status_from_tasks_then_return_outstanding()
        {
            // Given
            var action = new Action()
            {
                ActionTasks = new List<ActionTask>
                                                                              {
                                                                                  new ActionTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.Completed,
                                                                                      TaskCompletionDueDate = DateTime.Now.AddDays(1)
                                                                                  },
                                                                                  new ActionTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.Completed,
                                                                                      TaskCompletionDueDate = DateTime.Now
                                                                                  },
                                                                                  new ActionTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.Outstanding,
                                                                                      TaskCompletionDueDate = DateTime.Now.AddDays(1)
                                                                                  }
                                                                              }
            };

            var actionPlan = new ActionPlan {Actions = new List<Action> {action}};

            // When
            var result = actionPlan.GetStatusFromActions();

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.Outstanding));
        }

        [Test]
        public void given_outstanding_tasks_and_deleted_overdue_ones_when_get_status_from_tasks_then_return_outstanding()
        {
            // Given
            var action = new Action()
            {
                ActionTasks = new List<ActionTask>
                                                                              {
                                                                                  new ActionTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.Completed,
                                                                                      TaskCompletionDueDate = DateTime.Now.AddDays(1)
                                                                                  },
                                                                                  new ActionTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.Completed,
                                                                                      TaskCompletionDueDate = DateTime.Now
                                                                                  },
                                                                                  new ActionTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.Outstanding,
                                                                                      TaskCompletionDueDate = DateTime.Now.AddDays(1)
                                                                                  },
                                                                                  new ActionTask()
                                                                                  {
                                                                                      Deleted = true,
                                                                                      TaskStatus = TaskStatus.Outstanding,
                                                                                      TaskCompletionDueDate = DateTime.Now.AddDays(-1)
                                                                                  }
                                                                              }
            };

            var actionPlan = new ActionPlan { Actions = new List<Action> { action } };

            // When
            var result = actionPlan.GetStatusFromActions();

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.Outstanding));
        }

        [Test]
        public void given_outstanding_tasks_that_are_due_before_today_When_get_status_from_tasks_then_return_overdue()
        {
            // Given
            var action = new Action()
            {
                ActionTasks = new List<ActionTask>
                                                                              {
                                                                                  new ActionTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.Completed,
                                                                                      TaskCompletionDueDate = DateTime.Now.AddDays(1)
                                                                                  },
                                                                                  new ActionTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.Completed,
                                                                                      TaskCompletionDueDate = DateTime.Now
                                                                                  },
                                                                                  new ActionTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.Outstanding,
                                                                                      TaskCompletionDueDate = DateTime.Now.AddDays(-1)
                                                                                  }
                                                                              }
            };

            var actionPlan = new ActionPlan { Actions = new List<Action> { action } };

            // When
            var result = actionPlan.GetStatusFromActions();

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.Overdue));
        }

        [Test]
        public void given_all_tasks_completed_when_get_status_from_tasks_then_return_completed()
        {
            // Given
            var action = new Action()
            {
                ActionTasks = new List<ActionTask>
                                                                              {
                                                                                  new ActionTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.Completed,
                                                                                      TaskCompletionDueDate = DateTime.Now.AddDays(-1)
                                                                                  },
                                                                                  new ActionTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.NoLongerRequired,
                                                                                      TaskCompletionDueDate = DateTime.Now.AddDays(-1)
                                                                                  },
                                                                                  new ActionTask()
                                                                                  {
                                                                                      Deleted = true,
                                                                                      TaskStatus = TaskStatus.Outstanding,
                                                                                      TaskCompletionDueDate = DateTime.Now.AddDays(-1)
                                                                                  }
                                                                              }
            };

            var actionPlan = new ActionPlan { Actions = new List<Action> { action } };

            // When
            var result = actionPlan.GetStatusFromActions();

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.Completed));
        }
    }
}