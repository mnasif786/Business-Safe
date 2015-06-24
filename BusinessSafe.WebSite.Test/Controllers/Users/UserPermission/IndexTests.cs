using System;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.WebSite.Areas.Users.Controllers;
using BusinessSafe.WebSite.Areas.Users.Factories;
using BusinessSafe.WebSite.PeninsulaOnline;

using Moq;

using NUnit.Framework;
using System.Security.Principal;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.Tests.Controllers.Users.UserPermission
{
    [TestFixture]
    [Category("Unit")]
    public class IndexTests
    {
        private long _companyId;
        private Mock<INewRegistrationRequestService> _newRegistrationRequestService;
        private Mock<IAddUsersViewModelFactory> _userPermissionsViewModelFactory;
        private Mock<IUserService> _userService;
        private Mock<IEmployeeService> _employeeService;

        [SetUp]
        public void Setup()
        {
            _newRegistrationRequestService = new Mock<INewRegistrationRequestService>();
            _userPermissionsViewModelFactory = new Mock<IAddUsersViewModelFactory>();
            _userService = new Mock<IUserService>();
            _employeeService = new Mock<IEmployeeService>();

            _userPermissionsViewModelFactory
                .Setup(x => x.WithCurrentUser(It.IsAny<CustomPrincipal>()))
                .Returns(_userPermissionsViewModelFactory.Object);
        }

        [Test]
        public void When_no_user_id_supplied_then_correct_view_is_returned()
        {
            var controller = CreateUserRoleController();
            _companyId = 999L;
            var result = controller.Index(_companyId, null, false) as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo(""));
        }

        [Test]
        public void When_employee_id_supplied_then_should_call_correct_methods()
        {
            var controller = CreateUserRoleController();
            _companyId = 999L;
            var employeeId = Guid.NewGuid();
            controller.Index(_companyId, employeeId, false);
            _userPermissionsViewModelFactory.Verify(x => x.GetViewModel(_companyId, employeeId, false, false));
        }

        private AddUsersController CreateUserRoleController()
        {
            var controller = new AddUsersController(
                _userPermissionsViewModelFactory.Object, 
                _userService.Object, 
                _employeeService.Object, 
                _newRegistrationRequestService.Object, 
                null,null);

            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
