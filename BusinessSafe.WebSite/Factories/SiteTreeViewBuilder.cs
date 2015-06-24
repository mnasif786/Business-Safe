using System;
using System.Linq;
using System.Text;

using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Sites.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public class SiteTreeViewBuilder
    {
        private readonly CompositeSiteViewModel _site;
        private readonly StringBuilder _siteOrganisationalChart = new StringBuilder();

        public SiteTreeViewBuilder(CompositeSiteViewModel site)
        {
            _site = site;
        }

        public string Build()
        {

            if (MasterSiteHasNoChildren())
                return CreateCompleteListItem(_site);

            _siteOrganisationalChart.Append(CreateStartListItem(_site));
            _siteOrganisationalChart.Append(ProcessChildrenSites(_site));
            _siteOrganisationalChart.Append(CreateEndListItem());

            return _siteOrganisationalChart.ToString();
        }

        private bool MasterSiteHasNoChildren()
        {
            return _site.Children.Count == 0;
        }

        private StringBuilder ProcessChildrenSites(CompositeSiteViewModel master)
        {
            var result = new StringBuilder();
            result.Append(CreateStartUnorderedList());

            foreach (var site in master.Children)
            {
                result.Append(CreateStartListItem(site));

                if (site.Children.Any())
                {
                    var childrenSitesHtml = ProcessChildrenSites(site);
                    result.Append(childrenSitesHtml.ToString());
                }

                result.Append(CreateEndListItem());
            }

            result.Append(CreateEndUnorderedList());
            return result;
        }

        private static string CreateEndUnorderedList()
        {
            return "</ul>";
        }

        private static string CreateStartUnorderedList()
        {
            return "<ul>";
        }

        private static string CreateEndListItem()
        {
            return "</li>";
        }

        private static string CreateStartListItem(CompositeSiteViewModel site)
        {
            return string.Format("<li><div class='linked-site' data-type='{0}' data-id='{1}'>{2}</div>", GetSiteType(site), GetDataIdToLoad(site), site.Name);
        }

        private string CreateCompleteListItem(CompositeSiteViewModel site)
        {
            return string.Format("<li><div class='linked-site' data-type='{0}' data-id='{1}'>{2}</div></li>", GetSiteType(site), GetDataIdToLoad(site), site.Name);
        }

        private static string GetSiteType(CompositeSiteViewModel site)
        {
            if (site.SiteType == CompositeSiteType.SiteAddress)
                return "siteaddress";

            if (site.SiteType == CompositeSiteType.SiteGroup)
                return "sitegroup";

            throw new ArgumentException("Site Type not defined. Neither SiteAddress or SiteGroup");

        }

        private static string GetDataIdToLoad(CompositeSiteViewModel site)
        {
            if (site.SiteType == CompositeSiteType.SiteAddress)
                return site.Id.ToString();

            if (site.SiteType == CompositeSiteType.SiteGroup)
                return site.Id.ToString();

            throw new ArgumentException("Site Type not defined. Neither SiteAddress or SiteGroup");
        }

        //private readonly SiteOrganisationalUnitDto _site;
        //private readonly StringBuilder _siteOrganisationalChart = new StringBuilder();

        //public SiteTreeViewBuilder(SiteOrganisationalUnitDto site)
        //{
        //    _site = site;
        //}

        //public string Build()
        //{
            
        //    if (MasterSiteHasNoChildren())
        //        return CreateCompleteListItem(_site);

        //    _siteOrganisationalChart.Append(CreateStartListItem(_site));
        //    _siteOrganisationalChart.Append(ProcessChildrenSites(_site));
        //    _siteOrganisationalChart.Append(CreateEndListItem());

        //    return _siteOrganisationalChart.ToString();
        //}

        //private  bool MasterSiteHasNoChildren()
        //{
        //    return  _site.Children.Count == 0;
        //}

        //private StringBuilder ProcessChildrenSites(SiteOrganisationalUnitDto master)
        //{
        //    var result = new StringBuilder();
        //    result.Append(CreateStartUnorderedList());

        //    foreach (var site in master.Children)
        //    {
        //        result.Append(CreateStartListItem(site));

        //        if (site.Children.Any())
        //        {
        //            var childrenSitesHtml = ProcessChildrenSites(site);
        //            result.Append(childrenSitesHtml.ToString());
        //        }

        //        result.Append(CreateEndListItem());
        //    }

        //    result.Append(CreateEndUnorderedList());
        //    return result;
        //}

        //private static string CreateEndUnorderedList()
        //{
        //    return "</ul>";
        //}

        //private static string CreateStartUnorderedList()
        //{
        //    return "<ul>";
        //}

        //private static string CreateEndListItem()
        //{
        //    return "</li>";
        //}

        //private static string CreateStartListItem(SiteOrganisationalUnitDto site)
        //{
        //    return string.Format("<li><div class='linked-site' data-type='{0}' data-id='{1}'>{2}</div>", GetSiteType(site), GetDataIdToLoad(site), site.Name);
        //}

        //private string CreateCompleteListItem(SiteOrganisationalUnitDto site)
        //{
        //    return string.Format("<li><div class='linked-site' data-type='{0}' data-id='{1}'>{2}</div></li>", GetSiteType(site), GetDataIdToLoad(site), site.Name);
        //}

        //private static string GetSiteType(SiteOrganisationalUnitDto site)
        //{
        //    if (site.SiteType == SiteTypeDto.SiteAddress)
        //        return "siteaddress";

        //    if (site.SiteType == SiteTypeDto.SiteGroup)
        //        return "sitegroup";

        //    throw new ArgumentException("Site Type not defined. Neither SiteAddress or SiteGroup");
            
        //}

        //private static string GetDataIdToLoad(SiteOrganisationalUnitDto site)
        //{
        //    if (site.SiteType == SiteTypeDto.SiteAddress)
        //        return site.Id.ToString();

        //    if (site.SiteType == SiteTypeDto.SiteGroup)
        //        return site.Id.ToString();

        //    throw new ArgumentException("Site Type not defined. Neither SiteAddress or SiteGroup");

        //}
    }
}