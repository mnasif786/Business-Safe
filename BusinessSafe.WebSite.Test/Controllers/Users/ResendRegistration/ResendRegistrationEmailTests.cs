using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Employees.ViewModels;
using BusinessSafe.WebSite.Areas.Users.Controllers;
using BusinessSafe.WebSite.PeninsulaOnline;
using Moq;
using NServiceBus;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using Peninsula.Online.Messages.Commands;

namespace BusinessSafe.WebSite.Tests.Controllers.Users.ResendRegistration
{
    [TestFixture]
    public class ResendRegistrationEmailTests
    {
        private Guid _userId;
        private Guid _employeeId;
        private string _email;

        private Mock<IEmployeeService> _employeeService;
        private Mock<IBus> _bus;
        public Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;
        public Mock<INewRegistrationRequestService> _newRegistrationRequestService;
        private Mock<IUserService> _userService;
        [SetUp]
        public void Setup()
        {
            _userId = Guid.NewGuid();
            _employeeId = Guid.NewGuid();
            _email = "test@hotmail.com";

            _employeeService = new Mock<IEmployeeService>();
            _employeeService.Setup(x => x.UpdateEmailAddress(It.IsAny<UpdateEmployeeEmailAddressRequest>()));

            _bus = new Mock<IBus>();
            _bus.Setup(x => x.Send());

            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();
            _newRegistrationRequestService = new Mock<INewRegistrationRequestService>();
            _userService = new Mock<IUserService>();
        }

        [Test]
        [Ignore]
        // Index superceeded, UpdateEmailAddress called in Controllers/ViewUserController
        public void When_Index_Then_calls_correct_methods()
        {
            // Given
            var target = GetTarget();

            var currentUserId = target.CurrentUser.UserId;
            var userIdUpdating = Guid.NewGuid();
            var companyId = target.CurrentUser.CompanyId;

            _newRegistrationRequestService
                .Setup(x => x.HasEmailBeenRegistered(It.IsAny<string>()))
                .Returns(false);

            // When
            target.Index(new ResendUserRegistrationEmailViewModel
                             {
                                 EmployeeId = _employeeId,
                                 UserId = userIdUpdating,
                                 Email = _email,
                                 CompanyId = companyId,
                             });
        
            // Then
            _employeeService.Verify(x => x.UpdateEmailAddress(It.Is<UpdateEmployeeEmailAddressRequest>(
                y =>
                    y.EmployeeId == _employeeId &&
                    y.Email == _email &&
                    y.CompanyId == companyId &&
                    y.CurrentUserId == currentUserId
                    )), Times.Once());
            _bus.Verify(x => x.Send(It.Is<ResetPendingUsersUserName>(y => y.UserId == userIdUpdating && y.Email == _email)), Times.Once());
            _businessSafeSessionManager.Verify(x => x.CloseSession());
            _newRegistrationRequestService.VerifyAll();
        }

        [Test]
        public void Given_valid_request_When_Index_Then_returns_json_result_success_is_true()
        {
            // Given
            var target = GetTarget();

            var currentUserId = target.CurrentUser.UserId;
            var companyId = target.CurrentUser.CompanyId;

            _newRegistrationRequestService
               .Setup(x => x.HasEmailBeenRegistered(It.IsAny<string>()))
               .Returns(false);

            var controller = getViewUsersController();
         
            var result = controller.UpdateUserRegistration(_userId.ToString(), "", _email);
            
            // Then
            Assert.That(result.Data.ToString(), Is.EqualTo("{ Success = True }"));
        }

        [Test]
        public void Given_invalid_request_When_Index_Then_returns_json_result_is_success_is_false()
        {
            // Given
            var target = GetTarget();

            target.ModelState.AddModelError("some error", "some message");

            var currentUserId = target.CurrentUser.UserId;
            var companyId = target.CurrentUser.CompanyId;

            // When
            var result = target.Index(new ResendUserRegistrationEmailViewModel
            {
                EmployeeId = _employeeId,
                UserId = currentUserId,
                Email = "not valid email",
                CompanyId = companyId,
            }) as JsonResult;

            // Then
            Assert.That(result.Data.ToString(),Contains.Substring("Success = False"));
        }

        [Test]
        public void Given_invalid_request_registration_email_is_already_in_use_When_Index_Then_returns_json_result_is_success_is_false()
        {
            // Given
            var target = GetTarget();

            _newRegistrationRequestService
               .Setup(x => x.HasEmailBeenRegistered(It.IsAny<string>()))
               .Returns(true);

            var currentUserId = target.CurrentUser.UserId;
            var companyId = target.CurrentUser.CompanyId;

            // When
            var result = target.Index(new ResendUserRegistrationEmailViewModel
            {
                EmployeeId = _employeeId,
                UserId = currentUserId,
                Email = "not valid email",
                CompanyId = companyId,
            }) as JsonResult;

            // Then
            Assert.That(result.Data.ToString(), Contains.Substring("Success = False"));
        }

        private ResendRegistrationEmailController GetTarget()
        {
            var controller = new ResendRegistrationEmailController(
                _employeeService.Object,
                _bus.Object,
                _businessSafeSessionManager.Object,
                _newRegistrationRequestService.Object
                );

            return TestControllerHelpers.AddUserToController(controller);
        }

        private ViewUsersController getViewUsersController()
        {
            var result = new ViewUsersController(null, null, _userService.Object, _bus.Object, _newRegistrationRequestService.Object, _businessSafeSessionManager.Object, null);
            TestControllerHelpers.AddUserToController(result);
            return result;
        }
    }
}
