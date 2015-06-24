using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.Checklists;
using BusinessSafe.Application.Request.Checklists;
using BusinessSafe.Domain.Entities;
using BusinessSafe.MessageHandlers.CommandHandlers;
using BusinessSafe.Messages.Commands;
using BusinessSafe.Messages.Events;
using Moq;
using NServiceBus;
using NUnit.Framework;

namespace BusinessSafe.MessageHandlers.Emails.Test
{
    [TestFixture]
    public class ResendEmployeeChecklistEmailHandlerTests
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

            var message = new ResendEmployeeChecklistEmail()
                              {
                                 EmployeeChecklistId = Guid.NewGuid(),
                                 ResendUserId = Guid.NewGuid(),
                                 RiskAssessmentId = 500
                              };

            var employeeChecklistEmail = new Mock<EmployeeChecklistEmail>();
            employeeChecklistEmail.Setup(x => x.RecipientEmail).Returns("Hey@hotmail.com");
            employeeChecklistEmail.Setup(x => x.Id).Returns(Guid.NewGuid());
            employeeChecklistEmail.Setup(x => x.Message).Returns("Hello Message");
            employeeChecklistEmail.Setup(x => x.EmployeeChecklists).Returns(new List<EmployeeChecklist>()
                                                                   {
                                                                       new EmployeeChecklist()
                                                                           {
                                                                               Id = message.EmployeeChecklistId
                                                                           }
                                                                   });

            _emailChecklistEmailService.Setup(
                x =>
                x.Regenerate(
                    It.Is<ResendEmployeeChecklistEmailRequest>(
                        y =>
                        y.EmployeeChecklistId == message.EmployeeChecklistId &&
                        y.RiskAssessmentId == message.RiskAssessmentId && 
                        y.ResendUserId == message.ResendUserId))).Returns(employeeChecklistEmail.Object); 
            

            //When
            handler.Handle(message);

            //Then
            _emailChecklistEmailService.VerifyAll();
            _bus.Verify(x => x.Publish(It.Is<EmployeeChecklistEmailGenerated>(y => y.Message == employeeChecklistEmail.Object.Message
                                                                                && y.RecipientEmail == employeeChecklistEmail.Object.RecipientEmail
                                                                                && y.EmployeeChecklistEmailId == employeeChecklistEmail.Object.Id
                                                                                )));
        }
        
        private ResendEmployeeChecklistEmailHandler CreateTarget()
        {
            var handler = new ResendEmployeeChecklistEmailHandler(_emailChecklistEmailService.Object, _bus.Object);
            return handler;
        }
    }


}
