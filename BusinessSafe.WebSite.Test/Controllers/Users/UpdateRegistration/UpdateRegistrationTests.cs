using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.Custom_Exceptions;
using BusinessSafe.WebSite.Areas.Users.Controllers;
using BusinessSafe.WebSite.PeninsulaOnline;
using Moq;
using NServiceBus;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using Peninsula.Online.Messages.Commands;

namespace BusinessSafe.WebSite.Tests.Controllers.Users.UpdateRegistration
{
    public class UpdateRegistrationTests
    {
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;
        private Mock<INewRegistrationRequestService> _newRegistrationRequestService;
        private Mock<IUserService> _userService;

        private Mock<IBus> _bus;
        [SetUp]
        public void Setup()
        {
            _newRegistrationRequestService = new Mock<INewRegistrationRequestService>();
            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();
            _userService = new Mock<IUserService>();

            _bus = new Mock<IBus>();
            _bus.Setup(x => x.Send());
        }

        [Test]
        public void Given_valid_request_when_update_registration_details_then_call_service_with_correct_parameters()
        {
            // Given
            var target = GetTarget();
            
            var currentUserId = target.CurrentUser.UserId;
            var securityAnswer = "212151551";
            var assigningUserId = Guid.NewGuid().ToString();
            var email = "somefellow@email.com";

            //when

            var controller = GetTarget();
            controller.UpdateUserRegistration(assigningUserId, securityAnswer, email);
            //then

            _bus.Verify(x => x.Send(It.Is<UpdateUserRegistration>(y => y.UserId.ToString() == assigningUserId && y.ActioningUserId == currentUserId && y.Email == email)), Times.Once());
        }

        [Test]
        public void Given_email_address_already_registered_in_peninsula_online_return_error()
        {
            // Given
            var securityAnswer = "212151551";
            var assigningUserId = Guid.NewGuid().ToString();
            var email = "somefellow@email.com";

            //when
            _newRegistrationRequestService.Setup(x => x.HasEmailBeenRegistered(It.IsAny<string>()))
                                         .Returns(true);

            _userService.Setup(
                x => x.UpdateEmailAddress(It.IsAny<Guid>(), It.IsAny<long>(), It.IsAny<string>(), It.IsAny<Guid>())).
                Throws(new EmailRegisteredToOtherUserException());

            var controller = GetTarget();

            var result = controller.UpdateUserRegistration(assigningUserId, securityAnswer, email);

            //then
            Assert.That(result.Data.ToString(), Contains.Substring("{ Success = False, Errors = Sorry you are unable to update this user: the email address has been registered to another user }"));
        }
        
        private ViewUsersController GetTarget()
        {
            var result = new ViewUsersController(null, null, _userService.Object, _bus.Object, _newRegistrationRequestService.Object, _businessSafeSessionManager.Object, null);
            TestControllerHelpers.AddUserToController(result);
            return result;
        }
    }
}
