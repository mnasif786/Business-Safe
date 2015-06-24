using System.Collections.Generic;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class SafetyPhraseRepository : Repository<SafetyPhrase, long>, ISafetyPhraseRepository
    {
        public SafetyPhraseRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IEnumerable<SafetyPhrase> GetByStandard(HazardousSubstanceStandard standard)
        {
            return SessionManager.Session
                .CreateCriteria<SafetyPhrase>()
                .Add(Restrictions.Eq("Standard", standard))
                .List<SafetyPhrase>();
        }
    }
}
