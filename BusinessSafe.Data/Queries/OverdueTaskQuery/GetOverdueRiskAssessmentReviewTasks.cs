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

namespace BusinessSafe.Data.Queries.OverdueTaskQuery
{

    public interface IGetOverdueRiskAssessmentReviewTasksForEmployeeQuery
    {
        List<RiskAssessmentReviewTask> Execute(Guid employeeId, ISession session);
    }
    
    public class GetOverdueRiskAssessmentReviewTasks : IGetOverdueRiskAssessmentReviewTasksForEmployeeQuery
    {
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public GetOverdueRiskAssessmentReviewTasks(IQueryableWrapper<RiskAssessment> queryableTask)
        {
        }

        public List<RiskAssessmentReviewTask> Execute(Guid employeeId, ISession session)
        {
            try
            {
                //Log.Debug("GetOverdueRiskAssessmentReviewTasks - Execute()");
                var queryableRiskAssessmentsReviews = session.Query<RiskAssessment>()
                    .Where(
                        x =>
                            x.Deleted == false &&
                            (x.Status == RiskAssessmentStatus.Draft || x.Status == RiskAssessmentStatus.Live))
                    .SelectMany(x => x.Reviews)
                    .Where(ReviewTaskIsAssignedToEmployeeOrEmployeeIsTheRiskAssessor(employeeId))
                    .Where(ReviewTasksAreOverdue());

                var riskAssessmentReviewTasks = new List<RiskAssessmentReviewTask>();

                queryableRiskAssessmentsReviews.ForEach(
                    task =>
                    {
                        if (DoesTaskNeedToBeNotified(task, employeeId))
                            riskAssessmentReviewTasks.Add(task.RiskAssessmentReviewTask);
                    });

                return riskAssessmentReviewTasks;
            }
            catch (Exception ex)
            {
                Log.Debug("Exception - GetOverdueRiskAssessmentReviewTasks - Execute()" + ex.Message);
                throw;
            }
        }


        private static Expression<Func<RiskAssessmentReview, bool>> ReviewTasksAreOverdue()
        {
            return review => review.Deleted == false // dont return deleted tasks
                        && (review.RiskAssessmentReviewTask.TaskStatus == TaskStatus.Outstanding || review.RiskAssessmentReviewTask.TaskStatus == TaskStatus.Overdue) //only return outstanding/overdue tasks
                        && review.RiskAssessmentReviewTask.TaskCompletedDate == null // ignore completed
                        && review.RiskAssessmentReviewTask.TaskCompletionDueDate != null // only include tasks with due date
                        && review.RiskAssessmentReviewTask.TaskCompletionDueDate.Value < DateTime.Today.AddDays(-1);
        }
        
        private static Expression<Func<RiskAssessmentReview, bool>> ReviewTaskIsAssignedToEmployeeOrEmployeeIsTheRiskAssessor(Guid employeeId)
        {
            return review => (review.RiskAssessmentReviewTask.TaskAssignedTo != null && review.RiskAssessmentReviewTask.TaskAssignedTo.Id == employeeId)
                || (review.RiskAssessmentReviewTask.RiskAssessmentReview.RiskAssessment.RiskAssessor != null && 
                    review.RiskAssessmentReviewTask.RiskAssessmentReview.RiskAssessment.RiskAssessor.Employee.Id == employeeId);
        }


        private static bool DoesTaskNeedToBeNotified(RiskAssessmentReview task, Guid employeeId)
        {
            if (task.RiskAssessmentReviewTask == null) return false;

            // Risk Assessor
            if (task.RiskAssessment != null &&
                task.RiskAssessment.RiskAssessor != null &&
                task.RiskAssessment.RiskAssessor.Employee != null &&
                task.RiskAssessment.RiskAssessor.Employee.Id == employeeId)
            {
                var notificationType = task.RiskAssessment.RiskAssessor.Employee.NotificationType;
                var notificationFrequency =
                    task.RiskAssessment.RiskAssessor.Employee.NotificationFrequecy;

                return (
                            (notificationType == NotificationType.Daily ||
                            (notificationType == NotificationType.Weekly && task.RiskAssessmentReviewTask.TaskCompletionDueDate.HasValue &&
                            QueriesHelper.SimplifyDate(task.RiskAssessmentReviewTask.TaskCompletionDueDate.Value) >= QueriesHelper.SimplifyDate(DateTime.Today.AddDays(-7))) ||
                            (notificationType == NotificationType.Monthly && notificationFrequency.HasValue && task.RiskAssessmentReviewTask.TaskCompletionDueDate.HasValue &&
                            QueriesHelper.SimplifyDate(task.RiskAssessmentReviewTask.TaskCompletionDueDate.Value) >= QueriesHelper.GetPreviousMonthsDate(notificationFrequency.Value)))
                       );
            }
            else if (task.RiskAssessmentReviewTask.TaskAssignedTo != null && task.RiskAssessmentReviewTask.TaskAssignedTo.Id == employeeId)
            {
                var notificationType = task.RiskAssessmentReviewTask.TaskAssignedTo.NotificationType;
                var notificationFrequency = task.RiskAssessmentReviewTask.TaskAssignedTo.NotificationFrequecy;

                return (
                            (notificationType == NotificationType.Daily) ||
                            (notificationType == NotificationType.Weekly && task.RiskAssessmentReviewTask.TaskCompletionDueDate.HasValue &&
                            QueriesHelper.SimplifyDate(task.RiskAssessmentReviewTask.TaskCompletionDueDate.Value) >= QueriesHelper.SimplifyDate(DateTime.Today.AddDays(-7))) 
                       ||
                            (notificationType == NotificationType.Monthly && notificationFrequency.HasValue && task.RiskAssessmentReviewTask.TaskCompletionDueDate.HasValue &&
                            QueriesHelper.SimplifyDate(task.RiskAssessmentReviewTask.TaskCompletionDueDate.Value) >= QueriesHelper.GetPreviousMonthsDate(notificationFrequency.Value))
                       );
            }
            else
            {
                return false;
            }
        }
    }
}
