using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Tests.Builder
{
    public class SiteDetailsViewModelBuilder
    {
        private static SiteDetailsViewModel _siteDetailsViewModel;
        private string _reference = "Test Reference";
        private string _name = "Test";
        private long _linkToSiteId = 1;
        private long _siteId = 200;
        private long _clientId = 200;
        private long _siteStructureId;
        private IEnumerable<AutoCompleteViewModel> _existingSites = new List<AutoCompleteViewModel>();
        private long _linkToGroupId;

        public static SiteDetailsViewModelBuilder Create()
        {
            _siteDetailsViewModel = new SiteDetailsViewModel();
            return new SiteDetailsViewModelBuilder();
        }

        public SiteDetailsViewModel Build()
        {
            _siteDetailsViewModel.SiteId = _siteId;
            _siteDetailsViewModel.ClientId = _clientId;
            _siteDetailsViewModel.Reference = _reference;
            _siteDetailsViewModel.Name = _name;
            _siteDetailsViewModel.LinkToSiteId = _linkToSiteId;
            _siteDetailsViewModel.LinkToGroupId = _linkToGroupId;
            _siteDetailsViewModel.SiteStructureId = _siteStructureId;
            _siteDetailsViewModel.ExistingSites = _existingSites;            
            return _siteDetailsViewModel;
        }

        public SiteDetailsViewModelBuilder WithReference(string reference)
        {
            _reference = reference;
            return this;
        }

        public SiteDetailsViewModelBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public SiteDetailsViewModelBuilder WithLinkToSiteId(long linkToSiteId)
        {
            _linkToSiteId =linkToSiteId;
            return this;
        }

        public SiteDetailsViewModelBuilder WithSiteId(long siteId)
        {
            _siteId = siteId;
            return this;
        }

        public SiteDetailsViewModelBuilder WithSiteStructureId(long siteStructureId)
        {
            _siteStructureId = siteStructureId;
            return this;
        }

        public SiteDetailsViewModelBuilder WithExistingSites(IEnumerable<AutoCompleteViewModel> list)
        {
            _existingSites = list;
            return this;
        }

        public SiteDetailsViewModelBuilder WithLinkToGroupId(long id)
        {
            _linkToGroupId = id;
            return this;
        }

        public SiteDetailsViewModelBuilder WithParentId(long? parentId)
        {
            return this;
        }

        public SiteDetailsViewModelBuilder WithClientId(long clientId)
        {
            _clientId = clientId;
            return this;
        }
    }
}