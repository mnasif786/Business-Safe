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
    public class NextReoccurringTaskLiveEscalation
    // : IEscalate  Removed from Escalation service pending implementation of TaskAssigned message when completing recurring tasks
    {

        private readonly IGetNextReoccurringTasksLiveQuery _getNextReoccurringTasksLiveQuery;
        private readonly INextReoccurringTaskNotificationEmailSentCommand _nextReoccurringTaskNotificationEmailSentCommand;
        private readonly IBus _bus;
        private readonly IBusinessSafeSessionManager _sessionManager;

        public NextReoccurringTaskLiveEscalation(IGetNextReoccurringTasksLiveQuery getNextReoccurringTasksLiveQuery, INextReoccurringTaskNotificationEmailSentCommand nextReoccurringTaskNotificationEmailSentCommand, IBus bus, IBusinessSafeSessionManager sessionManager)
        {
            _getNextReoccurringTasksLiveQuery = getNextReoccurringTasksLiveQuery;
            _nextReoccurringTaskNotificationEmailSentCommand = nextReoccurringTaskNotificationEmailSentCommand;
            _bus = bus;
            _sessionManager = sessionManager;
        }

        public void Execute()
        {
            Log4NetHelper.Log.Info("Processing NextReoccurringTaskLiveEscalation");

            using (var session = _sessionManager.Session)
            {
                var nextReoccurringTasksLive = _getNextReoccurringTasksLiveQuery.Execute(session);
                foreach (var nextReoccurringLiveTask in nextReoccurringTasksLive)
                {
                    Log4NetHelper.Log.Debug(string.Format("Processing {0} Next Reoccurring Live Tasks", nextReoccurringTasksLive.Count));

                    try
                    {
                        if (CanSendEmailToAssignedUser(nextReoccurringLiveTask))
                        {
                            SendNextReoccurringTaskLiveToAssignedUserEmail(nextReoccurringLiveTask);
                            _nextReoccurringTaskNotificationEmailSentCommand.Execute(session, nextReoccurringLiveTask.Id, DateTime.Now);
                        }

                    }
                    catch (Exception exception)
                    {
                        Log4NetHelper.Log.Error("Exception encountered NextReoccurringTaskLiveEscalation", exception);
                        EventLog.WriteEntry("EscalationService", exception.ToString(), EventLogEntryType.Error, EscalationServiceLogging.EventId);
                    }
                } 
                _sessionManager.CloseSession();
            }
        }

        private void SendNextReoccurringTaskLiveToAssignedUserEmail(Task nextReoccurringLiveTask)
        {
            var recipientEmail = GetRecipientEmail(nextReoccurringLiveTask);
            var taskCompletionDueDate = GetTaskCompletionDueDate(nextReoccurringLiveTask);
            var riskAssessor = GetRiskAssessor(nextReoccurringLiveTask);

            _bus.Send(new SendNextReoccurringLiveTaskEmail
                          {
                              RecipientEmail = recipientEmail,
                              Title = nextReoccurringLiveTask.Title,
                              TaskReference = nextReoccurringLiveTask.Reference,
                              Description = nextReoccurringLiveTask.Description,
                              RiskAssessor = riskAssessor,
                              TaskCompletionDueDate = taskCompletionDueDate
                          });

            Log4NetHelper.Log.Debug("Sent TaskOverdueToRiskAssessorEmail");
        }

        private static string GetRecipientEmail(Task nextReoccurringLiveTask)
        {
            var recipientEmail = nextReoccurringLiveTask.TaskAssignedTo.GetEmail();
            return recipientEmail;
        }

        private static string GetTaskCompletionDueDate(Task nextReoccurringLiveTask)
        {
            var taskCompletionDueDate = nextReoccurringLiveTask.TaskCompletionDueDate.HasValue
                                            ? nextReoccurringLiveTask.TaskCompletionDueDate.Value.ToString("d", new CultureInfo("en-GB"))
                                            : string.Empty;
            return taskCompletionDueDate;
        }

        private static string GetRiskAssessor(Task nextReoccurringLiveTask)
        {
            var riskAssessor = "Currently No Risk Assessor";
            if (nextReoccurringLiveTask.RiskAssessment != null &&
                nextReoccurringLiveTask.RiskAssessment.RiskAssessor != null)
            {
                riskAssessor = nextReoccurringLiveTask.RiskAssessment.RiskAssessor.Employee.FullName;
            }
            return riskAssessor;
        }

        private bool CanSendEmailToAssignedUser(Task nextReoccurringLiveTask)
        {
            var taskAssignedTo = nextReoccurringLiveTask.TaskAssignedTo;
            return taskAssignedTo != null && taskAssignedTo.HasEmail;
        }
    }
}