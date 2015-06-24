using System;
using System.Collections.Generic;
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
    public class SendEmployeeDigestEmailTests
    {
        private Mock<IEmailSender> _emailSender;
        private Mock<IBusinessSafeEmailLinkBaseUrlConfiguration> _businesSafeEmailLinkBaseUrlConfiguration;

        [SetUp]
        public void SetUp()
        {
            _emailSender = new Mock<IEmailSender>();
            _businesSafeEmailLinkBaseUrlConfiguration = new Mock<IBusinessSafeEmailLinkBaseUrlConfiguration>();
        }

        [Test]
        public void When_handle_then_should_call_Are_correct_methods()
        {
            //Given
            var handler = CreateTarget();

            var message = new SendEmployeeDigestEmail()
            {
                RecipientEmail = "hot@hotmail.com",
                GeneralRiskAssessmentsOverdueTasks = CreateRiskAssessmentsOverdueTasks(2),
                FireRiskAssessmentsOverdueTasks = CreateRiskAssessmentsOverdueTasks(2),
            };

            //When
            handler.Handle(message);

            //Then
            _emailSender.Verify(x => x.Send(It.IsAny<RazorEmailResult>()), Times.AtLeast(1));
        }

        private MySendEmployeeDigestEmailHandler CreateTarget()
        {
            var handler = new MySendEmployeeDigestEmailHandler(_emailSender.Object, _businesSafeEmailLinkBaseUrlConfiguration.Object);
            return handler;
        }

        private List<TaskDetails> CreateRiskAssessmentsOverdueTasks(int numberOfTasksToGenerate)
        {
            var taskDetailList = new List<TaskDetails>();
            for (int i = 1; i <= numberOfTasksToGenerate; i++)
            {
                 taskDetailList.Add(new TaskDetails()
                 {
                     TaskReference = "Task Ref",
                     RiskAssesmentReference = "Risk Ass Reference",
                     Title = "Overdue Task Title " + i,
                     Description = "Task Description " + i,
                     RiskAssessor = "Risk Assessor " + i,
                     TaskAssignedTo = "Task Assigned To " + i,
                     CompletionDueDate = DateTime.Now,
                     CompletedDate = null
                 });     
            }

            return taskDetailList;
        }
    }

    public class MySendEmployeeDigestEmailHandler : SendEmployeeDigestEmailHandler
    {
        public MySendEmployeeDigestEmailHandler(IEmailSender emailSender, IBusinessSafeEmailLinkBaseUrlConfiguration urlConfiguration)
            : base(emailSender, urlConfiguration)
        {
        }

        protected override RazorEmailResult CreateRazorEmailResult(SendEmployeeDigestEmailViewModel viewModel)
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
