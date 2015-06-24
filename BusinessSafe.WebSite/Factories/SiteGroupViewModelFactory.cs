using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using BusinessSafe.WebSite.AutoMappers;
using BusinessSafe.WebSite.Contracts;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public class SiteGroupViewModelFactory : ISiteGroupViewModelFactory
    {
        private readonly ISiteGroupService _siteGroupService;
        private readonly ISiteService _siteService;
        private long _clientId;
        private long _siteGroupId;

        public SiteGroupViewModelFactory(ISiteGroupService siteGroupService, ISiteService siteService)
        {
            _siteGroupService = siteGroupService;
            _siteService = siteService;
        }

        public SiteGroupDetailsViewModel GetViewModel()
        {

            var siteGroupDto = new SiteGroupDto { ClientId = _clientId, Id = 0 };


            if (_siteGroupId != 0)
            {
                siteGroupDto = _siteGroupService.GetSiteGroup(_siteGroupId, _clientId);
            }

            var linkToGroupId = siteGroupDto.Parent != null && (siteGroupDto.Parent as SiteGroupDto != null)
                                    ? siteGroupDto.Parent.Id
                                    : 0;

            var linkToSiteId = siteGroupDto.Parent != null && (siteGroupDto.Parent as SiteDto != null)
                                   ? siteGroupDto.Parent.Id
                                   : 0;

            var siteDetailsViewModel = new SiteGroupDetailsViewModel
                                           {
                                               Name = siteGroupDto.Name,
                                               GroupId = siteGroupDto.Id,
                                               ClientId = _clientId,
                                               HasChildren = siteGroupDto.HasChildren,
                                               GroupLinkToGroupId = linkToGroupId,
                                               GroupLinkToSiteId = linkToSiteId,
                                               ExistingSites = GetExistingSites(siteGroupDto),
                                               ExistingGroups = GetExistingSiteGroups(siteGroupDto)
                                           };


            return siteDetailsViewModel;
        }

        public ISiteGroupViewModelFactory WithSiteGroupId(long siteGroupId)
        {
            _siteGroupId = siteGroupId;
            return this;
        }

        public ISiteGroupViewModelFactory WithClientId(long clientId)
        {
            _clientId = clientId;
            return this;
        }

        private IEnumerable<AutoCompleteViewModel> GetExistingSiteGroups(SiteGroupDto siteGroupDto)
        {
            var sitesGroups = _siteGroupService
                                    .GetByCompanyIdExcludingSiteGroup(_clientId, siteGroupDto.Id).OrderBy(x => x.Name);
            return sitesGroups
                        .Where(x => siteGroupDto.ChildIdsThatCannotBecomeParent.Contains(x.Id) == false)
                        .Select(AutoCompleteViewModel.ForSiteGroup)
                        .AddDefaultOption();

        }

        private IEnumerable<AutoCompleteViewModel> GetExistingSites(SiteGroupDto siteGroupDto)
        {
            var sites = _siteService.GetByCompanyId(_clientId).OrderBy(x => x.Name);
            return sites
                    .Where(x => siteGroupDto.ChildIdsThatCannotBecomeParent.Contains(x.Id) == false)
                    .Select(AutoCompleteViewModel.ForSite)
                    .AddDefaultOption();
        }

    }
}