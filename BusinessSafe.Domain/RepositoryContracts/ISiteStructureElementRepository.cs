using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface ISiteStructureElementRepository : IRepository<SiteStructureElement, long>
    {
        IEnumerable<SiteStructureElement> GetByName(long clientId, string name);
        SiteStructureElement GetByIdAndCompanyId(long siteId, long companyId);
        IEnumerable<SiteStructureElement> GetByIdsAndCompanyId(IList<long> ids, long companyId);
        IEnumerable<SiteStructureElement> GetByCompanyId(long companyId);

        //IEnumerable<SiteStructureElement> GetSitesByIdsAndCompanyId(IList<long> ids, long companyId);
        //IEnumerable<SiteStructureElement> GetSitesByCompanyId(long companyId);
        IEnumerable<SiteStructureElement> GetSiteGroupsByCompanyId(long companyId);
        IEnumerable<long> GetChildIdsThatCannotBecomeParent(long id);
    }
}