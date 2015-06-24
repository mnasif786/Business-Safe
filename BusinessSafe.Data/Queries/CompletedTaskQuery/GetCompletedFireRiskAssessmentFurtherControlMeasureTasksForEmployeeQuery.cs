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

namespace BusinessSafe.Data.Queries.CompletedTaskQuery
{
    
    public interface IGetCompletedFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery
    {
        List<FireRiskAssessmentFurtherControlMeasureTask> Execute(Guid employeeId, ISession session);
    }

    public class GetCompletedFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery : IGetCompletedFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery
    {
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public GetCompletedFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(IQueryableWrapper<FireRiskAssessment> queryableRiskAssessments)
        {
        }

        public void Dispose() { }

        public List<FireRiskAssessmentFurtherControlMeasureTask> Execute(Guid employeeId, ISession session)
        {
            try
            {
                //Log.Debug("GetCompletedFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery - Execute");

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
                    .Where(TasksAreCompleted())
                    .Where(EmployeeIsRiskAssessor(employeeId));

                var fireRiskAssessmentFurtherControlMeasureTask =
                    new List<FireRiskAssessmentFurtherControlMeasureTask>();

                queryTasksWhereEmployeeIsRiskAssessor.ForEach(
                    task =>
                    {
                        if (DoesTaskNeedToBeNotified(task))
                            fireRiskAssessmentFurtherControlMeasureTask.Add(task);

                    }
                    );

                return fireRiskAssessmentFurtherControlMeasureTask;
            }
            catch (Exception ex)
            {
                Log.Debug("Exception - GetCompletedFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery - Execute()" + ex.Message);
                throw;
            }
        }

        private static Expression<Func<FireRiskAssessmentFurtherControlMeasureTask, bool>> EmployeeIsRiskAssessor(Guid employeeId)
        {
            return
                task =>
                    task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessor != null && 
                        task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessor.Employee.Id == employeeId;
        }

        private static Expression<Func<FireRiskAssessmentFurtherControlMeasureTask, bool>> TasksAreCompleted()
        {
            return task => task.Deleted == false // dont return deleted tasks
                           && (task.TaskStatus == TaskStatus.Completed) //only return completed tasks
                           && task.TaskCompletedDate != null;
        }

        private static bool DoesTaskNeedToBeNotified(FireRiskAssessmentFurtherControlMeasureTask task)
        {
            var notificationType = task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessor.Employee.NotificationType;
            var notificationFrequency = task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessor.Employee.NotificationFrequecy;

            return (notificationType == NotificationType.Daily) ||
                   (notificationType == NotificationType.Weekly && task.TaskCompletedDate.HasValue &&  task.TaskCompletedDate.Value > DateTime.Now.AddDays(-7))
                   || (notificationType == NotificationType.Monthly && notificationFrequency.HasValue && task.TaskCompletedDate.HasValue &&
                       task.TaskCompletedDate.Value > QueriesHelper.GetPreviousMonthsDate(notificationFrequency.Value));

        }
        
    }
}
