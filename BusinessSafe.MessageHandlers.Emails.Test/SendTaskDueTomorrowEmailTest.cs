using System;
using System.Collections.Generic;
using System.Linq;
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
    [TestFixture]
    public class SendTaskDueTomorrowEmailTest
    {
        private Mock<IEmailSender> _emailSender;
        private Mock<IBusinessSafeEmailLinkBaseUrlConfiguration> _urlConfig;
        private SendTaskDueTomorrowEmail _message;

        [SetUp]
        public void SetUp()
        {
            _emailSender = new Mock<IEmailSender>();
            _urlConfig = new Mock<IBusinessSafeEmailLinkBaseUrlConfiguration>();

            _message = new SendTaskDueTomorrowEmail()
            {
                TaskAssignedBy = "assignedBy"
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

        private SendTaskDueTomorrowEmailHandler CreateTarget()
        {
            var handler = new MySendTaskDueTomorrowEmailHandler(_emailSender.Object, _urlConfig.Object);
            return handler;
        }

        public class MySendTaskDueTomorrowEmailHandler : SendTaskDueTomorrowEmailHandler
        {
            public MySendTaskDueTomorrowEmailHandler(IEmailSender emailSender, IBusinessSafeEmailLinkBaseUrlConfiguration urlConfiguration)
                : base(emailSender, urlConfiguration)
            {
            }

            protected override RazorEmailResult CreateRazorEmailResult(TaskDueTomorrowViewModel viewModel)
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
