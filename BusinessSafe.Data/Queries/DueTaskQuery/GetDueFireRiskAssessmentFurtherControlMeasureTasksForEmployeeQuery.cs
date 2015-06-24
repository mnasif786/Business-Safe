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

namespace BusinessSafe.Data.Queries.DueTaskQuery
{
    public interface IGetDueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery
    {
        List<FireRiskAssessmentFurtherControlMeasureTask> Execute(Guid employeeId, ISession session);
    }

    public class GetDueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery : IGetDueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery
    {       
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public GetDueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(IQueryableWrapper<FireRiskAssessment> queryableRiskAssessments)
        {           
        }

        public List<FireRiskAssessmentFurtherControlMeasureTask> Execute(Guid employeeId, ISession session)
        {
            try
            {
                //Log.Debug("GetDueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery - Execute()");

                var queryTasksWhereEmployeeIsRiskAssessor = session.Query<FireRiskAssessment>()
                    .Where(x => x.Deleted == false // dont return tasks for deleted risk assessments
                                && (x.Status == RiskAssessmentStatus.Draft || x.Status == RiskAssessmentStatus.Live))
                    .SelectMany(x => x.FireRiskAssessmentChecklists)
                    .SelectMany(x => x.Answers)
                    .Where(x => x.Deleted == false) // dont return deleted answers
                    .Where(x => x.SignificantFinding != null && x.SignificantFinding.Deleted == false)
                    // dont return tasks for deleted significant findings
                    .SelectMany(x => x.SignificantFinding.FurtherControlMeasureTasks)
                    .Where(x => x.Deleted == false)
                    .Where(TasksDue())
                    .Where(EmployeeIsTheAssignee(employeeId));

                var fireRiskAssessmentFurtherControlMeasureTasks =
                    new List<FireRiskAssessmentFurtherControlMeasureTask>();

                queryTasksWhereEmployeeIsRiskAssessor.ForEach(
                    task =>
                    {
                        if (IsTaskDue(task))
                            fireRiskAssessmentFurtherControlMeasureTasks.Add(task);
                    });

                return fireRiskAssessmentFurtherControlMeasureTasks;
            }
            catch (Exception ex)
            {
                Log.Debug("Exception - GetDueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery - Execute()" + ex.Message);
                throw;
            }
        }

        private static Expression<Func<FireRiskAssessmentFurtherControlMeasureTask, bool>> TasksDue()
        {
            return task => task.Deleted == false // dont return deleted tasks
                           && task.TaskStatus == TaskStatus.Outstanding
                            && task.TaskCompletionDueDate.Value > DateTime.Now;
        }
        
        private static Expression<Func<FireRiskAssessmentFurtherControlMeasureTask, bool>> EmployeeIsTheAssignee(Guid employeeId)
        {
            return  task => (task.TaskAssignedTo != null && task.TaskAssignedTo.Id == employeeId);
        }

        private static bool IsTaskDue(FireRiskAssessmentFurtherControlMeasureTask task)
        {
            return (
               (task.TaskAssignedTo.NotificationType == NotificationType.Daily && task.TaskCompletionDueDate.HasValue && QueriesHelper.SimplifyDate(task.TaskCompletionDueDate.Value) == QueriesHelper.SimplifyDate(DateTime.Now.AddDays(1))) ||
               (task.TaskAssignedTo.NotificationType == NotificationType.Weekly && task.TaskCompletionDueDate.HasValue && task.TaskCompletionDueDate.Value < DateTime.Now.AddDays(7)) ||
               (task.TaskAssignedTo.NotificationType == NotificationType.Monthly && task.TaskCompletionDueDate.HasValue && task.TaskCompletionDueDate.Value < DateTime.Now.AddDays(30))
               );
        }
    }
}
