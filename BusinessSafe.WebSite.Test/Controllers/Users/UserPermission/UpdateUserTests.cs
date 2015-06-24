using System;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Users.Controllers;
using BusinessSafe.WebSite.Areas.Users.Factories;
using BusinessSafe.WebSite.Areas.Users.ViewModels;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.PeninsulaOnline;
using FluentValidation.Results;
using Moq;
using NServiceBus;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Users.UserPermission
{
    [TestFixture]
    [Category("Unit")]
    public class UpdateUserTests
    {
        private Mock<INewRegistrationRequestService> _newRegistrationRequestService;
        private Mock<IAddUsersViewModelFactory> _userPermissionsViewModelFactory;
        private Mock<IUserService> _userService;
        private Mock<IEmployeeService> _employeeService;
        private Mock<ICacheHelper> _cacheHelper;
        private Mock<IBus> _bus;
 
        [SetUp]
        public void Setup()
        {
            _newRegistrationRequestService = new Mock<INewRegistrationRequestService>();
            _userPermissionsViewModelFactory = new Mock<IAddUsersViewModelFactory>();
            _userService = new Mock<IUserService>();
            _employeeService = new Mock<IEmployeeService>();
            _cacheHelper = new Mock<ICacheHelper>();
            _bus = new Mock<IBus>();
        }

        [Test]
        public void Given_valid_viewmodel_is_entered_with_registered_employee_When_update_is_clicked_with_site_id_selected_Then_correct_user_service_is_called()
        {
            // Given
            var controller = CreateUserRoleController();

            var viewModel = new AddUsersViewModel
            {
                CompanyId = 999L,
                EmployeeId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                RoleId = Guid.NewGuid(),
                SiteId = 10,
                SiteGroupId = null,
                EmployeeAlreadyExistsAsUser = true
            };

            _userPermissionsViewModelFactory.Setup(x => x.GetViewModel(viewModel.CompanyId, viewModel.EmployeeId, true, true)).Returns(
               new AddUsersViewModel());

            // When
            controller.UpdateUser(viewModel);

            // Then
            _userService.Verify(x => x.SetRoleAndSite(It.Is<SetUserRoleAndSiteRequest>(
                y => y.CompanyId == viewModel.CompanyId
                    && y.UserToUpdateId == viewModel.UserId
                    && y.RoleId == viewModel.RoleId
                    && y.SiteId == viewModel.SiteId
                )));

            _cacheHelper.Verify(x => x.RemoveUser(viewModel.UserId));

            Assert.IsTrue(controller.ModelState.IsValid);
        }

        [Test]
        public void Given_valid_viewmodel_with_deleted_user_When_update_is_clicked_with_site_id_selected_Then_correct_user_service_is_called()
        {
            // Given
            var controller = CreateUserRoleController();

            var viewModel = new AddUsersViewModel
            {
                CompanyId = 999L,
                EmployeeId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                RoleId = Guid.NewGuid(),
                SiteId = 10,
                SiteGroupId = null,
                EmployeeAlreadyExistsAsUser = true,
                IsUserDeleted = true
            };

            _userPermissionsViewModelFactory.Setup(x => x.GetViewModel(viewModel.CompanyId, viewModel.EmployeeId, true, true)).Returns(
               new AddUsersViewModel());

            UserDto user = new UserDto
                               {
                                   Id = Guid.NewGuid(),
                                   Employee = new EmployeeDto
                                                  {
                                                      MainContactDetails = new EmployeeContactDetailDto
                                                                               {
                                                                                   Email = "1@1.com",
                                                                                   Telephone1 = "098098",
                                                                                   Telephone2 = "098098"

                                                                               }
                                                  }
                               };
            _userService.Setup(x => x.GetByIdAndCompanyIdIncludeDeleted(It.IsAny<Guid>(), It.IsAny<long>())).Returns(
                user);
            // When
            controller.UpdateUser(viewModel);

            // Then
            _userService.Verify(
                x => x.ReinstateUser(viewModel.UserId, TestControllerHelpers.UserIdAssigned));
        }
    
        private AddUsersController CreateUserRoleController()
        {
            var controller = new AddUsersController(
                _userPermissionsViewModelFactory.Object, 
                _userService.Object, 
                _employeeService.Object, 
                _newRegistrationRequestService.Object, 
                _cacheHelper.Object, _bus.Object);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
