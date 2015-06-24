using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Data.Queries.DueTaskQuery;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Data.Tests.Queries.DueTomorrowTaskQueryTests
{
    class GetDueResponsibilitiesTasksForEmployeeQueryTests
    {
        private Mock<IQueryableWrapper<Responsibility>> _queryableWrapper;
        private List<Responsibility> _responsibilities;

        [SetUp]
        public void Setup()
        {
            _queryableWrapper = new Mock<IQueryableWrapper<Responsibility>>();
            _responsibilities = new List<Responsibility>();

            _queryableWrapper.Setup(x => x.Queryable())
                .Returns(() => _responsibilities.AsQueryable());
        }


        [Test]
        [Ignore]
        public void Given_Employee_is_assignee_task_due_tomorrow_and_notified_daily_then_return_due_tomorrow_responsibility_task()
        {
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily};
            var responsibilityTask = new ResponsibilityTask()
            {
                TaskAssignedTo = employee,
                TaskStatus = TaskStatus.Outstanding,
                TaskCompletedDate = null,
                TaskCompletionDueDate = DateTime.Now.AddDays(1)
            };

            var responsibility = new Responsibility() { };
            responsibility.ResponsibilityTasks.Add(responsibilityTask);

            _responsibilities.Add(responsibility);

            var target = new GetDueResponsibilityTasksForEmployeeQuery(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(1));
        }


        [Test]
        [Ignore]
        public void Given_Employee_is_assignee_task_due_in_2_days_and_notified_daily_then_dont_return_due_tomorrow_responsibility_task()
        {
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily };
            var responsibilityTask = new ResponsibilityTask()
            {
                TaskAssignedTo = employee,
                TaskStatus = TaskStatus.Outstanding,
                TaskCompletedDate = null,
                TaskCompletionDueDate = DateTime.Now.AddDays(2)
            };

            var responsibility = new Responsibility() { };
            responsibility.ResponsibilityTasks.Add(responsibilityTask);

            _responsibilities.Add(responsibility);

            var target = new GetDueResponsibilityTasksForEmployeeQuery(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        [Ignore]
        public void Given_notification_set_to_weekly_and_task_is_due_in_less_than_a_week_return_task()
        {
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Weekly};
            var responsibilityTask = new ResponsibilityTask()
            {
                TaskAssignedTo = employee,
                TaskStatus = TaskStatus.Outstanding,
                TaskCompletedDate = null,
                TaskCompletionDueDate = DateTime.Now.AddDays(5)
            };

            var responsibility = new Responsibility() { };
            responsibility.ResponsibilityTasks.Add(responsibilityTask);

            _responsibilities.Add(responsibility);

            var target = new GetDueResponsibilityTasksForEmployeeQuery(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        [Ignore]
        public void Given_notification_set_to_weekly_and_task_is_due_in_more_than_a_week__then_dont_return_task()
        {
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Weekly };
            var responsibilityTask = new ResponsibilityTask()
            {
                TaskAssignedTo = employee,
                TaskStatus = TaskStatus.Outstanding,
                TaskCompletedDate = null,
                TaskCompletionDueDate = DateTime.Now.AddDays(15)
            };

            var responsibility = new Responsibility() { };
            responsibility.ResponsibilityTasks.Add(responsibilityTask);

            _responsibilities.Add(responsibility);

            var target = new GetDueResponsibilityTasksForEmployeeQuery(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(0));
        }


        [Test]
        [Ignore]
        public void Given_notification_set_to_monthly_and_task_is_due_in_less_than_a_month_return_task()
        {
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Monthly };
            var responsibilityTask = new ResponsibilityTask()
            {
                TaskAssignedTo = employee,
                TaskStatus = TaskStatus.Outstanding,
                TaskCompletedDate = null,
                TaskCompletionDueDate = DateTime.Now.AddDays(20)
            };

            var responsibility = new Responsibility() { };
            responsibility.ResponsibilityTasks.Add(responsibilityTask);

            _responsibilities.Add(responsibility);

            var target = new GetDueResponsibilityTasksForEmployeeQuery(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        [Ignore]
        public void Given_notification_set_to_monthly_and_task_is_due_in_more_than_a_month__then_dont_return_task()
        {
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Monthly };
            var responsibilityTask = new ResponsibilityTask()
            {
                TaskAssignedTo = employee,
                TaskStatus = TaskStatus.Outstanding,
                TaskCompletedDate = null,
                TaskCompletionDueDate = DateTime.Now.AddDays(35)
            };

            var responsibility = new Responsibility() { };
            responsibility.ResponsibilityTasks.Add(responsibilityTask);

            _responsibilities.Add(responsibility);

            var target = new GetDueResponsibilityTasksForEmployeeQuery(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(0));
        }
    }
}
