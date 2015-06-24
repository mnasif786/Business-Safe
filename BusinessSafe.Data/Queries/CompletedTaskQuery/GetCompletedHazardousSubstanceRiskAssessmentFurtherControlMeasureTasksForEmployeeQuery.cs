using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Data.Queries.SafeCheck;
using log4net;
using NHibernate;
using NHibernate.Linq;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Queries.CompletedTaskQuery
{
    public class GetCompletedHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery 
        : IGetCompletedHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery
    {
        private readonly IBusinessSafeSessionManager _sessionManager;
     //   private readonly IQueryable<HazardousSubstanceRiskAssessment> _queryableRiskAssessments;
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public GetCompletedHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(IBusinessSafeSessionManager sessionManager)
        {
            //_queryableRiskAssessments = queryableTask.Queryable();
            _sessionManager = sessionManager;
        }
        
        public List<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask> Execute(Guid employeeId, ISession session)
        {
            try
            {
                //Log.Debug("Exception - GetCompletedHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery");


                var queryTasksWhereEmployeeIsRiskAssessor = session.Query<HazardousSubstanceRiskAssessment>()
                    .Where(
                        x =>
                            x.Deleted == false &&
                            (x.Status == RiskAssessmentStatus.Draft || x.Status == RiskAssessmentStatus.Live))
                    .SelectMany(x => x.FurtherControlMeasureTasks)
                    .Where(TasksAreCompleted())
                    .Where(EmployeeIsRiskAssessor(employeeId));

                var hsFurtherControlMesureTasks = new List<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>();

                queryTasksWhereEmployeeIsRiskAssessor.ForEach(
                    task =>
                    {
                        if (DoesTaskNeedToBeNotified(task))
                            hsFurtherControlMesureTasks.Add(task);

                    }
                    );

                return hsFurtherControlMesureTasks;
            }
            catch (Exception ex)
            {
                Log.Debug("Exception - GetCompletedHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery - Execute()" + ex.Message);
                throw;
            }
        }

        private static Expression<Func<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask, bool>> TasksAreCompleted()
        {
          return task => task.Deleted == false 
                        && task.TaskStatus == TaskStatus.Completed
                        && task.TaskCompletedDate != null;
        }

        private static Expression<Func<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask, bool>> EmployeeIsRiskAssessor(Guid employeeId)
        {
            return task => task.HazardousSubstanceRiskAssessment.RiskAssessor != null
                            && task.HazardousSubstanceRiskAssessment.RiskAssessor.Employee.Id == employeeId;

        }
        
        private static bool DoesTaskNeedToBeNotified(HazardousSubstanceRiskAssessmentFurtherControlMeasureTask task)
        {
            var notificationType = task.HazardousSubstanceRiskAssessment.RiskAssessor.Employee.NotificationType;
            var notificationFrequency = task.HazardousSubstanceRiskAssessment.RiskAssessor.Employee.NotificationFrequecy;


            return (notificationType == NotificationType.Daily) ||
                   (notificationType == NotificationType.Weekly && task.TaskCompletedDate.HasValue && task.TaskCompletedDate.Value > DateTime.Now.AddDays(-7))
                   || (notificationType == NotificationType.Monthly && notificationFrequency.HasValue && task.TaskCompletedDate.HasValue &&
                       task.TaskCompletedDate.Value > QueriesHelper.GetPreviousMonthsDate(notificationFrequency.Value));

        }
    }
}
