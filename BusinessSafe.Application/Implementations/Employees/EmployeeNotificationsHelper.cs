using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Messages.Emails.Commands;
using NHibernate.Linq;

namespace BusinessSafe.Application.Implementations.Employees
{
    public static class EmployeeNotificationsHelper
    {
        public static SendEmployeeDigestEmail CreateSendEmployeeDigestEmailCommand(Employee employee, EmployeeTasks employeeTasks)
        {
            var employeeDigestEmail = new SendEmployeeDigestEmail()
            {
                RecipientEmail = employee.GetEmail()

                //overdue tasks
                ,GeneralRiskAssessmentsOverdueTasks = employeeTasks.GeneralRiskAssessmentTasksOverdue
                    .Where(task => CanSendTaskOverdueNotification(task,employee))
                    .Select(MapToTaskDetails).ToList()

                ,PersonalRiskAssessmentTasksOverdue = employeeTasks.PersonalRiskAssessmentTasksOverdue
                    .Where(task => CanSendTaskOverdueNotification(task, employee))
                    .Select(MapToTaskDetails).ToList()

                ,FireRiskAssessmentsOverdueTasks = employeeTasks.FireRiskAssessmentTasksOverdue
                     .Where(task => CanSendTaskOverdueNotification(task, employee))
                    .Select(MapToTaskDetails).ToList()

                ,HazardousSubstanceRiskAssessmentTasksOverdue = employeeTasks.HazadousSubstanceRiskAssessmentTasksOverdue
                    .Where(task => CanSendTaskOverdueNotification(task, employee))
                    .Select(MapToTaskDetails).ToList()

                ,RiskAssessmentReviewTasksOverdue = employeeTasks.RiskAssessmentReviewTasksOverdue
                    .Where(task => CanSendReviewTaskOverdueNotification(task, employee))
                    .Select(MapToTaskDetails).ToList()

                , ResponsibilitiesTasksOverdue = employeeTasks.ResponsibilityTaskOverdue
                    .Where(task => CanSendResponsibilityTaskOverdueNotification(task, employee))
                    .Select(MapToTaskDetails).ToList()

                ,ActionTasksOverdue = employeeTasks.ActionTasksOverdue
                    .Where( task => CanSendTaskOverdueNotification(task, employee))
                    .Select(MapToTaskDetails)
                    .ToList()

                //Completed tasks
                ,HazardousSubstanceTasksCompleted = employeeTasks.HazardousSubstanceTasksCompleted
                    .Where(task => CanSendTaskCompletedNotification(task, employee))
                    .Select(MapToTaskDetails)
                    .ToList()

                , FireRiskAssessmentTasksCompleted = employeeTasks.FireRiskAssessmentTasksCompleted
                    .Where(task => CanSendTaskOverdueNotification(task, employee))
                    .Select(MapToTaskDetails).ToList()

               , GeneralRiskAssessmentTasksCompleted = employeeTasks.GeneralRiskAssessmentTasksCompleted
                    .Where(task => CanSendTaskCompletedNotification(task, employee))
                    .Select(MapToTaskDetails)
                    .ToList()

                ,PersonalRiskAssessmentTasksCompleted = employeeTasks.PersonalRiskAssessmentTasksCompleted
                    .Where(task => CanSendTaskCompletedNotification(task, employee))
                    .Select(MapToTaskDetails)
                    .ToList()

                ,RiskAssessmentReviewTasksCompleted = employeeTasks.RiskAssessmentReviewTasksCompleted
                    .Where(task => CanSendTaskCompletedNotification(task, employee))
                    .Select(MapToTaskDetails)
                    .ToList()

                //Due Tomorrow tasks
                ,GeneralRiskAssessmentsTasksDueTomorrow = employeeTasks.GeneralRiskAssessmentTasksDueTomorrow
                    .Where(task => CanSendTaskDueTomorrowNotification(task, employee))
                    .Select(MapToTaskDetails).ToList()

                 ,PersonalRiskAssessmentTasksDueTomorrow = employeeTasks.PersonalRiskAssessmentTasksDueTomorrow
                    .Where(task => CanSendTaskDueTomorrowNotification(task, employee))
                    .Select(MapToTaskDetails).ToList()

                  ,FireRiskAssessmentsTasksDueTomorrow = employeeTasks.FireRiskAssessmentTasksDueTomorrow
                    .Where(task => CanSendTaskDueTomorrowNotification(task, employee))
                    .Select(MapToTaskDetails).ToList()

                  ,HazardousSubstanceRiskAssessmentTasksDueTomorrow = employeeTasks.HazardousSubstanceTasksDueTomorrow
                    .Where(task => CanSendTaskDueTomorrowNotification(task, employee))
                    .Select(MapToTaskDetails).ToList()

                 ,RiskAssessmentReviewTasksDueTomorrow = employeeTasks.RiskAssessmentReviewTasksDueTomorrow
                    .Where(task => CanSendReviewTaskDueTomorrowNotification(task, employee))
                    .Select(MapToTaskDetails).ToList()

                 ,ResponsibilitiesTasksDueTomorrow = employeeTasks.ResponsibilityTasksDueTomorrow
                    .Where(task => CanSendTaskDueTomorrowNotification(task, employee))
                    .Select(MapToTaskDetails).ToList()

                  ,ActionTasksDueTomorrow = employeeTasks.ActionTasksDueTomorrow
                        .Where(task => CanSendActionTaskDueTomorrowNotification(task, employee))
                        .Select(MapToTaskDetails).ToList()
            };

            return employeeDigestEmail;
        }

        private static bool CanSendTaskOverdueRiskAssessorNotification(Task x, RiskAssessor riskAssessor)
        {
            return x.SendTaskOverdueNotification.HasValue &&
                        x.SendTaskOverdueNotification == true &&
                            riskAssessor.DoNotSendTaskOverdueNotifications == false;
        }

        public static bool CanSendTaskOverdueNotification(Task x, Employee employee)
        {
            return ((x.TaskAssignedTo != null && x.TaskAssignedTo.Id == employee.Id) ||
                    (IsRiskAssessorThisEmployee(x, employee) && CanSendTaskOverdueRiskAssessorNotification(x, employee.RiskAssessor)));
        }

        public static bool CanSendReviewTaskOverdueNotification(Task x, Employee employee)
        {
            return ((x.TaskAssignedTo != null && x.TaskAssignedTo.Id == employee.Id) ||
                    (IsRiskAssessorThisEmployee(x, employee) &&  employee.RiskAssessor.DoNotSendTaskOverdueNotifications == false));
        }

        public static bool CanSendResponsibilityTaskOverdueNotification(Task x, Employee employee)
        {
            return (x.TaskAssignedTo != null && x.TaskAssignedTo.Id == employee.Id);
        }

        public static bool CanSendTaskCompletedNotification(Task x, Employee employee)
        {
            return
                IsRiskAssessorThisEmployee(x, employee)
                && x.RiskAssessment.RiskAssessor.DoNotSendTaskCompletedNotifications == false
                && x.DoesEmployeeTaskNotificationExist(employee, TaskNotificationEventEnum.Completed) == false
                && x.SendTaskCompletedNotification.HasValue && x.SendTaskCompletedNotification == true;
        }

        public static bool CanSendTaskDueTomorrowNotification(Task x, Employee employee)
        {
            return
                x.TaskAssignedTo != null && x.TaskAssignedTo.Id == employee.Id && x.SendTaskDueTomorrowNotification == true;
        }

        public static bool CanSendActionTaskDueTomorrowNotification(Task x, Employee employee)
        {
            return x.TaskAssignedTo != null && x.TaskAssignedTo.Id == employee.Id;
        }

        public static bool CanSendReviewTaskDueTomorrowNotification(Task x, Employee employee)
        {
            return (x.TaskAssignedTo != null && x.TaskAssignedTo.Id == employee.Id
                        //Exclude this below check because when Review is added Review Task is automaticaly created. 
                        //There is no way on the UI to set this flag manually thats why we ignore it at the moment.
                        //&& x.SendTaskDueTomorrowNotification == true; 
                        ) || (IsRiskAssessorThisEmployee(x, employee) && employee.RiskAssessor.DoNotSendReviewDueNotification == false);
        }

        private static bool IsRiskAssessorThisEmployee(Task x, Employee employee)
        {
            return x.RiskAssessment.RiskAssessor != null && x.RiskAssessment.RiskAssessor.Employee.Id == employee.Id;
        }

        public static TaskDetails MapToTaskDetails(Task x)
        {
           
            return new TaskDetails()
            {
                Description = x.Description,
                Title = x.Title,
                TaskReference = x.Reference,
                CompletionDueDate = x.TaskCompletionDueDate,
                TaskAssignedTo = x.TaskAssignedTo != null ? x.TaskAssignedTo.FullName : string.Empty,
                RiskAssessor = (x.RiskAssessment != null && x.RiskAssessment.RiskAssessor != null) ? x.RiskAssessment.RiskAssessor.FormattedName : string.Empty
            };
        }
        
        //public static void AddEmployeeTaskOverdueNofication(List<MultiHazardRiskAssessmentFurtherControlMeasureTask> tasks, Employee employee, UserForAuditing systemUser)
        //{
        //    tasks.Where(x => CanSendTaskOverdueNotification(x,employee))
        //        .ForEach(x => x.AddEmployeeTaskNotificationHistory(employee, TaskNotificationEventEnum.Completed, DateTime.Now, systemUser));
        //}

        public static void AddEmployeeTaskCompletedNotification(Task task, Employee employee, UserForAuditing systemUser)
        {
            task.AddEmployeeTaskNotificationHistory(employee, TaskNotificationEventEnum.Completed, DateTime.Now, systemUser);
        }
    }
}