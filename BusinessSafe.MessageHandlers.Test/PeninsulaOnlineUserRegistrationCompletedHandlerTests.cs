using System;

using BusinessSafe.Application.MessageHandlers.EventHandlers;
using NUnit.Framework;
using Peninsula.Online.Messages.Events;

namespace BusinessSafe.Application.MessageHandlers.Test
{
    [TestFixture]
    public class PeninsulaOnlineUserRegistrationCompletedHandlerTests
    {
        [Test]
        public void Test1()
        {
            Bootstrapper.Run();
            var message = new PeninsulaOnlineAdminUserRegistrationCompleted
                              {
                                  ClientId = 999L,
                                  UserId = Guid.NewGuid()
                              };

            var handler = new PeninsulaOnlineUserRegistrationCompletedHandler();
            handler.Handle(message);
        }
    }
}
