using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class BodyPartRepository : Repository<BodyPart, long>, IBodyPartRepository
    {
        public BodyPartRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}