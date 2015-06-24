using System.IO;
using System.Linq;
using System.Text;
using ActionMailer.Net.Standalone;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.EmailPusherService;

namespace BusinessSafe.MessageHandlers.Emails.EmailSender
{
    public class EmailSender : IEmailSender
    {
        public void Send(RazorEmailResult email)
        {
            var sr = new StreamReader(email.Mail.AlternateViews[0].ContentStream);
            var body = sr.ReadToEnd();
            email.Mail.AlternateViews[0].ContentStream.Position = 0;

            const string endpointConfigurationName = "EmailPusherService_basicHttpBinding";
            IEmailPusherService emailPusher = new EmailPusherServiceClient(endpointConfigurationName);
            
            Log4NetHelper.Log.Debug("EmailSenderHelper EmailPusherServiceClient Binding : " + endpointConfigurationName);
            
            emailPusher.AddNewMessageGetAddNewMessageResponse(email.Mail.To.Select(x => x.Address).ToArray(),
                                                              new string[0],
                                                              new string[0],
                                                              email.Mail.Subject,
                                                              body,
                                                              email.Mail.From.Address
                );

            
            Log4NetHelper.Log.Info("EmailSenderHelper Send Completed For Email : " + string.Join(";", email.Mail.To.Select(x => x.Address) )); 
        }

        // not a suitable holder in email type for our use of attachment
        public void SendWithDifferentAttachmentName(RazorEmailResult email, AttachmentType[] attachments)
        {
            var sr = new StreamReader(email.Mail.AlternateViews[0].ContentStream);
            var body = sr.ReadToEnd();
            email.Mail.AlternateViews[0].ContentStream.Position = 0;

            const string endpointConfigurationName = "EmailPusherService_basicHttpBinding";
            IEmailPusherService emailPusher = new EmailPusherServiceClient(endpointConfigurationName);

            Log4NetHelper.Log.Debug("EmailSenderHelper EmailPusherServiceClient Binding : " + endpointConfigurationName);
            
            var response = emailPusher.AddNewMessageWithAttachmentNameGetAddNewMessageResponse(email.Mail.To.Select(x => x.Address).ToArray(),
                                                              email.Mail.CC.Select(x => x.ToString()).ToArray(),
                                                              attachments,
                                                              email.Mail.Subject,
                                                              body,
                                                              email.Mail.From.Address
                );

            Log4NetHelper.Log.Info("EmailSenderHelper Send Completed For Email : " + string.Join( ";", email.Mail.To.Select(x => x.Address) ) + "messageId" + response.MessageId); 
        }
    }

    
}