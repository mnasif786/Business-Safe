using System;
using ActionMailer.Net.Standalone;
using BusinessSafe.Application.RestAPI;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.ClientDocumentService;
using BusinessSafe.MessageHandlers.Emails.Controllers;
using BusinessSafe.MessageHandlers.Emails.DocumentLibraryService;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using NServiceBus;

namespace BusinessSafe.MessageHandlers.Emails.MessageHandlers
{
    public class SendHardCopyToOfficeEmailHandler : IHandleMessages<SendHardCopyToOfficeEmail>
    {
        private readonly IEmailSender _emailSender;        

        public SendHardCopyToOfficeEmailHandler( IEmailSender emailSender )                   
        {
            _emailSender = emailSender;
        }

        public void Handle(SendHardCopyToOfficeEmail message)
        {
                var viewModel = new SendHardCopyToOfficeEmailViewModel()
                {
                    ChecklistId = message.ChecklistId,
                    To = "H&S.Reports@peninsula-uk.com",
                    From = "BusinessSafeProject@peninsula-uk.com",

                    Subject = string.Format("Hard Copy SafeCheck Report Required for {0} {1} {2}", message.CAN, message.SiteAddressLine1, message.SitePostcode),
                    VisitDate = message.VisitDate.HasValue ? message.VisitDate.Value.ToShortDateString() : "",
                    VisitBy = message.VisitBy,
                    SubmittedDate = message.SubmittedDate.HasValue ? message.SubmittedDate.Value.ToShortDateString() : "",
                    SubmittedBy = message.SubmittedBy,                    
                    CAN = message.CAN,  

                    SiteAddress = String.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}", 
                                                    message.SiteName,
                                                    message.SiteAddressLine1,
                                                    message.SiteAddressLine2,
                                                    message.SiteAddressLine3,
                                                    message.SiteAddressLine4,
                                                    message.SiteAddressLine5,
                                                    message.SitePostcode)
                };

                var email = CreateRazorEmailResult(viewModel);
                _emailSender.Send(email);

                Log4NetHelper.Log.Info("SendHardCopyToOfficeEmail Command Handled");
        }

        protected virtual RazorEmailResult CreateRazorEmailResult(SendHardCopyToOfficeEmailViewModel viewModel)
        {
            var email = new MailerController().HardCopyToOfficeEmail(viewModel);
            return email;
        }
    }
}
