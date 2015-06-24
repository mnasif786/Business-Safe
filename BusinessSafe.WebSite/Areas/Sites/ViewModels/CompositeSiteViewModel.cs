using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.WebSite.Areas.Sites.ViewModels
{
    public enum CompositeSiteType
    {
        SiteAddress = 1,
        SiteGroup = 2
    }

    public class CompositeSiteViewModel
    {
        public long? Id { get; set; } 
        public long? SiteId { get; set; }
        public CompositeSiteType SiteType { get; set; }
        public IList<CompositeSiteViewModel> Children { get; set; }
        public string Name { get;  set; }
        public IList<SiteAddressDto> UnlinkedSites { get; set; }
        
        public CompositeSiteViewModel()
        {
            Children = new List<CompositeSiteViewModel>();
            UnlinkedSites = new List<SiteAddressDto>();
        }

        public CompositeSiteViewModel(long? siteId, long? id, string siteName)
            : this()
        {
            SiteId = siteId;
            Id = id;                        
            Name = siteName;
        }
    }
}