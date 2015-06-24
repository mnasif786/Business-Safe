using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.ActionPlan;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.ActionPlans.Tasks;
using BusinessSafe.WebSite.Areas.ActionPlans.ViewModels;
using Moq;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.ActionPlan
{
    [TestFixture]
    [Category("Unit")]
    public class AssignActionPlanCommandTests
    {
        private Mock<IActionService> _actionService;
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;
      [SetUp]
        public void Setup()
        {
            _actionService = new Mock<IActionService>();
            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();
        }

        [Test]
        public void Given_Model_When_Create_Then_Result_Is_Correct_Type()
        {
            //given
            var companyId = 1234L;
            var target = GetTarget();
            
            //when
            var command = target.WithCompanyId(companyId);
            //then
            Assert.That(command,Is.TypeOf<AssignActionPlanTaskCommand>());
        }

        [Test]
        public void Given_Command_When_Execute_Then_Maps_Request()
        {
            //given
            var companyId = 1234L;
            var actionId = 1L;
            var assignedTo = Guid.NewGuid();
            DateTime? dueDate = DateTime.Now.Date;
            bool sendNotifications = true;
            string title = "Area of non compliance";
            string description = "Description of problem";

            _actionService.
          Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>())).
          Returns(new ActionDto());

            var target =
                GetTarget()
                    .WithCompanyId(companyId)
                    .WithActionId(actionId)
                    .WithAssignedTo(assignedTo)
                    .WithDueDate(dueDate.Value.ToShortDateString())
                    .WithSendTaskNotification(sendNotifications)
                    .WithSendTaskCompletedNotification(sendNotifications)
                    .WithSendTaskOverdueNotification(sendNotifications)
                    .WithAreaOfNonCompliance(title)
                    .WithActionRequired(description);
            
            //when
            target.Execute();

            //then
            _actionService.Verify(x => x.AssignActionTask(It.Is<AssignActionTaskRequest>(y => y.CompanyId == companyId)));
            _actionService.Verify(x => x.AssignActionTask(It.Is<AssignActionTaskRequest>(y => y.ActionId == actionId)));
            _actionService.Verify(x => x.AssignActionTask(It.Is<AssignActionTaskRequest>(y => y.AssignedTo == assignedTo)));
            _actionService.Verify(x => x.AssignActionTask(It.Is<AssignActionTaskRequest>(y => y.DueDate == dueDate)));
            _actionService.Verify(x => x.AssignActionTask(It.Is<AssignActionTaskRequest>(y => y.SendTaskNotification == sendNotifications)));
            _actionService.Verify(x => x.AssignActionTask(It.Is<AssignActionTaskRequest>(y => y.SendTaskCompletedNotification == sendNotifications)));
            _actionService.Verify(x => x.AssignActionTask(It.Is<AssignActionTaskRequest>(y => y.SendTaskOverdueNotification == sendNotifications)));
            _actionService.Verify(x => x.AssignActionTask(It.Is<AssignActionTaskRequest>(y => y.AreaOfNonCompliance == title)));
            _actionService.Verify(x => x.AssignActionTask(It.Is<AssignActionTaskRequest>(y => y.ActionRequired == description)));
          }

        [Test]
        public void Given_Assigned_To_Modified_And_Send_Task_Notificaton_When_Execute_Then_SendTaskAssignedEmail_Is_Called()
        {
            //given
            var companyId = 1234L;
            var actionId = 1L;
            var assignedTo = Guid.NewGuid();
            DateTime? dueDate = DateTime.Now.Date;
            bool sendNotifications = true;
            string title = "Area of non compliance";
            string description = "Description of problem";
            var action = new ActionDto
                             {
                                 Id = default(long),
                                 ActionTasks = new List<ActionTaskDto>
                                                   {
                                                       new ActionTaskDto
                                                           {
                                                               Id = default(long),
                                                               TaskAssignedTo = new EmployeeDto
                                                                                    {
                                                                                        Id = Guid.NewGuid()
                                                                                    }
                                                           }
                                                   }
                             };

            _actionService.
                Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>())).
                Returns(action);

            var target =
                GetTarget()
                    .WithCompanyId(companyId)
                    .WithActionId(actionId)
                    .WithAssignedTo(assignedTo)
                    .WithDueDate(dueDate.Value.ToShortDateString())
                    .WithSendTaskNotification(sendNotifications)
                    .WithSendTaskCompletedNotification(sendNotifications)
                    .WithSendTaskOverdueNotification(sendNotifications)
                    .WithAreaOfNonCompliance(title)
                    .WithActionRequired(description);

            //when
            target.Execute();

            //then
            _actionService.Verify(x => x.SendTaskAssignedEmail(It.IsAny<long>(), It.IsAny<long>()));
            
        }

        [Test]
        public void Given_Assigned_To_Not_Modified_And_Send_Task_Notificaton_When_Execute_Then_SendTaskAssignedEmail_Is_Not_Called()
        {
            //given
            var companyId = 1234L;
            var actionId = 1L;
            var assignedTo = Guid.NewGuid();
            DateTime? dueDate = DateTime.Now.Date;
            bool sendNotifications = true;
            string title = "Area of non compliance";
            string description = "Description of problem";
            var action = new ActionDto
            {
                Id = default(long),
                ActionTasks = new List<ActionTaskDto>
                                                   {
                                                       new ActionTaskDto
                                                           {
                                                               Id = default(long),
                                                               TaskAssignedTo = new EmployeeDto
                                                                                    {
                                                                                        Id = assignedTo
                                                                                    }
                                                           }
                                                   }
            };

            _actionService.
                Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>())).
                Returns(action);

            var target =
                GetTarget()
                    .WithCompanyId(companyId)
                    .WithActionId(actionId)
                    .WithAssignedTo(assignedTo)
                    .WithDueDate(dueDate.Value.ToShortDateString())
                    .WithSendTaskNotification(sendNotifications)
                    .WithSendTaskCompletedNotification(sendNotifications)
                    .WithSendTaskOverdueNotification(sendNotifications)
                    .WithAreaOfNonCompliance(title)
                    .WithActionRequired(description);

            //when
            target.Execute();

            //then
            _actionService.Verify(x => x.SendTaskAssignedEmail(It.IsAny<long>(), It.IsAny<long>()), Times.Never());

        }

        private IAssignActionPlanTaskCommand GetTarget()
        {
            return new AssignActionPlanTaskCommand(_actionService.Object, _businessSafeSessionManager.Object);
        }
    }

}
