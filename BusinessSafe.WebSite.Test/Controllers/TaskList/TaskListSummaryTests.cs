using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.TaskList.Controllers;
using BusinessSafe.WebSite.Areas.TaskList.ViewModels;
using BusinessSafe.WebSite.Contracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.TaskList
{
    [TestFixture]
    [Category("Unit")]
    public class TaskListSummaryTests
    {
        private Mock<ITaskListViewModelFactory> _mockViewModelFactory;
        private Mock<IUserService> _userService;

        [SetUp]
        public void SetUp()
        {
            _mockViewModelFactory = new Mock<ITaskListViewModelFactory>();
            _userService = new Mock<IUserService>();

            _mockViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
               .Setup(x => x.WithSiteGroupId(It.IsAny<long?>()))
               .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
                .Setup(x => x.WithSiteId(It.IsAny<long?>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
                .Setup(x => x.WithTaskCategoryId(It.IsAny<long>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
                .Setup(x => x.WithEmployeeId(It.IsAny<Guid?>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
               .Setup(x => x.WithAllowedSiteIds(It.IsAny<IList<long>>()))
               .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
               .Setup(x => x.GetSummaryViewModel())
               .Returns(new TaskListSummaryViewModel()
                            {
                                TotalOverdueTasks = 10,
                                TotalPendingTasks = 10
                            });

            _userService
                .Setup(x => x.GetIncludingEmployeeAndSiteByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(new UserDto() { Id = Guid.NewGuid(), Employee = new EmployeeDto() { Id = Guid.NewGuid() } });
        }

        [Test]
        public void Given_GetTaskListSummary_Then_correct_json_result_is_returned()
        {
            //Given
            var target = CreateController();

            //When
            var result = target.GetTaskListSummary(0, 0, 0, null);

            //Then            
            Assert.IsInstanceOf<JsonResult>(result);
        }

        [Test]
        public void Given_GetTaskListSummary_Then_ViewModelFactory_called()
        {
            //Given
            var target = CreateController();
            var companyId = 1; // defined in TestControllerHelpers.AddUserToController
            var siteGroupId = 456;
            var siteId = 789;
            var taskCategoryId = 135;
            var allowedSiteIds = target.CurrentUser.GetSitesFilter();
            var employeeId = Guid.NewGuid();

            //When
            target.GetTaskListSummary(siteGroupId, siteId, taskCategoryId, employeeId);

            //Then     
            _mockViewModelFactory
                .Verify(x => x.WithCompanyId(companyId));

            _mockViewModelFactory
                .Verify(x => x.WithSiteGroupId(siteGroupId));

            _mockViewModelFactory
                .Verify(x => x.WithSiteId(siteId));

            _mockViewModelFactory
                .Verify(x => x.WithTaskCategoryId(taskCategoryId));

            _mockViewModelFactory
                .Verify(x => x.WithEmployeeId(employeeId));

            _mockViewModelFactory
                .Verify(x => x.WithAllowedSiteIds(allowedSiteIds));
        }

        [Test]
        public void Given_GetTaskListSummary_Then_correct_result_returned()
        {
            //Given
            var target = CreateController();

            //When
            var result = target.GetTaskListSummary(0, 0);
            dynamic data = result.Data;

            //Then            
            Assert.That(data.TotalOverdueTasks, Is.EqualTo(10));
            Assert.That(data.TotalPendingTasks, Is.EqualTo(10));
        }

        private TaskListController CreateController()
        {
            var result = new TaskListController(_mockViewModelFactory.Object, _userService.Object);

            return TestControllerHelpers.AddUserToController(result);
        }    
    }
}