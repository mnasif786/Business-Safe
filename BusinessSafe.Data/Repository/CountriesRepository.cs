using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class CountriesRepository : Repository<Country, int>, ICountriesRepository
    {
        public CountriesRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

    }
}