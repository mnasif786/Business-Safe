using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using ActionMailer.Net;
using ActionMailer.Net.Standalone;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using BusinessSafe.MessageHandlers.Emails.MessageHandlers;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.MessageHandlers.Emails.Test
{
    [TestFixture]
    internal class SendSafeCheckChecklistCompletedEmailTests
    {
        private Mock<IEmailSender> _emailSender;
        private Mock<ISafeCheckEmailLinkBaseUrlConfiguration> _urlConfiguration;
        private Mock<ICheckListRepository> _checklistRepository;
        private Mock<IQaAdvisorRepository> _qaRepository;
        private Checklist _checklist;

        [SetUp]
        public void SetUp()
        {
            _emailSender = new Mock<IEmailSender>();
            _checklistRepository = new Mock<ICheckListRepository>();
            _qaRepository = new Mock<IQaAdvisorRepository>();

            _urlConfiguration = new Mock<ISafeCheckEmailLinkBaseUrlConfiguration>();
            _urlConfiguration.Setup(x => x.GetBaseUrl()).Returns(string.Empty);

            var checklistId = Guid.NewGuid();
            var assignedToId = Guid.NewGuid();
            _checklist = new Checklist() {ClientId = 1111, Id = checklistId, ChecklistCompletedBy = "Gareth Wilby"};

            _checklistRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(_checklist);

            var qaAdvisor = new QaAdvisor() {Id = Guid.NewGuid()};
            _qaRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(qaAdvisor);
        }
        
        [Test]
        public void When_handle_Then_should_call_correct_methods()
        {
            // Given
            var handler = CreateTarget();
            var message = new SendSafeCheckChecklistCompletedEmail()
            {
                ChecklistId = _checklist.Id,
                Can = "Demo001",
                Postcode = "M1 1AA"
            };

            // When
            handler.Handle(message);

            // Then
            _emailSender.Verify(x => x.Send(It.IsAny<RazorEmailResult>()));
        }
        
        private SendSafeCheckChecklistCompletedEmailHandler CreateTarget()
        {
            var handler = new MySendSafeCheckChecklistCompletedEmailHandler(_emailSender.Object, _urlConfiguration.Object, _checklistRepository.Object, _qaRepository.Object);
            return handler;
        }
    }

    public class MySendSafeCheckChecklistCompletedEmailHandler : SendSafeCheckChecklistCompletedEmailHandler
    {
        public MySendSafeCheckChecklistCompletedEmailHandler(IEmailSender emailSender, ISafeCheckEmailLinkBaseUrlConfiguration urlConfiguration, ICheckListRepository checklistRepository,
            IQaAdvisorRepository qaRepository)
            : base(emailSender, urlConfiguration, checklistRepository, qaRepository)
        {
        }

        protected override RazorEmailResult CreateRazorEmailResult(SafeCheckChecklistCompletedViewModel viewModel)
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
