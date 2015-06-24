using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using BusinessSafe.Application.Request.Attributes;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Sites.ViewModels
{
    public enum SiteStatus
    {
        [Description("Open")]
        Open = 0,
        [Description("Close")]
        Close = 1,
        [Description("No Change")]
        NoChange = 2
    }
    public class SiteDetailsViewModel
    {
        public string ActioningUserName;
        public long SiteStructureId { get; set; }
        public long SiteId { get; set; }
        public long ClientId { get; set; }        
        public bool IsMainSite { get; set; }

        [Required(ErrorMessage = "Site name required")]
        public string Name { get; set; }       
        public string Reference { get; set; }

        [OneDropDownSelected("LinkToGroupId")]
        [AtLeastOneDropDownSelectedMainSite("LinkToGroupId-IsMainSite")]
        public long? LinkToSiteId { get; set; }
        
        [OneDropDownSelected("LinkToSiteId")]
        [AtLeastOneDropDownSelectedMainSite("LinkToSiteId-IsMainSite")]
        public long? LinkToGroupId { get; set; }

        public long? OriginalLinkId { get; set; }
        
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string AddressLine5 { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string Telephone { get; set; }
        [MaxLength(100, ErrorMessage = "Site contact cannot be longer than 100 characters")]
        public string SiteContact { get; set; }
        
        public IEnumerable<AutoCompleteViewModel> ExistingSites { get; set; }
        public IEnumerable<AutoCompleteViewModel> ExistingGroups { get; set; }

        public string NameBeforeUpdate { get; set; }

        public bool FormEnabled
        {
            get { return SiteId > 0; }
        }


        public SiteDetailsViewModel()
        {
            ExistingSites = new List<AutoCompleteViewModel>();
            ExistingGroups = new List<AutoCompleteViewModel>();
        }

        public bool IsSaveButtonEnabled(IPrincipal user)
        {
            return user.IsInRole(SiteStructureId == 0 ? Permissions.AddSiteDetails.ToString() : Permissions.EditSiteDetails.ToString());
        }

        public bool SiteClosed { get; set; }

        public SiteStatus SiteStatusCurrent { get; set; }
        public SiteStatus SiteStatusUpdated { get; set; }
    }
}