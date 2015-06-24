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
using Remotion.Linq.Parsing;

namespace BusinessSafe.Data.Queries.OverdueTaskQuery
{
    public interface IGetOverdueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery
    {
        List<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask> Execute(Guid employeeId, ISession session);
    }

    public class GetOverdueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery : IGetOverdueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery
    {
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public GetOverdueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(
            IQueryableWrapper<HazardousSubstanceRiskAssessment> queryableTask)
        {
        }

     public List<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>  Execute(Guid employeeId, ISession session)
        {
            try
            {
                //Log.Debug("GetOverdueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery - Execute()");
                var queryTasksWhereEmployeeIsRiskAssessor = session.Query<HazardousSubstanceRiskAssessment>()
                    .Where(x => x.Deleted == false // dont return tasks for deleted risk assessments
                                && (x.Status == RiskAssessmentStatus.Draft || x.Status == RiskAssessmentStatus.Live))
                    .SelectMany(x => x.FurtherControlMeasureTasks)
                    .Where(x => x.Deleted == false)
                    .Where(TaskIsAssignedToEmployeeOrEmployeeIsTheRiskAssessor(employeeId))
                    .Where(TasksAreOverdue());

                var hazardousSubstanceRiskAssessmentFurtherControlMeasureTasks =
                    new List<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>();

                queryTasksWhereEmployeeIsRiskAssessor.ForEach(
                    task =>
                    {
                        if (DoesTaskNeedToBeNotified(task, employeeId))
                            hazardousSubstanceRiskAssessmentFurtherControlMeasureTasks.Add(task);
                    });

                return hazardousSubstanceRiskAssessmentFurtherControlMeasureTasks.ToList();
            }
            catch (Exception ex)
            {
                Log.Debug("Exception - GetOverdueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery - Execute()" + ex.Message);
                throw;
            }
        }


        private static Expression<Func<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask, bool>> TasksAreOverdue()
        {
            return task => task.Deleted == false // dont return deleted tasks
                        && (task.TaskStatus == TaskStatus.Outstanding || task.TaskStatus == TaskStatus.Overdue) //only return outstanding/overdue tasks
                        && task.TaskCompletedDate == null // ignore completed
                        && task.TaskCompletionDueDate != null // only include tasks with due date
                        && task.TaskCompletionDueDate < DateTime.Today.AddDays(-1);
        }


        private static Expression<Func<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask, bool>> TaskIsAssignedToEmployeeOrEmployeeIsTheRiskAssessor(Guid employeeId)
        {
            return task => (task.TaskAssignedTo != null && task.TaskAssignedTo.Id == employeeId)
                || (task.HazardousSubstanceRiskAssessment.RiskAssessor != null && task.HazardousSubstanceRiskAssessment.RiskAssessor.Employee.Id == employeeId);
        }

        private static bool DoesTaskNeedToBeNotified(HazardousSubstanceRiskAssessmentFurtherControlMeasureTask task, Guid employeeId)
        {
            // Risk Assessor
            if (task.HazardousSubstanceRiskAssessment != null &&
                task.HazardousSubstanceRiskAssessment.RiskAssessor != null &&
                task.HazardousSubstanceRiskAssessment.RiskAssessor.Employee.Id == employeeId)
            {
                var notificationType = task.HazardousSubstanceRiskAssessment.RiskAssessor.Employee.NotificationType;
                var notificationFrequency = task.HazardousSubstanceRiskAssessment.RiskAssessor.Employee.NotificationFrequecy;
                
                return (task.HazardousSubstanceRiskAssessment.RiskAssessor != null && task.HazardousSubstanceRiskAssessment.RiskAssessor.Employee.Id == employeeId &&
                        ( 
                            (notificationType == NotificationType.Daily) ||
                            (notificationType == NotificationType.Weekly && task.TaskCompletionDueDate.HasValue && 
                            QueriesHelper.SimplifyDate(task.TaskCompletionDueDate.Value) >= QueriesHelper.SimplifyDate(DateTime.Now.AddDays(-7))) ||
                            (notificationType == NotificationType.Monthly && notificationFrequency.HasValue && task.TaskCompletionDueDate.HasValue && 
                            QueriesHelper.SimplifyDate(task.TaskCompletionDueDate.Value) >= QueriesHelper.GetPreviousMonthsDate(notificationFrequency.Value))
                        ));
            }
            else if (task.TaskAssignedTo != null && task.TaskAssignedTo.Id == employeeId)
            {
                var notificationType = task.TaskAssignedTo.NotificationType;
                var notificationFrequency = task.TaskAssignedTo.NotificationFrequecy;

                return ( 
                            (notificationType == NotificationType.Daily) ||
                            (notificationType == NotificationType.Weekly && task.TaskCompletionDueDate.HasValue &&
                            QueriesHelper.SimplifyDate(task.TaskCompletionDueDate.Value) >= QueriesHelper.SimplifyDate(DateTime.Now.AddDays(-7))) ||
                            (notificationType == NotificationType.Monthly && notificationFrequency.HasValue && task.TaskCompletionDueDate.HasValue && 
                            QueriesHelper.SimplifyDate(task.TaskCompletionDueDate.Value) >= QueriesHelper.GetPreviousMonthsDate(notificationFrequency.Value))
                      );
            }
            else
            {
                return false;
            }
        }
    }
}
