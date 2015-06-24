using System.Collections.Generic;

using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.HazardousSubstanceInventory
{
    public class RiskPhraseService : IRiskPhraseService
    {
        private readonly IRiskPhraseRepository riskPhraseRepository;

        public RiskPhraseService(IRiskPhraseRepository riskPhraseRepository)
        {
            this.riskPhraseRepository = riskPhraseRepository;
        }

        public IEnumerable<RiskPhraseDto> GetByStandard(HazardousSubstanceStandard standard)
        {
            var riskPhrases = riskPhraseRepository.GetByStandard(standard);
            return new RiskPhraseDtoMapper().Map(riskPhrases);
        }

        public IEnumerable<RiskPhraseDto> GetAll()
        {
            var riskPhrases = riskPhraseRepository.GetAll();
            return new RiskPhraseDtoMapper().Map(riskPhrases);
        }
    }
}
