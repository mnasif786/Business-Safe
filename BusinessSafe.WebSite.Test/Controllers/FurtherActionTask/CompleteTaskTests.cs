using System;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Messages.Emails.Commands;
using BusinessSafe.WebSite.Areas.TaskList.Controllers;
using BusinessSafe.WebSite.Areas.TaskList.ViewModels;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NServiceBus;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.FurtherActionTask
{
    [TestFixture]
    public class CompleteTaskTests
    {
        private Mock<IFurtherControlMeasureTaskService> _furtherControlMeasureTaskService;
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;
        private FurtherControlMeasureTaskDto _furtherControlMeasureTaskDtoWithCompletedNotification;
        private FurtherControlMeasureTaskDto _furtherControlMeasureTaskDtoWithNoCompletedNotification;
        private Mock<IBus> _bus;

        [SetUp]
        public void Setup()
        {
            _furtherControlMeasureTaskService = new Mock<IFurtherControlMeasureTaskService>();
            _bus = new Mock<IBus>();

            var sendTaskCompletedEmail = new SendTaskCompletedEmail
                                             {
                                                 TaskReference = "Task Ref",
                                                 Title = "Title",
                                                 Description = "Description",
                                                 RiskAssessorName = "Fred Bloggs",
                                                 RiskAssessorEmail = "fred.bloggs@example.com"
                                             };

            _furtherControlMeasureTaskDtoWithCompletedNotification = new FurtherControlMeasureTaskDto
            {
                Reference = "Task Ref",
                Title = "Title",
                Description = "Description",
                RiskAssessment = new GeneralRiskAssessmentDto
                {
                    Id = 1001L,
                    Title = "Risk Title",
                    Reference = "Risk Reference",
                    RiskAssessor = new RiskAssessorDto()
                    {
                        Id = 213L,
                        FormattedName = "Fred Bloggs",
                        Employee = new EmployeeDto
                                       {
                                           FullName = "Fred Bloggs",
                                           MainContactDetails = new EmployeeContactDetailDto { Email = "fred@bloggs.com" }
                                       }
                    }
                },
                SendTaskCompletedNotification = true
            };

            _furtherControlMeasureTaskDtoWithNoCompletedNotification = new FurtherControlMeasureTaskDto
            {
                Reference = "Task Ref",
                Title = "Title",
                Description = "Description",
                RiskAssessment = new GeneralRiskAssessmentDto
                {
                    Id = 1001L,
                    Title = "Risk Title",
                    Reference = "Risk Reference",
                    RiskAssessor = new RiskAssessorDto()
                    {
                        Id = 324L,
                        FormattedName = "Fred Bloggs",

                    }
                },
                SendTaskCompletedNotification = false
            };

            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();

            _bus.Setup(x => x.Send(sendTaskCompletedEmail));
        }


        [Test]
        public void When_complete_task_with_completed_notification_Then_should_call_correct_methods()
        {
            // Given
            var target = CreateController();
            var viewModel = new CompleteTaskViewModel()
                                {
                                    CompanyId = 1,
                                    FurtherControlMeasureTaskId = 4,
                                    CompletedComments = "Testing"
                                };

            _furtherControlMeasureTaskService
                .Setup(x => x.CompleteFurtherControlMeasureTask(It.IsAny<CompleteTaskRequest>()));

            _furtherControlMeasureTaskService
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(_furtherControlMeasureTaskDtoWithCompletedNotification);

            // When
            target.CompleteTask(viewModel, new DocumentsToSaveViewModel());


            // Then
            _furtherControlMeasureTaskService
                .Verify(x => x.CompleteFurtherControlMeasureTask(It.Is<CompleteTaskRequest>(y => y.GetType() == typeof(CompleteTaskRequest))));
            _furtherControlMeasureTaskService
                .Verify(x => x.CompleteFurtherControlMeasureTask(It.Is<CompleteTaskRequest>(y => y.CompanyId == viewModel.CompanyId)));
            _furtherControlMeasureTaskService
                .Verify(x => x.CompleteFurtherControlMeasureTask(It.Is<CompleteTaskRequest>(y => y.FurtherControlMeasureTaskId == viewModel.FurtherControlMeasureTaskId)));
            _furtherControlMeasureTaskService
                .Verify(x => x.CompleteFurtherControlMeasureTask(It.Is<CompleteTaskRequest>(y => y.CompletedComments == viewModel.CompletedComments)));
            _furtherControlMeasureTaskService
                .Verify(x => x.CompleteFurtherControlMeasureTask(It.Is<CompleteTaskRequest>(y => y.UserId == target.CurrentUser.UserId)));
             _furtherControlMeasureTaskService
                .Verify(x => x.CompleteFurtherControlMeasureTask(It.Is<CompleteTaskRequest>(y => y.CompletedDate.Date == DateTime.Now.Date)));
            
            _furtherControlMeasureTaskService
                .Verify(x => x.SendTaskCompletedEmail(It.IsAny<CompleteTaskRequest>()),Times.Once());


        }

        [Test]
        public void When_complete_task_with__no_completed_notification_Then_not_call_bus()
        {
            // Given
            var target = CreateController();
            var viewModel = new CompleteTaskViewModel()
            {
                CompanyId = 1,
                FurtherControlMeasureTaskId = 4,
                CompletedComments = "Testing"
            };

            _furtherControlMeasureTaskService
                .Setup(x => x.CompleteFurtherControlMeasureTask(It.IsAny<CompleteTaskRequest>()));

            _furtherControlMeasureTaskService
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(_furtherControlMeasureTaskDtoWithNoCompletedNotification);

            // When
            target.CompleteTask(viewModel, new DocumentsToSaveViewModel());


            // Then
            _furtherControlMeasureTaskService
                .Verify(x => x.CompleteFurtherControlMeasureTask(It.Is<CompleteTaskRequest>(y => y.GetType() == typeof(CompleteTaskRequest))));
            _furtherControlMeasureTaskService
                .Verify(x => x.CompleteFurtherControlMeasureTask(It.Is<CompleteTaskRequest>(y => y.CompanyId == viewModel.CompanyId)));
            _furtherControlMeasureTaskService
                .Verify(x => x.CompleteFurtherControlMeasureTask(It.Is<CompleteTaskRequest>(y => y.FurtherControlMeasureTaskId == viewModel.FurtherControlMeasureTaskId)));
            _furtherControlMeasureTaskService
                .Verify(x => x.CompleteFurtherControlMeasureTask(It.Is<CompleteTaskRequest>(y => y.CompletedComments == viewModel.CompletedComments)));
            _furtherControlMeasureTaskService
                .Verify(x => x.CompleteFurtherControlMeasureTask(It.Is<CompleteTaskRequest>(y => y.UserId == target.CurrentUser.UserId)));

            _bus.Verify(x => x.Send(It.IsAny<SendTaskCompletedEmail>()), Times.Never());
        }

        [Test]
        public void When_complete_task_with_valid_request_Then_should_return_correct_result()
        {
            // Given
            var target = CreateController();
            var viewModel = new CompleteTaskViewModel();

            _furtherControlMeasureTaskService
               .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
               .Returns(_furtherControlMeasureTaskDtoWithCompletedNotification);

            // When
            var result = target.CompleteTask(viewModel, new DocumentsToSaveViewModel());

            // Then
            dynamic data = result.Data;
            Assert.That(data.Success, Is.EqualTo(true));
        }

        [Test]
        public void When_invalid_view_model_Then_should_throw_correct_exception()
        {
            // Given
            var target = CreateController();
            var viewModel = new CompleteTaskViewModel();

            target.ModelState.AddModelError("Anything", "Some Error Doesn't Matter");

            // When

            // Then
            Assert.Throws<ArgumentException>(() => target.CompleteTask(viewModel, null));
        }


        private TaskListActionController CreateController()
        {
            var result = new TaskListActionController(null, _furtherControlMeasureTaskService.Object, _businessSafeSessionManager.Object, _bus.Object);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}