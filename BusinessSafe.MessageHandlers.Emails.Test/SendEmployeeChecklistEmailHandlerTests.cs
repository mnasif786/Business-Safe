using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using ActionMailer.Net;
using ActionMailer.Net.Standalone;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.EventHandlers;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Events;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.MessageHandlers.Emails.Test
{
    [TestFixture]
    public class SendEmployeeChecklistEmailHandlerTests
    {
        private Mock<IEmailSender> _emailSender;
        private Mock<IBusinessSafeEmailLinkBaseUrlConfiguration> _emailLinkBaseUrlConfiguration;

        [SetUp]
        public void SetUp()
        {
            _emailSender = new Mock<IEmailSender>();
            _emailLinkBaseUrlConfiguration = new Mock<IBusinessSafeEmailLinkBaseUrlConfiguration>();

            _emailLinkBaseUrlConfiguration.Setup(x => x.GetBaseUrl()).Returns(string.Empty);
        }

        [Test]
        public void When_handle_Then_should_call_correct_methods()
        {
            //Given
            var handler = CreateTarget();
            var message = new EmployeeChecklistEmailGenerated()
                              {
                                  EmployeeChecklistIds = new List<Guid>(),
                                  EmployeeChecklistEmailId = Guid.NewGuid(),
                                  Message = "Message",
                                  RecipientEmail = "hot@hotmail.com"
                              };

            //When
            handler.Handle(message);

            //Then
            _emailSender.Verify(x => x.Send(It.IsAny<RazorEmailResult>()));
        }
        
        [Test]
        public void Given_a_message_override_has_not_been_specified_When_Handle_Then_message_override_email_address_is_used()
        {
            //Given
            EmployeeChecklistEmailGeneratedViewModel viewModel = null;

            var handler = new Mock<EmployeeChecklistEmailGeneratedHandler>(new object[] { _emailSender.Object, _emailLinkBaseUrlConfiguration.Object }) { CallBase = true };
            
            handler.Setup(x => x.CreateRazorEmailResult(It.IsAny<EmployeeChecklistEmailGeneratedViewModel>()))
                .Callback<EmployeeChecklistEmailGeneratedViewModel>(x => viewModel = x);

            var message = new EmployeeChecklistEmailGenerated()
            {
                EmployeeChecklistIds = new List<Guid>(),
                EmployeeChecklistEmailId = Guid.NewGuid(),
                Message = "Message",
                RecipientEmail = "hot@hotmail.com"
            };

            //When
            handler.Object.Handle(message);

            //Then
            Assert.AreEqual(message.RecipientEmail, viewModel.To);
        }

        private MySendEmployeeChecklistEmailHandler CreateTarget()
        {
            var handler = new MySendEmployeeChecklistEmailHandler(_emailSender.Object, _emailLinkBaseUrlConfiguration.Object);
            return handler;
        }
    }

    public class MySendEmployeeChecklistEmailHandler : EmployeeChecklistEmailGeneratedHandler
    {
        public MySendEmployeeChecklistEmailHandler(IEmailSender emailSender, IBusinessSafeEmailLinkBaseUrlConfiguration businessSafeEmailLinkBaseUrlConfiguration)
            : base(emailSender, businessSafeEmailLinkBaseUrlConfiguration)
        {
        }

        public override RazorEmailResult CreateRazorEmailResult(EmployeeChecklistEmailGeneratedViewModel viewModel)
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
