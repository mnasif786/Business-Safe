using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NServiceBus;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.GenericFurtherControlMeasureTask
{
    [TestFixture]
    [Category("Unit")]
    public class CreateFurtherControlMeasureTaskTests
    {
        Mock<IFurtherControlMeasureTaskService> _furtherControlMeasureTaskService;
        Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;
        Mock<IBus> _bus;

        [SetUp]
        public void Setup()
        {
            _furtherControlMeasureTaskService = new Mock<IFurtherControlMeasureTaskService>();
            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();
            _bus = new Mock<IBus>();
        }

        [Test]
        public void When_CreateFurtherControlMeasureTask_called_Then_correct_methods_are_called()
        {
            // Arrange
            var target = GetTarget();

            var documentsToSaveViewModel = new DocumentsToSaveViewModel();
            var viewmodel = new AddRiskAssessmentFurtherControlMeasureTaskViewModel()
                                {
                                    TaskAssignedToId = Guid.NewGuid(),
                                    RiskAssessmentHazardId = 500,
                                    
                                };

            var userId = target.CurrentUser.UserId;

            var result = new MultiHazardRiskAssessmentFurtherControlMeasureTaskDto()
                             {
                                 CreatedDate = DateTime.Now.ToShortDateString()
                             };

            _furtherControlMeasureTaskService
                .Setup(x => x.AddFurtherControlMeasureTask(It.Is<SaveFurtherControlMeasureTaskRequest>(y => y.CompanyId == viewmodel.CompanyId &&
                                                                                           y.Title == viewmodel.Title &&
                                                                                           y.Description == viewmodel.Description &&
                                                                                           y.Reference == viewmodel.Reference &&
                                                                                           y.UserId == userId)))
                .Returns(result);

            // Act
            target.CreateFurtherControlMeasureTask(viewmodel, documentsToSaveViewModel);

            // Assert
            _furtherControlMeasureTaskService.VerifyAll();
            _businessSafeSessionManager.Verify(x => x.CloseSession(), Times.Exactly(1));
        }


        [Test]
        public void Give_IsRecurring_is_false_When_CreateFurtherControlMeasureTask_Then_Set_TaskRecurringType_to_None()
        {
            //given
            var target = GetTarget();
            
            var viewModel = new AddRiskAssessmentFurtherControlMeasureTaskViewModel()
            {
                TaskAssignedToId = Guid.NewGuid(),
                RiskAssessmentHazardId = 500,
                IsRecurring = false,
                TaskReoccurringTypeId = (int)TaskReoccurringType.Monthly

            };

            var result = new MultiHazardRiskAssessmentFurtherControlMeasureTaskDto()
            {
                CreatedDate = DateTime.Now.ToShortDateString()
            };

            _furtherControlMeasureTaskService
                .Setup(x => x.AddFurtherControlMeasureTask(It.IsAny<SaveFurtherControlMeasureTaskRequest>()))
                .Returns(result);
            
            //when

            target.CreateFurtherControlMeasureTask(viewModel, new DocumentsToSaveViewModel());

            //then
            _furtherControlMeasureTaskService
                .Verify(x => x.AddFurtherControlMeasureTask(It.Is<SaveFurtherControlMeasureTaskRequest>(y => y.TaskReoccurringTypeId == (int)TaskReoccurringType.None)), Times.Once());
        }

        [Test]
        public void When_CreateFurtherControlMeasureTask_called_Then_correct_view_is_returned()
        {
            // Arrange
            var target = GetTarget();

            var documentsToSaveViewModel = new DocumentsToSaveViewModel();
            var viewmodel = new AddRiskAssessmentFurtherControlMeasureTaskViewModel()
                                {
                                    TaskAssignedToId = Guid.NewGuid(),
                                    RiskAssessmentHazardId = 500
                                };

            var dto = new MultiHazardRiskAssessmentFurtherControlMeasureTaskDto()
            {
                CreatedDate = DateTime.Now.ToShortDateString()
            };
            _furtherControlMeasureTaskService
              .Setup(x => x.AddFurtherControlMeasureTask(It.Is<SaveFurtherControlMeasureTaskRequest>(y => y.CompanyId == viewmodel.CompanyId)))
              .Returns(dto);

            // Act
            var result = target.CreateFurtherControlMeasureTask(viewmodel, documentsToSaveViewModel) as JsonResult;


            // Assert
            Assert.That(result.Data.ToString(), Contains.Substring("Success = True, Id = 0, RiskAssessmentHazardId = 500"));
        }

        [Test]
        public void Given_invalid_viewmodel_When_CreateFurtherControlMeasureTask_called_Then_correct_view_is_returned()
        {
            // Arrange
            var target = GetTarget();

            var documentsToSaveViewModel = new DocumentsToSaveViewModel();
            var viewmodel = new AddRiskAssessmentFurtherControlMeasureTaskViewModel();

            target.ModelState.AddModelError("Any", "Any");

            // Act
            var result = target.CreateFurtherControlMeasureTask(viewmodel, documentsToSaveViewModel) as JsonResult;


            // Assert
            Assert.That(result.Data.ToString(), Contains.Substring("Success = False"));
        }

        private GenericFurtherControlMeasureTaskController GetTarget()
        {
            var target = new GenericFurtherControlMeasureTaskController(_furtherControlMeasureTaskService.Object, _businessSafeSessionManager.Object, _bus.Object);
            target = TestControllerHelpers.AddUserToController(target);
            return target;
        }
    }
}