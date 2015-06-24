using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Implementations.Employees;
using BusinessSafe.Domain.Entities;
using NHibernate;

namespace BusinessSafe.Application.Contracts.Employee
{
	public interface IEmployeeNotificationsService
	{
		/// <summary>
		/// Returns a list of employees who need to be notified on the given notification date 
		/// </summary>
		/// <param name="notificationDateTime"></param>
		/// <returns></returns>
		List<Domain.Entities.Employee> GetEmployeesToBeNotified(ISession session, DateTime notificationDateTime);

		void CreateAndSendEmployeeEmailDigest(ISession session, Domain.Entities.Employee employee);
	}

	public class EmployeeTasks
	{
        //Overdue Tasks
		public List<MultiHazardRiskAssessmentFurtherControlMeasureTask> GeneralRiskAssessmentTasksOverdue { get; set; }
		public List<MultiHazardRiskAssessmentFurtherControlMeasureTask> PersonalRiskAssessmentTasksOverdue { get; set; }
		public List<FireRiskAssessmentFurtherControlMeasureTask> FireRiskAssessmentTasksOverdue { get; set; }
		public List<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask> HazadousSubstanceRiskAssessmentTasksOverdue { get; set; }
		public List<RiskAssessmentReviewTask> RiskAssessmentReviewTasksOverdue { get; set; }
		public List<ResponsibilityTask> ResponsibilityTaskOverdue { get; set; }
		public List<ActionTask> ActionTasksOverdue { get; set; }
		
        //Completed Tasks
        public List<MultiHazardRiskAssessmentFurtherControlMeasureTask> GeneralRiskAssessmentTasksCompleted { get; set; }
        public List<MultiHazardRiskAssessmentFurtherControlMeasureTask> PersonalRiskAssessmentTasksCompleted { get; set; }
        public List<FireRiskAssessmentFurtherControlMeasureTask> FireRiskAssessmentTasksCompleted { get; set; }
        public List<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask> HazardousSubstanceTasksCompleted { get; set; }
        public List<RiskAssessmentReviewTask> RiskAssessmentReviewTasksCompleted { get; set; }

        //DueTomorrow Tasks
        public List<MultiHazardRiskAssessmentFurtherControlMeasureTask> GeneralRiskAssessmentTasksDueTomorrow { get; set; }
        public List<MultiHazardRiskAssessmentFurtherControlMeasureTask> PersonalRiskAssessmentTasksDueTomorrow { get; set; }
        public List<FireRiskAssessmentFurtherControlMeasureTask> FireRiskAssessmentTasksDueTomorrow { get; set; }
        public List<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask> HazardousSubstanceTasksDueTomorrow { get; set; }
        public List<RiskAssessmentReviewTask> RiskAssessmentReviewTasksDueTomorrow { get; set; }
        public List<ResponsibilityTask> ResponsibilityTasksDueTomorrow { get; set; }
        public List<ActionTask> ActionTasksDueTomorrow { get; set; }

	    public EmployeeTasks()
		{
            //Overdue Tasks
			GeneralRiskAssessmentTasksOverdue = new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>();
			PersonalRiskAssessmentTasksOverdue = new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>();
			FireRiskAssessmentTasksOverdue = new List<FireRiskAssessmentFurtherControlMeasureTask>();
			HazadousSubstanceRiskAssessmentTasksOverdue = new List<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>();
			RiskAssessmentReviewTasksOverdue = new List<RiskAssessmentReviewTask>();
			ResponsibilityTaskOverdue = new List<ResponsibilityTask>();
			ActionTasksOverdue = new List<ActionTask>();

            //Completed Tasks
            GeneralRiskAssessmentTasksCompleted = new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>();
            PersonalRiskAssessmentTasksCompleted = new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>();
            HazardousSubstanceTasksCompleted = new List<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>();
            FireRiskAssessmentTasksCompleted = new List<FireRiskAssessmentFurtherControlMeasureTask>();
            RiskAssessmentReviewTasksCompleted = new List<RiskAssessmentReviewTask>();

            //DueTomorrow Tasks
            GeneralRiskAssessmentTasksDueTomorrow = new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>();
            PersonalRiskAssessmentTasksDueTomorrow= new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>();
            FireRiskAssessmentTasksDueTomorrow = new List<FireRiskAssessmentFurtherControlMeasureTask>();
            HazardousSubstanceTasksDueTomorrow = new List<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>();
            RiskAssessmentReviewTasksDueTomorrow = new List<RiskAssessmentReviewTask>();
            ResponsibilityTasksDueTomorrow = new List<ResponsibilityTask>();
            ActionTasksDueTomorrow = new List<ActionTask>();

		}

	    public void ClearEmployeeTasks()
	    {
            GeneralRiskAssessmentTasksOverdue = null;
            PersonalRiskAssessmentTasksOverdue = null;
            FireRiskAssessmentTasksOverdue = null;
            HazadousSubstanceRiskAssessmentTasksOverdue = null;
            RiskAssessmentReviewTasksOverdue = null;
            ResponsibilityTaskOverdue = null;
            ActionTasksOverdue = null;

            //Completed Tasks
            GeneralRiskAssessmentTasksCompleted = null;
            PersonalRiskAssessmentTasksCompleted = null;
            HazardousSubstanceTasksCompleted = null;
            FireRiskAssessmentTasksCompleted = null;
            RiskAssessmentReviewTasksCompleted = null;

            //DueTomorrow Tasks
            GeneralRiskAssessmentTasksDueTomorrow = null;
            PersonalRiskAssessmentTasksDueTomorrow = null;
            FireRiskAssessmentTasksDueTomorrow = null;
            HazardousSubstanceTasksDueTomorrow = null;
            RiskAssessmentReviewTasksDueTomorrow = null;
            ResponsibilityTasksDueTomorrow = null;
            ActionTasksDueTomorrow = null;
	    }

		public bool AnyOverdueTasks(Domain.Entities.Employee employee)
		{
			return 
                      GeneralRiskAssessmentTasksOverdue.Any(task => EmployeeNotificationsHelper.CanSendTaskOverdueNotification(task, employee))
				   || PersonalRiskAssessmentTasksOverdue.Any(task => EmployeeNotificationsHelper.CanSendTaskOverdueNotification(task, employee))
				   || FireRiskAssessmentTasksOverdue.Any(task => EmployeeNotificationsHelper.CanSendTaskOverdueNotification(task, employee))
				   || HazadousSubstanceRiskAssessmentTasksOverdue.Any(task => EmployeeNotificationsHelper.CanSendTaskOverdueNotification(task, employee))
				   || RiskAssessmentReviewTasksOverdue.Any(task => EmployeeNotificationsHelper.CanSendReviewTaskOverdueNotification(task, employee))
                   || ResponsibilityTaskOverdue.Any(task => EmployeeNotificationsHelper.CanSendResponsibilityTaskOverdueNotification(task, employee))                 
				   || ActionTasksOverdue.Any( task => EmployeeNotificationsHelper.CanSendTaskOverdueNotification(task, employee));
		}

        public bool AnyCompletedTasks(Domain.Entities.Employee employee)
        {
            return
                       HazardousSubstanceTasksCompleted.Any(task => EmployeeNotificationsHelper.CanSendTaskCompletedNotification(task, employee))
                    || FireRiskAssessmentTasksCompleted.Any(task => EmployeeNotificationsHelper.CanSendTaskCompletedNotification(task, employee))
                    || PersonalRiskAssessmentTasksCompleted.Any(task => EmployeeNotificationsHelper.CanSendTaskCompletedNotification(task, employee))
                    || GeneralRiskAssessmentTasksCompleted.Any(task => EmployeeNotificationsHelper.CanSendTaskCompletedNotification(task, employee))
                    || RiskAssessmentReviewTasksCompleted.Any(task => EmployeeNotificationsHelper.CanSendTaskCompletedNotification(task, employee));
        }

        public bool AnyDueTomorrowTasks(Domain.Entities.Employee employee)
        {
            return
                       GeneralRiskAssessmentTasksDueTomorrow.Any(task => EmployeeNotificationsHelper.CanSendTaskDueTomorrowNotification(task, employee))
                    || PersonalRiskAssessmentTasksDueTomorrow.Any(task => EmployeeNotificationsHelper.CanSendTaskDueTomorrowNotification(task, employee))
                    || FireRiskAssessmentTasksDueTomorrow.Any(task => EmployeeNotificationsHelper.CanSendTaskDueTomorrowNotification(task, employee))
                    || HazardousSubstanceTasksDueTomorrow.Any(task => EmployeeNotificationsHelper.CanSendTaskDueTomorrowNotification(task, employee))
                    || RiskAssessmentReviewTasksDueTomorrow.Any(task => EmployeeNotificationsHelper.CanSendTaskDueTomorrowNotification(task, employee))
                    || ResponsibilityTasksDueTomorrow.Any(task => EmployeeNotificationsHelper.CanSendTaskDueTomorrowNotification(task, employee))
                    || ActionTasksDueTomorrow.Any(task => EmployeeNotificationsHelper.CanSendTaskDueTomorrowNotification(task, employee));
        }
	}
}