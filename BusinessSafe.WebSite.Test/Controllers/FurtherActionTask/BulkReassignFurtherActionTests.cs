using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.TaskList.Controllers;
using BusinessSafe.WebSite.Areas.TaskList.ViewModels;
using BusinessSafe.WebSite.Controllers;
using Moq;
using NServiceBus;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.FurtherActionTask
{
    [TestFixture]
    public class BulkReassignFurtherActionTests
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
        public void Given_invalid_required_more_than_one_assigned_to_ids_When_bulk_reassign_further_action_Then_should_throw_correct_exception()
        {
            // Given
            var target = CreateController();
            
            var viewModel = new List<ReassignTaskViewModel>
                                {
                                    new ReassignTaskViewModel()
                                        {
                                            ReassignTaskToId = Guid.NewGuid()
                                        },
                                    new ReassignTaskViewModel()
                                        {
                                            ReassignTaskToId = Guid.NewGuid()
                                        }
                                };

            
            // When
            // Then
            Assert.Throws<MultipleReassignToIdsSpecifiedInBulkReassignRequestException>(() => target.BulkReassignTask(viewModel));
            
        }

        [Test]
        public void When_bulk_reassign_further_action_Then_should_call_correct_methods()
        {
            // Given
            var target = CreateController();
            var reassignIdTo = Guid.NewGuid();

            var viewModel = new List<ReassignTaskViewModel>
                                {
                                    new ReassignTaskViewModel()
                                        {
                                            CompanyId = 1,
                                            FurtherControlMeasureTaskId = 4,
                                            ReassignTaskToId = reassignIdTo
                                        },
                                    new ReassignTaskViewModel()
                                        {
                                            CompanyId = 1,
                                            FurtherControlMeasureTaskId = 40,
                                            ReassignTaskToId = reassignIdTo
                                        }
                                };

            _taskService
                .Setup(x => x.BulkReassignTasks(It.IsAny<BulkReassignTasksToEmployeeRequest>()));

            // When
            target.BulkReassignTask(viewModel);


            // Then
            _taskService
                .Verify(x => x.BulkReassignTasks(It.Is<BulkReassignTasksToEmployeeRequest>(y => y.GetType() == typeof(BulkReassignTasksToEmployeeRequest))));
            _taskService
                .Verify(x => x.BulkReassignTasks(It.Is<BulkReassignTasksToEmployeeRequest>(y => y.ReassignRequests.First().CompanyId == viewModel.First().CompanyId)));
            _taskService
               .Verify(x => x.BulkReassignTasks(It.Is<BulkReassignTasksToEmployeeRequest>(y => y.ReassignRequests.First().TaskId == viewModel.First().FurtherControlMeasureTaskId)));
            _taskService
              .Verify(x => x.BulkReassignTasks(It.Is<BulkReassignTasksToEmployeeRequest>(y => y.ReassignRequests.First().ReassignTaskToId == viewModel.First().ReassignTaskToId)));
            _taskService
            .Verify(x => x.BulkReassignTasks(It.Is<BulkReassignTasksToEmployeeRequest>(y => y.ReassignRequests.First().UserId == target.CurrentUser.UserId)));


            _taskService
                .Verify(x => x.BulkReassignTasks(It.Is<BulkReassignTasksToEmployeeRequest>(y => y.ReassignRequests.Last().CompanyId == viewModel.Last().CompanyId)));
            _taskService
               .Verify(x => x.BulkReassignTasks(It.Is<BulkReassignTasksToEmployeeRequest>(y => y.ReassignRequests.Last().TaskId == viewModel.Last().FurtherControlMeasureTaskId)));
            _taskService
              .Verify(x => x.BulkReassignTasks(It.Is<BulkReassignTasksToEmployeeRequest>(y => y.ReassignRequests.Last().ReassignTaskToId == viewModel.Last().ReassignTaskToId)));
            _taskService
            .Verify(x => x.BulkReassignTasks(It.Is<BulkReassignTasksToEmployeeRequest>(y => y.ReassignRequests.Last().UserId == target.CurrentUser.UserId)));

        }

        [Test]
        public void When_bulk_reassign_further_action_with_valid_request_Then_should_return_correct_result()
        {
            // Given
            var target = CreateController();
            var viewModel = new List<ReassignTaskViewModel>
                                {
                                    new ReassignTaskViewModel()
                                };

            // When
            var result = target.BulkReassignTask(viewModel);

            // Then
            dynamic data = result.Data;
            Assert.That(data.Success, Is.EqualTo(true));
        }

        [Test]
        public void When_invalid_view_model_Then_should_throw_correct_exception()
        {
            // Given
            var target = CreateController();
            var viewModel = new List<ReassignTaskViewModel>
                                {
                                    new ReassignTaskViewModel()
                                };

            target.ModelState.AddModelError("Anything", "Some Error Doesn't Matter");

            // When
            // Then
            Assert.Throws<ArgumentException>(() => target.BulkReassignTask(viewModel));
        }


        private TaskListActionController CreateController()
        {
            var result = new TaskListActionController(_taskService.Object, null, _businessSafeSessionManager.Object, _bus.Object);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}