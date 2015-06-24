
using System;
using System.Collections.Generic;

using BusinessSafe.Domain.Entities;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.Responsibilities
{
    [TestFixture]
    public class StatusDerivedFromTasksTests
    {
        [Test]
        public void Given_no_tasks_When_StatusDerivedFromTasks_Then_return_none()
        {
            // Given
            var responsibility = new Responsibility();

            // When
            var result = responsibility.GetStatusDerivedFromTasks();

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.None));
        }

        [Test]
        public void Given_only_deleted_tasks_When_StatusDerivedFromTasks_Then_return_none()
        {
            // Given
            var responsibility = new Responsibility
            {
                ResponsibilityTasks = new List<ResponsibilityTask>
                                                           {
                                                               new ResponsibilityTask()
                                                               {
                                                                   Deleted = true,
                                                                   TaskStatus = TaskStatus.Outstanding,
                                                                   TaskCompletionDueDate = DateTime.Now
                                                               }
                                                           }
            };

            // When
            var result = responsibility.GetStatusDerivedFromTasks();

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.None));
        }

        [Test]
        public void Given_outstanding_tasks_When_StatusDerivedFromTasks_Then_return_outstanding()
        {
            // Given
            var responsibility = new Responsibility()
            {
                ResponsibilityTasks = new List<ResponsibilityTask>
                                                                              {
                                                                                  new ResponsibilityTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.Completed,
                                                                                      TaskCompletionDueDate = DateTime.Now.AddDays(1)
                                                                                  },
                                                                                  new ResponsibilityTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.Completed,
                                                                                      TaskCompletionDueDate = DateTime.Now
                                                                                  },
                                                                                  new ResponsibilityTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.Outstanding,
                                                                                      TaskCompletionDueDate = DateTime.Now.AddDays(1)
                                                                                  }
                                                                              }
            };

            // When
            var result = responsibility.GetStatusDerivedFromTasks();

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.Outstanding));
        }

        [Test]
        public void Given_outstanding_tasks_and_deleted_overdue_ones_When_StatusDerivedFromTasks_Then_return_outstanding()
        {
            // Given
            var responsibility = new Responsibility()
            {
                ResponsibilityTasks = new List<ResponsibilityTask>
                                                                              {
                                                                                  new ResponsibilityTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.Completed,
                                                                                      TaskCompletionDueDate = DateTime.Now.AddDays(1)
                                                                                  },
                                                                                  new ResponsibilityTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.Completed,
                                                                                      TaskCompletionDueDate = DateTime.Now
                                                                                  },
                                                                                  new ResponsibilityTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.Outstanding,
                                                                                      TaskCompletionDueDate = DateTime.Now.AddDays(1)
                                                                                  },
                                                                                  new ResponsibilityTask()
                                                                                  {
                                                                                      Deleted = true,
                                                                                      TaskStatus = TaskStatus.Outstanding,
                                                                                      TaskCompletionDueDate = DateTime.Now.AddDays(-1)
                                                                                  }
                                                                              }
            };

            // When
            var result = responsibility.GetStatusDerivedFromTasks();

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.Outstanding));
        }

        [Test]
        public void Given_outstanding_tasks_that_are_due_before_today_When_StatusDerivedFromTasks_Then_return_overdue()
        {
            // Given
            var responsibility = new Responsibility()
            {
                ResponsibilityTasks = new List<ResponsibilityTask>
                                                                              {
                                                                                  new ResponsibilityTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.Completed,
                                                                                      TaskCompletionDueDate = DateTime.Now.AddDays(1)
                                                                                  },
                                                                                  new ResponsibilityTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.Completed,
                                                                                      TaskCompletionDueDate = DateTime.Now
                                                                                  },
                                                                                  new ResponsibilityTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.Outstanding,
                                                                                      TaskCompletionDueDate = DateTime.Now.AddDays(-1)
                                                                                  }
                                                                              }
            };

            // When
            var result = responsibility.GetStatusDerivedFromTasks();

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.Overdue));
        }

        [Test]
        public void Given_all_tasks_completed_When_StatusDerivedFromTasks_Then_return_completed()
        {
            // Given
            var responsibility = new Responsibility()
            {
                ResponsibilityTasks = new List<ResponsibilityTask>
                                                                              {
                                                                                  new ResponsibilityTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.Completed,
                                                                                      TaskCompletionDueDate = DateTime.Now.AddDays(-1)
                                                                                  },
                                                                                  new ResponsibilityTask()
                                                                                  {
                                                                                      Deleted = false,
                                                                                      TaskStatus = TaskStatus.NoLongerRequired,
                                                                                      TaskCompletionDueDate = DateTime.Now.AddDays(-1)
                                                                                  },
                                                                                  new ResponsibilityTask()
                                                                                  {
                                                                                      Deleted = true,
                                                                                      TaskStatus = TaskStatus.Outstanding,
                                                                                      TaskCompletionDueDate = DateTime.Now.AddDays(-1)
                                                                                  }
                                                                              }
            };

            // When
            var result = responsibility.GetStatusDerivedFromTasks();

            // Then
            Assert.That(result, Is.EqualTo(DerivedTaskStatusForDisplay.Completed));
        }
    }
}