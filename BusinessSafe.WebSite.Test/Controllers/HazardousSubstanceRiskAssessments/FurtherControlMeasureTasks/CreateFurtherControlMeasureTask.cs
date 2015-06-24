using System;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NServiceBus;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceRiskAssessments.FurtherControlMeasureTasks
{
    [TestFixture]
    public class CreateFurtherControlMeasureTask
    {

        private const long CompanyId = 1234;
        private const long RiskAssessmentId = 5678;
        private Mock<IHazardousSubstanceRiskAssessmentFurtherControlMeasureTaskService> _furtherControlMeasureTaskService;
        private Mock<IFurtherControlMeasureTaskService> _furtherActionService;

        private Mock<IAddHazardousSubstanceFurtherControlMeasureTaskViewModelFactory>
            _addHazardousSubstanceFurtherControlMeasureTaskViewModelFactory;

        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;
        private Mock<IBus> _bus;

        [SetUp]
        public void Setup()
        {
            _furtherActionService = new Mock<IFurtherControlMeasureTaskService>();
            _furtherControlMeasureTaskService = new Mock<IHazardousSubstanceRiskAssessmentFurtherControlMeasureTaskService>();

            _furtherControlMeasureTaskService
                .Setup(x => x.AddFurtherControlMeasureTask(It.IsAny<SaveFurtherControlMeasureTaskRequest>()));

            _addHazardousSubstanceFurtherControlMeasureTaskViewModelFactory = new Mock<IAddHazardousSubstanceFurtherControlMeasureTaskViewModelFactory>();
            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();
            _bus = new Mock<IBus>();
        }

        [Test]
        public void Given_invalid_post_When_CreateFurtherControlMeasureTask_Then_return_json_result_with_success_equal_false()
        {
            // Given
            var controller = GetTarget();

            // When
            controller.ModelState.AddModelError("an error", "this is a dummy error");
            var result = controller.NewFurtherControlMeasureTask(It.IsAny<AddHazardousSubstanceFurtherControlMeasureTaskViewModel>(), It.IsAny<DocumentsToSaveViewModel>());

            // Then
            dynamic data = result.Data;
            var success = data.GetType().GetProperty("Success").GetValue(data, null);
            var errors = data.GetType().GetProperty("Errors").GetValue(data, null);
            Assert.That(success, Is.EqualTo(false));
            Assert.That(errors.Length, Is.EqualTo(1));
        }

        [Test]
        public void Given_valid_post_When_CreateFurtherControlMeasureTask_Then_mapped_request_to_service_has_correct_values_and_returns_json()
        {
            // Given
            var controller = GetTarget();
            var returnedTaskDto = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTaskDto()
            {
                Id = 123,
                Title = "title",
                Description = "description",
                CreatedDate = "23/06/1980",
                TaskCompletionDueDate = "23/06/2016",
                TaskAssignedTo = new EmployeeDto()
                {
                    Id = Guid.NewGuid(),
                    FullName = "Marcellas Wallace"
                }
            };
            var passedSaveFurtherControlMeasureTaskRequest = new SaveFurtherControlMeasureTaskRequest();

            _furtherControlMeasureTaskService
                .Setup(x => x.AddFurtherControlMeasureTask(It.IsAny<SaveFurtherControlMeasureTaskRequest>()))
                .Returns(returnedTaskDto)
                .Callback<SaveFurtherControlMeasureTaskRequest>(y => passedSaveFurtherControlMeasureTaskRequest = y);

            // When
            var postedAddEditFurtherControlMeasureTaskViewModel = new AddHazardousSubstanceFurtherControlMeasureTaskViewModel()
                                                                  {
                                                                      RiskAssessmentId = 1,
                                                                      Reference = "Test",
                                                                      Description = "Test",
                                                                      Title = "Test",
                                                                      CompanyId = 1,
                                                                      TaskAssignedToId = new Guid(),
                                                                      TaskCompletionDueDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                                                                      ExistingDocuments = new ExistingDocumentsViewModel()
                                                                  };

            var result = controller.NewFurtherControlMeasureTask(postedAddEditFurtherControlMeasureTaskViewModel, new DocumentsToSaveViewModel());

            // Then
            _furtherControlMeasureTaskService
                .Verify(x => x.AddFurtherControlMeasureTask(It.IsAny<SaveFurtherControlMeasureTaskRequest>()));
            Assert.That(passedSaveFurtherControlMeasureTaskRequest.RiskAssessmentId, Is.EqualTo(postedAddEditFurtherControlMeasureTaskViewModel.RiskAssessmentId));
            Assert.That(passedSaveFurtherControlMeasureTaskRequest.Reference, Is.EqualTo(postedAddEditFurtherControlMeasureTaskViewModel.Reference));
            Assert.That(passedSaveFurtherControlMeasureTaskRequest.Description, Is.EqualTo(postedAddEditFurtherControlMeasureTaskViewModel.Description));
            Assert.That(passedSaveFurtherControlMeasureTaskRequest.Title, Is.EqualTo(postedAddEditFurtherControlMeasureTaskViewModel.Title));
            Assert.That(passedSaveFurtherControlMeasureTaskRequest.CompanyId, Is.EqualTo(postedAddEditFurtherControlMeasureTaskViewModel.CompanyId));
            Assert.That(passedSaveFurtherControlMeasureTaskRequest.TaskAssignedToId, Is.EqualTo(postedAddEditFurtherControlMeasureTaskViewModel.TaskAssignedToId));
            Assert.That(passedSaveFurtherControlMeasureTaskRequest.TaskCompletionDueDate.Value.ToString("dd/MM/yyyy HH:mm:ss"), Is.EqualTo(postedAddEditFurtherControlMeasureTaskViewModel.TaskCompletionDueDate));


            dynamic data = result.Data;
            var success = data.GetType().GetProperty("Success").GetValue(data, null);
            var id = data.GetType().GetProperty("Id").GetValue(data, null);
            var createdDate = data.GetType().GetProperty("CreatedOn").GetValue(data, null);

            Assert.That(success, Is.EqualTo(true));
            Assert.That(id, Is.EqualTo(returnedTaskDto.Id));
            Assert.That(createdDate, Is.EqualTo(returnedTaskDto.CreatedDate));
        }



        [Test]
        public void Given_isrecurring_is_false_When_CreateFurtherControlMeasureTask_Then_setl_recurringType_to_none()
        {
            // Given
            var controller = GetTarget();
            var returnedTaskDto = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTaskDto()
            {
                Id = 123,
                Title = "title",
                Description = "description",
                CreatedDate = "23/06/1980",
                TaskCompletionDueDate = "23/06/2016",
                TaskAssignedTo = new EmployeeDto()
                {
                    Id = Guid.NewGuid(),
                    FullName = "Marcellas Wallace"
                },
                IsReoccurring = false,
                TaskReoccurringType = TaskReoccurringType.FiveYearly
            };
            _furtherControlMeasureTaskService
                .Setup(x => x.AddFurtherControlMeasureTask(It.IsAny<SaveFurtherControlMeasureTaskRequest>()))
                .Returns(returnedTaskDto);

            // When
            var postedAddEditFurtherControlMeasureTaskViewModel = new AddHazardousSubstanceFurtherControlMeasureTaskViewModel()
            {
                RiskAssessmentId = 1,
                Reference = "Test",
                Description = "Test",
                Title = "Test",
                CompanyId = 1,
                TaskAssignedToId = new Guid(),
                TaskCompletionDueDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                ExistingDocuments = new ExistingDocumentsViewModel()
            };

            var result = controller.NewFurtherControlMeasureTask(postedAddEditFurtherControlMeasureTaskViewModel, new DocumentsToSaveViewModel());

            // Then
            _furtherControlMeasureTaskService
                .Verify(x => x.AddFurtherControlMeasureTask(It.Is<SaveFurtherControlMeasureTaskRequest>(y => y.TaskReoccurringTypeId == (int)TaskReoccurringType.None)));

        }

        private FurtherControlMeasureTaskController GetTarget()
        {
            var result = new FurtherControlMeasureTaskController(
                _furtherControlMeasureTaskService.Object, 
                _addHazardousSubstanceFurtherControlMeasureTaskViewModelFactory.Object, null, null,null,null,_businessSafeSessionManager.Object, _bus.Object);

            return TestControllerHelpers.AddUserToController(result);
        }    
    }
}