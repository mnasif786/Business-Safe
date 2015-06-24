using System;

using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations
{
    public class ApplicationTokenService : IApplicationTokenService
    {
        private readonly IApplicationTokenRepository _applicationTokenRepository;

        public ApplicationTokenService(IApplicationTokenRepository applicationTokenRepository)
        {
            _applicationTokenRepository = applicationTokenRepository;
        }

        public ApplicationTokenDto GetById(Guid id)
        {
            var token = _applicationTokenRepository.GetById(id);
            return ApplicationTokenMapper.Map(token);
        }
    }
}