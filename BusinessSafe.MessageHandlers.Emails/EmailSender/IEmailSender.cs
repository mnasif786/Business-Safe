using ActionMailer.Net.Standalone;
using BusinessSafe.MessageHandlers.Emails.EmailPusherService;

namespace BusinessSafe.MessageHandlers.Emails.EmailSender
{
    public interface IEmailSender
    {
        void Send(RazorEmailResult email);
        void SendWithDifferentAttachmentName(RazorEmailResult email, AttachmentType[] attachments);
    }
}