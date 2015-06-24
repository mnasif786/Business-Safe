using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Tests.Entities.Responsibilities
{
    [TestFixture]
    public class NextDueDateTests
    {
        [Test]
        public void Given_responsibility_has_non_recurring_task_which_has_due_date_When_NextDueDate_called_Then_correct_value_returned()
        {
            var responsibility = GetResponsibility();
            CreateTask(new DateTime(2050, 06, 01), TaskStatus.Outstanding, TaskReoccurringType.None, null, responsibility);
            Assert.That(responsibility.NextDueDate, Is.EqualTo(new DateTime(2050, 06, 01)));
        }

        [Test]
        public void Given_responsibility_has_non_recurring_tasks_which_have_due_date_When_NextDueDate_called_Then_correct_value_returned()
        {
            var responsibility = GetResponsibility();
            CreateTask(new DateTime(2050, 06, 01), TaskStatus.Outstanding, TaskReoccurringType.None, null, responsibility);
            CreateTask(new DateTime(2050, 07, 01), TaskStatus.Outstanding, TaskReoccurringType.None, null, responsibility);
            CreateTask(new DateTime(2050, 08, 01), TaskStatus.Outstanding, TaskReoccurringType.None, null, responsibility);
            Assert.That(responsibility.NextDueDate, Is.EqualTo(new DateTime(2050, 06, 01)));
        }

        [Test]
        public void Given_responsibility_has_recurring_tasks_which_have_due_date_When_NextDueDate_called_Then_correct_value_returned()
        {
            var responsibility = GetResponsibility();
            CreateTask(new DateTime(2050, 06, 01), TaskStatus.Outstanding, TaskReoccurringType.Weekly, new DateTime(2051, 06, 01), responsibility);
            CreateTask(new DateTime(2050, 07, 01), TaskStatus.Outstanding, TaskReoccurringType.Weekly, new DateTime(2051, 06, 01), responsibility);
            CreateTask(new DateTime(2050, 08, 01), TaskStatus.Outstanding, TaskReoccurringType.None, null, responsibility);
            Assert.That(responsibility.NextDueDate, Is.EqualTo(new DateTime(2050, 06, 01)));
        }

        [Test]
        public void Given_responsibility_has_recurring_tasks_which_have_due_date_and_some_are_completed_and_deleted_When_NextDueDate_called_Then_correct_value_returned()
        {
            var responsibility = GetResponsibility();
            CreateTask(new DateTime(2050, 06, 01), TaskStatus.Completed, TaskReoccurringType.Weekly, new DateTime(2051, 06, 01), responsibility);
            CreateTask(new DateTime(2050, 07, 01), TaskStatus.Outstanding, TaskReoccurringType.Weekly, new DateTime(2051, 06, 01), responsibility);
            CreateTask(new DateTime(2050, 08, 01), TaskStatus.Outstanding, TaskReoccurringType.None, null, responsibility);
            responsibility.ResponsibilityTasks[1].MarkForDelete(new UserForAuditing());
            Assert.That(responsibility.NextDueDate, Is.EqualTo(new DateTime(2050, 08, 01)));
        }

        [Test]
        public void Given_responsibility_no_tasks_When_NextDueDate_called_Then_returns_null()
        {
            var responsibility = GetResponsibility();
            Assert.That(responsibility.NextDueDate, Is.Null);
        }

        [Test]
        public void Given_responsibility_has_non_recurring_tasks_which_have_no_due_date_When_NextDueDate_called_Then_returns_null()
        {
            var responsibility = GetResponsibility();
            CreateTask(null, TaskStatus.Outstanding, TaskReoccurringType.None, null, responsibility);
            Assert.That(responsibility.NextDueDate, Is.Null);
        }

        private Responsibility GetResponsibility()
        {
            return Responsibility.Create(
                4233L,
                new ResponsibilityCategory(),
                "Test Responsibility 3",
                "Test Responsibility 3",
                new Site(),
                new ResponsibilityReason(),
                new Employee(),
                TaskReoccurringType.Annually, null,
                new UserForAuditing());
        }

        private ResponsibilityTask CreateTask(DateTime? dueDate, TaskStatus status, TaskReoccurringType taskReoccurringType, DateTime? reocurringEndDate, Responsibility responsibility)
        {
            return ResponsibilityTask.Create(
                "Test Task",
                "Test Task",
                dueDate,
                status,
                new Employee(),
                new UserForAuditing(),
                new List<CreateDocumentParameters>(),
                new TaskCategory(),
                (int)taskReoccurringType,
                reocurringEndDate,
                false,
                false,
                false,
                false,
                Guid.NewGuid(),
                new Site(),
                responsibility);
        }
    }
}
