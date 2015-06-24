using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace BusinessSafe.WebSite.Areas.Sites.ViewModels
{
    public class SiteStructureViewModel
    {
        public bool ValidationError { get; set; }
        public long ClientId { get; set; }
        public MvcHtmlString SiteChartsHtml { get; set; }
        public MvcHtmlString UnlinkedSitesHtml { get; set; }
        public SiteDetailsViewModel SiteDetailsViewModel { get; set; }
        public SiteGroupDetailsViewModel SiteSiteGroupsViewModel { get; set; }
        public bool DisplaySiteDetails { get; set; }
        public bool DisplaySiteGroups { get; set; }

        public bool ShowClosedSites { get; private set; }


        public bool HasUnlinkedSites
        {
           get { return UnlinkedSitesHtml.ToString().Length > 0; }
        }

        public int UnlinkedSitesCount
        {
            get
            {
                return Regex.Matches(UnlinkedSitesHtml.ToString(), "<li>").Count;
            }
        }



        public SiteStructureViewModel(long clientId, MvcHtmlString siteChartsHtml, MvcHtmlString unlinkedSitesHtml, SiteDetailsViewModel siteDetailsViewModel, SiteGroupDetailsViewModel siteSiteGroupViewModel, bool showSiteDetailsSection, bool showGroupDetailsSection, bool validationError, bool showClosedSites)
        {
            ValidationError = validationError;
            SiteChartsHtml = siteChartsHtml;
            UnlinkedSitesHtml = unlinkedSitesHtml;
            SiteDetailsViewModel = siteDetailsViewModel;
            ClientId = clientId;
            SiteSiteGroupsViewModel = siteSiteGroupViewModel;
            DisplaySiteDetails = showSiteDetailsSection;
            DisplaySiteGroups = showGroupDetailsSection;
            ShowClosedSites = showClosedSites;
        }
    }

}
