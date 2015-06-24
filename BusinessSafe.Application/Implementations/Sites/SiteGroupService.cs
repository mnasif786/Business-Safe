using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.Sites
{
    public class SiteGroupService : ISiteGroupService
    {
        private readonly ISiteGroupRepository _siteGroupRepository;
        private readonly ISiteStructureElementRepository _siteStructureElementRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IPeninsulaLog _log;

        public SiteGroupService(ISiteGroupRepository siteGroupRepository, ISiteStructureElementRepository siteStructureElementRepository, IUserForAuditingRepository userForAuditingRepository, IPeninsulaLog log)
        {
            _siteGroupRepository = siteGroupRepository;
            _siteStructureElementRepository = siteStructureElementRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _log = log;
        }

        public void CreateUpdate(CreateUpdateSiteGroupRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetById(request.CurrentUserId);
                SiteGroup siteGroup = null;

                SiteStructureElement parent = null;

                if (request.LinkToSiteId != null && request.LinkToSiteId > 0)
                    parent = _siteStructureElementRepository.GetById((long)request.LinkToSiteId);

                if (request.LinkToGroupId != null && request.LinkToGroupId > 0)
                    parent = _siteStructureElementRepository.GetById((long)request.LinkToGroupId);

                if (request.GroupId == default(long))
                {
                    siteGroup = SiteGroup.Create(parent, request.CompanyId, request.Name, user);
                }
                else
                {
                    siteGroup = _siteGroupRepository.GetById(request.GroupId);
                    siteGroup.Update(parent, request.Name, user);
                }

                var allSitesWithGivenName = _siteStructureElementRepository.GetByName(siteGroup.ClientId, request.Name).Where(sg => sg.Id != request.GroupId);
                siteGroup.Validate(allSitesWithGivenName);
                _siteGroupRepository.SaveOrUpdate(siteGroup);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public IEnumerable<SiteGroupDto> GetByCompanyId(long companyId)
        {
            var siteGroups = _siteGroupRepository.GetCompanyId(companyId);
            return new SiteGroupDtoMapper().Map(siteGroups);
        }

        public IEnumerable<SiteGroupDto> GetByCompanyIdExcludingSiteGroup(long clientId, long siteGroupId)
        {
            var siteGroups = _siteGroupRepository.GetCompanyId(clientId).Where(x => x.Id != siteGroupId);
            return new SiteGroupDtoMapper().Map(siteGroups);
        }

        public void Delete(DeleteSiteGroupRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var siteGroup = _siteGroupRepository.GetByIdAndCompanyId(request.GroupId, request.CompanyId);
                siteGroup.MarkForDelete(user);
                _siteGroupRepository.SaveOrUpdate(siteGroup);

            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public SiteGroupDto GetSiteGroup(long siteGroupId, long companyId)
        {
            var siteGroup = _siteGroupRepository.GetByIdAndCompanyId(siteGroupId, companyId);
            var childIdsThatCannotBecomeParent = _siteStructureElementRepository.GetChildIdsThatCannotBecomeParent(siteGroupId);
            var result = new SiteGroupDtoMapper().MapWithHasChildrenAndParent(siteGroup, childIdsThatCannotBecomeParent);
            return result;
        }
    }
}
