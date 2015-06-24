using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class OthersInvolvedAccidentDetailsRepository : Repository<OthersInvolvedAccidentDetails, int>, IOthersInvolvedAccidentDetailsRepository
    {
        public OthersInvolvedAccidentDetailsRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
