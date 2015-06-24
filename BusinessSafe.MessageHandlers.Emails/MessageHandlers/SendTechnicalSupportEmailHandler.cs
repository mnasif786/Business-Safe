using ActionMailer.Net.Standalone;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.Controllers;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using NServiceBus;

namespace BusinessSafe.MessageHandlers.Emails.MessageHandlers
{
    public class SendTechnicalSupportEmailHandler : IHandleMessages<SendTechnicalSupportEmail>
    {
        private readonly IEmailSender _emailSender;

        public SendTechnicalSupportEmailHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public void Handle(SendTechnicalSupportEmail command)
        {
            
            var viewModel = new SendTechnicalSupportEmailViewModel()
                                {
                                    Name = command.Name,
                                    From = command.FromEmailAddress,
                                    Subject = "BusinessSafe Online Enquiry",
                                    To = "support@peninsula-online.com",
                                    Message = command.Message
                                };

            
            var email = CreateRazorEmailResult(viewModel);

            _emailSender.Send(email);

            Log4NetHelper.Log.Info("SendTechnicalSupportEmail Command Handled");
        }

        protected virtual RazorEmailResult CreateRazorEmailResult(SendTechnicalSupportEmailViewModel viewModel)
        {
            var email = new MailerController().SendTechnicalSupportEmail(viewModel);
            return email;
        }
    }
}