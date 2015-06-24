using System;
using System.Diagnostics;
using System.Globalization;
using BusinessSafe.Domain.Entities;
using BusinessSafe.EscalationService.Activation;
using BusinessSafe.EscalationService.Commands;
using BusinessSafe.EscalationService.Queries;
using BusinessSafe.Messages.Emails.Commands;
using NServiceBus;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.EscalationService.EscalateTasks
{
    public class TaskOverDueEscalation : IEscalate
    {
        private readonly IGetOverDueTasksQuery _getOverDueTasksQuery;
        private readonly IOverdueTaskNotificationEmailSentCommand _overdueTaskNotificationEmailSentCommand;
        private readonly IBus _bus;
        private readonly IBusinessSafeSessionManager _sessionManager;

        public TaskOverDueEscalation(IGetOverDueTasksQuery getOverDueTasksQuery, IOverdueTaskNotificationEmailSentCommand overdueTaskNotificationEmailSentCommand, IBus bus, IBusinessSafeSessionManager sessionManager)
        {
            _getOverDueTasksQuery = getOverDueTasksQuery;
            _overdueTaskNotificationEmailSentCommand = overdueTaskNotificationEmailSentCommand;
            _bus = bus;
            _sessionManager = sessionManager;
        }

        public void Execute()
        {
            Log4NetHelper.Log.Info("Processing TaskOverDueEscalation");

            using (var session = _sessionManager.Session)
            {
                var overDueTasks = _getOverDueTasksQuery.Execute(session);
                Log4NetHelper.Log.Debug(string.Format("Processing {0} Overdue Tasks", overDueTasks.Count));
                foreach (var overDueTask in overDueTasks)
                {
                    var hasOverdueTaskEmailBeenSent = false;

                    try
                    {
                        var taskAssignee = overDueTask.TaskAssignedTo;
                        var employee = GetOwnerOrAssessor(overDueTask);

                        if (CanSendEmailToTaskAssignedTo(taskAssignee))
                        {
                            SendTaskOverdueToUserEmail(overDueTask, taskAssignee, employee);
                            hasOverdueTaskEmailBeenSent = true;
                        }

                        if (CanSendEmailToRiskAssessor(employee))
                        {
                            SendTaskOverdueToRiskAssessor(overDueTask, taskAssignee, employee);
                            hasOverdueTaskEmailBeenSent = true;
                        }                       
                    }
                    catch (Exception exception)
                    {
                        Log4NetHelper.Log.Error("Exception encountered TaskOverDueEscalation", exception);
                        EventLog.WriteEntry("EscalationService", exception.ToString(), EventLogEntryType.Error, EscalationServiceLogging.EventId);
                    }
                    finally
                    {
                        if (hasOverdueTaskEmailBeenSent)
                        {
                            _overdueTaskNotificationEmailSentCommand.Execute(session, overDueTask.Id, DateTime.Now);
                        }
                    }
                }

                _sessionManager.CloseSession();
            }

        }

        private static Employee GetOwnerOrAssessor(Task overDueTask)
        {
            Employee employee = null;

            if (overDueTask is ActionTask)
            {                 
                return employee; // todo action owner
            }

            if (overDueTask is ResponsibilityTask)
            {
                var task = overDueTask as ResponsibilityTask;
                employee = task.Responsibility.Owner ?? null;
            }
            else
            {
                if (overDueTask.RiskAssessment != null && overDueTask.RiskAssessment.RiskAssessor != null)
                {
                    employee = overDueTask.RiskAssessment.RiskAssessor.Employee;
                }
            }
            return employee;
        }
      
        private static bool CanSendEmailToRiskAssessor(Employee riskAssessorEmployee)
        {
            return riskAssessorEmployee != null && riskAssessorEmployee.HasEmail;
        }

        private static bool CanSendEmailToTaskAssignedTo(Employee taskAssignedTo)
        {
            return taskAssignedTo != null && taskAssignedTo.HasEmail;
        }

        private void SendTaskOverdueToRiskAssessor(Task overDueTask, Employee taskAssignedTo, Employee riskAssessorEmployee)
        {
            var recipientEmail = riskAssessorEmployee.GetEmail();
            if (string.IsNullOrEmpty(recipientEmail))
            {
                Log4NetHelper.Log.Debug("ERROR - Sent TaskOverdueToRiskAssessorEmail - no recipient email specified - taskID " + overDueTask.TaskGuid.ToString());
            }
            else
            {
                _bus.Send(new SendTaskOverdueRiskAssessorEmail()
                              {
                                  TaskGuid = overDueTask.TaskGuid,
                                  RecipientEmail = recipientEmail,
                                  TaskReference = overDueTask.Reference,
                                  Title = overDueTask.Title,
                                  Description = overDueTask.Description,
                                  TaskAssignedTo = taskAssignedTo.FullName,
                                  TaskCompletionDueDate =
                                      overDueTask.TaskCompletionDueDate.Value.ToString("d", new CultureInfo("en-GB"))
                              });

                Log4NetHelper.Log.Debug("Sent TaskOverdueToRiskAssessorEmail");
            }
        }

        private void SendTaskOverdueToUserEmail(Task overDueTask, Employee taskAssignedTo, Employee riskAssessorEmployee)
        {
            var riskAssessor = "Currently No Risk Assessor";
            if (riskAssessorEmployee != null)
            {
                riskAssessor = riskAssessorEmployee.FullName;
            }

            var recipientEmail = taskAssignedTo.GetEmail();

            if (string.IsNullOrEmpty(recipientEmail))
            {
                Log4NetHelper.Log.Debug("ERROR - Sent TaskOverdueToUserEmail - no recipient email specified - taskID " + overDueTask.TaskGuid.ToString() );
            }
            else
            {
                _bus.Send(new SendTaskOverdueUserEmail()
                              {
                                  TaskGuid = overDueTask.TaskGuid,
                                  RecipientEmail = recipientEmail,
                                  TaskReference = overDueTask.Reference,
                                  Title = overDueTask.Title,
                                  Description = overDueTask.Description,
                                  RiskAssessor = riskAssessor,
                                  TaskCompletionDueDate =
                                      overDueTask.TaskCompletionDueDate.Value.ToString("d", new CultureInfo("en-GB")),
                                  TaskAssignedTo = overDueTask.TaskAssignedTo.FullName
                              });

                Log4NetHelper.Log.Debug("Sent TaskOverdueToUserEmail");
            }
        }
    }
}