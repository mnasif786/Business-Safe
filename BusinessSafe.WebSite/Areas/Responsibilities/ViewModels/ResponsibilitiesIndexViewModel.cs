using System;
using System.Collections.Generic;
using System.Data;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Responsibilities.ViewModels
{
    public class ResponsibilitiesIndexViewModel
    {
        public long? CategoryId { get; set; }
        public long? SiteId { get; set; }
        public long? SiteGroupId { get; set; }
        public string Title { get; set; }
        public string CreatedFrom { get; set; }
        public string CreatedTo { get; set; }
        public bool IsShowDeleted { get; set; }
        public bool IsShowCompleted { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public string OrderBy { get; set; }

        public IEnumerable<SearchResponsibilitiesResultViewModel> Responsibilities { get; set; }

        public IEnumerable<AutoCompleteViewModel> Sites { get; set; }
        public IEnumerable<AutoCompleteViewModel> SiteGroups { get; set; }
        public IEnumerable<AutoCompleteViewModel> Categories { get; set; }

        public string GetAdditionalTitle()
        {
            return string.Empty;
        }
    }
}