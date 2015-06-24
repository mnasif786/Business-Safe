using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class EmploymentStatusRepository : Repository<EmploymentStatus, int>, IEmploymentStatusRepository
    {
        public EmploymentStatusRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

    }
}