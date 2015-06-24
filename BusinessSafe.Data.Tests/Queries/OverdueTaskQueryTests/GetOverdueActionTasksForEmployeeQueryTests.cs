using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Data.Queries;
using BusinessSafe.Data.Queries.OverdueTaskQuery;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;


namespace BusinessSafe.Data.Tests.Queries.OverdueTaskQueryTests
{
   [TestFixture]
    public class GetOverdueActionTasksForEmployeeQueryTests
    {       
        private Mock<IQueryableWrapper<BusinessSafe.Domain.Entities.Action>> _queryableWrapper;

        private List<BusinessSafe.Domain.Entities.Action> _actions;
         
         [SetUp]
         public void Setup()
         {
             _queryableWrapper = new Mock<IQueryableWrapper<BusinessSafe.Domain.Entities.Action>>();
             _actions = new List<BusinessSafe.Domain.Entities.Action>();

             _queryableWrapper.Setup(x => x.Queryable())
                 .Returns(() => _actions.AsQueryable());
         }

         [Test]
         [Ignore]
         public void Given_Employee_is_assigned_a_task_then_return_task()
         {
             var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily};
             var actionTask = new ActionTask()
             {
                 TaskAssignedTo = employee,
                 TaskCompletedDate = null,
                 TaskCompletionDueDate = DateTime.Now.AddDays(-5)
             };

             var action = new BusinessSafe.Domain.Entities.Action();
             action.ActionTasks.Add(actionTask);             

             _actions.Add(action);

             var target = new GetOverdueActionTasksForEmployeeQuery(_queryableWrapper.Object);

             //WHEN
             var result = target.Execute(employee.Id, null);

             //THEN
             Assert.That(result.Count, Is.EqualTo(1));
         }

       [Test]
       [Ignore]
       public void Given_employee_is_asignee_and_notification_frequency_is_set_to_weekly_then_return_tasks_since_previous_week()
       {
           var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Weekly, NotificationFrequecy = (int)DayOfWeek.Wednesday };
           var action = new BusinessSafe.Domain.Entities.Action();
           
           action.ActionTasks.Add( new ActionTask()
           {
               TaskAssignedTo = employee,
               TaskCompletedDate = null,
               TaskCompletionDueDate = DateTime.Now.AddDays(-5)
           });

           action.ActionTasks.Add(new ActionTask()
           {
               TaskAssignedTo = employee,
               TaskCompletedDate = null,
               TaskCompletionDueDate = DateTime.Now.AddDays(-2)
           });

           action.ActionTasks.Add(new ActionTask()
           {
               TaskAssignedTo = employee,
               TaskCompletedDate = null,
               TaskCompletionDueDate = DateTime.Now.AddDays(-12)
           });

           _actions.Add(action);

           var target = new GetOverdueActionTasksForEmployeeQuery(_queryableWrapper.Object);

           //WHEN
           var result = target.Execute(employee.Id, null);

           //THEN
           Assert.That(result.Count, Is.EqualTo(2));
       }

        [Ignore]
       [Test]
       public void Given_employee_is_assignee_and_notification_frequency_is_set_to_monthly_on_26th_then_return_tasks_since_26th_of_previous_month()
       {
           //GIVEN
           var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Monthly, NotificationFrequecy = 26 };
           
           var action = new BusinessSafe.Domain.Entities.Action();

           action.ActionTasks.Add(new ActionTask()
           {
               TaskAssignedTo = employee,
               TaskCompletedDate = null,
               TaskCompletionDueDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 27), //27th of last month,
               Title = "one"
           });

           action.ActionTasks.Add(new ActionTask()
           {
               TaskAssignedTo = employee,
               TaskCompletedDate = null,
               TaskCompletionDueDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 28), //28th of last month,
               Title = "two"
           });

           action.ActionTasks.Add(new ActionTask()
           {
               TaskAssignedTo = employee,
               TaskCompletedDate = null,
               TaskCompletionDueDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 23), //23th of last month,
               Title = "three"
           });

           _actions.Add(action);
           
           var target = new GetOverdueActionTasksForEmployeeQuery(_queryableWrapper.Object);

           //WHEN
           var result = target.Execute(employee.Id, null);
           //THEN
           Assert.That(result.Count, Is.EqualTo(2));
           Assert.That(result[0].Title, Is.EqualTo("one"));
           Assert.That(result[1].Title, Is.EqualTo("two"));
       }

    }
}
