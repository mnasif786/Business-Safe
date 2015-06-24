using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.Request.Users;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.MessageHandlers.EventHandlers;
using Moq;
using NUnit.Framework;
using Peninsula.Online.Messages.Events;

namespace BusinessSafe.MessageHandlers.Test
{
    [TestFixture]
    public class RegistrationCompletedHandlerTests
    {

        [Test]
        [Ignore]
        public void Given_a_admin_user_registration_has_been_completed_When_handling_the_event_then_the_correct_request_object_is_created()
        {
            //GIVEN
            CreateAdminUserRequest requestObject = null; 
            var userService = new Mock<IUserService>();
            var userRepository = new Mock<IUserRepository>();
            userService.Setup(x => x.CreateAdminUser(It.IsAny<CreateAdminUserRequest>()))
                .Callback<CreateAdminUserRequest>(x => requestObject = x);
            var target = new RegistrationCompletedHandler(userService.Object, userRepository.Object);
            var forename = "RaRa";
            var surname = "Rasputin";

            //var message = new Peninsula.Online.Messages.Events.PeninsulaOnlineAdminUserRegistrationCompleted()();
            var message = new RegistrationCompleted
                              {
                                  ClientId = 123,
                                  IsAdmin = true,
                                  UserId = Guid.NewGuid()
                              };
            //WHEN
            target.Handle(message);

            //THEN
            Assert.IsNotNull(requestObject);
            Assert.AreEqual(forename, requestObject.Forename);
            Assert.AreEqual(surname, requestObject.Surname);
        }
    }
}
