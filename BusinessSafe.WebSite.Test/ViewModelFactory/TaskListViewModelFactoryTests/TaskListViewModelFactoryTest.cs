using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.TaskList.Factories;
using BusinessSafe.WebSite.AuthenticationService;
using Moq;
using NHibernate;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory
{
    [TestFixture]
    [Category("Unit")]
    public class TaskListViewModelFactoryTest
    {
        private Mock<IEmployeeService> _employeeService;
        private Mock<ITaskService> _taskService;
        private Mock<IUserService> _userService;
        private Mock<ISiteGroupService> _siteGroupService;
        private Mock<ISiteService> _siteService;

        private long _companyId;

        [SetUp]
        public void SetUp()
        {
            _employeeService = new Mock<IEmployeeService>();
            _taskService = new Mock<ITaskService>();
            _userService = new Mock<IUserService>();
            _siteGroupService = new Mock<ISiteGroupService>();
            _siteService = new Mock<ISiteService>();
            _companyId = 250;

            _employeeService.Setup(x => x.GetEmployeeNames(It.IsAny<long>()))
                .Returns(() => new List<EmployeeName>());
        }

        [Test]
        public void Given_search_for_current_user_When_GetViewModel_is_called_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateTarget();

            var user = new UserDto()
                              {
                                  CompanyId = _companyId, 
                                  Id = Guid.NewGuid(),
                                  Employee =  new EmployeeDto() {Id = Guid.NewGuid()}
                                  
                              };

            _userService
                .Setup(x => x.GetIncludingEmployeeAndSiteByIdAndCompanyId(user.Id, user.CompanyId))
                .Returns(user);

            var createdFrom = DateTime.Now.AddDays(1);
            var createdTo = DateTime.Now.AddDays(40);

            _taskService
                .Setup(x => x.Search(It.Is<SearchTasksRequest>(y => y.CompanyId == _companyId &&
                                                                    y.CompletedFrom.Value.ToShortDateString() == createdFrom.ToShortDateString() && 
                                                                    y.CompletedTo.Value.ToShortDateString() == createdTo.ToShortDateString())));

            

            //When
            var allowedSiteIds = new List<long>(){1,2,3};
            target
                .WithEmployeeId(null)
                .WithUser(CreateCustomPrincipal(user))
                .WithCompanyId(_companyId)
                .WithCompletedFrom(createdFrom.ToShortDateString())
                .WithCompletedTo(createdTo.ToShortDateString())
                .WithAllowedSiteIds(allowedSiteIds)
                .WithUserEmployeeId(CreateCustomPrincipal(user))
                .GetViewModel();

            //Then
            _userService.VerifyAll();
            _taskService.VerifyAll();
            
            _siteGroupService.Verify(x => x.GetByCompanyId(_companyId));
            _siteService.Verify(
                x =>
                x.Search(It.Is<SearchSitesRequest>(y => y.CompanyId == _companyId && y.AllowedSiteIds == allowedSiteIds)));
        }


        [Test]
        public void Given_search_by_title_When_GetViewModel_Then_passed_requested_title_to_task_service()
        {
            //Given
            var target = CreateTarget();

            const string title = "title";

            var user = new UserDto()
            {
                CompanyId = _companyId,
                Id = Guid.NewGuid(),
                Employee = new EmployeeDto() { Id = Guid.NewGuid() }

            };

            _userService
                .Setup(x => x.GetIncludingEmployeeAndSiteByIdAndCompanyId(user.Id, user.CompanyId))
                .Returns(user);

            var createdFrom = DateTime.Now.AddDays(1);
            var createdTo = DateTime.Now.AddDays(40);

            _taskService
                .Setup(x => x.Search(It.IsAny<SearchTasksRequest>()));



            //When
            var allowedSiteIds = new List<long>() { 1, 2, 3 };
            target
                .WithTitle(title)
                .GetViewModel();

            //Then
            _taskService.Verify(x => x.Search(It.Is<SearchTasksRequest>(y => y.Title == title)));
        }

        private static CustomPrincipal CreateCustomPrincipal(UserDto userDto)
        {
            var customPrincipal = new CustomPrincipal(userDto, new CompanyDto());
            return customPrincipal;
        }

        private TaskListViewModelFactory CreateTarget()
        {
            var sessionManager = new Mock<IBusinessSafeSessionManager>();
            var session = new Mock<ISession>();
            sessionManager.SetupGet(x => x.Session).Returns(session.Object);

            return new TaskListViewModelFactory(_employeeService.Object, _taskService.Object, _userService.Object, _siteGroupService.Object, _siteService.Object, sessionManager.Object);
        }
    }
}
