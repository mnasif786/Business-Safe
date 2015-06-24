using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.Sites
{
    public class SiteService : ISiteService
    {
        private readonly ISiteRepository _siteRepository;
        private readonly ISiteStructureElementRepository _siteStructureElementRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public SiteService(ISiteRepository siteRepository,
                           ISiteStructureElementRepository siteStructureElementRepository,
                           IUserForAuditingRepository userForAuditingRepository,
            IEmployeeRepository employeeRepository)
        {
            _siteRepository = siteRepository;
            _siteStructureElementRepository = siteStructureElementRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _employeeRepository = employeeRepository;
        }

        public SiteDto GetSiteStructureByCompanyId(long companyId)
        {
            var site = _siteRepository.GetRootSiteByCompanyId(companyId);
            return new SiteDtoMapper().MapWithChildren(site);
        }

        public void CreateMainSite(long companyId, long mainPeninsulaSiteId)
        {
            //could move some of this logic to domain.
            if (!MainSiteExists(companyId))
            {
                var systemUser = _userForAuditingRepository.GetSystemUser();
                var site = Site.Create(mainPeninsulaSiteId, null, companyId, "Main Site", "Main Site Reference", "", systemUser);
                _siteRepository.Save(site);
            }

        }

        public bool MainSiteExists(long companyId)
        {
            var siteStructureElement = _siteStructureElementRepository.GetByCompanyId(companyId);
            return siteStructureElement.Any();
        }

        public IEnumerable<SiteDto> GetByCompanyId(long clientId)
        {
            var sites = GetAll(clientId, false); //_siteRepository.GetSiteAddressByCompanyId(clientId);
            return sites;
            //return new SiteDtoMapper().Map(sites.Cast<Site>());
        }

        public IEnumerable<SiteDto> GetByCompanyIdNotIncluding(long clientId, long siteId)
        {
            var sites = GetAll(clientId, false);  //_siteRepository.GetSiteAddressByCompanyId(clientId);
            var linkedSites = sites.Where(x => x.SiteId != siteId);
            return linkedSites;
            //return new SiteDtoMapper().Map(linkedSites);
        }

        public void DelinkSite(DelinkSiteRequest delinkSiteRequest)
        {
            _siteRepository.DelinkSite(delinkSiteRequest.SiteId, delinkSiteRequest.CompanyId);
        }

        public IEnumerable<SiteDto> Search(SearchSitesRequest request)
        {
            var sites = _siteRepository.Search(
                request.CompanyId,
                request.NameStartsWith,
                request.AllowedSiteIds,
                request.PageLimit);

            return new SiteDtoMapper().Map(sites.Cast<Site>());
        }

        public IEnumerable<SiteDto> GetAll(long companyId)
        {
            return GetAll(companyId, false); // _siteRepository.GetSiteAddressByCompanyId(companyId);

            //return new SiteDtoMapper().Map(sites.Cast<Site>());
        }

        public IEnumerable<SiteDto> GetAll(long companyId, bool includeClosedSites )
        {
            var sites = _siteRepository.GetSiteAddressByCompanyId(companyId);

            if (!includeClosedSites)
            {
               sites = sites.Where(s => s.SiteClosedDate == null);
            }
            
            sites = sites.OrderBy(x => x.Name);
            return new SiteDtoMapper().Map(sites.Cast<Site>());
        }

        public SiteDto GetByIdAndCompanyId(long id, long companyId)
        {
            var site = _siteRepository.GetByIdAndCompanyId(id, companyId);
            var childIdsThatCannotBecomeParent = _siteStructureElementRepository.GetChildIdsThatCannotBecomeParent(id);
            // Not loading with children beacuase of flush exception - need child id's for filtering site drop down lists
            return new SiteDtoMapper().MapWithParent(site, childIdsThatCannotBecomeParent);
        }

        public void GetSiteOpenClosedRequest(CreateUpdateSiteRequest request,  out bool isSiteOpenRequest, out bool isSiteClosedRequest)
        {
            var site = _siteRepository.GetById(request.Id);
            isSiteOpenRequest = site.SiteClosedDate != null && !request.SiteClosed;
            isSiteClosedRequest = site.SiteClosedDate == null && request.SiteClosed;
        }

        public IEnumerable<string> ValidateSiteCloseRequest(CreateUpdateSiteRequest request)
        {
            var validationMessages = new List<string>();
            var site = _siteRepository.GetById(request.Id);

            bool isSiteClosed = site.SiteClosedDate != null;

            //do the validation if siteClosed status is being changed from the current database status and 
            //site is being closed
            if (isSiteClosed != request.SiteClosed && request.SiteClosed)
            {
                var children = site.Children.Where(x => x.SiteClosedDate == null);

                if (children.Any())
                {
                    validationMessages.Add("Please remove all linked sites.");
                }

                var employees = _employeeRepository.GetBySite(site.Id);

                if (employees.Any())
                {
                    validationMessages.Add("Please remove all attached employees.");
                }
            }

            return validationMessages;
        }

        public long CreateUpdate(CreateUpdateSiteRequest request)
        {
            var parent = request.ParentId != null ? _siteStructureElementRepository.GetById(request.ParentId.Value) : null;
            var site = _siteRepository.GetById(request.Id);
            var currentUser = _userForAuditingRepository.GetByIdAndCompanyId(request.CurrentUserId, request.ClientId);

            if (request.Id == default(long))
            {
                site = Site.Create(
                    request.SiteId,
                    parent,
                    request.ClientId,
                    request.Name,
                    request.Reference,
                    request.SiteContact,
                    currentUser);
            }
            else
            {
                site.Update(
                    parent,
                    request.Name,
                    request.Reference,
                    request.ClientId,
                    request.SiteContact,
                    currentUser,
                    request.IsSiteOpenRequest,
                    request.IsSiteClosedRequest);
            }

            var sitesWithSameName =
                _siteStructureElementRepository.GetByName(request.ClientId, site.Name).Where(sg => sg.Id != request.Id);

            site.Validate(sitesWithSameName);
            _siteRepository.SaveOrUpdate(site);

            return site.Id;
        }

        public IList<AccidentRecordNotificationMember> GetAccidentRecordNotificationMembers(long siteId)
        {
            var site = _siteRepository.GetById(siteId);
            return site.AccidentRecordNotificationMembers;
        }

        public void AddAccidentRecordNotificationMemberToSite(long siteId, Guid employeeId, Guid userId)
        {
            var site = _siteRepository.GetById(siteId);
            var employee = _employeeRepository.GetById(employeeId);
            var user = _userForAuditingRepository.GetById(userId);

            site.AddAccidentRecordNotificationMember(employee, user);
            

            _siteRepository.SaveOrUpdate(site);
        }

        public void AddNonEmployeeToAccidentRecordNotificationMembers(long siteId, string nonEmployeeName, string nonEmployeeEmail, Guid userId)
        {        
            var site = _siteRepository.GetById(siteId);        
            var user = _userForAuditingRepository.GetById(userId);

            site.AddNonEmployeeToAccidentRecordNotificationMembers(nonEmployeeName, nonEmployeeEmail, user);
            _siteRepository.SaveOrUpdate(site);
        }

        public void RemoveAccidentRecordNotificationMemberFromSite(long siteId, Guid employeeId, Guid userId)
        {
            var site = _siteRepository.GetById(siteId);
            var employee = _employeeRepository.GetById(employeeId);
            var user = _userForAuditingRepository.GetById(userId);

            site.RemoveAccidentRecordNotificationMember(employee, user);

            _siteRepository.SaveOrUpdate(site);
        }

        public void RemoveNonEmployeeAccidentRecordNotificationMemberFromSite(long siteId, string nonEmployeeEmail, Guid userId)
        {
            var site = _siteRepository.GetById(siteId);            
            var user = _userForAuditingRepository.GetById(userId);

            site.RemoveNonEmployeeAccidentRecordNotificationMember(nonEmployeeEmail, user);

            _siteRepository.SaveOrUpdate(site);
        }
    }
}