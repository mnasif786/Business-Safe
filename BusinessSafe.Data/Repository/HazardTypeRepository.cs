using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class HazardTypeRepository : Repository<HazardType, long>, IHazardTypeRepository
    {
        public HazardTypeRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

    }
}