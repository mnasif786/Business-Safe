using System.Collections.Generic;

using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Contracts.Sites
{
    public interface ISiteGroupService
    {
        void CreateUpdate(CreateUpdateSiteGroupRequest request);
        IEnumerable<SiteGroupDto> GetByCompanyId(long companyId);
        IEnumerable<SiteGroupDto> GetByCompanyIdExcludingSiteGroup(long clientId, long siteGroupId);
        SiteGroupDto GetSiteGroup(long siteGroupId, long companyId);
        void Delete(DeleteSiteGroupRequest request);
    }
}