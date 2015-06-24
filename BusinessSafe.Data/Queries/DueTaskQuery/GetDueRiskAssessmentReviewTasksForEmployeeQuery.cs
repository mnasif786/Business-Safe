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
    public class GetDueRiskAssessmentReviewTasksForEmployeeQuery : IGetDueRiskAssessmentReviewTasksForEmployeeQuery
    {
         public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public GetDueRiskAssessmentReviewTasksForEmployeeQuery(IQueryableWrapper<RiskAssessment> queryableTask)
        {
        }

        public List<RiskAssessmentReviewTask> Execute(Guid employeeId, ISession session)
        {
            try
            {
                //Log.Debug("GetDueRiskAssessmentReviewTasksForEmployeeQuery - Execute()");
                var queryableRiskAssessmentsReviews = session.Query<RiskAssessment>()
                    .Where(
                        x =>
                            x.Deleted == false &&
                            (x.Status == RiskAssessmentStatus.Draft || x.Status == RiskAssessmentStatus.Live))
                    .SelectMany(x => x.Reviews)
                    .Where(ReviewTasksAreDue())
                    .Where(ReviewTaskEmployeeIsAssigneeeOrRiskAssessor(employeeId));

                var riskAssessmentReviewTasks = new List<RiskAssessmentReviewTask>();

                queryableRiskAssessmentsReviews.ForEach(
                    review =>
                    {
                        if (IsTaskDue(review))
                            riskAssessmentReviewTasks.Add(review.RiskAssessmentReviewTask);
                    });

                return riskAssessmentReviewTasks;
            }
            catch (Exception ex)
            {
                Log.Debug("Exception - GetDueRiskAssessmentReviewTasksForEmployeeQuery - Execute()" + ex.Message);
                throw;
            }
        }

        private static Expression<Func<RiskAssessmentReview, bool>> ReviewTasksAreDue()
        {
            return review => review.Deleted == false 
                        && review.RiskAssessmentReviewTask.TaskStatus == TaskStatus.Outstanding 
                        && review.RiskAssessmentReviewTask.TaskCompletionDueDate > DateTime.Now;
        }

        private static Expression<Func<RiskAssessmentReview, bool>> ReviewTaskEmployeeIsAssigneeeOrRiskAssessor(Guid employeeId)
        {
            return review => ( review.ReviewAssignedTo != null && review.ReviewAssignedTo.Id == employeeId )
                || (    review.RiskAssessmentReviewTask.RiskAssessmentReview.RiskAssessment.RiskAssessor != null &&
                        review.RiskAssessmentReviewTask.RiskAssessmentReview.RiskAssessment.RiskAssessor.Employee.Id == employeeId );
        }


        private static bool IsTaskDue(RiskAssessmentReview task)
        {
            return (
                       task.ReviewAssignedTo != null &&
                       task.RiskAssessmentReviewTask != null &&
                       task.RiskAssessmentReviewTask.TaskCompletionDueDate != null &&
                       (
                            (task.ReviewAssignedTo.NotificationType == NotificationType.Daily &&
                            QueriesHelper.SimplifyDate(task.RiskAssessmentReviewTask.TaskCompletionDueDate.Value) == QueriesHelper.SimplifyDate(DateTime.Now.AddDays(1)))
                            ||
                            (task.ReviewAssignedTo.NotificationType == NotificationType.Weekly &&
                            task.RiskAssessmentReviewTask.TaskCompletionDueDate.Value < DateTime.Now.AddDays(7))
                            ||
                            (task.ReviewAssignedTo.NotificationType == NotificationType.Monthly &&
                            task.RiskAssessmentReviewTask.TaskCompletionDueDate.Value < DateTime.Now.AddDays(30)))

                    ||

                        (task.RiskAssessmentReviewTask != null &&
                        task.RiskAssessmentReviewTask.TaskCompletionDueDate != null &&
                        task.RiskAssessmentReviewTask.RiskAssessmentReview != null &&
                        task.RiskAssessmentReviewTask.RiskAssessmentReview.RiskAssessment != null &&
                        task.RiskAssessmentReviewTask.RiskAssessmentReview.RiskAssessment.RiskAssessor != null &&
                        task.RiskAssessmentReviewTask.RiskAssessmentReview.RiskAssessment.RiskAssessor.Employee != null &&
                        (
                        (task.RiskAssessmentReviewTask.RiskAssessmentReview.RiskAssessment.RiskAssessor.Employee.NotificationType == NotificationType.Daily &&
                        QueriesHelper.SimplifyDate(task.RiskAssessmentReviewTask.TaskCompletionDueDate.Value) == QueriesHelper.SimplifyDate(DateTime.Now.AddDays(1)))
                        || 
                        (task.RiskAssessmentReviewTask.RiskAssessmentReview.RiskAssessment.RiskAssessor.Employee.NotificationType == NotificationType.Weekly
                                && task.RiskAssessmentReviewTask.TaskCompletionDueDate.Value < DateTime.Now.AddDays(7))
                        ||
                        (task.RiskAssessmentReviewTask.RiskAssessmentReview.RiskAssessment.RiskAssessor.Employee.NotificationType == NotificationType.Monthly
                            && task.RiskAssessmentReviewTask.TaskCompletionDueDate.Value < DateTime.Now.AddDays(30)))
                        )
                    );
        }
       }
}
