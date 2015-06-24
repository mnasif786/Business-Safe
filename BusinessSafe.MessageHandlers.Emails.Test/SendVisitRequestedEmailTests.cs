using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SendVisitRequestedEmailTests
    {
        private Mock<IEmailSender> _emailSender;
        private SendVisitRequestedEmail _message;

        [SetUp]
        public void Setup()
        {
            _emailSender = new Mock<IEmailSender>();

            _message = new SendVisitRequestedEmail() { };
        }

        [Test]
        public void Given_SendVistRequested_message_When_handled_Then_email_is_sent()
        {
            //Given
            var handler = CreateTarget();

            //RazorEmailResult result = null;
            //_emailSender.Setup(x => x.Send(It.IsAny<RazorEmailResult>()))
            //    .Callback(
            //        delegate(RazorEmailResult x)
            //        {
            //            result = x;
            //        }
            //    );

            // When
            handler.Handle(_message);

            // Then
           _emailSender.Verify(x => x.Send(It.IsAny<RazorEmailResult>()));
        }

        private SendVisitRequestedEmailHandler CreateTarget()
        {
            return new MySendVisitRequestedEmailHandler(_emailSender.Object);            
        }
     

        public class MySendVisitRequestedEmailHandler : SendVisitRequestedEmailHandler
        {
            public MySendVisitRequestedEmailHandler(IEmailSender emailSender)
                : base(emailSender)
            {
            }

            protected override RazorEmailResult CreateRazorEmailResult(VisitRequestedViewModel viewModel)
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
}
