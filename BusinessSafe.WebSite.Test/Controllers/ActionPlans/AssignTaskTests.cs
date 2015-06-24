using System;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.WebSite.Areas.ActionPlans.Controllers;
using BusinessSafe.WebSite.Areas.ActionPlans.Factories;
using BusinessSafe.WebSite.Areas.ActionPlans.Tasks;
using BusinessSafe.WebSite.Areas.ActionPlans.ViewModels;
using Moq;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.ActionPlans
{
    [TestFixture]
    [Category("Unit")]
    public class AssignTaskTests
    {
        private Mock<IAssignActionPlanTaskCommand> _assignActionPlanTaskCommand;
        private Mock<IReassignActionTaskViewModelFactory> _reassignActionTaskViewModelFactory;
        
        [SetUp]
        public void Setup()
        {
            _assignActionPlanTaskCommand = new Mock<IAssignActionPlanTaskCommand>();
            _reassignActionTaskViewModelFactory = new Mock<IReassignActionTaskViewModelFactory>();
            
            _assignActionPlanTaskCommand
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_assignActionPlanTaskCommand.Object);

            _assignActionPlanTaskCommand
                .Setup(x => x.WithUserId(It.IsAny<Guid>()))
                .Returns(_assignActionPlanTaskCommand.Object);

            _assignActionPlanTaskCommand
                .Setup(x => x.WithActionId(It.IsAny<long>()))
                .Returns(_assignActionPlanTaskCommand.Object);

            _assignActionPlanTaskCommand
                .Setup(x => x.WithAssignedTo(It.IsAny<Guid?>()))
                .Returns(_assignActionPlanTaskCommand.Object);

            _assignActionPlanTaskCommand
                .Setup(x => x.WithDueDate(It.IsAny<string>()))
                .Returns(_assignActionPlanTaskCommand.Object);

            _assignActionPlanTaskCommand
                .Setup(x => x.WithSendTaskNotification(It.IsAny<bool>()))
                .Returns(_assignActionPlanTaskCommand.Object);

            _assignActionPlanTaskCommand
                .Setup(x => x.WithSendTaskCompletedNotification(It.IsAny<bool>()))
                .Returns(_assignActionPlanTaskCommand.Object);

            _assignActionPlanTaskCommand
                .Setup(x => x.WithSendTaskOverdueNotification(It.IsAny<bool>()))
                .Returns(_assignActionPlanTaskCommand.Object);

            _assignActionPlanTaskCommand
              .Setup(x => x.WithSendTaskDueTomorrowNotification(It.IsAny<bool>()))
              .Returns(_assignActionPlanTaskCommand.Object);

            _reassignActionTaskViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_reassignActionTaskViewModelFactory.Object);

            _reassignActionTaskViewModelFactory
                .Setup(x => x.WithActionPlanId(It.IsAny<long>()))
                .Returns(_reassignActionTaskViewModelFactory.Object);

            _reassignActionTaskViewModelFactory
                .Setup(x => x.WithActionId(It.IsAny<long>()))
                .Returns(_reassignActionTaskViewModelFactory.Object);

            _reassignActionTaskViewModelFactory
                .Setup(x => x.GetViewModel()).Returns(new ReassignActionTaskViewModel());
        }


        [Test]
        public void Given_valid_model_When_Assign_Task_Then_Creates_Assign_Task_Command()
        {
            // Given
            var model = new AssignActionPlanTaskViewModel
                            {
                                ActionId = 1L,
                                AssignedTo = Guid.NewGuid(),
                                DueDate = DateTime.Now.ToShortDateString()
                            };

            var target = GetTarget();

            // When
            target.AssignTask(model);

            // Then
            _assignActionPlanTaskCommand.Verify(x => x.WithCompanyId(It.Is<long>(y => y == TestControllerHelpers.CompanyIdAssigned)));
            _assignActionPlanTaskCommand.Verify(x => x.WithActionId(It.Is<long>(y => y == model.ActionId)));
            _assignActionPlanTaskCommand.Verify(x => x.WithAssignedTo(It.Is<Guid?>(y => y == model.AssignedTo)));
            _assignActionPlanTaskCommand.Verify(x => x.WithDueDate(It.Is<string>(y => y == model.DueDate)));
        }

        [Test]
        public void Given_valid_model_When_Assign_Task_Then_Executes_Assign_Task_Command()
        {
            // Given
            var model = new AssignActionPlanTaskViewModel
            {
                ActionId = 1L,
                AssignedTo = Guid.NewGuid(),
                DueDate = DateTime.Now.ToShortDateString()
            };
            var target = GetTarget();
            _assignActionPlanTaskCommand.Setup(x => x.Execute()).Verifiable();
            // When
            target.AssignTask(model);

            // Then
            _assignActionPlanTaskCommand.Verify();
        }

        [Test]
        public void Given_Valid_Model_When_Assign_Task_Then_Return_Json_Ssuccess_Equals_True()
        {
            //given
            var model = new AssignActionPlanTaskViewModel
            {
                ActionId = 1L,
                AssignedTo = Guid.NewGuid(),
                DueDate = DateTime.Now.ToShortDateString()
            };

            var target = GetTarget();

            //when
            dynamic result = target.AssignTask(model);
            //then
            Assert.That(result.Data.Success, Is.True);
        }

        [Test]
        public void Given_Invalid_Model_When_Assign_Task_Then_Modelstate_Contains_Errors()
        {
            //given
            var model = new AssignActionPlanTaskViewModel();
        
            var target = GetTarget();

            //when
            target.AssignTask(model);

            //then
            Assert.That(target.ModelState.IsValid, Is.False);
            Assert.That(target.ModelState.ContainsKey("ActionId"), Is.True);
            Assert.That(target.ModelState.ContainsKey("AssignedTo"), Is.True);
            Assert.That(target.ModelState.ContainsKey("DueDate"), Is.True);
        }


        [Test]
        public void Given_actionid_When_Reassign_Task_Then_Viewmodel_is_correct_type()
        {
            // Given
            var actionId = 1L;
            var actionPlanId = 1L;

            var target = GetTarget();

            // When
            var result = target.Reassign(actionPlanId, actionId);

            // Then
            Assert.That(result.Model, Is.InstanceOf<ReassignActionTaskViewModel>());
        }

        [Test]
        public void Given_actionid_When_Reassign_Task_Then_maps_request()
        {
            // Given
            var actionId = 1L;
            var actionPlanId = 1L;

            var target = GetTarget();

            // When
            var result = target.Reassign(actionPlanId, actionId);

            // Then
            _reassignActionTaskViewModelFactory.Verify(x => x.WithCompanyId(It.Is<long>(y => y == TestControllerHelpers.CompanyIdAssigned)));
            _reassignActionTaskViewModelFactory.Verify(x => x.WithActionId(It.Is<long>(y => y == actionId)));
        }


        private ImmediateRiskNotificationsActionsController GetTarget()
        {
            var controller = new ImmediateRiskNotificationsActionsController(null, null, _assignActionPlanTaskCommand.Object,_reassignActionTaskViewModelFactory.Object, null, null, null);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
