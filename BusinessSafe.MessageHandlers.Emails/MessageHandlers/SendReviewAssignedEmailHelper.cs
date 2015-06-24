using BusinessSafe.MessageHandlers.Emails.Controllers;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.ViewModels;

namespace BusinessSafe.MessageHandlers.Emails.MessageHandlers
{
    public class SendReviewAssignedEmailHelper
    {
        public static void SendReviewAssignedEmail(ReviewAssignedEmailViewModel viewModel, IEmailSender emailSender)
        {
            var email = new MailerController().ReviewAssigned(viewModel);
            emailSender.Send(email);
        }
    }
}