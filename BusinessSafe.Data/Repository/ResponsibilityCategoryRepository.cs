using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class ResponsibilityCategoryRepository : Repository<ResponsibilityCategory, long>, IResponsibilityCategoryRepository
    {
        public ResponsibilityCategoryRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager) { }

    }
}
