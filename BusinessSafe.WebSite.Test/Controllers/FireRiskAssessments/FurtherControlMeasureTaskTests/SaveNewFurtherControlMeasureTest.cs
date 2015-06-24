using System.Web.Mvc;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request.FireRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NServiceBus;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.FireRiskAssessments.FurtherControlMeasureTaskTests
{
    [TestFixture]
    [Category("Unit")]
    public class SaveNewFurtherControlMeasureTest
    {
        private Mock<IFireRiskAssessmentFurtherControlMeasureTaskService> _furtherControlMeasureTaskService;
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;
        private Mock<IBus> _bus;

        [SetUp]
        public void Setup()
        {
         
            _furtherControlMeasureTaskService =
                new Mock<IFireRiskAssessmentFurtherControlMeasureTaskService>();

            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();
            _bus = new Mock<IBus>();
        }

        [Test]
        public void Given_invalid_model_state_When_NewFurtherControlMeasureTask_Request_Then_returns_correct_result()
        {
            // Given
            var controller = GetTarget();
            var viewModel = new AddEditFireRiskAssessmentFurtherControlMeasureTaskViewModel();
            var documentsToSave = new DocumentsToSaveViewModel();

            controller.ModelState.AddModelError("error", "some problem");

            // When
            var result = controller.NewFurtherControlMeasureTask(viewModel, documentsToSave) as JsonResult;

            // Then
            Assert.That(result, Is.InstanceOf<JsonResult>());
            Assert.That(result.Data.ToString(), Is.EqualTo("{ Success = False, Errors = System.String[] }"));
        }

        [Test]
        public void Given_valid_model_state_When_NewFurtherControlMeasureTask_Request_Then_should_call_correct_methods()
        {
            // Given
            var controller = GetTarget();
            var viewModel = new AddEditFireRiskAssessmentFurtherControlMeasureTaskViewModel()
                                {
                                    CompanyId = 123L,
                                    RiskAssessmentId = 789L,
                                    Title = "TItle",
                                    Description = "A description",
                                    Reference = "My reference",
                                    SignificantFindingId = 888L,
                                    TaskStatusId = 3
                                };
            var documentsToSave = new DocumentsToSaveViewModel();

            var result = new FireRiskAssessmentFurtherControlMeasureTaskDto();
            _furtherControlMeasureTaskService
                .Setup(x => x.AddFurtherControlMeasureTask(It.Is<SaveFurtherControlMeasureTaskRequest>(
                    y => y.CompanyId == viewModel.CompanyId &&
                         y.RiskAssessmentId == viewModel.RiskAssessmentId &&
                         y.Title == viewModel.Title &&
                         y.Description == viewModel.Description &&
                         y.Reference == viewModel.Reference &&
                         y.SignificantFindingId == viewModel.SignificantFindingId &&
                         y.TaskStatus == (TaskStatus)viewModel.TaskStatusId
                    )))
                .Returns(result);

            // When
            controller.NewFurtherControlMeasureTask(viewModel, documentsToSave);

            // Then
            _furtherControlMeasureTaskService.VerifyAll();
        }

        [Test]
        public void Given_valid_model_state_When_NewFurtherControlMeasureTask_Request_Then_should_return_correct_result()
        {
            // Given
            var controller = GetTarget();
            var viewModel = new AddEditFireRiskAssessmentFurtherControlMeasureTaskViewModel()
            {
                CompanyId = 123L,
                RiskAssessmentId = 789L,
                Title = "TItle",
                Description = "A description",
                Reference = "My reference",
                SignificantFindingId = 888L,
                TaskStatusId = 3
            };
            var documentsToSave = new DocumentsToSaveViewModel();

            _furtherControlMeasureTaskService
                .Setup(x => x.AddFurtherControlMeasureTask(It.IsAny<SaveFurtherControlMeasureTaskRequest>()))
                .Returns(new FireRiskAssessmentFurtherControlMeasureTaskDto());

            // When
            var result = controller.NewFurtherControlMeasureTask(viewModel, documentsToSave) as JsonResult;

            // Then
            Assert.That(result.Data.ToString(), Is.EqualTo("{ Success = True, Id = 0, CreatedOn =  }"));
        }

        [Test]
        public void Given_is_recurring_is_false_When_NewFurtherControlMeasureTask_Request_Then_set_recurringtype_to_none()
        {
            // Given
            var controller = GetTarget();
            var viewModel = new AddEditFireRiskAssessmentFurtherControlMeasureTaskViewModel()
            {
                CompanyId = 123L,
                RiskAssessmentId = 789L,
                Title = "TItle",
                Description = "A description",
                Reference = "My reference",
                SignificantFindingId = 888L,
                TaskStatusId = 3,
                IsRecurring = false,
                TaskReoccurringTypeId = (int)TaskReoccurringType.Annually
            };
            var documentsToSave = new DocumentsToSaveViewModel();

            _furtherControlMeasureTaskService
                .Setup(x => x.AddFurtherControlMeasureTask(It.IsAny<SaveFurtherControlMeasureTaskRequest>()))
                .Returns(new FireRiskAssessmentFurtherControlMeasureTaskDto());

            // When
            var result = controller.NewFurtherControlMeasureTask(viewModel, documentsToSave);

            // Then
            _furtherControlMeasureTaskService
                .Verify(x => x.AddFurtherControlMeasureTask(It.Is<SaveFurtherControlMeasureTaskRequest>(y => y.TaskReoccurringTypeId == (int)TaskReoccurringType.None)));
        }


        private FurtherControlMeasureTaskController GetTarget()
        {
            var target = new FurtherControlMeasureTaskController(null, _furtherControlMeasureTaskService.Object, null, null, null, null, _businessSafeSessionManager.Object, _bus.Object);

            return TestControllerHelpers.AddUserToController(target);
        }
    }
}