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
    public class SendReviewCompletedEmailHandler : IHandleMessages<SendReviewCompletedEmail>
    {
        private readonly IEmailSender _emailSender;
        private readonly IBusinessSafeEmailLinkBaseUrlConfiguration _urlConfiguration;

        public SendReviewCompletedEmailHandler(
            IEmailSender emailSender,
            IBusinessSafeEmailLinkBaseUrlConfiguration urlConfiguration)
        {
            _emailSender = emailSender;
            _urlConfiguration = urlConfiguration;
        }

        public void Handle(SendReviewCompletedEmail message)
        {
            var viewModel = new ReviewCompletedEmailViewModel
                                {
                                    From = "BusinessSafeProject@peninsula-uk.com",
                                    Subject = "Risk Assessment Review Completed",
                                    To = message.RiskAssessorEmail,

                                    TaskReference = message.TaskReference,
                                    Title = message.Title,
                                    Description = message.Description,
                                    RiskAssessorName = message.RiskAssessorName,
                                    BusinessSafeOnlineLink = new HtmlString(_urlConfiguration.GetBaseUrl()),
                                    TaskAssignedTo = message.TaskAssignedTo
                                };

            var email = CreateRazorEmailResult(viewModel);

            _emailSender.Send(email);

            Log4NetHelper.Log.Info("SendReviewCompletedEmail Command Handled");
        }

        protected virtual RazorEmailResult CreateRazorEmailResult(ReviewCompletedEmailViewModel viewModel)
        {
            var email = new MailerController().ReviewCompleted(viewModel);
            return email;
        }
    }
}