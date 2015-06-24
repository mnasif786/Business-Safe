using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;
using BusinessSafe.WebSite.Areas.TaskList.Factories;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.TaskListViewModelFactoryTests
{
    [TestFixture]
    [Category("Unit")]
    public class TaskListSummaryViewModelFactoryTests
    {
        private Mock<IEmployeeService> _employeeService;
        private Mock<ITaskService> _taskService;
        private Mock<IUserService> _userService;
        private Mock<ISiteGroupService> _siteGroupService;
        private Mock<ISiteService> _siteService;
        private long _companyId;
        long? _siteGroupId;
        long? _siteId;
        long _taskCategoryId;
        Guid _employeeId;
        private List<long> _allowedSiteIds;

        [SetUp]
        public void SetUp()
        {
            _employeeService = new Mock<IEmployeeService>();
            _taskService = new Mock<ITaskService>();
            _userService = new Mock<IUserService>();
            _siteGroupService = new Mock<ISiteGroupService>();
            _siteService = new Mock<ISiteService>();
            _companyId = 250;
            _siteGroupId = 1;
            _siteId = 1;
            _taskCategoryId = 1;
            _employeeId = Guid.NewGuid();
            _allowedSiteIds = new List<long>() { 1, 2, 3 };
        }

        [Test]
        public void Given_GetSummaryViewModel_Then_calls_correct_methods()
        {
            //Given
            var target = CreateTarget();

            var user = new UserDto()
            {
                CompanyId = _companyId,
                Id = Guid.NewGuid()
            };

            var passedRequest = new SearchTasksRequest();

            _taskService
                .Setup(x => x.GetOutstandingTasksSummary(It.IsAny<SearchTasksRequest>()))
                .Returns(new TaskListSummaryResponse())
                .Callback<SearchTasksRequest>(y => passedRequest = y);

            //When
            target
                .WithCompanyId(_companyId)
                .WithSiteGroupId(_siteGroupId)
                .WithSiteId(_siteId)
                .WithTaskCategoryId(_taskCategoryId)
                .WithEmployeeId(_employeeId)
                .WithAllowedSiteIds(_allowedSiteIds)
                .GetSummaryViewModel();

            //Then
            _taskService.Verify(x => x.GetOutstandingTasksSummary(It.IsAny<SearchTasksRequest>()), Times.Once());
            Assert.That(passedRequest.CompanyId, Is.EqualTo(_companyId));
            Assert.That(passedRequest.SiteGroupId, Is.EqualTo(_siteGroupId));
            Assert.That(passedRequest.SiteId, Is.EqualTo(_siteId));
            Assert.That(passedRequest.TaskCategoryId, Is.EqualTo(_taskCategoryId));
            Assert.That(passedRequest.EmployeeIds, Is.EqualTo(new List<Guid>() { _employeeId }));
            Assert.That(passedRequest.AllowedSiteIds, Is.EqualTo(_allowedSiteIds));
        }

        private TaskListViewModelFactory CreateTarget()
        {
            return new TaskListViewModelFactory(_employeeService.Object, _taskService.Object, _userService.Object, _siteGroupService.Object, _siteService.Object, null);
        }
    }
}