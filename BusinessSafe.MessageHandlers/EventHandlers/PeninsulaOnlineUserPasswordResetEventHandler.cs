using BusinessSafe.Application.Contracts.Users;
using NServiceBus;
using Peninsula.Online.Messages.Events;

namespace BusinessSafe.MessageHandlers.EventHandlers
{
    public class PeninsulaOnlineUserPasswordResetEventHandler : IHandleMessages<UserPasswordReset>
    {
        private readonly IUserService _userService;

        public PeninsulaOnlineUserPasswordResetEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public void Handle(UserPasswordReset message)
        {
           _userService.DisableAuthenticationTokens(message.UserId, message.ActioningUserId);
        }
    }
}
