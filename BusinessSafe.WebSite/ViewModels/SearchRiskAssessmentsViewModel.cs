using System;
using System.Collections.Generic;
using Telerik.Web.Mvc.UI;
using Telerik.Web.Mvc.UI.Fluent;

namespace BusinessSafe.WebSite.ViewModels
{
    public class SearchRiskAssessmentsViewModel : ISearchRiskAssessmentsViewModel
    {
        public long CompanyId { get; set; }
        public string CreatedFrom { get; set; }
        public string CreatedTo { get; set; }
        public long? SiteGroupId { get; set; }
        public List<SearchRiskAssessmentResultViewModel> RiskAssessments { get; set; }
        public IEnumerable<AutoCompleteViewModel> Sites;
        public IEnumerable<SiteMultiSelectViewModel> MultiSelectSites { get; set; }
        public string Title { get; set; }
        public bool IsRiskAssessmentTemplating { get; set; }
        public IEnumerable<AutoCompleteViewModel> SiteGroups { get; set; }
        public bool IsShowDeleted { get; set; }
        public bool IsShowArchived { get; set; }
        public long SiteId { get; set; }
        public int Total { get; set; }
        public int PageSize { get; set; }

    }
}