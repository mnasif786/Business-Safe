using System;
using System.Collections.Generic;

using BusinessSafe.Application.Contracts.Checklists;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request.Checklists;
using BusinessSafe.MessageHandlers.CommandHandlers;
using BusinessSafe.Messages.Commands;
using BusinessSafe.Messages.Commands.GenerateEmployeeChecklistEmailsParameters;
using BusinessSafe.Messages.Events;

using Moq;

using NServiceBus;

using NUnit.Framework;

namespace BusinessSafe.MessageHandlers.Emails.Test
{
    [TestFixture]
    public class GenerateEmployeeChecklistEmailsHandlerTests
    {
        private Mock<IEmployeeChecklistEmailService> _emailChecklistEmailService;
        private Mock<IBus> _bus;

        [SetUp]
        public void SetUp()
        {
            _emailChecklistEmailService = new Mock<IEmployeeChecklistEmailService>();
            _bus = new Mock<IBus>();
        }

        [Test]
        public void When_handle_Then_should_call_correct_methods()
        {
            //Given
            var handler = CreateTarget();

            var message = new GenerateEmployeeChecklistEmails()
                              {
                                  Message = "Hello Message",
                                  RequestEmployees = new List<EmployeeWithNewEmail>
                                                         {
                                                             new EmployeeWithNewEmail()
                                                                 {
                                                                     EmployeeId = Guid.NewGuid()
                                                                 }
                                                         },
                                  ChecklistIds = new List<long>() { 1 },
                                  GeneratingUserId = Guid.NewGuid()
                              };

            var ids = new List<Guid>()
                          {
                              Guid.NewGuid()
                          };
            _emailChecklistEmailService
                .Setup(x => x.Generate(It.Is<GenerateEmployeeChecklistEmailRequest>(y =>
                                                                                    y.GeneratingUserId == message.GeneratingUserId &&
                                                                                    y.Message == message.Message &&
                                                                                    y.RequestEmployees.Count == message.RequestEmployees.Count &&
                                                                                    y.ChecklistIds == message.ChecklistIds)))
                .Returns(ids);

            var employeeChecklistEmailDto = new EmployeeChecklistEmailDto()
                                                {
                                                    RecipientEmail = "test@hotmail.com",
                                                    EmployeeChecklists =new[]{new EmployeeChecklistDto()},
                                                    Message = message.Message
                                                };
            var emailsChecklistEmails = new[] { employeeChecklistEmailDto };
            _emailChecklistEmailService
                .Setup(x => x.GetByIds(ids))
                .Returns(emailsChecklistEmails);

            //When
            handler.Handle(message);

            //Then
            _emailChecklistEmailService.VerifyAll();
            _bus.Verify(x => x.Publish(It.Is<EmployeeChecklistEmailGenerated>(y => y.Message == message.Message 
                                                                                && y.RecipientEmail == employeeChecklistEmailDto.RecipientEmail 
                                                                                && y.EmployeeChecklistEmailId == employeeChecklistEmailDto.Id
                                                                                )));
        }

        private GenerateEmployeeChecklistEmailsHandler CreateTarget()
        {
            var handler = new GenerateEmployeeChecklistEmailsHandler(_emailChecklistEmailService.Object, _bus.Object);
            return handler;
        }
    }


}
