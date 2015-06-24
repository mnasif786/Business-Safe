
using System;

using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.Application.Contracts.Users
{
    public interface IAuthenticationTokenService
    {
        AuthenticationTokenDto CreateAuthenticationToken(Guid userId, Guid applicationToken);
        AuthenticationTokenDto GetAuthenticationToken(Guid id);
        void DisableAuthenticationToken(Guid id);
    }
}
