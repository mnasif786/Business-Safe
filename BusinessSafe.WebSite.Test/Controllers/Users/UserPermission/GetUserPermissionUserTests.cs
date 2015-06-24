using System;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.WebSite.Areas.Users.Controllers;
using BusinessSafe.WebSite.Areas.Users.Factories;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.PeninsulaOnline;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Users.UserPermission
{
    [TestFixture]
    [Category("Unit")]
    public class GetUserPermissionUserTests
    {
        private long _companyId;
        private Mock<INewRegistrationRequestService> _newRegistrationRequestService;
        private Mock<IAddUsersViewModelFactory> _userPermissionsViewModelFactory;
        private Mock<IUserService> _userService;
        private Mock<IEmployeeService>  _employeeService;

        [SetUp]
        public void Setup()
        {
            _newRegistrationRequestService = new Mock<INewRegistrationRequestService>();
            _userPermissionsViewModelFactory = new Mock<IAddUsersViewModelFactory>() { CallBase = true };

            _userService = new Mock<IUserService>();
            _employeeService = new Mock<IEmployeeService>();

            _userPermissionsViewModelFactory
                .Setup(x => x.WithCurrentUser(It.IsAny<ICustomPrincipal>()))
                .Returns(_userPermissionsViewModelFactory.Object);
        }

        [Test]
        public void Given_correct_parameters_When_GetUserPermissionUser_is_called_without_userId_Then_should_call_appropiate_methods()
        {
            // Given
            var controller = CreateUserRoleController();
            _companyId = 999L;

            // When
            controller.GetUserPermissionsEmployee(_companyId, null);

            // Then
            _userPermissionsViewModelFactory.Verify(x => x.GetViewModel(_companyId, null, false, true));
        }

        [Ignore]
        [Test]
        public void Given_correct_parameters_When_GetUserPermissionUser_is_called_with_userId_Then_should_call_appropiate_methods()
        {
            var controller = CreateUserRoleController();
            _companyId = 999L;
            var employeeId = Guid.NewGuid();
            controller.GetUserPermissionsEmployee(_companyId, employeeId);
            _userPermissionsViewModelFactory.Verify(x => x.GetViewModel(_companyId, employeeId, false, true));
        }

        private AddUsersController CreateUserRoleController()
        {
            var controller = new AddUsersController(
                _userPermissionsViewModelFactory.Object, 
                _userService.Object, 
                _employeeService.Object, 
                _newRegistrationRequestService.Object, 
                null, null);

            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
