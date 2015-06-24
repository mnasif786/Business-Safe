using System.Collections.Generic;

using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IRiskPhraseRepository : IRepository<RiskPhrase, long>
    {
        IEnumerable<RiskPhrase> GetByStandard(HazardousSubstanceStandard standard);
    }
}
