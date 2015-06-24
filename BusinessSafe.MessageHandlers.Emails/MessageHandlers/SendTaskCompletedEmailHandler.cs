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
    public class SendTaskCompletedEmailHandler : IHandleMessages<SendTaskCompletedEmail>
    {
        private readonly IEmailSender _emailSender;
        private readonly IBusinessSafeEmailLinkBaseUrlConfiguration _urlConfiguration;

        public SendTaskCompletedEmailHandler(
            IEmailSender emailSender,
            IBusinessSafeEmailLinkBaseUrlConfiguration urlConfiguration)
        {
            _emailSender = emailSender;
            _urlConfiguration = urlConfiguration;
        }

        public void Handle(SendTaskCompletedEmail message)
        {
            var viewModel = new TaskCompletedViewModel
                                {
                                    From = "BusinessSafeProject@peninsula-uk.com",
                                    Subject = "Risk Assessment Task Completed",
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

            Log4NetHelper.Log.Info("SendTaskCompletedEmail Command Handled");
        }

        protected virtual RazorEmailResult CreateRazorEmailResult(TaskCompletedViewModel viewModel)
        {
            var email = new MailerController().TaskCompleted(viewModel);
            return email;
        }
    }
}
