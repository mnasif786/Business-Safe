using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;
using log4net;
using NHibernate;
using NHibernate.Linq;

namespace BusinessSafe.Data.Queries.OverdueTaskQuery
{
    public class GetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<T> : IGetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<T> where T : MultiHazardRiskAssessment
    {
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public GetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery(IQueryableWrapper<T> queryableTask)
        {
        }
        
        public List<MultiHazardRiskAssessmentFurtherControlMeasureTask> Execute(Guid employeeId, ISession session)
        {
            try
            {
                //Log.Debug(" GetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery - Execute()");
                var queryTasksWhereEmployeeIsRiskAssessor = session.Query<T>()
                    .Where(x => x.Deleted == false // dont return tasks for deleted risk assessments
                                && (x.Status == RiskAssessmentStatus.Draft || x.Status == RiskAssessmentStatus.Live))
                    .SelectMany(x => x.Hazards)
                    .Where(x => x.Deleted == false) // dont return tasks for deleted hazards
                    .SelectMany(x => x.FurtherControlMeasureTasks)
                    .Where(x => x.Deleted == false // dont return deleted tasks
                                && (x.TaskStatus == TaskStatus.Outstanding || x.TaskStatus == TaskStatus.Overdue)
                        //only return outstanding/overdue tasks
                                && x.TaskCompletedDate == null // ignore completed
                                && x.TaskCompletionDueDate != null // only include tasks with due date
                                && x.TaskCompletionDueDate < DateTime.Now.AddDays(-1))
                    .Where(x => (x.TaskAssignedTo != null && x.TaskAssignedTo.Id == employeeId)
                                ||
                                (x.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.RiskAssessor != null &&
                                 x.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.RiskAssessor.Employee.Id ==
                                 employeeId)
                    );

                var multiHazardRiskAssessmentFurtherControlMeasureTask =
                    new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>();

                queryTasksWhereEmployeeIsRiskAssessor.ForEach(
                    task =>
                    {
                        if (DoesTaskNeedToBeNotified(task, employeeId))
                            multiHazardRiskAssessmentFurtherControlMeasureTask.Add(task);
                    });

                return multiHazardRiskAssessmentFurtherControlMeasureTask;
            }
            catch (Exception ex)
            {
                Log.Debug("Exception - GetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery - Execute()" + ex.Message);
                throw;
            }
        }

        private static bool DoesTaskNeedToBeNotified(MultiHazardRiskAssessmentFurtherControlMeasureTask task, Guid employeeId)
        {
             var employee = (task.TaskAssignedTo != null && task.TaskAssignedTo.Id == employeeId) ? task.TaskAssignedTo
                            : (task.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.RiskAssessor.Employee != null && task.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.RiskAssessor.Employee.Id == employeeId)
                                ? task.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.RiskAssessor.Employee
                                : null;

            if (employee == null) return false;

            return (
                        (employee.NotificationType == NotificationType.Daily) ||
                        (employee.NotificationType == NotificationType.Weekly && task.TaskCompletionDueDate.HasValue &&
                        QueriesHelper.SimplifyDate(task.TaskCompletionDueDate.Value) >= QueriesHelper.SimplifyDate(DateTime.Now.AddDays(-7))) ||
                        (employee.NotificationType == NotificationType.Monthly && employee.NotificationFrequecy.HasValue && task.TaskCompletionDueDate.HasValue &&
                        QueriesHelper.SimplifyDate(task.TaskCompletionDueDate.Value) >= QueriesHelper.GetPreviousMonthsDate(employee.NotificationFrequecy.Value))
                    );
        }
    }
}
