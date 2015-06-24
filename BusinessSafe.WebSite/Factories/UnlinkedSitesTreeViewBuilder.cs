using System.Collections.Generic;
using System.Text;

namespace BusinessSafe.WebSite.Factories
{
    public class UnlinkedSitesTreeViewBuilder
    {
        private readonly List<UnlinkedSites> _unlinkedSites;

        public UnlinkedSitesTreeViewBuilder(List<UnlinkedSites> unlinkedSites)
        {
            _unlinkedSites = unlinkedSites;
        }

        public string Build()
        {
            var result = new StringBuilder();
            foreach (var site in _unlinkedSites)
            {
                result.Append(CreateStartListItem(site));
            }
            return result.ToString();
        }

        private string CreateStartListItem(UnlinkedSites site)
        {
            return string.Format("<li><div class='unlinked-site' data-type='siteaddress' data-id='{0}'>{1}</div></li>", site.SiteId, site.SiteDetails);
        }
    }
}