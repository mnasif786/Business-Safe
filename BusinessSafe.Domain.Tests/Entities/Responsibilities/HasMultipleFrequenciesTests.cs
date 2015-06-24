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
    public class HasMultipleFrequenciesTests
    {
        [Test]
        public void Given_responsibility_has_recurring_task_When_HasMultipleFrequencies_called_Then_correct_value_returned()
        {
            var responsibility = GetResponsibility();
            CreateTask(new DateTime(2050, 06, 01), TaskStatus.Outstanding, TaskReoccurringType.Weekly, new DateTime(2051, 06, 01), responsibility);
            Assert.That(responsibility.HasMultipleFrequencies, Is.False);
        }

        [Test]
        public void Given_responsibility_has_recurring_tasks_with_same_frequeny_When_HasMultipleFrequencies_called_Then_correct_value_returned()
        {
            var responsibility = GetResponsibility();
            CreateTask(new DateTime(2050, 06, 01), TaskStatus.Outstanding, TaskReoccurringType.Weekly, new DateTime(2051, 06, 01), responsibility);
            CreateTask(new DateTime(2050, 07, 01), TaskStatus.Outstanding, TaskReoccurringType.Weekly, new DateTime(2051, 07, 01), responsibility);

            Assert.That(responsibility.HasMultipleFrequencies, Is.False);
        }

        [Test]
        public void Given_responsibility_has_recurring_tasks_with_different_frequencies_When_HasMultipleFrequencies_called_Then_correct_value_returned()
        {
            var responsibility = GetResponsibility();
            CreateTask(new DateTime(2050, 06, 01), TaskStatus.Outstanding, TaskReoccurringType.Weekly, new DateTime(2051, 06, 01), responsibility);
            CreateTask(new DateTime(2050, 07, 01), TaskStatus.Outstanding, TaskReoccurringType.Monthly, new DateTime(2051, 07, 01), responsibility);
            Assert.That(responsibility.HasMultipleFrequencies, Is.True);
        }

        [Test]
        public void Given_responsibility_has_recurring_tasks_with_different_frequencies_and_one_is_none_When_HasMultipleFrequencies_called_Then_correct_value_returned()
        {
            var responsibility = GetResponsibility();
            CreateTask(new DateTime(2050, 06, 01), TaskStatus.Outstanding,  TaskReoccurringType.Weekly, new DateTime(2051, 06, 01), responsibility);
            CreateTask(new DateTime(2050, 07, 01), TaskStatus.Outstanding,TaskReoccurringType.None, null, responsibility);
            Assert.That(responsibility.HasMultipleFrequencies, Is.True);
        }

        [Test]
        public void Given_responsibility_tasks_with_same_frequencies_When_task_with_same_frequency_is_added_Then_event_is_not_raised()
        {
            var responsibility = GetResponsibility();
            CreateTask(new DateTime(2050, 06, 01), TaskStatus.Outstanding, TaskReoccurringType.Weekly, new DateTime(2051, 06, 01), responsibility);
            CreateTask(new DateTime(2050, 07, 01), TaskStatus.Outstanding, TaskReoccurringType.Weekly, new DateTime(2051, 07, 01), responsibility);
            var eventRaised = false;

            responsibility.HasMultipleFrequencyChangeToTrue += delegate(object sender, EventArgs e)
                                                                   {
                                                                       eventRaised = true;
                                                                   };

            CreateTask(new DateTime(2050, 08, 01), TaskStatus.Outstanding, TaskReoccurringType.Weekly, new DateTime(2051, 08, 01), responsibility);
            Assert.That(eventRaised, Is.False);
        }

        [Test]
        public void Given_responsibility_tasks_with_same_frequencies_When_task_with_different_frequency_is_added_Then_event_is_raised()
        {
            var responsibility = GetResponsibility();
            CreateTask(new DateTime(2050, 06, 01), TaskStatus.Outstanding, TaskReoccurringType.Weekly, new DateTime(2051, 06, 01), responsibility);
            CreateTask(new DateTime(2050, 07, 01), TaskStatus.Outstanding, TaskReoccurringType.Weekly, new DateTime(2051, 07, 01), responsibility);
            var eventRaised = false;

            responsibility.HasMultipleFrequencyChangeToTrue += delegate(object sender, EventArgs e)
            {
                eventRaised = true;
            };

            CreateTask(new DateTime(2050, 08, 01), TaskStatus.Outstanding, TaskReoccurringType.Monthly, new DateTime(2051, 08, 01), responsibility);
            Assert.That(eventRaised, Is.True);
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
