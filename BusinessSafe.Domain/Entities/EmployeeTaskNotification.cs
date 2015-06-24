using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public enum TaskNotificationEventEnum
    {
        Completed = 1,
    }

    public class EmployeeTaskNotification : BaseEntity<long>
    {
        public virtual Employee Employee { get; set; }
        public virtual Task Task { get; set; }
        public virtual TaskNotificationEventEnum TaskEvent  { get; set; }
        //public virtual DateTime NotificationDateTime { get; set; } //Not in use at the moment. May be this is not required but leaving it at the moment.

        public static EmployeeTaskNotification Create(Employee employee, Task task, TaskNotificationEventEnum eventType, DateTime notificationDateTime, UserForAuditing user)
        {
            var employeeTaskNotification = new EmployeeTaskNotification();
            employeeTaskNotification.CreatedBy = user;
            employeeTaskNotification.CreatedOn = DateTime.Now;
            employeeTaskNotification.LastModifiedBy = user;
            employeeTaskNotification.LastModifiedOn = DateTime.Now;
            employeeTaskNotification.Employee = employee;
            employeeTaskNotification.Task = task;
            employeeTaskNotification.TaskEvent = eventType;
            //employeeTaskNotification.NotificationDateTime = notificationDateTime;

            return employeeTaskNotification;
        }
    }
}
