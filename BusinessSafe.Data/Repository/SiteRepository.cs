using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class SiteRepository : Repository<Site, long>, ISiteRepository
    {
        public SiteRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager)
        {
        }

        public IEnumerable<Site> GetSiteAddressByCompanyId(long clientId)
        {
            var site = SessionManager
                        .Session
                        .CreateCriteria<Site>()
                        .Add(Restrictions.Eq("ClientId", clientId))
                        .Add(Restrictions.Eq("Deleted", false));
            return site.List<Site>();
        }

        public Site GetSiteStructureBySiteId(long siteId)
        {
            var site = SessionManager
                        .Session
                       .CreateCriteria<Site>()
                       .Add(Restrictions.Eq("SiteId", siteId))
                       .Add(Restrictions.Eq("Deleted", false))
                       .UniqueResult<Site>();
        
            if(site == null)
            {
                throw new SiteNotFoundException(siteId);
            }

            return site;
        }

        public void DelinkSite(long siteId, long companyId)
        {
            var siteAddress = SessionManager
                                .Session
                                .CreateCriteria<Site>()
                                .Add(Restrictions.Eq("SiteId", siteId))
                                .Add(Restrictions.Eq("ClientId", companyId))
                                .UniqueResult<Site>();
            if(siteAddress == null)
            {
                throw new DelinkSiteFailedException(siteId, companyId);
            }
            
            SessionManager
                        .Session
                        .Delete(siteAddress);
        }

        public IEnumerable<Site> Search(long companyId, string nameStartsWith, IList<long> allowedSiteIds, int? pageLimit)
        {
            var result = SessionManager
                           .Session
                           .CreateCriteria<Site>()
                           .Add(Restrictions.Eq("ClientId", companyId))
                           .Add(Restrictions.Eq("Deleted", false))
                           .Add(Restrictions.IsNull("SiteClosedDate"));

            if(nameStartsWith != null)
            {
                result.Add(Restrictions.Like("Name", nameStartsWith + "%"));
            }

            if(allowedSiteIds != null)
            {
                result.Add(Restrictions.In("Id", allowedSiteIds.ToArray()));
            }

            if(pageLimit.HasValue)
            {
                result.SetMaxResults(pageLimit.Value);
            }

            return result.List<Site>();
        }

        public Site GetByIdAndCompanyId(long id, long companyId)
        {
            var result = SessionManager
                            .Session
                           .CreateCriteria<Site>()
                           .Add(Restrictions.Eq("Id", id))
                           .Add(Restrictions.Eq("ClientId", companyId))
                           .UniqueResult<Site>();

            if (result == null)
                throw new SiteNotFoundException(id);

            return result;
        }

        public Site GetRootSiteByCompanyId(long companyId)
        {
            var result = SessionManager
                            .Session
                           .CreateCriteria<Site>()
                           .Add(Restrictions.Eq("ClientId", companyId))
                           .Add(Restrictions.IsNull("Parent"))
                           .UniqueResult<Site>();

            return result;
        }

        public Site GetByPeninsulaSiteId(long peninsulaSiteId)
        {            
            // Need a single ID so if multiple sites returned, order by non-deleted first
            // then most recently created
            IList<Site> siteList = SessionManager
                .Session
                .CreateCriteria<Site>()
                .Add(Restrictions.Eq("SiteId", peninsulaSiteId))
                .AddOrder( Order.Asc("Deleted"))
                .AddOrder( Order.Desc("CreatedOn"))                
                .List<Site>();

            if (siteList.Any())
                return siteList[0];
            
            return null;
       }
    }
}