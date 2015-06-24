using System;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository.SafeCheck
{
    public interface ITimescaleRepository : IRepository<Timescale, long>
    {
    }

    public class TimescaleRepository : Repository<Timescale, long>, ITimescaleRepository
    {
        public TimescaleRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }

    
}
