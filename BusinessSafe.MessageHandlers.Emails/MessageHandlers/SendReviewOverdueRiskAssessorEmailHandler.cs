using System.Web;
using ActionMailer.Net.Standalone;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.Controllers;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using NServiceBus;

namespace BusinessSafe.MessageHandlers.Emails.MessageHandlers
{
    public class SendReviewOverdueRiskAssessorEmailHandler : IHandleMessages<SendReviewOverdueRiskAssessorEmail>
    {
        private readonly IEmailSender _emailSender;
        private readonly IBusinessSafeEmailLinkBaseUrlConfiguration _businessSafeEmailLinkBaseUrlConfiguration;

        public SendReviewOverdueRiskAssessorEmailHandler(IEmailSender emailSender, IBusinessSafeEmailLinkBaseUrlConfiguration businessSafeEmailLinkBaseUrlConfiguration)
        {
            _emailSender = emailSender;
            _businessSafeEmailLinkBaseUrlConfiguration = businessSafeEmailLinkBaseUrlConfiguration;
        }

        public void Handle(SendReviewOverdueRiskAssessorEmail message)
        {
            var emailLink = _businessSafeEmailLinkBaseUrlConfiguration.GetBaseUrl();

            var viewModel = new ReviewOverdueViewModel()
                                {
                                    From = "BusinessSafeProject@peninsula-uk.com",
                                    Subject = "Risk Assessment Review Overdue",
                                    To = message.RecipientEmail,
                                    TaskReference = message.TaskReference,
                                    Title = message.Title,
                                    Description = message.Description,
                                    BusinessSafeOnlineLink = new HtmlString(emailLink),
                                    TaskAssignedTo = message.TaskAssignedTo,
                                    TaskCompletionDueDate = message.TaskCompletionDueDate
                                };

            var userEmail = CreateRazorEmailResult(viewModel);
            _emailSender.Send(userEmail);

            Log4NetHelper.Log.Info("SendReviewOverdueRiskAssessorEmail Command Handled");
        }

        protected virtual RazorEmailResult CreateRazorEmailResult(ReviewOverdueViewModel viewModel)
        {
            var email = new MailerController().ReviewOverdueRiskAssessor(viewModel);
            return email;
        }
    }
}
