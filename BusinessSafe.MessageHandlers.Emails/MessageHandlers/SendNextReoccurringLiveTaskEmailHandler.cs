using System.Web;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using NServiceBus;

namespace BusinessSafe.MessageHandlers.Emails.MessageHandlers
{
    public class SendNextReoccurringLiveTaskEmailHandler : IHandleMessages<SendNextReoccurringLiveTaskEmail>
    {
        private readonly IEmailSender _emailSender;
        private readonly IBusinessSafeEmailLinkBaseUrlConfiguration _businessSafeEmailLinkBaseUrlConfiguration;

        public SendNextReoccurringLiveTaskEmailHandler(IEmailSender emailSender, IBusinessSafeEmailLinkBaseUrlConfiguration businessSafeEmailLinkBaseUrlConfiguration)
        {
            _emailSender = emailSender;
            _businessSafeEmailLinkBaseUrlConfiguration = businessSafeEmailLinkBaseUrlConfiguration;
        }

        public void Handle(SendNextReoccurringLiveTaskEmail message)
        {
            var viewModel = new TaskAssignedEmailViewModel();
            viewModel.Subject = "New Risk Assessment Task";
            viewModel.From = "BusinessSafeProject@peninsula-uk.com";
            viewModel.To = message.RecipientEmail;
            viewModel.TaskTitle = message.Title;
            viewModel.TaskReference = message.TaskReference;
            viewModel.TaskDescription = message.Description;
            viewModel.AssignedBy = message.RiskAssessor;
            viewModel.TaskListUrl = _businessSafeEmailLinkBaseUrlConfiguration.GetBaseUrl();
            viewModel.CompletionDueDate = message.TaskCompletionDueDate;

            SendTaskAssignedEmailHelper.SendTaskAssignedEmail(viewModel, _emailSender);

            Log4NetHelper.Log.Info("SendNextReoccurringLiveTaskEmail Command Handled");  
        }
    }
}