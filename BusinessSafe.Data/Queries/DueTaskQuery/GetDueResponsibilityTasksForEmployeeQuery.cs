using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;
using log4net;
using NHibernate;
using NHibernate.Linq;

namespace BusinessSafe.Data.Queries.DueTaskQuery
{
    public interface IGetDueResponsibilityTasksForEmployeeQuery
    {
        List<ResponsibilityTask> Execute(Guid employeeId, ISession session);
    }
    public class GetDueResponsibilityTasksForEmployeeQuery : IGetDueResponsibilityTasksForEmployeeQuery
    {
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public GetDueResponsibilityTasksForEmployeeQuery(IQueryableWrapper<Responsibility> queryableWrapper)
        {
        }

        public List<ResponsibilityTask> Execute(Guid employeeId, ISession session)
        {
            try
            {
                //Log.Debug("GetDueResponsibilityTasksForEmployeeQuery - Execute()");
                var queryTasksWhereAssignedToIsEmployee = session.Query<Responsibility>()
                    .Where(r => r.Deleted == false)
                    .SelectMany(r => r.ResponsibilityTasks)
                    .Where(TasksAreDue())
                    .Where(EmployeeIsTheAssignee(employeeId));

                var responsibilityTasks = new List<ResponsibilityTask>();

                queryTasksWhereAssignedToIsEmployee.ForEach(
                    task =>
                    {
                        if (IsTaskDue(task))
                            responsibilityTasks.Add(task);
                    });

                return responsibilityTasks;
            }
            catch (Exception ex)
            {
                Log.Debug("Exception - GetDueResponsibilityTasksForEmployeeQuery - Execute()" + ex.Message);
                throw;
            }
        }

        private static Expression<Func<ResponsibilityTask, bool>> TasksAreDue()
        {
            return task => task.Deleted == false // dont return deleted tasks
                        && task.TaskStatus == TaskStatus.Outstanding 
                        && task.TaskCompletedDate == null // ignore completed
                        && task.TaskCompletionDueDate != null // only include tasks with due date
                        && task.TaskCompletionDueDate > DateTime.Now;
        }

        private static Expression<Func<ResponsibilityTask, bool>> EmployeeIsTheAssignee(Guid employeeId)
        {
            return task => (task.TaskAssignedTo != null && task.TaskAssignedTo.Id == employeeId);
        }

        private static bool IsTaskDue(ResponsibilityTask task)
        {
            return (
                (task.TaskAssignedTo.NotificationType == NotificationType.Daily && task.TaskCompletionDueDate.HasValue && QueriesHelper.SimplifyDate(task.TaskCompletionDueDate.Value) == QueriesHelper.SimplifyDate(DateTime.Now.AddDays(1))) ||
                (task.TaskAssignedTo.NotificationType == NotificationType.Weekly && task.TaskCompletionDueDate.HasValue && task.TaskCompletionDueDate.Value < DateTime.Now.AddDays(7)) ||
                (task.TaskAssignedTo.NotificationType == NotificationType.Monthly && task.TaskCompletionDueDate.HasValue && task.TaskCompletionDueDate.Value < DateTime.Now.AddDays(30)));
        }
    }
}
