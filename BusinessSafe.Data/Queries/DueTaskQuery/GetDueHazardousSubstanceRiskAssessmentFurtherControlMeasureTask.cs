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
    public interface IGetDueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery
    {
        List<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask> Execute(Guid employeeId, ISession session);
    }
    public class GetDueHazardousSubstanceRiskAssessmentFurtherControlMeasureTask : IGetDueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery
    {      
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public GetDueHazardousSubstanceRiskAssessmentFurtherControlMeasureTask(IQueryableWrapper<HazardousSubstanceRiskAssessment> queryableTask)
        {     
        }

        public List<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask> Execute(Guid employeeId, ISession session)
        {
            try
            {
                //Log.Debug("GetDueHazardousSubstanceRiskAssessmentFurtherControlMeasureTask - Execute()");
                var queryTasksWhereEmployeeIsRiskAssessor = session.Query<HazardousSubstanceRiskAssessment>()
                    .Where(x => x.Deleted == false // dont return tasks for deleted risk assessments
                                && (x.Status == RiskAssessmentStatus.Draft || x.Status == RiskAssessmentStatus.Live))
                    .SelectMany(x => x.FurtherControlMeasureTasks)
                    .Where(x => x.Deleted == false)
                    .Where(TasksAreDue())
                    .Where(EmployeeIsTheAssignee(employeeId));

                var hazardousSubstanceRiskAssessmentFurtherControlMeasureTasks =
                    new List<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>();

                queryTasksWhereEmployeeIsRiskAssessor.ForEach(
                    task =>
                    {
                        if (IsTaskDue(task))
                            hazardousSubstanceRiskAssessmentFurtherControlMeasureTasks.Add(task);
                    });

                return hazardousSubstanceRiskAssessmentFurtherControlMeasureTasks;
            }
            catch (Exception ex)
            {
                Log.Debug("Exception - GetDueHazardousSubstanceRiskAssessmentFurtherControlMeasureTask - Execute()" + ex.Message);
                throw;
            }
        }
        
        private static Expression<Func<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask, bool>> TasksAreDue()
        {
            return task => task.Deleted == false // dont return deleted tasks
                           && task.TaskStatus == TaskStatus.Outstanding
                           && (task.TaskCompletionDueDate.Value < DateTime.Now.AddDays(1) && task.TaskCompletionDueDate.Value > DateTime.Now);
        }

        private static Expression<Func<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask, bool>> EmployeeIsTheAssignee(Guid employeeId)
        {
            return task => (task.TaskAssignedTo != null && task.TaskAssignedTo.Id == employeeId);
        }

        private static bool IsTaskDue(HazardousSubstanceRiskAssessmentFurtherControlMeasureTask task)
        {
            return (
                (task.TaskAssignedTo.NotificationType == NotificationType.Daily && task.TaskCompletionDueDate.HasValue && 
                QueriesHelper.SimplifyDate(task.TaskCompletionDueDate.Value) == QueriesHelper.SimplifyDate(DateTime.Now.AddDays(1))) ||
                (task.TaskAssignedTo.NotificationType == NotificationType.Weekly && task.TaskCompletionDueDate.HasValue && task.TaskCompletionDueDate.Value < DateTime.Now.AddDays(7)) ||
                (task.TaskAssignedTo.NotificationType == NotificationType.Monthly && task.TaskCompletionDueDate.HasValue && task.TaskCompletionDueDate.Value < DateTime.Now.AddDays(30)));
        }
    }
}
