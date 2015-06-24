using System;

using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Users.Controllers;
using BusinessSafe.WebSite.PeninsulaOnline;
using Moq;
using NServiceBus;
using NUnit.Framework;
using Peninsula.Online.Messages.Commands;

namespace BusinessSafe.WebSite.Tests.Controllers.Users.UserSearch
{
    [TestFixture]
    [Category("Unit")]
    public class MarkUserAsEnabledOrDisabledTests
    {
        private Mock<IUserService> _userService;
        private Mock<IBus> _bus;
        private const long _companyId = 200;
        private readonly Guid _userId = Guid.NewGuid();
        private Mock<INewRegistrationRequestService> _newRegistrationRequestService;

        [SetUp]
        public void Setup()
        {
            _userService = new Mock<IUserService>();
            _userService.Setup(x => x.GetIncludingEmployeeAndSiteByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>())).Returns(() => new UserDto { Employee = new EmployeeDto { MainContactDetails = new EmployeeContactDetailDto { Email = "test@test.com" } } });
            _userService.Setup(x => x.GetByIdAndCompanyIdIncludeDeleted(It.IsAny<Guid>(), It.IsAny<long>())).Returns(() => new UserDto { Employee = new EmployeeDto { MainContactDetails = new EmployeeContactDetailDto { Email = "test@test.com" } } });
            _bus = new Mock<IBus>();

            _newRegistrationRequestService = new Mock<INewRegistrationRequestService>();
            _newRegistrationRequestService.Setup(x => x.HasEmailBeenRegistered(It.IsAny<string>())).Returns(false);
        }

        [Test]
        public void Given_invalid_request_When_MarkUserAsEnabledOrDisabled_Then_should_throw_exception()
        {
            // Given
            var controller = CreateUserSearchController();

            // When
            // Then
            Assert.Throws<ArgumentException>(() => controller.MarkUserAsEnabledOrDisabled(0, string.Empty, true, string.Empty));
        }

        [Test]
        [Ignore]
        public void Given_valid_request_to_disable_user_When_MarkUserAsEnabledOrDisabled_Then_should_call_delete_user()
        {
            // Given
            var controller = CreateUserSearchController();
            var currentUserId = controller.CurrentUser.UserId;

            // When
            controller.MarkUserAsEnabledOrDisabled(_companyId, _userId.ToString(), true, "test@test.com");

            // Then
            _userService.Verify(x => x.DeleteUser(_userId, _companyId, currentUserId));
        }

        [Test]
        [Ignore]
        public void Given_valid_request_to_enable_user_When_MarkUserAsEnabledOrDisabled_Then_should_call_Reinstate_User(){
            // Given
            var controller = CreateUserSearchController();
            var currentUserId = controller.CurrentUser.UserId;

            // When
            controller.MarkUserAsEnabledOrDisabled(_companyId, _userId.ToString(), false, "test@test.com");

            // Then
            _userService.Verify(x => x.ReinstateUser(_userId, _companyId, currentUserId));
        }

        private ViewUsersController CreateUserSearchController()
        {
            var result = new ViewUsersController(null, null, _userService.Object, _bus.Object, new Mock<INewRegistrationRequestService>().Object, null, null);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}