using System;
using BusinessSafe.Application.Contracts;
using BusinessSafe.WebSite.PeninsulaOnline;

namespace BusinessSafe.WebSite.ServiceProxies
{
    public class PeninsulaOnlineRegistrationService : IUserRegistrationService
    {
        private readonly INewRegistrationRequestService _newRegistrationRequestService;

        public PeninsulaOnlineRegistrationService( INewRegistrationRequestService newRegistrationRequestService)
        {
            _newRegistrationRequestService = newRegistrationRequestService;
        }

        public bool HasEmailBeenRegistered(string email)
        {
            return _newRegistrationRequestService.HasEmailBeenRegistered(email);
        }
    }
}