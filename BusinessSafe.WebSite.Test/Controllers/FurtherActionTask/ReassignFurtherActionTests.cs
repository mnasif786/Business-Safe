using System;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.TaskList.Controllers;
using BusinessSafe.WebSite.Areas.TaskList.ViewModels;
using Moq;
using NServiceBus;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.FurtherActionTask
{
    [TestFixture]
    public class ReassignFurtherActionTests
    {

        private Mock<ITaskService> _taskService;
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;
        private Mock<IBus> _bus;
        [SetUp]
        public void Setup()
        {
            _taskService = new Mock<ITaskService>();
            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();
            _bus = new Mock<IBus>();
        }

        [Test]
        public void When_reassign_further_action_Then_should_call_correct_methods()
        {
            // Given
            var target = CreateController();
            var viewModel = new ReassignTaskViewModel()
                                {
                                    CompanyId = 1,
                                    FurtherControlMeasureTaskId = 4,
                                    ReassignTaskToId = Guid.NewGuid()
                                };

            _taskService
                .Setup(x => x.ReassignTask(It.IsAny<ReassignTaskToEmployeeRequest>()));

            // When
            target.ReassignTask(viewModel);


            // Then
            _taskService
                .Verify(x => x.ReassignTask(It.Is<ReassignTaskToEmployeeRequest>(y => y.GetType() == typeof(ReassignTaskToEmployeeRequest))));
            _taskService
                .Verify(x => x.ReassignTask(It.Is<ReassignTaskToEmployeeRequest>(y => y.CompanyId == viewModel.CompanyId)));
            _taskService
                .Verify(x => x.ReassignTask(It.Is<ReassignTaskToEmployeeRequest>(y => y.TaskId == viewModel.FurtherControlMeasureTaskId)));
            _taskService
                .Verify(x => x.ReassignTask(It.Is<ReassignTaskToEmployeeRequest>(y => y.ReassignTaskToId == viewModel.ReassignTaskToId)));
            _taskService
                .Verify(x => x.ReassignTask(It.Is<ReassignTaskToEmployeeRequest>(y => y.UserId == target.CurrentUser.UserId)));

        }

        [Test]
        public void When_reassign_further_action_with_valid_request_Then_should_return_correct_result()
        {
            // Given
            var target = CreateController();
            var viewModel = new ReassignTaskViewModel();

            // When
            var result = target.ReassignTask(viewModel);

            // Then
            dynamic data = result.Data;
            Assert.That(data.Success, Is.EqualTo(true));
        }

        [Test]
        public void When_invalid_view_model_Then_should_throw_correct_exception()
        {
            // Given
            var target = CreateController();
            var viewModel = new ReassignTaskViewModel();

            target.ModelState.AddModelError("Anything", "Some Error Doesn't Matter");

            // When
            // Then
            Assert.Throws<ArgumentException>(() => target.ReassignTask(viewModel));
        }


        private TaskListActionController CreateController()
        {
            var result = new TaskListActionController(_taskService.Object, null, _businessSafeSessionManager.Object, _bus.Object);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}