using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class CompanyVehicleTypeRepository : Repository<CompanyVehicleType, int>, ICompanyVehicleTypeRepository
    {
        public CompanyVehicleTypeRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}