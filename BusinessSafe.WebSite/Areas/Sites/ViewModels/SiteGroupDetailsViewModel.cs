using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using BusinessSafe.Application.Request.Attributes;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Sites.ViewModels
{
    public class SiteGroupDetailsViewModel
    {        
        public long GroupId { get; set; }
        public long ClientId { get; set; }
        public bool HasChildren { private get; set; }

        public string SectionTitle
        {
            get
            {
                if (GroupId == 0) return "Add Site Group";

                return "Edit Site Group";
            }
        }

        [Required(ErrorMessage = "Group name required")]
        public string Name { get; set; }

        [OneDropDownSelected("GroupLinkToGroupId")]
        [AtLeastOneDropDownSelected("GroupLinkToGroupId")]
        public long? GroupLinkToSiteId { get; set; }

        [OneDropDownSelected("GroupLinkToSiteId")]
        [AtLeastOneDropDownSelected("GroupLinkToSiteId")]
        public long? GroupLinkToGroupId { get; set; }

        public IEnumerable<AutoCompleteViewModel> ExistingSites { get; set; }
        public IEnumerable<AutoCompleteViewModel> ExistingGroups { get; set; }

        public SiteGroupDetailsViewModel()
        {
            ExistingSites = new List<AutoCompleteViewModel>();
            ExistingGroups = new List<AutoCompleteViewModel>();
        }

        public SiteGroupDetailsViewModel(long groupId, long clientId, string name, IEnumerable<AutoCompleteViewModel> existingSites, IEnumerable<AutoCompleteViewModel> existingGroups)
        {
            GroupId = groupId;
            ClientId = clientId;
            Name = name;
            ExistingSites = existingSites ?? new List<AutoCompleteViewModel>();
            ExistingGroups = existingGroups ?? new List<AutoCompleteViewModel>();
        }

        public bool FormEnabled
        {
            get { return GroupId > 0; }
        }

        public long OriginalLinkId { get; set; }

        public bool IsSaveButtonEnabled(IPrincipal user)
        {
            return user.IsInRole(GroupId == 0 ? Permissions.AddSiteDetails.ToString() : Permissions.EditSiteDetails.ToString());
        }

        public bool IsDeleteButtonEnabled(IPrincipal user)
        {
            return !HasChildren && GroupId > 0 && user.IsInRole(Permissions.DeleteSiteDetails.ToString());
        }
    }
}