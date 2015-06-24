using BusinessSafe.MessageHandlers.Emails.Controllers;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.ViewModels;

namespace BusinessSafe.MessageHandlers.Emails.MessageHandlers
{
    public class SendTaskAssignedEmailHelper
    {
        public static void SendTaskAssignedEmail(TaskAssignedEmailViewModel viewModel, IEmailSender emailSender, bool isActionTask = false)
        {
            var email = (isActionTask) ? 
                new MailerController().ActionTaskAssigned(viewModel) :
                new MailerController().TaskAssigned(viewModel);
            emailSender.Send(email);
        }
    }
}