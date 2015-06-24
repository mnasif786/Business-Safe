using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;
using Action = BusinessSafe.Domain.Entities.Action;

namespace BusinessSafe.Domain.Tests.Entities.ActionTests
{
    [TestFixture]
    public class GetStatusFromTasksTests
    {
        [Test]
        public void given_no_tasks_when_get_status_from_tasks_then_return_none()
        {
            // Given
            var action = new Action();

            // When
            var result = action.GetStatusFromTasks();

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.None));
        }

        [Test]
        public void given_no_tasks_and_action_no_longer_required_when_get_status_from_tasks_then_return_no_longer_required()
        {
            // Given
            var action = new Action();
            action.NoLongerRequired = true;

            // When
            var result = action.GetStatusFromTasks();

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.NoLongerRequired));
        }

        [Test]
        public void given_only_deleted_tasks_when_get_status_from_tasks_then_return_none()
        {
            // Given
            var action = new Action
                             {
                                 ActionTasks = new List<ActionTask>
                                                   {
                                                       new ActionTask()
                                                           {
                                                               Deleted = true,
                                                               TaskStatus = TaskStatus.Outstanding,
                                                               TaskCompletionDueDate = DateTime.Now
                                                           }
                                                   }
                             };

            // When
            var result = action.GetStatusFromTasks();

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

            // When
            var result = action.GetStatusFromTasks();

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

            // When
            var result = action.GetStatusFromTasks();

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

            // When
            var result = action.GetStatusFromTasks();

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
            action.NoLongerRequired = false;

            // When
            var result = action.GetStatusFromTasks();

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.Completed));
        }

        [Test]
        public void given_all_tasks_completed_and_action_no_longer_required_when_get_status_from_tasks_then_return_completed()
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
                                              TaskStatus = TaskStatus.Completed,
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

            action.NoLongerRequired = true;

            // When
            var result = action.GetStatusFromTasks();

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.Completed));
        }

        [Test]
        public void given_no_tasks_completed_and_action_no_longer_required_when_get_status_from_tasks_then_return_no_longer_required()
        {
            // Given
            var action = new Action()
            {
                ActionTasks = new List<ActionTask>
                                                                              {
                                                                                  new ActionTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.Overdue                                                                                      
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

            action.NoLongerRequired = true;

            // When
            var result = action.GetStatusFromTasks();

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.NoLongerRequired));
        }
    }
}