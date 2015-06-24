using System;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class ApplicationTokenRepository : Repository<ApplicationToken, Guid>, IApplicationTokenRepository
    {
        public ApplicationTokenRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager)
        {
        }
    }
}