using System.Web;
using System.Web.UI.HtmlControls;
using ActionMailer.Net.Standalone;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.ActiveDirectory;
using BusinessSafe.MessageHandlers.Emails.Controllers;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using System.Linq;

using NServiceBus;


namespace BusinessSafe.MessageHandlers.Emails.MessageHandlers
{
    public class SendIRNNotificationEmailHandler : IHandleMessages<SendIRNNotificationEmail>
    {
        private readonly IEmailSender _emailSender;
        private readonly ICheckListRepository _checklistRepository;
        private readonly IBusinessSafeEmailLinkBaseUrlConfiguration _businessSafeEmailLinkBaseUrlConfiguration;

        public SendIRNNotificationEmailHandler(
            IEmailSender emailSender, ICheckListRepository checklistRepository, IBusinessSafeEmailLinkBaseUrlConfiguration businessSafeEmailLinkBaseUrlConfiguration)
        {
            _emailSender = emailSender;
            _checklistRepository = checklistRepository;
            _businessSafeEmailLinkBaseUrlConfiguration = businessSafeEmailLinkBaseUrlConfiguration;
        }
        
        public void Handle(SendIRNNotificationEmail message)
        {
            var checklist = _checklistRepository.GetById(message.ChecklistId);

            if (checklist != null 
                && checklist.ImmediateRiskNotifications != null
                && checklist.ImmediateRiskNotifications.Any())
            {
                var viewModel = new SendIRNNotificationEmailViewModel()
                                    {
                                        To = "BSAdvice@peninsula-uk.com",
                                        From = "BusinessSafeProject@peninsula-uk.com",
                                        Subject = "Immediate Risk Notification(s) Issued " + message.CAN,
                                        VisitDate =
                                            checklist.VisitDate.HasValue
                                                ? checklist.VisitDate.Value.ToShortDateString()
                                                : "",
                                        CAN = message.CAN,
                                        CompletedBy = checklist.ChecklistCompletedBy,
                                        CompletedOn =
                                            checklist.ChecklistCompletedOn.HasValue
                                                ? checklist.ChecklistCompletedOn.Value.ToShortDateString()
                                                : "",
                                        IRNList = checklist.ImmediateRiskNotifications,

                                        BusinessSafeOnlineLink =
                                            new HtmlString(_businessSafeEmailLinkBaseUrlConfiguration.GetBaseUrl())
                                    };

                var email = CreateRazorEmailResult(viewModel);
                _emailSender.Send(email);
            }
       
            Log4NetHelper.Log.Info("SendIRNNotificationEmail Command Handled");
        }

        protected virtual RazorEmailResult CreateRazorEmailResult(SendIRNNotificationEmailViewModel viewModel)
        {
            var email = new MailerController().IRNNotification(viewModel);
            return email;
        }
    }
}
