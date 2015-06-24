using System;
using System.Diagnostics;
using System.Globalization;
using BusinessSafe.Domain.Entities;
using BusinessSafe.EscalationService.Activation;
using BusinessSafe.EscalationService.Commands;
using BusinessSafe.EscalationService.Queries;
using BusinessSafe.Messages.Emails.Commands;
using NHibernate;
using NServiceBus;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.EscalationService.EscalateTasks
{
    public class TaskDueTomorrowEscalation : IEscalate
    {
        private readonly IBus _bus;
        private readonly IBusinessSafeSessionManager _sessionManager;
        private readonly IGetTaskDueTomorrowQuery _getTasksDueTomorrowQuery;
        private readonly ITaskDueTomorrowEmailSentCommand _taskDueTomorrowEmailSentCommand;    
        
        public TaskDueTomorrowEscalation(   IBus bus, 
                                            IBusinessSafeSessionManager sessionManager, 
                                            IGetTaskDueTomorrowQuery getTaskDueTomorrow,
                                            ITaskDueTomorrowEmailSentCommand taskDueTomorrowEmailSentCommand)
        {
            _bus = bus;
            _sessionManager = sessionManager;
            _getTasksDueTomorrowQuery = getTaskDueTomorrow;
            _taskDueTomorrowEmailSentCommand = taskDueTomorrowEmailSentCommand;           
        }

        public void Execute()
        {
            Log4NetHelper.Log.Info("Processing TaskDueTomorrowEscalation");

            using (var session = _sessionManager.Session)
            {
                var results = _getTasksDueTomorrowQuery.Execute(session);

                Log4NetHelper.Log.Debug(string.Format("Processing {0} DueTomorrow Tasks Reviews", results.Count));       
                foreach (var task in results)
                {
                    try
                    {   
                        if (task is ActionTask)
                        {
                            string assigneeEmail = task.TaskAssignedTo.GetEmail();
                            SendTasksDueTomorrowEmail(session, task, assigneeEmail);
                        }

                        if (task is ResponsibilityTask)
                        {
                            var tempTask = task as ResponsibilityTask;
                            if( tempTask.Responsibility != null && tempTask.Responsibility.Owner != null)
                            {
                                string assigneeEmail = tempTask.Responsibility.Owner.GetEmail();
                                SendTasksDueTomorrowEmail(session, task, assigneeEmail);
                            }                            
                        }
                        else 
                        {    
                            // task is RiskAssessmentReviewTask
                            
                            if (task.RiskAssessment != null && task.RiskAssessment.RiskAssessor != null)
                            {                                               
                                if (CanSendEmailToRiskAssessor(task.RiskAssessment.RiskAssessor))
                                {
                                    string assessorEmail = task.RiskAssessment.RiskAssessor.Employee.GetEmail();
                                    SendTasksDueTomorrowEmail(session, task, assessorEmail);
                                }
                            }
                        }

                           
                    }
                    catch (Exception exception)
                    {
                        Log4NetHelper.Log.Error(String.Format("Exception encountered TaskDueTomorrowEscalation: {0}-. TaskGuid: {1}- TaskId: {2}"
                         , exception, task.TaskGuid, task.Id));
                        EventLog.WriteEntry("EscalationService", exception.ToString(), EventLogEntryType.Error, EscalationServiceLogging.EventId);                      
                    }
                }
                _sessionManager.CloseSession();
            }
        }
        

        private static bool CanSendEmailToRiskAssessor(RiskAssessor riskAssessor)
        {
            return riskAssessor != null
                    && !riskAssessor.DoNotSendReviewDueNotification
                    && riskAssessor.Employee != null
                    &&  riskAssessor.Employee.HasEmail;
        }

        private void SendTasksDueTomorrowEmail(ISession session, Task task, string email)
        {
             var recipientEmail = email;
             if (string.IsNullOrEmpty(recipientEmail))
             {
                 Log4NetHelper.Log.Debug("ERROR - SendTasksDueTomorrowEmail - no recipient email specified - taskID " + task.TaskGuid.ToString());
             }
             else
             {
                 _bus.Send(new SendTaskDueTomorrowEmail()
                               {
                                   TaskReference = task.Reference,
                                   Description = task.Description,
                                   RecipientEmail = email,
                                   TaskType = task.Category != null ? task.Category.Category : "",
                                   TaskCompletionDueDate =
                                       task.TaskCompletionDueDate != null
                                           ? task.TaskCompletionDueDate.Value.ToString("d", new CultureInfo("en-GB"))
                                           : null,
                                   Title = task.Title,
                                   TaskAssignedBy = task.CreatedBy != null ? task.CreatedBy.Employee.FullName : "",
                               });

                 _taskDueTomorrowEmailSentCommand.Execute(session, task.Id, DateTime.Now);
             }
        }
    }
}