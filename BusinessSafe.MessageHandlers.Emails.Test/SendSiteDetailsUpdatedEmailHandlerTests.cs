using System.Net.Mail;
using System.Text;
using ActionMailer.Net;
using ActionMailer.Net.Standalone;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.MessageHandlers;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.MessageHandlers.Emails.Test
{
    [TestFixture]
    public class SendSiteDetailsUpdatedEmailHandlerTests
    {
        private Mock<IEmailSender> _emailSender;

        [SetUp]
        public void SetUp()
        {
            _emailSender = new Mock<IEmailSender>();
        }

        [Test]
        public void When_handle_Then_should_call_correct_methods()
        {
            //Given
            var handler = CreateTarget();
            var message = new SendSiteDetailsUpdatedEmail();

            //When
            handler.Handle(message);

            //Then
            _emailSender.Verify(x => x.Send(It.IsAny<RazorEmailResult>()));
        }

        private MySendSiteDetailsUpdatedEmailHandler CreateTarget()
        {
            var handler = new MySendSiteDetailsUpdatedEmailHandler(_emailSender.Object);
            return handler;
        }
    }

    public class MySendSiteDetailsUpdatedEmailHandler : SendSiteDetailsUpdatedEmailHandler
    {
        public MySendSiteDetailsUpdatedEmailHandler(IEmailSender emailSender)
            : base(emailSender)
        {
        }

        protected override RazorEmailResult CreateRazorEmailResult(SendSiteDetailsUpdatedEmailViewModel viewModel)
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
