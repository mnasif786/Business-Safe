using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.TaskList.Controllers;
using BusinessSafe.WebSite.Areas.TaskList.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Contracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.TaskList
{
    [TestFixture]
    [Category("Unit")]
    public class FindTests
    {
        private Mock<ITaskListViewModelFactory> _mockViewModelFactory;
        private Mock<IUserService> _userService;
        private Guid? _employeeId = Guid.NewGuid();


        [SetUp]
        public void SetUp()
        {
            _mockViewModelFactory = new Mock<ITaskListViewModelFactory>();
            _userService = new Mock<IUserService>();

            _mockViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
                .Setup(x => x.WithTitle(It.IsAny<string>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
                .Setup(x => x.WithEmployeeId(It.IsAny<Guid>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
                .Setup(x => x.WithCreatedFrom(It.IsAny<string>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
                .Setup(x => x.WithCreatedTo(It.IsAny<string>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
                .Setup(x => x.WithCompletedFrom(It.IsAny<string>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
                .Setup(x => x.WithCompletedTo(It.IsAny<string>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
                .Setup(x => x.WithTaskCategoryId(It.IsAny<long>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
                .Setup(x => x.WithTaskStatusId(It.IsAny<int>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
                .Setup(x => x.WithUser(It.IsAny<CustomPrincipal>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
                .Setup(x => x.WithBulkReassignMode(It.IsAny<bool>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
                .Setup(x => x.WithShowDeleted(It.IsAny<bool>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
                .Setup(x => x.WithShowCompleted(It.IsAny<bool>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
                .Setup(x => x.WithSiteId(It.IsAny<long?>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
                .Setup(x => x.WithSiteGroupId(It.IsAny<long?>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
                .Setup(x => x.WithAllowedSiteIds(It.IsAny<IList<long>>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
                .Setup(x => x.WithTitle(It.IsAny<String>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
                .Setup(x => x.WithPage(It.IsAny<int>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
                .Setup(x => x.WithPageSize(It.IsAny<int>()))
                .Returns(_mockViewModelFactory.Object);

            _mockViewModelFactory
                .Setup(x => x.WithOrderBy(It.IsAny<string>()))
                .Returns(_mockViewModelFactory.Object);

            _userService
                .Setup(x => x.GetIncludingEmployeeAndSiteByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(new UserDto() {Id = Guid.NewGuid(), Employee = new EmployeeDto(){Id = Guid.NewGuid()}});
        }

        [Test]
        public void Given_find_Then_correct_view_is_returned()
        {
            //Given
            const string viewNameExpected = "Index";
            var target = CreateController();

            //When
            var result = target.Find(_employeeId, null, null, null, null, null, 0, 0);

            //Then            
            Assert.That(result.ViewName, Is.EqualTo(viewNameExpected));
        }

        [Test]
        public void Given_find_Then_correct_view_model_is_returned()
        {
            //Given
            var target = CreateController();

            var viewModel = new TaskListViewModel();
            _mockViewModelFactory.Setup(x => x.GetViewModel()).Returns(viewModel);

            //When
            var result = target.Find(_employeeId, null, null, null, null, null, 0, 0);

            //Then
            Assert.That(result.Model, Is.Not.Null);
            Assert.That(result.Model, Is.TypeOf<TaskListViewModel>());
        }

        [Test]
        public void Given_find_Then_correct_methods_are_called()
        {
            //Given            
            var target = CreateController();

            //When
            target.Find(_employeeId, null, null, null, null, null, 0, 0);

            //Then
            _mockViewModelFactory.Verify(x => x.GetViewModel());
        }

        private TaskListController CreateController()
        {
            var result = new TaskListController(_mockViewModelFactory.Object, _userService.Object);

            return TestControllerHelpers.AddUserToController(result);
        }
    }
}