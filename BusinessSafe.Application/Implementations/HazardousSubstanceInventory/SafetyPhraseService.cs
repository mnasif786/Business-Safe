using System.Collections.Generic;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.HazardousSubstanceInventory
{
    public class SafetyPhraseService : ISafetyPhraseService
    {
        private readonly ISafetyPhraseRepository _safetyPhraseRepository;

        public SafetyPhraseService(ISafetyPhraseRepository SafetyPhraseRepository)
        {
            _safetyPhraseRepository = SafetyPhraseRepository;
        }

        public IEnumerable<SafetyPhraseDto> GetByStandard(HazardousSubstanceStandard standard)
        {
            var safetyPhrases = _safetyPhraseRepository.GetByStandard(standard);
            return new SafetyPhraseDtoMapper().Map(safetyPhrases);
        }

        public IEnumerable<SafetyPhraseDto> GetAll()
        {
            var safetyPhrases = _safetyPhraseRepository.GetAll();
            return new SafetyPhraseDtoMapper().Map(safetyPhrases);
        }
    }
}
