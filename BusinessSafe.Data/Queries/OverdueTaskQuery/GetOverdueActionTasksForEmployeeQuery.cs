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
    public class GetOverdueActionTasksForEmployeeQuery : IGetOverdueActionTasksForEmployeeQuery
    {
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public GetOverdueActionTasksForEmployeeQuery(IQueryableWrapper<BusinessSafe.Domain.Entities.Action> queryableActions)
        {
        }

        public List<ActionTask> Execute(Guid employeeId, ISession session)
        {
            try
            {
                //Log.Debug("GetOverdueResponsibilitiesTasksForEmployeeQuery - Execute()" );

                var overdueActionTasks = session
                    .Query<BusinessSafe.Domain.Entities.Action>()
                    .Where(x => x.Deleted == false)
                    .SelectMany(x => x.ActionTasks)
                    .Where(TasksAreAssignedToTheEmployee(employeeId))
                    .Where(TasksAreOverdue());

                var overdueActionTasksTemp = new List<ActionTask>();

                overdueActionTasks.ForEach(
                    task =>
                    {
                        if (DoesTaskNeedToBeNotified(task, employeeId))
                            overdueActionTasksTemp.Add(task);

                    });

                return overdueActionTasksTemp;
            }
            catch (Exception ex)
            {
                Log.Debug("Exception - GetOverdueResponsibilitiesTasksForEmployeeQuery - Execute()" + ex.Message);
                throw;
            }
        }

        private static Expression<Func<ActionTask,bool>> TasksAreOverdue()
        {
            return task => task.TaskCompletedDate == null
                           && task.TaskCompletionDueDate != null
                           && task.TaskCompletionDueDate < DateTime.Today.AddDays(-1);
        }


        private static bool DoesTaskNeedToBeNotified(ActionTask task, Guid employeeId)
        {
            // Risk Assessor
            if (task.TaskAssignedTo != null && task.TaskAssignedTo.Id == employeeId)
            {
                var notificationType = task.TaskAssignedTo.NotificationType;
                var notificationFrequency = task.TaskAssignedTo.NotificationFrequecy;

                return (notificationType == NotificationType.Daily ||
                         (notificationType == NotificationType.Weekly &&
                         task.TaskCompletionDueDate.HasValue && QueriesHelper.SimplifyDate(task.TaskCompletionDueDate.Value) >=  QueriesHelper.SimplifyDate(DateTime.Now.AddDays(-7))) ||
                         (notificationType == NotificationType.Monthly && notificationFrequency.HasValue &&  
                          task.TaskCompletionDueDate.HasValue && task.TaskCompletionDueDate.Value >= QueriesHelper.GetPreviousMonthsDate(notificationFrequency.Value)));
            }
            else
            {
                return false;
            }
        }

        private static Expression<Func<ActionTask, bool>> TasksAreAssignedToTheEmployee( Guid employeeId )
        {
            return task => (task.TaskAssignedTo != null && task.TaskAssignedTo.Id == employeeId);
        }
    }
}