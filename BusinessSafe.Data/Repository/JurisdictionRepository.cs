using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class JurisdictionRepository : Repository<Jurisdiction, long>, IJurisdictionRepository
    {
        public JurisdictionRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}