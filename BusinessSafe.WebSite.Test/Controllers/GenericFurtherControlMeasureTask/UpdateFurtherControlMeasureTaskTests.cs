using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Messages.Events;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NServiceBus;
using NUnit.Framework;
using BusinessSafe.WebSite.Controllers;

namespace BusinessSafe.WebSite.Tests.Controllers.GenericFurtherControlMeasureTask
{
    [TestFixture]
    [Category("Unit")]
    public class UpdateFurtherControlMeasureTaskTests
    {
        Mock<IFurtherControlMeasureTaskService> _furtherControlMeasureTaskService;
        Mock<IBus> _bus;

        private AddEditFurtherControlMeasureTaskViewModel _viewModel;
        private Guid _taskGuid;

        [SetUp]
        public void Setup()
        {
            _taskGuid = Guid.NewGuid();
            _viewModel = new AddEditFurtherControlMeasureTaskViewModel()
            {
                TaskAssignedToId = Guid.NewGuid(),
                Title = "Hello",
                FurtherControlMeasureTaskId = 400,
                Description = "DISCO",
                Reference = "REFERENCE",
                DoNotSendTaskCompletedNotification = true,
                DoNotSendTaskNotification = true,
                DoNotSendTaskOverdueNotification = true,
                DoNotSendTaskDueTomorrowNotification = true,
                CompanyId = TestControllerHelpers.CompanyIdAssigned
            };

            _furtherControlMeasureTaskService = new Mock<IFurtherControlMeasureTaskService>();
            _furtherControlMeasureTaskService
                .Setup(x => x.GetByIdAndCompanyId(_viewModel.FurtherControlMeasureTaskId, _viewModel.CompanyId))
                .Returns(new FurtherControlMeasureTaskDto()
                {
                    TaskGuid = _taskGuid,
                    TaskAssignedTo = new EmployeeDto()
                    {
                        Id = _viewModel.TaskAssignedToId.Value
                    }
                });

            _bus = new Mock<IBus>();
            _bus.Setup(x => x.Publish(It.IsAny<TaskAssigned>()));

        }

        [Test]
        public void When_UpdateFurtherControlMeasureTask_called_Then_correct_methods_are_called()
        {
            // Arrange
            UpdateFurtherControlMeasureTaskRequest passedUpdateFurtherControlMeasureTaskRequest = null;

            _furtherControlMeasureTaskService
                .Setup(x => x.Update(It.IsAny<UpdateFurtherControlMeasureTaskRequest>()))
                .Callback<UpdateFurtherControlMeasureTaskRequest>(y => passedUpdateFurtherControlMeasureTaskRequest = y);

            var target = GetTarget();

            var documentsToSaveViewModel = new DocumentsToSaveViewModel();

            var userId = target.CurrentUser.UserId;


            // Act
            target.UpdateFurtherControlMeasureTask(_viewModel, documentsToSaveViewModel);

            // Assert
            Assert.That(passedUpdateFurtherControlMeasureTaskRequest.CompanyId, Is.EqualTo( _viewModel.CompanyId));
            Assert.That(passedUpdateFurtherControlMeasureTaskRequest.Id, Is.EqualTo( _viewModel.FurtherControlMeasureTaskId));
            Assert.That(passedUpdateFurtherControlMeasureTaskRequest.Title, Is.EqualTo( _viewModel.Title));
            Assert.That(passedUpdateFurtherControlMeasureTaskRequest.Description, Is.EqualTo( _viewModel.Description));
            Assert.That(passedUpdateFurtherControlMeasureTaskRequest.Reference, Is.EqualTo( _viewModel.Reference));
            Assert.That(passedUpdateFurtherControlMeasureTaskRequest.UserId, Is.EqualTo( userId));
            Assert.That(passedUpdateFurtherControlMeasureTaskRequest.SendTaskCompletedNotification, Is.EqualTo(!_viewModel.DoNotSendTaskCompletedNotification));
            Assert.That(passedUpdateFurtherControlMeasureTaskRequest.SendTaskNotification, Is.EqualTo(!_viewModel.DoNotSendTaskNotification));
            Assert.That(passedUpdateFurtherControlMeasureTaskRequest.SendTaskOverdueNotification, Is.EqualTo(!_viewModel.DoNotSendTaskOverdueNotification));
            Assert.That(passedUpdateFurtherControlMeasureTaskRequest.SendTaskDueTomorrowNotification, Is.EqualTo(!_viewModel.DoNotSendTaskDueTomorrowNotification));
        }

        [Test]
        public void When_UpdateFurtherControlMeasureTask_called_Then_correct_view_is_returned()
        {
            // Arrange
            var target = GetTarget();

            var documentsToSaveViewModel = new DocumentsToSaveViewModel();

            // Act
            var result = target.UpdateFurtherControlMeasureTask(_viewModel, documentsToSaveViewModel) as JsonResult;

            // Assert
            Assert.That(result.Data.ToString(), Contains.Substring("{ Success = True, Id = 400 }"));
        }

        [Test]
        public void Given_assign_to_different_employee_When_UpdateFurtherControlMeasureTask_called_Then_publish_event_on_bus()
        {
            // Arrange
            TaskAssigned[] passedTaskAssigned = null;
            _bus
                .Setup(x => x.Publish(It.IsAny<TaskAssigned>()))
                .Callback<TaskAssigned[]>(y => passedTaskAssigned = y);


            _furtherControlMeasureTaskService
                .Setup(x => x.GetByIdAndCompanyId(_viewModel.FurtherControlMeasureTaskId, _viewModel.CompanyId))
                .Returns(new FurtherControlMeasureTaskDto()
                {
                    TaskGuid = _taskGuid,
                    TaskAssignedTo = new EmployeeDto()
                    {
                        Id = Guid.NewGuid()
                    }
                });

            var target = GetTarget();

            var documentsToSaveViewModel = new DocumentsToSaveViewModel();

            // Act
            target.UpdateFurtherControlMeasureTask(_viewModel, documentsToSaveViewModel);

            // Assert
            Assert.That(passedTaskAssigned[0].TaskGuid, Is.EqualTo(_taskGuid));
        }

        [Test]
        public void Given_assign_to_same_employee_When_UpdateFurtherControlMeasureTask_called_Then_dont_publish_event_on_bus()
        {
            // Arrange
            var target = GetTarget();

            var documentsToSaveViewModel = new DocumentsToSaveViewModel();

            // Act
            target.UpdateFurtherControlMeasureTask(_viewModel, documentsToSaveViewModel);

            // Assert
            _bus.Verify(x => x.Publish(It.IsAny<TaskAssigned[]>()), Times.Never());
        }

        [Test]
        public void Given_invalid__viewModel_When_UpdateFurtherControlMeasureTask_called_Then_correct_view_is_returned()
        {
            // Arrange
            var target = GetTarget();

            var documentsToSaveViewModel = new DocumentsToSaveViewModel();

            target.ModelState.AddModelError("Any", "Any");

            // Act
            var result = target.UpdateFurtherControlMeasureTask(_viewModel, documentsToSaveViewModel);


            // Assert
            Assert.That(result.Data.ToString(), Contains.Substring("Success = False"));
        }

        private GenericFurtherControlMeasureTaskController GetTarget()
        {
            var target = new GenericFurtherControlMeasureTaskController(_furtherControlMeasureTaskService.Object, null, _bus.Object);
            target = TestControllerHelpers.AddUserToController(target);
            return target;
        }
    }
}
