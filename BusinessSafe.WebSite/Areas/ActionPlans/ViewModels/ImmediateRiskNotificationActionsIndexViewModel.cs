using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.ActionPlans.ViewModels
{
    public class ImmediateRiskNotificationActionsIndexViewModel
    {
        public long ActionPlanId { get; set; }
        public long? SiteId { get; set; }
        public string SiteName { get; set; }
        public long? SiteGroupId { get; set; }
        public DateTime? VisitDate { get; set; }
        public int StatusId { get; set; }
        public IEnumerable<AutoCompleteViewModel> Status { get; set; }
        public string DueFrom { get; set; }
        public string DueTo { get; set; }
        public string AreasVisited { get; set; }
        public string AreasNotVisited { get; set; }
        public string PersonSeen { get; set; }
        public bool IsShowDeleted { get; set; }
        public Guid AssignedToId { get; set; }
        public IEnumerable<AutoCompleteViewModel> AssignedTo { get; set; }
        public bool NoLongerRequired { get; set; }

        public string VisitDateFormatted
        {
            get
            {
                return VisitDate.HasValue ? VisitDate.Value.ToShortDateString() : string.Empty;
            }
        }
        

        public IList<SearchActionResultViewModel> Actions { get; set; }
        public IEnumerable<SearchImmediateRiskNotificationResultViewModel> ImmediateRiskNotification { get; set; }

        public bool CanEdit(IPrincipal user)
        {
            return user.IsInRole(Permissions.EditActionPlan.ToString());
        }
    }
}