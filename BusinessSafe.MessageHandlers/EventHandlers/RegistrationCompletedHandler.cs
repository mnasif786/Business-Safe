using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request.Users;
using BusinessSafe.Domain.RepositoryContracts;
using NServiceBus;
using Peninsula.Online.Messages.Events;

namespace BusinessSafe.MessageHandlers.EventHandlers
{
    public class RegistrationCompletedHandler : IHandleMessages<RegistrationCompleted>
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public RegistrationCompletedHandler(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        public void Handle(RegistrationCompleted message)
        {
            var user = _userRepository.GetByIdAndCompanyId(message.UserId, message.ClientId);

            if(user == null)
            {
                var request = new CreateAdminUserRequest()
                              {
                                  UserId = message.UserId,
                                  ClientId = message.ClientId,
                                  Forename = message.Forename,
                                  Surname = message.Surname,
                                  Email = message.Email
                              };

                _userService.CreateAdminUser(request);
            }

            _userService.RegisterUser(message.UserId);

            Log4NetHelper.Log.Info(string.Format("Registration Completed Event Handled For UserId : {0} And ClientId : {1}", message.UserId, message.ClientId));
        }
    }
}
