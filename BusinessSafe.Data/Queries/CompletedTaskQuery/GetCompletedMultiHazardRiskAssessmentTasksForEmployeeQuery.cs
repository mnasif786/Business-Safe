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
    public class GetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery<T> : IGetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery<T> where T : MultiHazardRiskAssessment
    {
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public GetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery(IQueryableWrapper<T> queryableTask)
        {
       
        }

        public void Dispose() { }

        public List<MultiHazardRiskAssessmentFurtherControlMeasureTask> Execute(Guid employeeId, ISession session)
        {
            try
            {
               // Log.Debug("GetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery - Execute()");

                var queryTasksWhereEmployeeIsRiskAssessor = session.Query<T>()
                    .Where(
                        x =>
                            x.Deleted == false &&
                            (x.Status == RiskAssessmentStatus.Draft || x.Status == RiskAssessmentStatus.Live))
                    .SelectMany(x => x.Hazards)
                    .Where(x => x.Deleted == false)
                    .SelectMany(x => x.FurtherControlMeasureTasks)
                    .Where(
                        x => x.Deleted == false && x.TaskStatus == TaskStatus.Completed && x.TaskCompletedDate != null)
                    .Where(EmployeeIsRiskAssessor(employeeId));

                var multiHazardRiskAssessmentFurtherControlMeasureTask =
                    new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>();

                queryTasksWhereEmployeeIsRiskAssessor.ForEach(
                    task =>
                    {
                        if (DoesTaskNeedToBeNotified(task))
                            multiHazardRiskAssessmentFurtherControlMeasureTask.Add(task);

                    }
                    );

                return multiHazardRiskAssessmentFurtherControlMeasureTask;
            }
            catch (Exception ex)
            {
                Log.Debug("Exception - GetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery - Execute()" + ex.Message);
                throw;
            }
        }


        private static Expression<Func<MultiHazardRiskAssessmentFurtherControlMeasureTask, bool>> EmployeeIsRiskAssessor(Guid employeeId)
        {       
            return task => task.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.RiskAssessor != null
                           &&
                           task.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.RiskAssessor.Employee.Id == employeeId;
        }
        
        private static bool DoesTaskNeedToBeNotified(MultiHazardRiskAssessmentFurtherControlMeasureTask task)
        {         
            var notificationType = task.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.RiskAssessor.Employee.NotificationType;
            var notificationFrequency = task.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.RiskAssessor.Employee.NotificationFrequecy;


            return (notificationType == NotificationType.Daily) ||
                   (notificationType == NotificationType.Weekly && task.TaskCompletedDate.HasValue && task.TaskCompletedDate.Value > DateTime.Now.AddDays(-7))
                   || (notificationType == NotificationType.Monthly && notificationFrequency.HasValue && task.TaskCompletedDate.HasValue &&
                       task.TaskCompletedDate.Value > QueriesHelper.GetPreviousMonthsDate(notificationFrequency.Value));
        }
    }
}
   
