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
    public interface IGetDueActionTasksForEmployeeQuery
    {
        List<ActionTask> Execute(Guid employeeId, ISession session);
    }
    public class GetDueActionTasksForEmployeeQuery : IGetDueActionTasksForEmployeeQuery
    {       
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public GetDueActionTasksForEmployeeQuery(IQueryableWrapper<Domain.Entities.Action> queryableActions)
        {     
        }
        public List<ActionTask> Execute(Guid employeeId, ISession session)
        {
            try
            {
                //Log.Debug(" GetDueActionTasksForEmployeeQuery - Execute()");

                var overdueActionTasks = session.Query<Domain.Entities.Action>().Where(x => x.Deleted == false)
                    .SelectMany(x => x.ActionTasks)
                    .Where(EmployeeIsTheAssignee(employeeId))
                    .Where(TasksAreDue());

                var actionTasks = new List<ActionTask>();
  
                overdueActionTasks.ForEach(
                    task =>
                    {
                        if (IsTaskDue(task))
                         actionTasks.Add(task);   
                    }); 
                                     
                 return actionTasks;
            }
            catch (Exception ex)
            {
                Log.Debug("Exception - GetDueActionTasksForEmployeeQuery - Execute()" + ex.Message);
                throw;
            }
        }
        
        private static Expression<Func<ActionTask, bool>> TasksAreDue()
        {
            return task => task.Deleted == false
                           && task.TaskStatus == TaskStatus.Outstanding
                           && task.TaskCompletedDate == null
                           && task.TaskCompletionDueDate != null
                           && task.TaskCompletionDueDate.Value > DateTime.Now;
        }

        private static Expression<Func<ActionTask, bool>> EmployeeIsTheAssignee(Guid employeeId)
        {
            return task => (task.TaskAssignedTo != null && task.TaskAssignedTo.Id == employeeId);
        }

        private static bool IsTaskDue(ActionTask task)
        {
            return (
               (task.TaskAssignedTo.NotificationType == NotificationType.Daily && task.TaskCompletionDueDate.HasValue && QueriesHelper.SimplifyDate(task.TaskCompletionDueDate.Value) == QueriesHelper.SimplifyDate(DateTime.Now.AddDays(1))) ||
               (task.TaskAssignedTo.NotificationType == NotificationType.Weekly && task.TaskCompletionDueDate.HasValue && task.TaskCompletionDueDate.Value < DateTime.Now.AddDays(7)) ||
               (task.TaskAssignedTo.NotificationType == NotificationType.Monthly && task.TaskCompletionDueDate.HasValue &&  task.TaskCompletionDueDate.Value < DateTime.Now.AddDays(30))
               );
        }
    }
}
