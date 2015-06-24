using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface ISafetyPhraseRepository : IRepository<SafetyPhrase, long>
    {
        IEnumerable<SafetyPhrase> GetByStandard(HazardousSubstanceStandard standard);
    }
}
