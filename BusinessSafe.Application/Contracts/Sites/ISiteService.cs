using System;
using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Contracts.Sites
{
    public interface ISiteService
    {
        SiteDto GetSiteStructureByCompanyId(long companyId);
        void CreateMainSite(long companyId, long mainPeninsulaSiteId);
        bool MainSiteExists(long companyId);
        SiteDto GetByIdAndCompanyId(long id, long companyId);
        long CreateUpdate(CreateUpdateSiteRequest request);
        IEnumerable<SiteDto> GetByCompanyIdNotIncluding(long clientId, long siteId);
        IEnumerable<SiteDto> GetByCompanyId(long clientId);
        void DelinkSite(DelinkSiteRequest delinkSiteRequest);
        IEnumerable<SiteDto> Search(SearchSitesRequest request);
        IEnumerable<SiteDto> GetAll(long companyId);

        IEnumerable<SiteDto> GetAll(long companyId, bool includeClosedSites);
        IList<AccidentRecordNotificationMember> GetAccidentRecordNotificationMembers(long siteId);

        void AddAccidentRecordNotificationMemberToSite(long siteId, Guid employeeId, Guid userId);

        void AddNonEmployeeToAccidentRecordNotificationMembers(long siteId, string nonEmployeeName,
                                                               string nonEmployeeEmail, Guid userId);
       
        void RemoveAccidentRecordNotificationMemberFromSite(long siteId, Guid employeeId, Guid userId);
        void RemoveNonEmployeeAccidentRecordNotificationMemberFromSite(long siteId, string nonEmployeeEmail, Guid userId);

        void GetSiteOpenClosedRequest(CreateUpdateSiteRequest request, out bool isSiteOpenRequest, out bool isSiteClosedRequest);
        IEnumerable<string> ValidateSiteCloseRequest(CreateUpdateSiteRequest request);

    }
}
