using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Data.Queries.DueTaskQuery;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Data.Tests.Queries.DueTomorrowTaskQueryTests
{
    public class GetDueActionTasksForEmployeeQueryTests
    {
        private Mock<IQueryableWrapper<BusinessSafe.Domain.Entities.Action>> _queryableWrapper;

        private List<BusinessSafe.Domain.Entities.Action> _actions;

        [SetUp]
        [Ignore]
        public void Setup()
        {
            _queryableWrapper = new Mock<IQueryableWrapper<BusinessSafe.Domain.Entities.Action>>();
            _actions = new List<BusinessSafe.Domain.Entities.Action>();

            _queryableWrapper.Setup(x => x.Queryable())
                .Returns(() => _actions.AsQueryable());
        }

        
        [Test]
        [Ignore]
        public void Given_Employee_is_assignee_then_return_due_tomorrow_action_task_when_set_to_daily_notifications()
         {
             var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily};
             var actionTask = new ActionTask()
             {
                 TaskAssignedTo = employee,
                 TaskStatus = TaskStatus.Outstanding,
                 TaskCompletedDate = null,
                 TaskCompletionDueDate = DateTime.Now.AddDays(1)
             };

             var action = new Domain.Entities.Action();
             action.ActionTasks.Add(actionTask);             

             _actions.Add(action);

             var target = new GetDueActionTasksForEmployeeQuery(_queryableWrapper.Object);

             //WHEN
             var result = target.Execute(employee.Id, null);

             //THEN
             Assert.That(result.Count, Is.EqualTo(1));
         }

        [Test]
        [Ignore]
        public void Given_Employee_is_assignee_then_dont_return_due_2days_action_task_when_set_to_daily_notifications()
        {
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily };
            var actionTask = new ActionTask()
            {
                TaskAssignedTo = employee,
                TaskStatus = TaskStatus.Outstanding,
                TaskCompletedDate = null,
                TaskCompletionDueDate = DateTime.Now.AddDays(2)
            };

            var action = new Domain.Entities.Action();
            action.ActionTasks.Add(actionTask);

            _actions.Add(action);

            var target = new GetDueActionTasksForEmployeeQuery(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(0));
        }


        [Test]
        [Ignore]
        public void Given_notification_is_weekly_and_taskDueDate_more_than_week_away_then_dont_return_due_action_task()
        {
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Weekly};
            var actionTask = new ActionTask()
            {
                TaskAssignedTo = employee,
                TaskStatus = TaskStatus.Outstanding,
                TaskCompletedDate = null,
                TaskCompletionDueDate = DateTime.Now.AddDays(10)
            };

            var action = new Domain.Entities.Action();
            action.ActionTasks.Add(actionTask);

            _actions.Add(action);

            var target = new GetDueActionTasksForEmployeeQuery(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        [Ignore]
        public void Given_notification_is_weekly_and_taskDueDate_less_than_week_away_then_return_due_action_task()
        {
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Weekly };
            var actionTask = new ActionTask()
            {
                TaskAssignedTo = employee,
                TaskStatus = TaskStatus.Outstanding,
                TaskCompletedDate = null,
                TaskCompletionDueDate = DateTime.Now.AddDays(6)
            };

            var action = new Domain.Entities.Action();
            action.ActionTasks.Add(actionTask);

            _actions.Add(action);

            var target = new GetDueActionTasksForEmployeeQuery(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(1));
        }


        [Test]
        [Ignore]
        public void Given_notification_is_monthly_and_taskDueDate_more_than_month_away_then_dont_return_due_action_task()
        {
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Monthly };
            var actionTask = new ActionTask()
            {
                TaskAssignedTo = employee,
                TaskStatus = TaskStatus.Outstanding,
                TaskCompletedDate = null,
                TaskCompletionDueDate = DateTime.Now.AddDays(35)
            };

            var action = new Domain.Entities.Action();
            action.ActionTasks.Add(actionTask);

            _actions.Add(action);

            var target = new GetDueActionTasksForEmployeeQuery(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        [Ignore]
        public void Given_notification_is_weekly_and_taskDueDate_less_than_month_away_then_return_due_action_task()
        {
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Monthly };
            var actionTask = new ActionTask()
            {
                TaskAssignedTo = employee,
                TaskStatus = TaskStatus.Outstanding,
                TaskCompletedDate = null,
                TaskCompletionDueDate = DateTime.Now.AddDays(25)
            };

            var action = new Domain.Entities.Action();
            action.ActionTasks.Add(actionTask);

            _actions.Add(action);

            var target = new GetDueActionTasksForEmployeeQuery(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(1));
        }




    }
}
