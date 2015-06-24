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
    public class ReviewOverDueEscalation : IEscalate
    {
        private readonly IGetOverDueReviewsQuery _getOverDueReviewsQuery;
        private readonly IOverdueReviewNotificationEmailSentCommand _overdueReviewNotificationEmailSentCommand;
        private readonly IBus _bus;
        private readonly IBusinessSafeSessionManager _sessionManager;

        public ReviewOverDueEscalation(
            IGetOverDueReviewsQuery getOverDueReviewsQuery,
            IOverdueReviewNotificationEmailSentCommand overdueReviewNotificationEmailSentCommand,
            IBus bus,
            IBusinessSafeSessionManager sessionManager
            )
        {
            _getOverDueReviewsQuery = getOverDueReviewsQuery;
            _overdueReviewNotificationEmailSentCommand = overdueReviewNotificationEmailSentCommand;
            _bus = bus;
            _sessionManager = sessionManager;
        }

        public void Execute()
        {
            Log4NetHelper.Log.Info("Processing ReviewOverDueEscalation");

            using (var session = _sessionManager.Session)
            {
                var overDueReviews = _getOverDueReviewsQuery.Execute(session);
                Log4NetHelper.Log.Debug(string.Format("Processing {0} Overdue Reviews", overDueReviews.Count));
                foreach (var review in overDueReviews)
                {
                    try
                    {
                        var taskAssignedTo = review.TaskAssignedTo;
                        var riskAssessor = review.RiskAssessment != null ? review.RiskAssessment.RiskAssessor : null;

                        if (CanSendEmailToRiskAssessor(riskAssessor))
                        {
                            SendReviewOverdueToRiskAssessor(review, taskAssignedTo, riskAssessor.Employee);
                            _overdueReviewNotificationEmailSentCommand.Execute(session, review.Id, DateTime.Now);
                        }
                    }
                    catch (Exception exception)
                    {
                        Log4NetHelper.Log.Error(String.Format("Exception encountered ReviewOverDueEscalation: {0}-. TaskId: {1}- reviewId: {2}"
                            , exception, review.TaskGuid, review.Id));
                        EventLog.WriteEntry("EscalationService", exception.ToString(), EventLogEntryType.Error, EscalationServiceLogging.EventId);

                    }
                   
                }
                _sessionManager.CloseSession();
            }
        }

        private static bool CanSendEmailToRiskAssessor(RiskAssessor riskAssessor)
        {
            return riskAssessor != null && riskAssessor.Employee != null && riskAssessor.Employee.HasEmail;
        }

        private void SendReviewOverdueToRiskAssessor(Task overDueTask, Employee taskAssignedTo, Employee riskAssessorEmployee)
        {
            var recipientEmail = riskAssessorEmployee.GetEmail();

            if (string.IsNullOrEmpty(recipientEmail))
            {
                Log4NetHelper.Log.Debug("ERROR - Sent ReviewOverdueToRiskAssessorEmail - no recipient email specified - taskID " + overDueTask.TaskGuid.ToString());
            }
            else
            {
                _bus.Send(new SendReviewOverdueRiskAssessorEmail()
                              {
                                  RecipientEmail = recipientEmail,
                                  TaskReference = overDueTask.Reference,
                                  Title = overDueTask.Title,
                                  Description = overDueTask.Description,
                                  TaskAssignedTo = taskAssignedTo.FullName,
                                  TaskCompletionDueDate =
                                      overDueTask.TaskCompletionDueDate.Value.ToString("d", new CultureInfo("en-GB"))
                              });

                Log4NetHelper.Log.Debug("Sent ReviewOverdueToRiskAssessorEmail");
            }
        }
    }
}
