using System;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Responsibilities.Controllers;
using BusinessSafe.WebSite.Areas.Responsibilities.Factories;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels;
using BusinessSafe.WebSite.Areas.TaskList.Controllers;
using BusinessSafe.WebSite.Areas.TaskList.ViewModels;
using Moq;
using NServiceBus;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.Responsibilities.ResponsibilityTasks
{
    [TestFixture]
    public class MarkForDeleteTest
    {
        private Mock<ISearchResponsibilityViewModelFactory> _searchViewModelFactory;
        private Mock<ITaskService> _taskService;
        private Mock<IResponsibilityTaskService> _responsibilityTaskService;
        private Mock<IResponsibilityViewModelFactory> _createViewModelFactory;
        private Mock<IResponsibilitiesService> _responsibilitiesService;
        private Mock<ICreateUpdateResponsibilityTaskViewModelFactory> _createUpdateResponsibilityTaskViewModelFactory;
        private Mock<ICompleteResponsibilityTaskViewModelFactory> _completeResponsibilityTaskViewModelFactory;
        private Mock<IBus> _bus;
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;

        private long taskId;
        private long companyId;

        [SetUp]
        public void Setup()
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
            _taskService = new Mock<ITaskService>();
            _responsibilityTaskService = new Mock<IResponsibilityTaskService>();

            _createUpdateResponsibilityTaskViewModelFactory =
                new Mock<ICreateUpdateResponsibilityTaskViewModelFactory>();

            _completeResponsibilityTaskViewModelFactory =
                new Mock<ICompleteResponsibilityTaskViewModelFactory>();

            _bus = new Mock<IBus>();
            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();

            taskId = 2;
            companyId = 10;
        }

        [Test]
        public void
            Given_valid_responsibility_task_id_and_company_id_When_mark_responsibility_task_for_delete_Then_should_call_appropiate_methods
            ()
        {
            //Given
            var target = CreateController();

            var markTaskAsDeleteViewModel = new MarkResponsibilityTaskAsDeletedViewModel()
                                                {
                                                    CompanyId = companyId,
                                                    TaskId = taskId
                                                };

            //When
            target.MarkTaskAsDeleted(markTaskAsDeleteViewModel);

            //Then
            _taskService.Verify(x => x.MarkTaskAsDeleted(It.Is<MarkTaskAsDeletedRequest>(y => y.TaskId == taskId &&
                                                                                              y.CompanyId == companyId &&
                                                                                              y.UserId ==
                                                                                              target.CurrentUser.UserId)));
        }

        [Test]
        public void
            Given_invalid_responsibility_task_id_and_company_id_When_mark_responsibility_task_for_delete_Then_should_throw_correct_exception
            ()
        {
            //Given
            taskId = 0;
            companyId = 0;
            var target = CreateController();

            var markTaskAsDeleteViewModel = new MarkResponsibilityTaskAsDeletedViewModel()
                                                {
                                                    CompanyId = companyId,
                                                    TaskId = taskId
                                                };

            target.ModelState.AddModelError("", "");

            //When
            //Then
            Assert.Throws<ArgumentException>(() => target.MarkTaskAsDeleted(markTaskAsDeleteViewModel));
        }

        [Test]
        public void
            Given_valid_responsibility_task_id_and_company_id_When_mark_responsibility_task_for_delete_Then_should_return_correct_result
            ()
        {
            //Given
            var target = CreateController();
            var markTaskAsDeleteViewModel = new MarkResponsibilityTaskAsDeletedViewModel()
                                                {
                                                    CompanyId = companyId,
                                                    TaskId = taskId
                                                };

            //When
            var result = target.MarkTaskAsDeleted(markTaskAsDeleteViewModel);

            //Then
            dynamic data = result.Data;
            Assert.That(data.Success, Is.EqualTo(true));
        }

        private ResponsibilityController CreateController()
        {
            var result = new ResponsibilityController(
                _responsibilitiesService.Object,
                _taskService.Object,
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