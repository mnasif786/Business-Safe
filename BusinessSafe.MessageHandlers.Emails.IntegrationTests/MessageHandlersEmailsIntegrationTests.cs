using BusinessSafe.MessageHandlers.Emails.IntegrationTests.EmailPusherService;
using NUnit.Framework;

namespace BusinessSafe.MessageHandlers.Emails.IntegrationTests
{
    [TestFixture]
    public class MessageHandlersEmailsIntegrationTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void SendEmailWithoutAttachments()
        {
            const string endpointConfigurationName = "EmailPusherService_basicHttpBinding";
            IEmailPusherService emailPusher = new EmailPusherServiceClient(endpointConfigurationName);

            var recipients = new string[1] { "BusinessSafeProject@peninsula-uk.com" };
            var attachments = new string[0];

            var response = emailPusher.AddNewMessageGetAddNewMessageResponse(recipients, null, attachments, "subject",
                    "body", "BusinessSafeProject@peninsula-uk.com");

            // user this runs under probably wont hava access to file so error is correct, shows web services is working correctly
            Assert.That(response.MessageId, Is.GreaterThan(0));
        }

        [Test]
        public void SendEmailWithAttachments()
        {
            const string endpointConfigurationName = "EmailPusherService_basicHttpBinding";
            IEmailPusherService emailPusher = new EmailPusherServiceClient(endpointConfigurationName);

            var recipients = new string[1] { "BusinessSafeProject@peninsula-uk.com" };
            var attachments = new string[1] { @"\\pbsw23it\it\1.jpg" };
            
            var response = emailPusher.AddNewMessageGetAddNewMessageResponse(recipients, null, attachments, "subject",
                "body", "BusinessSafeProject@peninsula-uk.com");

            // user this runs under probably wont hava access to file so error is correct, shows web services is working correctly
            Assert.That(response.ErrorList[0], Is.EqualTo(@"File Doesnt Exist : \\pbsw23it\it\1.jpg"));
        }

        [Test]
        public void SendEmailWithAttachmentDifferentName()
        {
            const string endpointConfigurationName = "EmailPusherService_basicHttpBinding";
            IEmailPusherService emailPusher = new EmailPusherServiceClient(endpointConfigurationName);

            var recipients = new string[1] { "BusinessSafeProject@peninsula-uk.com" };
            var cc = new string[1] { "BusinessSafeProject@peninsula-uk.com" };
            var attachments = new string[1] { @"\\pbsw23it\it\1.jpg" };
            var attachmentName = new AttachmentType[1] {new AttachmentType() {OldFileName = attachments[0], NewFileName = "file1.jpg"}};

           
             var response = emailPusher.AddNewMessageWithAttachmentNameGetAddNewMessageResponse(recipients, null, attachmentName, "subject",
                    "body", "BusinessSafeProject@peninsula-uk.com");

             // user this runs under probably wont hava access to file so error is correct, shows web services is working correctly
             Assert.That(response.ErrorList[0], Is.EqualTo(@"File Doesnt Exist : \\pbsw23it\it\1.jpg"));
        }

        [Test]
        public void SendEmailWithAttachmentNull()
        {
            const string endpointConfigurationName = "EmailPusherService_basicHttpBinding";
            IEmailPusherService emailPusher = new EmailPusherServiceClient(endpointConfigurationName);

            var recipients = new string[1] { "BusinessSafeProject@peninsula-uk.com" };
               
            var response = emailPusher.AddNewMessageWithAttachmentNameGetAddNewMessageResponse(recipients, null, null, "subject",
                    "body", "BusinessSafeProject@peninsula-uk.com");

             // user this runs under probably wont hava access to file so error is correct, shows web services is working correctly
             Assert.That(response.MessageId, Is.GreaterThan(0));
       }
     }
}
