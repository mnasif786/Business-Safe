using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Data.Queries;
using BusinessSafe.Data.Queries.OverdueTaskQuery;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.Entities.SafeCheck;
using Moq;
using NHibernate;
using NUnit.Framework;

namespace BusinessSafe.Data.Tests.Queries.OverdueTaskQueryTests
{
     [TestFixture]
    public class GetOverdueResponsibilitiesTasksForEmployeeQueryTests
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
         public void Given_Employee_is_assigned_a_task_then_return_task()
         {
             var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily};
             var responsibilityTask = new ResponsibilityTask()
             {
                 TaskAssignedTo = employee,
                 TaskCompletionDueDate = DateTime.Now.AddDays(-5)
             };

             var responsibility = new Responsibility(){  };
             responsibility.ResponsibilityTasks.Add(responsibilityTask);

             _responsibilities.Add(responsibility);

             var target = new GetOverdueResponsibilitiesTasksForEmployeeQuery(_queryableWrapper.Object);

             //WHEN
             var result = target.Execute(employee.Id, null);

             //THEN
             Assert.That(result.Count, Is.EqualTo(1));
         }

         [Test]
         [Ignore]
         public void Given_employee_is_asignee_and_notification_frequency_is_set_to_weekly_then_return_tasks_since_previous_week()
         {
             var employee = new Employee() { NotificationType = NotificationType.Weekly, NotificationFrequecy = (int)System.DayOfWeek.Wednesday };
             var responsibility = new Responsibility() { };
             
             responsibility.ResponsibilityTasks.Add( new ResponsibilityTask()
             {
                 TaskAssignedTo = employee,
                 TaskCompletionDueDate = DateTime.Now.AddDays(-2)
             });

             responsibility.ResponsibilityTasks.Add(new ResponsibilityTask()
             {
                 TaskAssignedTo = employee,
                 TaskCompletionDueDate = DateTime.Now.AddDays(-5)
             });

             responsibility.ResponsibilityTasks.Add(new ResponsibilityTask()
             {
                 TaskAssignedTo = employee,
                 TaskCompletionDueDate = DateTime.Now.AddDays(-15)
             });

             _responsibilities.Add(responsibility);

             var target = new GetOverdueResponsibilitiesTasksForEmployeeQuery(_queryableWrapper.Object);

             //WHEN
             var result = target.Execute(employee.Id, null);

             //THEN
             Assert.That(result.Count, Is.EqualTo(2));
         }

         [Test]
         [Ignore]
         public void Given_employee_isassignee_and_notification_frequency_is_set_to_monthly_on_26th_then_return_tasks_since_26th_of_previous_month()
         {
             var employee = new Employee() { NotificationType = NotificationType.Monthly, NotificationFrequecy = 26};
             var responsibility = new Responsibility() { };

             responsibility.ResponsibilityTasks.Add(new ResponsibilityTask()
             {
                 TaskAssignedTo = employee,
                 TaskCompletionDueDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 27), //27th of last month,
             });

             responsibility.ResponsibilityTasks.Add(new ResponsibilityTask()
             {
                 TaskAssignedTo = employee,
                 TaskCompletionDueDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 28), //28th of last month,
             });

             responsibility.ResponsibilityTasks.Add(new ResponsibilityTask()
             {
                 TaskAssignedTo = employee,
                 TaskCompletionDueDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 23), //23th of last month,
             });

             _responsibilities.Add(responsibility);

             var target = new GetOverdueResponsibilitiesTasksForEmployeeQuery(_queryableWrapper.Object);

             //WHEN
             var result = target.Execute(employee.Id, null);

             //THEN
             Assert.That(result.Count, Is.EqualTo(2));
         }
    }

}
