using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.ActionPlans.ViewModels
{
    public class ActionPlanIndexViewModel
    {
        public long? SiteId { get; set; }
        public long? SiteGroupId { get; set; }
        public string SubmittedFrom { get; set; }
        public string SubmittedTo { get; set; }

        public IEnumerable<AutoCompleteViewModel> Sites { get; set; }
        public IEnumerable<AutoCompleteViewModel> SiteGroups { get; set; }

        public IEnumerable<SearchActionPlanResultViewModel> ActionPlans { get; set; }

        public bool IsShowDeleted { get; set; }
        public bool IsShowCompleted { get; set; }
        public bool IsShowArchived { get; set; }

        public int Size { get; set; }
        public int Total { get; set; }

        public int Page { get; set; }
        public string OrderBy { get; set; }
    }
}