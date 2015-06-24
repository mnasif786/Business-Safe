using System.Net.Mail;
using System.Text;
using ActionMailer.Net;
using ActionMailer.Net.Standalone;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.MessageHandlers;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.MessageHandlers.Emails.Test
{
    public class SendResponsibilityTaskCompletedEmailHandlerTests
    {
        private Mock<IEmailSender> _emailSender;
        private Mock<IBusinessSafeEmailLinkBaseUrlConfiguration> _urlConfiguration;

        [SetUp]
        public void SetUp()
        {
            _emailSender = new Mock<IEmailSender>();

            _urlConfiguration = new Mock<IBusinessSafeEmailLinkBaseUrlConfiguration>();
            _urlConfiguration.Setup(x => x.GetBaseUrl()).Returns(string.Empty);
        }

        [Test]
        public void When_handle_Then_should_call_correct_methods()
        {
            // Given
            var handler = CreateTarget();
            var message = new SendResponsibilityTaskCompletedEmail
                              {
                                  TaskReference = "reference",
                                  Title = "title",
                                  Description = "description",
                                  ResponsibilityOwnerEmail = "owner email",
                                  ResponsibilityOwnerName = "owner name",
                                  TaskAssignedTo = "assigned to",
                                  CompletedBy = "completed by"
                              };

            // When
            handler.Handle(message);

            // Then
            _emailSender.Verify(x => x.Send(It.IsAny<RazorEmailResult>()));
        }

        private MySendResponsbilityTaskCompletedEmailHandler CreateTarget()
        {
            var handler = new MySendResponsbilityTaskCompletedEmailHandler(_emailSender.Object, _urlConfiguration.Object);
            return handler;
        }
    }

    public class MySendResponsbilityTaskCompletedEmailHandler : SendResponsibilityTaskCompletedEmailHandler
    {
        public MySendResponsbilityTaskCompletedEmailHandler(IEmailSender emailSender, IBusinessSafeEmailLinkBaseUrlConfiguration urlConfiguration)
            : base(emailSender, urlConfiguration)
        {
        }
        
        protected override RazorEmailResult CreateRazorEmailResult(ResponsibilityTaskCompletedViewModel viewModel)
        {
            return new RazorEmailResult(new Mock<IMailInterceptor>().Object,
                                        new Mock<IMailSender>().Object,
                                        new Mock<MailMessage>().Object,
                                        "ViewName",
                                        Encoding.ASCII,
                                        "ViewPath");
        }
    }
}