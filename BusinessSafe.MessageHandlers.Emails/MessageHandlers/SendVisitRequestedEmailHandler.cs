using ActionMailer.Net.Standalone;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.Controllers;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using NServiceBus;

namespace BusinessSafe.MessageHandlers.Emails.MessageHandlers
{
    public class SendVisitRequestedEmailHandler : IHandleMessages<SendVisitRequestedEmail>
    {
        private readonly IEmailSender _emailSender;               

        public SendVisitRequestedEmailHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;            
        }

        public void Handle(SendVisitRequestedEmail message)
        {          
            VisitRequestedViewModel model = new VisitRequestedViewModel()
                                                {     
                                                    CompanyName = message.CompanyName,
                                                    CAN = message.CAN,                                                    
                                                    ContactName = message.ContactName,
                                                    ContactEmail = message.ContactEmail,
                                                    ContactPhone = message.ContactPhone,

                                                    DateFrom = message.DateFrom,
                                                    DateTo = message.DateTo,
                                                    RequestTime = message.RequestTime,
                    
                                                    Comments = message.Comments,
                                                    SiteName = message.SiteName,
                                                    Postcode = message.Postcode
                                                };

            var userEmail = CreateRazorEmailResult(model);
            _emailSender.Send(userEmail);

            Log4NetHelper.Log.Info("SendTaskOverdueUserEmail Command Handled");
        }

        protected virtual RazorEmailResult CreateRazorEmailResult(VisitRequestedViewModel model)
        {
           return new MailerController().SendVisitRequestedEmail(model);            
        }

      
    }
}
