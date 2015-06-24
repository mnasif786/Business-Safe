using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface ISiteRepository : IRepository<Site, long>
    {
        IEnumerable<Site> GetSiteAddressByCompanyId(long clientId);
        void DelinkSite(long siteId, long companyId);
        IEnumerable<Site> Search(long companyId, string nameStartsWith, IList<long> allowedSiteIds, int? pageLimit);
        Site GetByIdAndCompanyId(long id, long companyId);
        Site GetRootSiteByCompanyId(long companyId);
        Site GetByPeninsulaSiteId(long peninsulaId);
    }
}