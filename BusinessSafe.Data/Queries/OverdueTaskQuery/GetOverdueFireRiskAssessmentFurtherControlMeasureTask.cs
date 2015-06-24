using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;
using log4net;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Util;

namespace BusinessSafe.Data.Queries.OverdueTaskQuery
{
    public interface IGetOverdueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery
    {
        List<FireRiskAssessmentFurtherControlMeasureTask> Execute(Guid employeeId, ISession session);
    }

    public class GetOverdueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery : IGetOverdueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery
    {         
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public GetOverdueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(IQueryableWrapper<FireRiskAssessment> queryableTask)
        {     
        }
        
        public List<FireRiskAssessmentFurtherControlMeasureTask> Execute(Guid employeeId, ISession session)
        {
            try
            {
                //Log.Debug(" GetOverdueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery - Execute()");

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
                    .Where(TaskIsAssignedToEmployeeOrEmployeeIsTheRiskAssessor(employeeId))
                    .Where(TasksAreOverdue());

                var fireRiskAssessmentFurtherControlMeasureTask =
                    new List<FireRiskAssessmentFurtherControlMeasureTask>();

                queryTasksWhereEmployeeIsRiskAssessor.ForEach(
                    task =>
                    {
                        if (DoesTaskNeedToBeNotified(task, employeeId))
                            fireRiskAssessmentFurtherControlMeasureTask.Add(task);
                    });


                return fireRiskAssessmentFurtherControlMeasureTask;
            }
            catch (Exception ex)
            {
                Log.Debug("Exception - GetOverdueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery - Execute()" + ex.Message);
                throw;
            }
        }

        private static Expression<Func<FireRiskAssessmentFurtherControlMeasureTask, bool>> TaskIsAssignedToEmployeeOrEmployeeIsTheRiskAssessor(Guid employeeId)
        {
            return task => (task.TaskAssignedTo != null && task.TaskAssignedTo.Id == employeeId)
                        || (task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessor != null
                            && task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessor.Employee.Id == employeeId);
        }

        private static Expression<Func<FireRiskAssessmentFurtherControlMeasureTask, bool>> TasksAreOverdue()
        {
            return task => task.Deleted == false // dont return deleted tasks
                           && (task.TaskStatus == TaskStatus.Outstanding || task.TaskStatus == TaskStatus.Overdue)//only return outstanding/overdue tasks
                           && task.TaskCompletedDate == null // ignore completed
                           && task.TaskCompletionDueDate != null // only include tasks with due date
                           && task.TaskCompletionDueDate < DateTime.Today.AddDays(-1);
        }


        private static bool DoesTaskNeedToBeNotified(FireRiskAssessmentFurtherControlMeasureTask task, Guid employeeId)
        {
            // Risk Assessor
            if (task.SignificantFinding != null &&
                task.SignificantFinding.FireAnswer != null &&
                task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist != null &&
                task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment != null &&
                task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessor != null &&
                task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessor.Employee != null &&
                task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessor.Employee.Id == employeeId)
            {
                var notificationType = task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessor.Employee.NotificationType;
                var notificationFrequency = task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessor.Employee.NotificationFrequecy;

                return (
                            (notificationType == NotificationType.Daily) ||
                            (notificationType == NotificationType.Weekly && task.TaskCompletionDueDate.HasValue && 
                            QueriesHelper.SimplifyDate(task.TaskCompletionDueDate.Value) >=  QueriesHelper.SimplifyDate(DateTime.Now.AddDays(-7))) ||
                            (notificationType == NotificationType.Monthly && notificationFrequency.HasValue && task.TaskCompletionDueDate.HasValue && 
                            QueriesHelper.SimplifyDate(task.TaskCompletionDueDate.Value) >= QueriesHelper.GetPreviousMonthsDate(notificationFrequency.Value))
                        );
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
