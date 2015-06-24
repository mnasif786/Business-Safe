using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.TaskTests
{
    [TestFixture]
    public class EmployeeTaskNotificationTests
    {
        [Test]
        public void Given_employee_has_been_notified_regarding_a_task_overdue_when_AddEmployeeTaskNotification_then_added_to_notification_list()
        {
            //given
            var task = new DerivedDisplayStatusTests.TestableTask() { Id = 123123 };
            var employee = new Employee() {Id = Guid.NewGuid()};
            var expectedEventDateTime = new DateTime(2003, 03, 12, 4, 23, 2);

            //when
            task.AddEmployeeTaskNotificationHistory(employee, TaskNotificationEventEnum.Completed, expectedEventDateTime, null);
            
            //then
            Assert.That(task.EmployeeTaskNotificationHistory, Is.Not.Null);
            Assert.That(task.EmployeeTaskNotificationHistory.Count, Is.EqualTo(1));
            Assert.That(task.EmployeeTaskNotificationHistory[0].Task, Is.EqualTo(task));
            Assert.That(task.EmployeeTaskNotificationHistory[0].Employee, Is.EqualTo(employee));
            Assert.That(task.EmployeeTaskNotificationHistory[0].TaskEvent, Is.EqualTo(TaskNotificationEventEnum.Completed));
            //Assert.That(task.EmployeeTaskNotificationHistory[0].NotificationDateTime, Is.EqualTo(expectedEventDateTime));
        }
    }
}
