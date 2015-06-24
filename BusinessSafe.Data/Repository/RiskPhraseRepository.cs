using System.Collections.Generic;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class RiskPhraseRepository : Repository<RiskPhrase, long>, IRiskPhraseRepository
    {
        public RiskPhraseRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IEnumerable<RiskPhrase> GetByStandard(HazardousSubstanceStandard standard)
        {
            return SessionManager.Session
                .CreateCriteria<RiskPhrase>()
                .Add(Restrictions.Eq("Standard", standard))
                .List<RiskPhrase>();
        }
    }
}
