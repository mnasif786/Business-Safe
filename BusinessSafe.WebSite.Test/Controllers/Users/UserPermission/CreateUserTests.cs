using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Users.Controllers;
using BusinessSafe.WebSite.Areas.Users.Factories;
using BusinessSafe.WebSite.Areas.Users.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.PeninsulaOnline;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Users.UserPermission
{
    [TestFixture]
    public class CreateUserTests
    {

        private Mock<INewRegistrationRequestService> _newRegistrationRequestService;
        private Mock<IAddUsersViewModelFactory> _userPermissionsViewModelFactory;
        private Mock<IEmployeeService> _employeeService;

        private AddUsersViewModel _baseAddUsersViewModel;
        private EmployeeDto _baseEmployeeDto;
        private Guid _newUserId;

        [SetUp]
        public void Setup()
        {
            _baseAddUsersViewModel = new AddUsersViewModel
            {
                CompanyId = 999L,
                EmployeeId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                RoleId = Guid.NewGuid(),
                SiteId = 10,
                SiteGroupId = null
            };

            _baseEmployeeDto = new EmployeeDto
            {
                Id = _baseAddUsersViewModel.EmployeeId,
                MainContactDetails = new EmployeeContactDetailDto
                {
                    Email = "testing@test.com",
                    Telephone2 = "333444555"
                }
            };
            _employeeService = new Mock<IEmployeeService>();
            _employeeService
                .Setup(x => x.ValidateRegisterAsUser(It.IsAny<CreateEmployeeAsUserRequest>()))
                .Returns(new ValidationResult());
            _employeeService
                .Setup(x => x.GetEmployee(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(_baseEmployeeDto);
            _employeeService
                .Setup(x => x.CreateUser(It.IsAny<CreateEmployeeAsUserRequest>()));


            _newUserId = Guid.NewGuid();
            _newRegistrationRequestService = new Mock<INewRegistrationRequestService>();
            _newRegistrationRequestService
                .Setup(x => x.RegisterNonAdminUser(It.IsAny<RegisterNonAdminUserRequest>()))
                .Returns(_newUserId);

            _newRegistrationRequestService
                .Setup(x => x.HasEmailBeenRegistered(It.IsAny<string>()))
                .Returns(false);

            _userPermissionsViewModelFactory = new Mock<IAddUsersViewModelFactory>();
            _userPermissionsViewModelFactory
                .Setup(x => x.GetViewModel(It.IsAny<long>(), It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(new AddUsersViewModel());

            _userPermissionsViewModelFactory
                .Setup(x => x.WithCurrentUser(It.IsAny<ICustomPrincipal>()))
                .Returns(_userPermissionsViewModelFactory.Object);
        }

        [Test]
        public void Given_valid_viewmodel_When_CreateUser_Then_INewRegistrationRequestService_RegisterNonAdminUser_called()
        {
            // Given
            var controller = CreateUserRoleController();

            // When
            controller.CreateUser(_baseAddUsersViewModel);

            // Then
            _newRegistrationRequestService.Verify(x => x.RegisterNonAdminUser(It.Is<RegisterNonAdminUserRequest>(
                y => y.ClientId == TestControllerHelpers.CompanyIdAssigned
                && y.RegistrationEmail == _baseEmployeeDto.MainContactDetails.Email
                && y.TelephoneNumber == _baseEmployeeDto.MainContactDetails.Telephone2
                )));
        }

        [Test]
        public void Given_valid_viewmodel_When_CreateUser_Then_IEmployeeService_ValidateRegisterAsUser_called()
        {
            // Given
            var controller = CreateUserRoleController();

            // When
            controller.CreateUser(_baseAddUsersViewModel);

            // Then
            _employeeService.Verify(x => x.ValidateRegisterAsUser(It.Is<CreateEmployeeAsUserRequest>(
                y => y.CompanyId == _baseAddUsersViewModel.CompanyId
                && y.EmployeeId == _baseAddUsersViewModel.EmployeeId
                && y.NewUserId == _newUserId
                && y.RoleId == _baseAddUsersViewModel.RoleId
                && y.SiteId == _baseAddUsersViewModel.SiteId
                )));
        }

        [Test]
        public void Given_valid_viewmodel_When_CreateUser_Then_IEmployeeService_RegisterAsUser_called()
        {
            // Given
            var controller = CreateUserRoleController();

            // When
            controller.CreateUser(_baseAddUsersViewModel);

            // Then
            _employeeService.Verify(x => x.CreateUser(It.Is<CreateEmployeeAsUserRequest>(
                    y => y.CompanyId == _baseAddUsersViewModel.CompanyId
                    && y.EmployeeId == _baseAddUsersViewModel.EmployeeId
                    && y.NewUserId == _newUserId
                    && y.RoleId == _baseAddUsersViewModel.RoleId
                    && y.SiteId == _baseAddUsersViewModel.SiteId
                    )));

            Assert.IsTrue(controller.ModelState.IsValid);
        }

        [Test]
        public void Given_valid_viewmodel_When_CreateUser_Then_call_PeninsulaOnline_user_service_to_see_if_user_exists_with_that_email()
        {
            // Given
            var controller = CreateUserRoleController();

            // When
            controller.CreateUser(_baseAddUsersViewModel);

            // Then
            _newRegistrationRequestService.Verify(x => x.HasEmailBeenRegistered(_baseEmployeeDto.MainContactDetails.Email));
        }

        [Test]
        public void Given_username_is_already_in_use_When_CreateUser_Then_do_not_call_EmployeeService_RegisterUser()
        {
            // Given
            _newRegistrationRequestService
                .Setup(x => x.HasEmailBeenRegistered(It.IsAny<string>()))
                .Returns(true);
            var controller = CreateUserRoleController();

            // When
            controller.CreateUser(_baseAddUsersViewModel);

            // Then
            _employeeService.Verify(x => x.CreateUser(It.IsAny<CreateEmployeeAsUserRequest>()), Times.Never());
        }

        [Test]
        public void Given_username_is_already_in_use_When_CreateUser_Then_return_Index_view()
        {
            // Given
            _newRegistrationRequestService
                .Setup(x => x.HasEmailBeenRegistered(It.IsAny<string>()))
                .Returns(true);
            var controller = CreateUserRoleController();

            // When
            var result = controller.CreateUser(_baseAddUsersViewModel) as ViewResult;

            // Then
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void Given_username_is_already_in_use_When_CreateUser_Then_returned_viewModel_is_rehydrated_by_viewModelFactory()
        {
            // Given
            _newRegistrationRequestService
                .Setup(x => x.HasEmailBeenRegistered(It.IsAny<string>()))
                .Returns(true);
            var controller = CreateUserRoleController();

            // When
            var result = controller.CreateUser(_baseAddUsersViewModel) as ViewResult;

            // Then
            _userPermissionsViewModelFactory.Verify(x => x.GetViewModel(_baseAddUsersViewModel.CompanyId, _baseAddUsersViewModel.EmployeeId, false, _baseAddUsersViewModel.CanChangeEmployeeDdl));
        }

        [Test]
        public void Given_username_is_already_in_use_When_CreateUser_Then_error_is_added_to_ModelState()
        {
            // Given
            _newRegistrationRequestService
                .Setup(x => x.HasEmailBeenRegistered(It.IsAny<string>()))
                .Returns(true);
            var controller = CreateUserRoleController();

            // When
            var result = controller.CreateUser(_baseAddUsersViewModel) as ViewResult;

            // Then
            Assert.That(controller.ModelState.Count, Is.GreaterThan(0));
        }

        private AddUsersController CreateUserRoleController()
        {
            var controller = new AddUsersController(
                _userPermissionsViewModelFactory.Object,
                null,
                _employeeService.Object,
                _newRegistrationRequestService.Object,
                null, null);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}