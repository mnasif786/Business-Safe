using System.Collections.Generic;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using System.Linq;

namespace BusinessSafe.Data.Repository
{
    public class SiteStructureElementRepository : Repository<SiteStructureElement, long>, ISiteStructureElementRepository
    {
        public SiteStructureElementRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager)
        {
        }

        public IEnumerable<SiteStructureElement> GetByName(long clientId, string name)
        {
            return SessionManager
                        .Session
                       .CreateCriteria<SiteStructureElement>()
                       .Add(Restrictions.Eq("ClientId", clientId))
                       .Add(Restrictions.Eq("Name", name))
                       .Add(Restrictions.Eq("Deleted", false))
                       .List<SiteStructureElement>();
        }

        public SiteStructureElement GetByIdAndCompanyId(long siteId, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<SiteStructureElement>()
                .Add(Restrictions.Eq("Id", siteId))
                .Add(Restrictions.Eq("ClientId", companyId))
                .Add(Restrictions.Eq("Deleted", false))
                .SetMaxResults(1)
                .UniqueResult<SiteStructureElement>();

            if (result == null)
                throw new SiteNotFoundException(siteId);

            return result;
        }

        public IEnumerable<SiteStructureElement> GetByIdsAndCompanyId(IList<long> ids, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<SiteStructureElement>()
                .Add(Restrictions.In("Id", ids.ToArray()))
                .Add(Restrictions.Eq("ClientId", companyId))
                .Add(Restrictions.Eq("Deleted", false))
                .List<SiteStructureElement>();

            return result;
        }

        public IEnumerable<SiteStructureElement> GetByCompanyId(long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<SiteStructureElement>()
                .Add(Restrictions.Eq("ClientId", companyId))
                .Add(Restrictions.Eq("Deleted", false))
                .List<SiteStructureElement>();

            return result;
        }

        public IEnumerable<SiteStructureElement> GetSiteGroupsByCompanyId(long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<SiteStructureElement>()
                .Add(Restrictions.Eq("class", typeof(SiteGroup)))
                .Add(Restrictions.Eq("ClientId", companyId))
                .Add(Restrictions.Eq("Deleted", false))
                .List<SiteStructureElement>();

            return result;
        }

        public IEnumerable<long> GetChildIdsThatCannotBecomeParent(long id)
        {
             return SessionManager.Session
                .GetNamedQuery("GetSiteElementStructureChildIds")
                .SetParameter("id", id)
                .List<long>();
           
        }
    }
}