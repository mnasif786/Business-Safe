using System.Collections.Generic;

using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Contracts.HazardousSubstanceInventory
{
    public interface IRiskPhraseService
    {
        IEnumerable<RiskPhraseDto> GetByStandard(HazardousSubstanceStandard standard);

        IEnumerable<RiskPhraseDto> GetAll();
    }
}
