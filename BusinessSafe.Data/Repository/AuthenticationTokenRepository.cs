using System;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class AuthenticationTokenRepository : Repository<AuthenticationToken, Guid>, IAuthenticationTokenRepository
    {
        public AuthenticationTokenRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager)
        {
        }
    }
}
