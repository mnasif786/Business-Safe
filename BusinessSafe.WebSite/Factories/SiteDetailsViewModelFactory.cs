using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.RestAPI;
using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using BusinessSafe.WebSite.AutoMappers;
using BusinessSafe.WebSite.Contracts;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.WebSite.Factories
{
    public class SiteDetailsViewModelFactory : ISiteDetailsViewModelFactory
    {
        private long _id;
        private long _siteId;
        private long _clientId;
        private readonly ISiteService _siteService;
        private readonly ISiteGroupService _siteGroupService;
        private IClientService _clientService;

        public SiteDetailsViewModelFactory(ISiteService siteService, ISiteGroupService siteGroupService, IClientService clientService)
        {
            _clientService = clientService;
            _siteService = siteService;
            _siteGroupService = siteGroupService;
        }

        public SiteDetailsViewModel GetViewModel()
        {
            SiteDto localSiteDto = null;
            SiteAddressDto peninsulaSiteDto = null;

            if (_id != default(long))
            {
                localSiteDto = _siteService.GetByIdAndCompanyId(_id, _clientId);
                peninsulaSiteDto = _clientService.GetSite(_clientId, localSiteDto.SiteId.Value);
            }

            else if (_siteId != default(long))
            {
                localSiteDto = new SiteDto();
                peninsulaSiteDto = _clientService.GetSite(_clientId, _siteId);
            }

            var siteDetailsViewModel = new SiteDetailsViewModel
                                           {
                                               SiteStructureId = localSiteDto.Id,
                                               ClientId = _clientId,
                                               Name = localSiteDto.Name,
                                               Reference = peninsulaSiteDto.Reference,
                                               LinkToSiteId =
                                                   localSiteDto.Parent != null &&
                                                   (localSiteDto.Parent as SiteDto != null)   //todo: replace with when proxy issue fixed
                                                   //localSiteDto.Parent.SiteStructureElementType == "Site"
                                                       ? (long?)localSiteDto.Parent.Id
                                                       : null,
                                               LinkToGroupId =
                                                   localSiteDto.Parent != null &&
                                                   (localSiteDto.Parent as SiteGroupDto != null)  //todo: replace with when proxy issue fixed
                                                   //localSiteDto.Parent.SiteStructureElementType == "SiteGroup"
                                                       ? (long?)localSiteDto.Parent.Id
                                                       : null,
                                               OriginalLinkId =
                                                   localSiteDto.Parent != null ? (long?)localSiteDto.Parent.Id : null,
                                               AddressLine1 = peninsulaSiteDto.AddressLine1,
                                               AddressLine2 = peninsulaSiteDto.AddressLine2,
                                               AddressLine3 = peninsulaSiteDto.AddressLine3,
                                               AddressLine4 = peninsulaSiteDto.AddressLine4,
                                               AddressLine5 = peninsulaSiteDto.AddressLine5,
                                               County = peninsulaSiteDto.County,
                                               Postcode = peninsulaSiteDto.Postcode,
                                               Telephone = peninsulaSiteDto.Telephone,
                                               SiteContact = localSiteDto.SiteContact,
                                               SiteClosed = localSiteDto.SiteClosed,
                                               SiteStatusCurrent = localSiteDto.SiteClosed ? SiteStatus.Close : SiteStatus.Open
                                           };

            if (_id != default(long))
            {
                siteDetailsViewModel.SiteId = localSiteDto.SiteId.HasValue ? localSiteDto.SiteId.Value : 0;
                siteDetailsViewModel.IsMainSite = localSiteDto.Parent == null;
            }

            else if (_siteId != default(long))
            {
                siteDetailsViewModel.SiteId = _siteId;
                siteDetailsViewModel.IsMainSite = false;
            }

            if (localSiteDto.Id == 0 || localSiteDto.Parent != null)
            {
                long siteId = _siteId != default(long) ? _siteId : localSiteDto.SiteId.Value;

                var sites = _siteService
                    .GetByCompanyIdNotIncluding(_clientId, siteId)
                    .OrderBy(x => x.Name);

                siteDetailsViewModel.ExistingSites = sites
                                                        .Where(x => localSiteDto.ChildIdsThatCannotBecomeParent.Contains(x.Id) == false)
                                                        .Select(AutoCompleteViewModel.ForSite)
                                                        .AddDefaultOption();

                var siteGroups = _siteGroupService
                    .GetByCompanyIdExcludingSiteGroup(_clientId, siteId)
                    .OrderBy(x => x.Name);


                siteDetailsViewModel.ExistingGroups = siteGroups
                                                        .Where(x => localSiteDto.ChildIdsThatCannotBecomeParent.Contains(x.Id) == false)
                                                        .Select(AutoCompleteViewModel.ForSiteGroup)
                                                        .AddDefaultOption();
            }
            else
            {
                siteDetailsViewModel.ExistingSites = new List<AutoCompleteViewModel>()
                                                         {
                                                             new AutoCompleteViewModel("--Select Option--","")
                                                         };
                siteDetailsViewModel.ExistingGroups = new List<AutoCompleteViewModel>() 
                                                         {
                                                             new AutoCompleteViewModel("--Select Option--","")
                                                         };
            }

            return siteDetailsViewModel;
        }

        public ISiteDetailsViewModelFactory WithId(long id)
        {
            _id = id;
            return this;
        }

        public ISiteDetailsViewModelFactory WithSiteId(long id)
        {
            _siteId = id;
            return this;
        }

        public ISiteDetailsViewModelFactory WithClientId(long id)
        {
            _clientId = id;
            return this;
        }


    }
}