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

namespace BusinessSafe.Data.Queries.CompletedTaskQuery
{
    public class GetCompletedRiskAssessmentReviewTasksForEmployeeQuery : IGetCompletedRiskAssessmentReviewTasksForEmployeeQuery
    {
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        public GetCompletedRiskAssessmentReviewTasksForEmployeeQuery(IQueryableWrapper<RiskAssessment> queryableTask)
        {      
        }
        
        public List<RiskAssessmentReviewTask> Execute(Guid employeeId, ISession session)
        {
            try
            {
                //Log.Debug("GetCompletedRiskAssessmentReviewTasksForEmployeeQuery - Execute()");
                var queryableRiskAssessmentsReviews = session.Query<RiskAssessment>()
                    .Where(
                        x =>
                            x.Deleted == false &&
                            (x.Status == RiskAssessmentStatus.Draft || x.Status == RiskAssessmentStatus.Live))
                    .SelectMany(x => x.Reviews)
                    .Where(ReviewTasksAreCompleted())
                    .Where(EmployeeIsRiskAssessor(employeeId));

                var riskAssessmentReviewTasks = new List<RiskAssessmentReviewTask>();

                queryableRiskAssessmentsReviews.ForEach(
                    review =>
                    {
                        if (DoesTaskNeedToBeNotified(review.RiskAssessmentReviewTask))
                            riskAssessmentReviewTasks.Add(review.RiskAssessmentReviewTask);
                    });

                return riskAssessmentReviewTasks;
            }
            catch (Exception ex)
            {
                Log.Debug("Exception - GetCompletedRiskAssessmentReviewTasksForEmployeeQuery - Execute()" + ex.Message);
                throw;
            }
        }


        private static Expression<Func<RiskAssessmentReview, bool>> ReviewTasksAreCompleted()
        {            
            return review => review.Deleted == false
                        && (review.RiskAssessmentReviewTask.TaskStatus == TaskStatus.Completed && review.RiskAssessmentReviewTask.TaskCompletedDate != null);
        }

        private static Expression<Func<RiskAssessmentReview, bool>> EmployeeIsRiskAssessor(Guid employeeId)
        {                        
            return review => review.RiskAssessmentReviewTask.RiskAssessmentReview.RiskAssessment.RiskAssessor != null
                            && review.RiskAssessmentReviewTask.RiskAssessmentReview.RiskAssessment.RiskAssessor.Employee.Id == employeeId;

        }

        private static bool DoesTaskNeedToBeNotified(RiskAssessmentReviewTask task)
        {            
            var notificationType = task.RiskAssessmentReview.RiskAssessment.RiskAssessor.Employee.NotificationType;
            var notificationFrequency = task.RiskAssessmentReview.RiskAssessment.RiskAssessor.Employee.NotificationFrequecy;


            return (notificationType == NotificationType.Daily) ||
                   (notificationType == NotificationType.Weekly && task.TaskCompletedDate.HasValue && task.TaskCompletedDate.Value > DateTime.Now.AddDays(-7))
                   || (notificationType == NotificationType.Monthly && notificationFrequency.HasValue && task.TaskCompletedDate.HasValue &&
                       task.TaskCompletedDate.Value > QueriesHelper.GetPreviousMonthsDate(notificationFrequency.Value));

        }
    }
}
