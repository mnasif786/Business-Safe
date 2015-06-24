using BusinessSafe.Application.Contracts.Users;
using NServiceBus;
using Peninsula.Online.Messages.Events;

namespace BusinessSafe.MessageHandlers.EventHandlers
{
    public class PeninsulaOnlineUserDeletedEventHandler : IHandleMessages<UserDeleted>
    {
        private readonly IUserService _userService;

        public PeninsulaOnlineUserDeletedEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public void Handle(UserDeleted message)
        {
            _userService.DeleteUser(message.UserId, message.ActioningUserId);
        }
    }
}
