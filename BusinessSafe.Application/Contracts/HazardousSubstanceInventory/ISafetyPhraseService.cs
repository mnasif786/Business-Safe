using System.Collections.Generic;

using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Contracts.HazardousSubstanceInventory
{
    public interface ISafetyPhraseService
    {
        IEnumerable<SafetyPhraseDto> GetByStandard(HazardousSubstanceStandard standard);

        IEnumerable<SafetyPhraseDto> GetAll();
    }
}
