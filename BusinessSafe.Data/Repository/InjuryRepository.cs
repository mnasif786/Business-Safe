using System.Collections.Generic;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class InjuryRepository : Repository<Injury, long>, IInjuryRepository
    {
        public InjuryRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IEnumerable<Injury> GetAllInjuriesForAccidentRecord(long companyId, long accidentRecord)
        {
            return SessionManager
                 .Session
                 .CreateCriteria<Injury>()
                 .Add(Restrictions.Or(Restrictions.Eq("CompanyId", companyId), Restrictions.IsNull("CompanyId")))
                 .Add(Restrictions.Eq("Deleted", false))
                 .Add(Restrictions.Or(Restrictions.Eq("AccidentRecordId", accidentRecord), Restrictions.IsNull("AccidentRecordId")))
                 .SetMaxResults(500)
                 .List<Injury>();
        }
    }
}