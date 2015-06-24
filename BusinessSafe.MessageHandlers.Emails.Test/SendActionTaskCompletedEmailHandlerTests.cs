using System;
using System.Collections.Generic;
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
    public class SendActionTaskCompletedEmailHandlerTests
    {
        private Mock<IEmailSender> _emailSender;
        private Mock<IBusinessSafeEmailLinkBaseUrlConfiguration> _urlConfiguration;
        private Mock<ITasksRepository> _taskRepository;
        private Mock<IUserRepository> _userRepository;
        private ActionTask _task;
        private BusinessSafe.Domain.Entities.Action _action;

        [SetUp]
        public void SetUp()
        {
            _emailSender = new Mock<IEmailSender>();
            _userRepository = new Mock<IUserRepository>();
            _taskRepository = new Mock<ITasksRepository>();

            _urlConfiguration = new Mock<IBusinessSafeEmailLinkBaseUrlConfiguration>();
            _urlConfiguration.Setup(x => x.GetBaseUrl()).Returns(string.Empty);
            _userRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new User() 
                { Employee = new Employee() 
                    { ContactDetails = new List<EmployeeContactDetail>() 
                        { new EmployeeContactDetail() {Email = "sendto@test.com"}} } });

            _action = new BusinessSafe.Domain.Entities.Action()
            {
                ActionPlan = new ActionPlan(),
                ActionPlanId = 1,
                Id = 1,
                Category = ActionCategory.Action,
                ActionRequired = "You required this action",
            };

            _task = new ActionTask()
            {
                TaskAssignedTo = new Employee
                {
                    ContactDetails = new List<EmployeeContactDetail>() { new EmployeeContactDetail() { Email = "assignedto@test.com" } }
                },
                CreatedBy = new UserForAuditing() { Id = Guid.NewGuid(),  },
                Title = "Action task title",
                Reference = "Action task reference",
                Action = _action,
            };

        }

        [Test]
        public void When_handle_Then_should_call_correct_methods()
        {
            // Given
            var handler = CreateTarget();
            var message = new SendActionTaskCompletedEmail()
                              {
                                  TaskGuid = Guid.NewGuid()
                              };

            _taskRepository.Setup(x => x.GetByTaskGuid(message.TaskGuid))
               .Returns(() => _task);

            // When
            handler.Handle(message);

            // Then
            _emailSender.Verify(x => x.Send(It.IsAny<RazorEmailResult>()));
        }

        private MySendActionTaskCompletedEmailHandler CreateTarget()
        {
            var handler = new MySendActionTaskCompletedEmailHandler(_emailSender.Object, _urlConfiguration.Object, _taskRepository.Object, _userRepository.Object);
            return handler;
        }
    }

    public class MySendActionTaskCompletedEmailHandler : SendTaskActionCompletedEmailHandler
    {
        public MySendActionTaskCompletedEmailHandler(IEmailSender emailSender, IBusinessSafeEmailLinkBaseUrlConfiguration urlConfiguration, ITasksRepository taskRepository,
            IUserRepository userRepository)
            : base(emailSender, urlConfiguration, taskRepository, userRepository)
        {
        }

        protected override RazorEmailResult CreateRazorEmailResult(ActionTaskCompletedViewModel viewModel)
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