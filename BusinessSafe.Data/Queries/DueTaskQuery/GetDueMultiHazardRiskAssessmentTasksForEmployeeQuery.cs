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
    public interface IGetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<T> where T : MultiHazardRiskAssessment
    {
        List<MultiHazardRiskAssessmentFurtherControlMeasureTask> Execute(Guid employeeId, ISession session);
    }


    public class GetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<T> : IGetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<T> where T : MultiHazardRiskAssessment
    {      
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public GetDueMultiHazardRiskAssessmentTasksForEmployeeQuery(IQueryableWrapper<T> queryableTask)
        {
        }

       public List<MultiHazardRiskAssessmentFurtherControlMeasureTask> Execute(Guid employeeId, ISession session)
       {
           try
           {
               //Log.Debug("GetDueMultiHazardRiskAssessmentTasksForEmployeeQuery - Execute()");
               var queryTasksWhereEmployeeIsRiskAssessor = session.Query<T>()
                   .Where(
                       x =>
                           x.Deleted == false &&
                           (x.Status == RiskAssessmentStatus.Draft || x.Status == RiskAssessmentStatus.Live))
                   .SelectMany(x => x.Hazards)
                   .Where(x => x.Deleted == false)
                   .SelectMany(x => x.FurtherControlMeasureTasks)
                   .Where(TaskAreDue())
                   .Where(EmployeeIsTheAssignee(employeeId));

               var multiHazardRiskAssessmentFurtherControlMeasureTasks =
                   new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>();

               queryTasksWhereEmployeeIsRiskAssessor.ForEach(
                   task =>
                   {
                       if (IsTaskDue(task))
                           multiHazardRiskAssessmentFurtherControlMeasureTasks.Add(task);
                   });

               return multiHazardRiskAssessmentFurtherControlMeasureTasks;
           }
           catch (Exception ex)
           {
               Log.Debug("Exception - GetDueMultiHazardRiskAssessmentTasksForEmployeeQuery - Execute()" + ex.Message);
               throw;
           }
       }

       private static Expression<Func<MultiHazardRiskAssessmentFurtherControlMeasureTask, bool>> TaskAreDue()
       {
           return task => task.Deleted == false // dont return deleted tasks
                          && task.TaskStatus == TaskStatus.Outstanding
                          && task.TaskCompletionDueDate > DateTime.Now;
       }

       private static Expression<Func<MultiHazardRiskAssessmentFurtherControlMeasureTask, bool>> EmployeeIsTheAssignee(Guid employeeId)
       {
           return task => (task.TaskAssignedTo != null && task.TaskAssignedTo.Id == employeeId);
       }


       private static bool IsTaskDue(MultiHazardRiskAssessmentFurtherControlMeasureTask task)
       {
           return  (
                (task.TaskAssignedTo.NotificationType == NotificationType.Daily && task.TaskCompletionDueDate.HasValue &&
                QueriesHelper.SimplifyDate(task.TaskCompletionDueDate.Value) == QueriesHelper.SimplifyDate(DateTime.Now.AddDays(1)))
                || (task.TaskAssignedTo.NotificationType == NotificationType.Weekly && task.TaskCompletionDueDate.HasValue && task.TaskCompletionDueDate.Value < DateTime.Now.AddDays(7))
                || (task.TaskAssignedTo.NotificationType == NotificationType.Monthly && task.TaskCompletionDueDate.HasValue && task.TaskCompletionDueDate.Value < DateTime.Now.AddDays(30)));
       }
    }
}
