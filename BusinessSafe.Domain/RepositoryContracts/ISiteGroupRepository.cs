using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface ISiteGroupRepository : IRepository<SiteGroup, long>
    {
        SiteGroup GetByIdAndCompanyId(long siteGroupId, long clientId);
        IEnumerable<SiteGroup> GetCompanyId(long clientId);
    }
}