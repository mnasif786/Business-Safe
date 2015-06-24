using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Custom_Exceptions;
using BusinessSafe.Application.Implementations.ActionPlan;
using BusinessSafe.Data.Repository;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Messages.Emails.Commands;
using Moq;
using NServiceBus;
using NUnit.Framework;
using Action = BusinessSafe.Domain.Entities.Action;

namespace BusinessSafe.Application.Tests.Implementations.ActionTaskServiceTests
{
    [TestFixture]
    public class SendNotificationEmailTests
    {

        private Mock<IActionTaskRepository> _actionTaskRepository;
        private Mock<IBus> _bus;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void Setup()
        {
            _actionTaskRepository = new Mock<IActionTaskRepository>();
            _bus = new Mock<IBus>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_Task_And_Company_When_Send_Task_Completed_Notification_Email_Then_Get_Task_From_Repository()
        {
            // Given
            var taskId = 1L;
            var companyId = 1234L;
            
            _actionTaskRepository.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>())).Returns(new ActionTask());
            var target = GetTarget();

            // When
            target.SendTaskCompletedNotificationEmail(taskId,companyId);

            // Then
            _actionTaskRepository.Verify(x => x.GetByIdAndCompanyId(taskId, companyId));
        }

        [Test]
        public void Given_Task_Not_Found_When_Send_Task_Completed_Notification_Email_Then_log_and_throw_exception()
        {
            // Given
            var taskId = 1L;
            var companyId = 1234L;
            _actionTaskRepository.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()));

            // When
            
            var target = GetTarget();

            // Then
            var e = Assert.Throws<ActionTaskNotFoundException>(() => target.SendTaskCompletedNotificationEmail(taskId,companyId));
            _log.Verify(x => x.Add(e));
        }

        [Test]
        public void Given_send_notification_is_required_for_task_When_SendTaskCompletedNotificationEmail_Then_tell_bus_to_send()
        {
            // Given
            var taskId = 1L;
            var taskGuid = Guid.NewGuid();
            var companyId = 1234L;

            Employee newEmployee = new Employee();

            newEmployee.AddContactDetails( new EmployeeContactDetail(){Email = "some_email@testing.com"});
                                       

        _actionTaskRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new ActionTask
                {
                    Id = taskId,
                    TaskGuid = taskGuid,
                    SendTaskCompletedNotification = true,
                    Action = new Action()
                                 {
                                     AssignedTo = newEmployee                                                                                                                                                              
                                 }
                });

            SendActionTaskCompletedEmail expected = null;
            _bus
                .Setup(x => x.Send(It.IsAny<object[]>()))
                .Callback<object[]>(y => expected = y.First() as SendActionTaskCompletedEmail);

            var target = GetTarget();

            // When
            target.SendTaskCompletedNotificationEmail(taskId, companyId);

            // Then
            _bus.Verify(x => x.Send(It.IsAny<object[]>()));
            Assert.That(expected.TaskGuid, Is.EqualTo(taskGuid));
        }

        public ActionTaskService GetTarget()
        {
            return new ActionTaskService( _actionTaskRepository.Object, 
                                            null /*doc parameter*/,
                                            null, // userforauditing,
                                            null, // userRepository,
                                            _bus.Object, 
                                            _log.Object,
                                            null // actionRepository:
                                            );
        }
    }
}
