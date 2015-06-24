using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class ResponsibilityReasonRepository : Repository<ResponsibilityReason, long>, IResponsibilityReasonRepository
    {
        public ResponsibilityReasonRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager) { }

    }
}
