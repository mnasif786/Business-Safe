using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;
using log4net;
using NHibernate;
using NHibernate.Linq;

namespace BusinessSafe.Data.Queries.OverdueTaskQuery
{
    public interface IGetOverdueResponsibilitiesTasksForEmployeeQuery
    {
        List<ResponsibilityTask> Execute(Guid employeeId, ISession session);
    }

    public class GetOverdueResponsibilitiesTasksForEmployeeQuery : IGetOverdueResponsibilitiesTasksForEmployeeQuery
    {
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public GetOverdueResponsibilitiesTasksForEmployeeQuery(IQueryableWrapper<Responsibility> queryableWrapper)
        {
        }
        
        public List<ResponsibilityTask> Execute(Guid employeeId, ISession session)
        {
            try
            {
                //Log.Debug("GetOverdueResponsibilitiesTasksForEmployeeQuery - Execute()");
                var queryTasksWhereAssignedToIsEmployee = session.Query<Responsibility>()
                    .Where(r => r.Deleted == false)
                    .SelectMany(r => r.ResponsibilityTasks)
                    .Where(TaskIsAssignedToEmployee(employeeId))
                    .Where(TasksAreOverdue());

                var responsibilityTasks = new List<ResponsibilityTask>();

                queryTasksWhereAssignedToIsEmployee.ForEach(
                    task =>
                    {
                        if (DoesTaskNeedToBeNotified(task, employeeId))
                            responsibilityTasks.Add(task);
                    });

                return responsibilityTasks;
            }
            catch (Exception ex)
            {
                Log.Debug("Exception - GetOverdueResponsibilitiesTasksForEmployeeQuery - Execute()" + ex.Message);
                throw;
            }
        }

        private static Expression<Func<ResponsibilityTask, bool>> TasksAreOverdue()
        {
            return task => task.Deleted == false // dont return deleted tasks
                        && (task.TaskStatus == TaskStatus.Outstanding || task.TaskStatus == TaskStatus.Overdue) //only return outstanding/overdue tasks
                        && task.TaskCompletedDate == null // ignore completed
                        && task.TaskCompletionDueDate != null // only include tasks with due date
                        && task.TaskCompletionDueDate < DateTime.Today.AddDays(-1);
        }

        private static bool DoesTaskNeedToBeNotified(ResponsibilityTask task, Guid employeeId)
        {
            // Risk Assessor
            if (task.TaskAssignedTo != null && task.TaskAssignedTo.Id == employeeId)
            {
                var notificationType = task.TaskAssignedTo.NotificationType;
                var notificationFrequency = task.TaskAssignedTo.NotificationFrequecy;

                return (
                             (notificationType == NotificationType.Daily ||
                             (notificationType == NotificationType.Weekly && task.TaskCompletionDueDate.HasValue &&
                             QueriesHelper.SimplifyDate(task.TaskCompletionDueDate.Value) >= QueriesHelper.SimplifyDate(DateTime.Now.AddDays(-7))) ||
                             (notificationType == NotificationType.Monthly && task.TaskCompletionDueDate.HasValue && notificationFrequency.HasValue && 
                             QueriesHelper.SimplifyDate(task.TaskCompletionDueDate.Value) >= QueriesHelper.GetPreviousMonthsDate(notificationFrequency.Value)))
                       );
            }
            else
            {
                return false;
            }
        }
        
        private static Expression<Func<ResponsibilityTask, bool>> TaskIsAssignedToEmployee(Guid employeeId)
        {
            return task => (task.TaskAssignedTo != null && task.TaskAssignedTo.Id == employeeId);
        }

    }
}
