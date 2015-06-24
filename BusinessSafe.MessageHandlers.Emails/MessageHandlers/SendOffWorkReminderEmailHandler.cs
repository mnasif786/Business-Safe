using System.Web;
using ActionMailer.Net.Standalone;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.Controllers;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using NServiceBus;

namespace BusinessSafe.MessageHandlers.Emails.MessageHandlers
{
    public class SendOffWorkReminderEmailHandler : IHandleMessages<SendOffWorkReminderEmail>
    {
        private readonly IEmailSender _emailSender;
        private readonly IBusinessSafeEmailLinkBaseUrlConfiguration _businessSafeEmailLinkBaseUrlConfiguration;
        private readonly IAccidentRecordRepository _accidentRecordRepository;

        public SendOffWorkReminderEmailHandler(IEmailSender emailSender,
                                               IBusinessSafeEmailLinkBaseUrlConfiguration
                                                   businessSafeEmailLinkBaseUrlConfiguration,
                                               IAccidentRecordRepository accidentRecordRepository)
        {
            _emailSender = emailSender;
            _businessSafeEmailLinkBaseUrlConfiguration = businessSafeEmailLinkBaseUrlConfiguration;
            _accidentRecordRepository = accidentRecordRepository;
        }

        public void Handle(SendOffWorkReminderEmail message)
        {
            var accidentRecord = _accidentRecordRepository.GetById(message.AccidentRecordId);
            
            var emailLink = _businessSafeEmailLinkBaseUrlConfiguration.GetBaseUrl();

            var viewModel = new OffWorkReminderViewModel()
            {
                From = "BusinessSafeProject@peninsula-uk.com",
                Subject = "Accident Record",
                To = message.RecipientEmail,
                AccidentRecordReference = message.AccidentRecordReference,
                Title = "Accident Record Off Work Reminder",
                InjuredPerson = accidentRecord.InjuredPersonFullName,
                DateOfAccident = accidentRecord.DateAndTimeOfAccident.Value.ToShortDateString(),
                Jurisdiction = accidentRecord.Jurisdiction.Name,
                BusinessSafeOnlineLink = new HtmlString(emailLink),
            };

            var userEmail = CreateRazorEmailResult(viewModel);
            _emailSender.Send(userEmail);

            Log4NetHelper.Log.Info("SendOffWorkReminderEmail Command Handled");
        }

        protected virtual RazorEmailResult CreateRazorEmailResult(OffWorkReminderViewModel viewModel)
        {
            var email = new MailerController().OffWorkReminder(viewModel);
            return email;
        }
    }
}