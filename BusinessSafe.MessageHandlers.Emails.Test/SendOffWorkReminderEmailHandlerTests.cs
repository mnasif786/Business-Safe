using System;
using System.Net.Mail;
using System.Text;
using ActionMailer.Net;
using ActionMailer.Net.Standalone;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.MessageHandlers;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.MessageHandlers.Emails.Test
{
    public class SendOffWorkReminderEmailHandlerTests
    {
        private Mock<IEmailSender> _emailSender;
        private Mock<IBusinessSafeEmailLinkBaseUrlConfiguration> _urlConfiguration;
        private Mock<IAccidentRecordRepository> _accidentRecordRepository;

        [SetUp]
        public void SetUp()
        {
            _emailSender = new Mock<IEmailSender>();
            _accidentRecordRepository = new Mock<IAccidentRecordRepository>();
            _urlConfiguration = new Mock<IBusinessSafeEmailLinkBaseUrlConfiguration>();
            _urlConfiguration.Setup(x => x.GetBaseUrl()).Returns(string.Empty);
        }

        [Test]
        public void When_handle_Then_should_call_correct_methods()
        {
            // Given
            var handler = GetTarget();
            var message = new SendOffWorkReminderEmail
            {
                AccidentRecordId = 1L,
                RecipientEmail = "Test@test.com",
                Title = "Test",
                AccidentRecordReference = "AR99",
                DateOfAccident = DateTime.Now
            };

            AccidentRecord accidentRecord = new AccidentRecord
            {
                EmployeeInjured = new Employee { Forename = "Injured", Surname = "Person" },
                DateAndTimeOfAccident = DateTime.Now,
                Jurisdiction = new Jurisdiction { Name = "GB" }
            };

            _accidentRecordRepository.Setup(x => x.GetById(It.IsAny<long>())).Returns(accidentRecord);

            // When
            handler.Handle(message);

            // Then
            _emailSender.Verify(x => x.Send(It.IsAny<RazorEmailResult>()));
        }

        private FakeSendOffWorkReminderEmailHandler GetTarget()
        {
            var handler = new FakeSendOffWorkReminderEmailHandler(_emailSender.Object, _urlConfiguration.Object, _accidentRecordRepository.Object);
            return handler;
        }
    }

    public class FakeSendOffWorkReminderEmailHandler : SendOffWorkReminderEmailHandler
    {
        public FakeSendOffWorkReminderEmailHandler(IEmailSender emailSender, IBusinessSafeEmailLinkBaseUrlConfiguration urlConfiguration, IAccidentRecordRepository accidentRecordRepository)
            : base(emailSender, urlConfiguration, accidentRecordRepository)
        {
        }
        protected override RazorEmailResult CreateRazorEmailResult(OffWorkReminderViewModel viewModel)
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