using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using BusinessSafe.WebSite.Contracts;
using BusinessSafe.WebSite.Custom_Exceptions;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;
using BusinessSafe.Application.RestAPI;

namespace BusinessSafe.WebSite.Factories
{
    public class SiteStructureViewModelFactory : ISiteStructureViewModelFactory
    {
        private readonly ISiteService _siteService;
        private readonly ISiteGroupService _siteGroupService;
        private long _clientId;
        private SiteDetailsViewModel _siteDetailsViewModel = new SiteDetailsViewModel();
        private SiteGroupDetailsViewModel _siteSiteGroupViewModel = new SiteGroupDetailsViewModel();
        private bool _showSiteDetailsSection;
        private bool _showGroupDetailsSection;
        private bool _validationError;
        private bool _showClosedSites;
        private readonly IClientService _clientService;

        public SiteStructureViewModelFactory(ISiteService siteService, ISiteGroupService siteGroupService, IClientService clientService)
        {
            _clientService = clientService;
            _siteService = siteService;
            _siteGroupService = siteGroupService;
        }

        public ISiteStructureViewModelFactory WithClientId(long id)
        {
            _clientId = id;
            return this;
        }

        public ISiteStructureViewModelFactory WithSiteDetailsViewModel(SiteDetailsViewModel siteDetailsViewModel)
        {
            _siteDetailsViewModel = siteDetailsViewModel;

            //var linkToSiteGroupId = siteDetailsViewModel.LinkToGroupId == siteDetailsViewModel.OriginalLinkId ? siteDetailsViewModel.LinkToGroupId : 0;
            _siteDetailsViewModel.ExistingGroups = GetExistingSiteGroups();

            //var linkToSiteId = siteDetailsViewModel.LinkToSiteId == siteDetailsViewModel.OriginalLinkId ? siteDetailsViewModel.LinkToSiteId : 0;
            _siteDetailsViewModel.ExistingSites = GetExistingSites();

            return this;
        }

        public ISiteStructureViewModelFactory WithGroupDetailsViewModel(SiteGroupDetailsViewModel siteGroupDetailsViewModel)
        {
            _siteSiteGroupViewModel = siteGroupDetailsViewModel;


            _siteSiteGroupViewModel.ExistingGroups = GetExistingSiteGroups();
            var linkToSiteGroupId = siteGroupDetailsViewModel.GroupLinkToGroupId == siteGroupDetailsViewModel.OriginalLinkId ? siteGroupDetailsViewModel.GroupLinkToGroupId : 0;
            _siteSiteGroupViewModel.GroupLinkToGroupId = linkToSiteGroupId;

            _siteSiteGroupViewModel.ExistingSites = GetExistingSites();
            var linkToSiteId = siteGroupDetailsViewModel.GroupLinkToSiteId == siteGroupDetailsViewModel.OriginalLinkId ? siteGroupDetailsViewModel.GroupLinkToSiteId : 0;
            _siteSiteGroupViewModel.GroupLinkToSiteId = linkToSiteId;
            return this;
        }

        public ISiteStructureViewModelFactory HideSiteDetails()
        {
            _showSiteDetailsSection = false;
            return this;
        }

        public ISiteStructureViewModelFactory DisplaySiteGroups()
        {
            _showGroupDetailsSection = true;
            return this;
        }

        public ISiteStructureViewModelFactory DisplaySiteDetails()
        {

            _showSiteDetailsSection = true;
            return this;
        }

        public ISiteStructureViewModelFactory WithValidationError(bool validationError)
        {
            _validationError = validationError;
            return this;
        }

        public ISiteStructureViewModelFactory ShowClosedSites(bool value)
        {
            _showClosedSites = value;
            return this;
        }

        private IEnumerable<AutoCompleteViewModel> GetExistingSites()
        {
            var sites = _siteService.GetByCompanyIdNotIncluding(_clientId, _siteDetailsViewModel.SiteId).OrderBy(x => x.Name);
            return sites
                        .Select(AutoCompleteViewModel.ForSite)
                        .AddDefaultOption();
        }

        private IEnumerable<AutoCompleteViewModel> GetExistingSiteGroups()
        {
            var siteGroups = _siteGroupService.GetByCompanyId(_clientId).OrderBy(x => x.Name);
            return siteGroups
                        .Select(AutoCompleteViewModel.ForSiteGroup)
                        .AddDefaultOption();
        }

        public SiteStructureViewModel GetViewModel()
        {
            
            var sites = _siteService.GetAll(_clientId, _showClosedSites);
            
            //Ensure the main site exists.
            if (!_siteService.MainSiteExists(_clientId))
            {
                throw new NoMainSiteAvailableException(_clientId);
            }

            var allSitesOnPeninsulaDB = _clientService.GetSites(_clientId);

            //Get unlinked sites.
            var unlinkedSites = allSitesOnPeninsulaDB.Where(peninsulaSite => !sites.Select(bsoSite => bsoSite.SiteId).Contains(peninsulaSite.SiteId));

            var rootSite = _siteService.GetSiteStructureByCompanyId(_clientId);

            var compositeRootSiteViewModel = GetCompositeSiteViewModel(rootSite, _showClosedSites);
            compositeRootSiteViewModel.UnlinkedSites = unlinkedSites.ToList();

            var unlinkedSiteViews = unlinkedSites.Select(siteAddressDto => new UnlinkedSites
                                                                                                  {
                                                                                                      SiteId = siteAddressDto.SiteId,
                                                                                                      SiteDetails = GetSiteDetails(siteAddressDto)
                                                                                                  }).ToList();

            var sitesTreeViewHtml = new MvcHtmlString(new SiteTreeViewBuilder(compositeRootSiteViewModel).Build());
            var unlinkedSitesHtml = new MvcHtmlString(new UnlinkedSitesTreeViewBuilder(unlinkedSiteViews).Build());

            return new SiteStructureViewModel(_clientId, sitesTreeViewHtml, unlinkedSitesHtml, _siteDetailsViewModel, _siteSiteGroupViewModel, _showSiteDetailsSection, _showGroupDetailsSection, _validationError, _showClosedSites);
        }

        private static string GetSiteDetails(SiteAddressDto siteDetailsDto)
        {
            return String.Format("{0},{1},{2}", siteDetailsDto.AddressLine1, siteDetailsDto.Postcode, siteDetailsDto.AddressLine4);
        }

        private static CompositeSiteViewModel GetCompositeSiteViewModel(SiteStructureElementDto siteStructureElement, bool showClosedSites)
        {
            var compositeSiteViewModel = new CompositeSiteViewModel();
            compositeSiteViewModel.Id = siteStructureElement.Id;
            compositeSiteViewModel.Name = siteStructureElement.Name;
            
            var site = siteStructureElement as SiteDto;

            if (site != null)
            {
                compositeSiteViewModel.SiteType = CompositeSiteType.SiteAddress;
                compositeSiteViewModel.SiteId = site.SiteId;
            }

            var siteGroup = siteStructureElement as SiteGroupDto;

            if (siteGroup != null)
            {
                compositeSiteViewModel.SiteType = CompositeSiteType.SiteGroup;
            }

            ////todo: do this nicer.
            //if(site == null && siteGroup == null)
            //{
            //    if(siteStructureElement.SiteStructureElementType == "Site")
            //        compositeSiteViewModel.SiteType = CompositeSiteType.SiteAddress;
            //    else
            //        compositeSiteViewModel.SiteType = CompositeSiteType.SiteGroup;
            //}

            if (siteStructureElement.Children != null)
            {
                compositeSiteViewModel.Children = new List<CompositeSiteViewModel>();

                foreach (var child in siteStructureElement.Children)
                {
                    var areClosedSitesToBeShown = showClosedSites || !child.SiteClosed;
                    
                    if (!child.Deleted && areClosedSitesToBeShown)
                    {
                        compositeSiteViewModel.Children.Add(GetCompositeSiteViewModel(child, showClosedSites));
                    }
                }
            }

            return compositeSiteViewModel;
        }
    }
}