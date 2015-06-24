using System;

using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.Users
{
    public class AuthenticationTokenService : IAuthenticationTokenService
    {
        private readonly IUserRepository _userForAuditingRepository;
        private readonly IApplicationTokenRepository _applicationTokenRepository;
        private readonly IAuthenticationTokenRepository _authenticationTokenRepository;

        public AuthenticationTokenService(IAuthenticationTokenRepository authenticationTokenRepository, IUserRepository userForAuditingRepository, IApplicationTokenRepository applicationTokenRepository)
        {
            _userForAuditingRepository = userForAuditingRepository;
            _authenticationTokenRepository = authenticationTokenRepository;
            _applicationTokenRepository = applicationTokenRepository;
        }

        public AuthenticationTokenDto CreateAuthenticationToken(Guid userId, Guid applicationTokenId)
        {
            var user = _userForAuditingRepository.GetById(userId);
            var applicationToken = _applicationTokenRepository.GetById(applicationTokenId);
            var newAuthenticationToken = AuthenticationToken.Create(user, applicationToken);

            _authenticationTokenRepository.Save(newAuthenticationToken);

            return AuthenticationTokenDtoMapper.Map(newAuthenticationToken);
        }

        public AuthenticationTokenDto GetAuthenticationToken(Guid id)
        {
            var token = _authenticationTokenRepository.GetById(id);
            return AuthenticationTokenDtoMapper.Map(token);
        }

        public void DisableAuthenticationToken(Guid id)
        {
            var token = _authenticationTokenRepository.GetById(id);

            if (token != null)
            {
                token.IsEnabled = false;
                _authenticationTokenRepository.Save(token);
            }

        }

    }
}