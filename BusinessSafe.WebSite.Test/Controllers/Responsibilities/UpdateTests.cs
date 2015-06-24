using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.Application.Response;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Responsibilities.Controllers;
using BusinessSafe.WebSite.Areas.Responsibilities.Factories;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels;
using Moq;
using NUnit.Framework;
using BusinessSafe.WebSite.ViewModels;
using NServiceBus;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.Responsibilities
{
    [TestFixture]
    [Category("Unit")]
    public class UpdateWithViewModelTests
    {
        private Mock<ISearchResponsibilityViewModelFactory> _searchViewModelFactory;
        private Mock<IResponsibilityViewModelFactory> _createViewModelFactory;
        private Mock<IResponsibilitiesService> _responsibilitiesService;
        private Mock<IResponsibilityTaskService> _responsibilityTaskService;
        private Mock<ICreateUpdateResponsibilityTaskViewModelFactory> _createUpdateResponsibilityTaskViewModelFactory;
        private Mock<ICompleteResponsibilityTaskViewModelFactory> _completeResponsibilityTaskViewModelFactory;
        private Mock<IBus> _bus;
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;

        [SetUp]
        public void SetUp()
        {
            _searchViewModelFactory = new Mock<ISearchResponsibilityViewModelFactory>();

            _searchViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_searchViewModelFactory.Object);

            _createViewModelFactory = new Mock<IResponsibilityViewModelFactory>();

            _createViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_createViewModelFactory.Object);


            _createViewModelFactory
                .Setup(x => x.WithResponsibilityId(It.IsAny<long>()))
                .Returns(_createViewModelFactory.Object);

            _createViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new ResponsibilityViewModel());

            _responsibilitiesService = new Mock<IResponsibilitiesService>();

            _responsibilitiesService
                .Setup(x => x.SaveResponsibilityTask(It.IsAny<SaveResponsibilityTaskRequest>()))
                .Returns(new SaveResponsibilityTaskResponse());

            _responsibilityTaskService = new Mock<IResponsibilityTaskService>();

            _createUpdateResponsibilityTaskViewModelFactory =
                new Mock<ICreateUpdateResponsibilityTaskViewModelFactory>();

            _completeResponsibilityTaskViewModelFactory =
                new Mock<ICompleteResponsibilityTaskViewModelFactory>();

            _bus = new Mock<IBus>();
            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();
        }

        [Test]
        public void When_post_update_valid_viewmodel_Then_should_call_correct_methods()
        {
            //Given
            var target = GetTarget();
            var viewModel = new ResponsibilityViewModel
                                {
                                    ResponsibilityId = 1L,
                                    CategoryId = default(int),
                                    Title = "R1",
                                    Description = "Responsibility1",
                                    SiteId = default(int),
                                    ReasonId = default(int),
                                    OwnerId = new Guid(),
                                    FrequencyId = (int) TaskReoccurringType.Annually
                                };

            //When
            var result = target.Update(viewModel) as ViewResult;

            //Then
            _responsibilitiesService
                .Verify(x => x.SaveResponsibility(It.IsAny<SaveResponsibilityRequest>()), Times.Once());
        }

        [Test]
        public void when_create_task_viewmodel_should_call_correct_methods()
        {
            //given
            var target = GetTarget();
            //when
            var viewModel = new CreateUpdateResponsibilityTaskViewModel
                                {
                                    CompanyId = 1L,
                                    ResponsibilityId = 1L,
                                    TaskId = default(long),
                                    Title = "the title 12345",
                                    Description = "test description 12345",
                                    DoNotSendTaskCompletedNotification = true,
                                    DoNotSendTaskAssignedNotification = false,
                                    DoNotSendTaskOverdueNotification = true,
                                    DoNotSendTaskDueTomorrowNotification = true,
                                    AssignedTo = "User",
                                    AssignedToId = Guid.NewGuid(),
                                    IsRecurring = false,
                                    ReoccurringStartDate = null,
                                    ReoccurringEndDate = null,
                                    TaskReoccurringType = "3 Monthly",
                                    TaskReoccurringTypeId = 3,
                                    CompletionDueDate = null,
                                    ResponsibilityTaskSite = "Site",
                                    ResponsibilityTaskSiteId = 1L
                                };

            target.CreateResponsibilityTask(viewModel, new DocumentsToSaveViewModel());
            //then
            _responsibilitiesService
                .Verify(x => x.SaveResponsibilityTask(It.IsAny<SaveResponsibilityTaskRequest>()), Times.Once());
        }

        [Test]
        public void Give_IsRecurring_is_false_When_CreateResponsibilityTask_Then_Set_TaskRecurringType_to_None()
        {
            //given
            var target = GetTarget();
            //when
            var viewModel = new CreateUpdateResponsibilityTaskViewModel
            {
                CompanyId = 1L,
                ResponsibilityId = 1L,
                TaskId = default(long),
                Title = "the title 12345",
                Description = "test description 12345",
                DoNotSendTaskCompletedNotification = true,
                DoNotSendTaskAssignedNotification = false,
                DoNotSendTaskOverdueNotification = true,
                DoNotSendTaskDueTomorrowNotification = true,
                AssignedTo = "User",
                AssignedToId = Guid.NewGuid(),
                IsRecurring = false,
                ReoccurringStartDate = null,
                ReoccurringEndDate = null,
                TaskReoccurringType = "3 Monthly",
                TaskReoccurringTypeId = 3,
                CompletionDueDate = null,
                ResponsibilityTaskSite = "Site",
                ResponsibilityTaskSiteId = 1L
            };

            target.CreateResponsibilityTask(viewModel, new DocumentsToSaveViewModel());

            //then
            _responsibilitiesService
                .Verify(x => x.SaveResponsibilityTask(It.Is<SaveResponsibilityTaskRequest>(y => y.TaskReoccurringTypeId == (int)TaskReoccurringType.None)), Times.Once());
        }

        [Test]
        public void Give_IsRecurring_is_Multiple_When_UpdateResponsibility_Then_Update()
        {
            //given
            var target = GetTarget();
            //when
            var viewModel = new ResponsibilityViewModel
            {
                CompanyId = 1L,
                ResponsibilityId = 1L,
                Title = "the title 12345",
                Description = "test description 12345",
                HasMultipleFrequencies = true,
                CategoryId =  123,
                SiteId = 123123,
                OwnerId = Guid.NewGuid(),
                ReasonId = 43534
            };

            target.Update(viewModel);

            //then
            _responsibilitiesService.Verify(x => x.SaveResponsibility(It.IsAny<SaveResponsibilityRequest>()), Times.Once());
            
        }

        [Test]
        public void Given_MultipleFrequency_is_false_and_frequency_not_specfied_When_UpdateResponsibility_Then_validation_error()
        {
            //given
            var target = GetTarget();
            //when
            var viewModel = new ResponsibilityViewModel
            {
                CompanyId = 1L,
                ResponsibilityId = 1L,
                Title = "the title 12345",
                Description = "test description 12345",
                HasMultipleFrequencies = false,
                FrequencyId = null
            };

            target.Update(viewModel);

            //then the save responsibilty method is not called
            _responsibilitiesService.Verify(x => x.SaveResponsibility(It.IsAny<SaveResponsibilityRequest>()), Times.Never());
            Assert.IsFalse(target.ModelState.IsValid);
            Assert.True(target.ModelState.Keys.Contains("FrequencyId"), "FrequencyId is mandatory when not mulitple frequencies");


        }

        private ResponsibilityController GetTarget()
        {
            var result = new ResponsibilityController(
                _responsibilitiesService.Object,null,
                _responsibilityTaskService.Object,
                _searchViewModelFactory.Object,
                _createViewModelFactory.Object,
                _createUpdateResponsibilityTaskViewModelFactory.Object,
                _completeResponsibilityTaskViewModelFactory.Object, null,
                null, 
                _bus.Object,
                _businessSafeSessionManager.Object);

            return TestControllerHelpers.AddUserToController(result);
        }
    }
}