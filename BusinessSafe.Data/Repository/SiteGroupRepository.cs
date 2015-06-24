using System.Collections.Generic;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class SiteGroupRepository : Repository<SiteGroup, long>, ISiteGroupRepository
    {
        public SiteGroupRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager)
        {
        }

        public SiteGroup GetByIdAndCompanyId(long siteGroupId, long clientId)
        {
            var result = SessionManager
                            .Session
                           .CreateCriteria<SiteGroup>()
                           .Add(Restrictions.Eq("Id", siteGroupId))
                           .Add(Restrictions.Eq("ClientId", clientId))
                           .Add(Restrictions.Eq("Deleted", false))
                           .UniqueResult<SiteGroup>();  
            if(result == null)
                throw new SiteGroupNotFoundException(siteGroupId);
            return result;
        }

        public IEnumerable<SiteGroup> GetCompanyId(long clientId)
        {
            return SessionManager
                        .Session
                       .CreateCriteria<SiteGroup>()
                       .Add(Restrictions.Eq("ClientId", clientId))
                       .Add(Restrictions.Eq("Deleted", false))
                       .SetMaxResults(1000)
                       .List<SiteGroup>();
        }
    }
}