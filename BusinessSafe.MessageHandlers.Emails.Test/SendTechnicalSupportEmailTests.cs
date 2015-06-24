using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using ActionMailer.Net;
using ActionMailer.Net.Standalone;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.MessageHandlers;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.MessageHandlers.Emails.Test
{
    [TestFixture]
    public class SendTechnicalSupportEmailTests
    {
        private Mock<IEmailSender> _emailSender;
        private SendTechnicalSupportEmail _message;
        
        [SetUp]
        public void SetUp()
        {
            _emailSender = new Mock<IEmailSender>();
           
            _message = new SendTechnicalSupportEmail()
            {
                Message = "My Message",
                Name = "Abc",
                FromEmailAddress = "abc@abc.com"
            };
        }
        
        [Test]
        public void When_handle_Then_should_call_correct_methods()
        {
            //Given
            var handler = CreateTarget();

            // When
            handler.Handle(_message);
            
            // Then
            _emailSender.Verify(x => x.Send(It.IsAny<RazorEmailResult>()));
        }
        
        private SendTechnicalSupportEmailHandler CreateTarget()
        {
            var handler = new MySendTechnicalSupportEmailHandler(_emailSender.Object);
            return handler;
        }

        public class MySendTechnicalSupportEmailHandler : SendTechnicalSupportEmailHandler
        {
            public MySendTechnicalSupportEmailHandler(IEmailSender emailSender)
                : base(emailSender)
            {
            }

            protected override RazorEmailResult CreateRazorEmailResult(SendTechnicalSupportEmailViewModel viewModel)
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
