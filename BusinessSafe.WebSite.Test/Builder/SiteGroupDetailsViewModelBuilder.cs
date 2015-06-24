using System.Collections.Generic;
using System.Web.Mvc;

using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Tests.Builder
{
    public class SiteGroupDetailsViewModelBuilder
    {
        private static long _groupId;
        private static long _clientId;
        private static string _name;
        private static long _linkToSiteId;
        private static long _linkedToGroupId;
        private static IEnumerable<AutoCompleteViewModel> _existingSites;
        private static IEnumerable<AutoCompleteViewModel> _existingGroups;

        public static SiteGroupDetailsViewModelBuilder Create()
        {
            _groupId = 0;
            _name = default(string);
            _existingSites = default(IEnumerable<AutoCompleteViewModel>);
            _existingGroups = default(IEnumerable<AutoCompleteViewModel>);

            return new SiteGroupDetailsViewModelBuilder();
        }

        public SiteGroupDetailsViewModel Build()
        {
            return new SiteGroupDetailsViewModel(_groupId, _clientId, _name, _existingSites, _existingGroups) { GroupLinkToSiteId = _linkToSiteId, GroupLinkToGroupId = _linkedToGroupId };
        }

        public SiteGroupDetailsViewModelBuilder WithGroupId(long groupId)
        {
            _groupId = groupId;
            return this;
        }

        public SiteGroupDetailsViewModelBuilder WithClientId(long clientId)
        {
            _clientId = clientId;
            return this;
        }

        public SiteGroupDetailsViewModelBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public SiteGroupDetailsViewModelBuilder WithLinkToSiteId(long linkToSiteId)
        {
            _linkToSiteId = linkToSiteId;
            return this;
        }

        public SiteGroupDetailsViewModelBuilder WithLinkToGroupId(long linkToGroupId)
        {
            _linkedToGroupId = linkToGroupId;
            return this;
        }
    }
}