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
    public class SendResponsibilityTaskCompletedEmailHandler : IHandleMessages<SendResponsibilityTaskCompletedEmail>
    {
        private readonly IEmailSender _emailSender;
        private readonly IBusinessSafeEmailLinkBaseUrlConfiguration _urlConfiguration;

        public SendResponsibilityTaskCompletedEmailHandler(
            IEmailSender emailSender,
            IBusinessSafeEmailLinkBaseUrlConfiguration urlConfiguration)
        {
            _emailSender = emailSender;
            _urlConfiguration = urlConfiguration;
        }

        public void Handle(SendResponsibilityTaskCompletedEmail message)
        {
            var viewModel = new ResponsibilityTaskCompletedViewModel
                                {
                                    From = "BusinessSafeProject@peninsula-uk.com",
                                    Subject = "Responsibility Task Completed",
                                    To = message.ResponsibilityOwnerEmail,

                                    TaskReference = message.TaskReference,
                                    Title = message.Title,
                                    Description = message.Description,
                                    OwnerName = message.ResponsibilityOwnerName,
                                    BusinessSafeOnlineLink = new HtmlString(_urlConfiguration.GetBaseUrl()),
                                    TaskAssignedTo = message.TaskAssignedTo,
                                    CompletedBy = message.CompletedBy
                                };

            var email = CreateRazorEmailResult(viewModel);

            _emailSender.Send(email);

            //Log4NetHelper.Log.Info("SendTaskCompletedEmail Command Handled");
        }

        protected virtual RazorEmailResult CreateRazorEmailResult(ResponsibilityTaskCompletedViewModel viewModel)
        {
            return new MailerController().ResponsibilityTaskCompleted(viewModel);
        }
    }
}
