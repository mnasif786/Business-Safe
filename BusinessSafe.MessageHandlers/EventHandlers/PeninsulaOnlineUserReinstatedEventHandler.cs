using BusinessSafe.Application.Contracts.Users;
using NServiceBus;
using Peninsula.Online.Messages.Events;

namespace BusinessSafe.MessageHandlers.EventHandlers
{
    public class PeninsulaOnlineUserReinstatedEventHandler: IHandleMessages<UserReinstated>
    {
        private readonly IUserService _userService;

        public PeninsulaOnlineUserReinstatedEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public void Handle(UserReinstated message)
        {
            _userService.ReinstateUser(message.UserId,message.ActioningUserId);
        }


    }
}
