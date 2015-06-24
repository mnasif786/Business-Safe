using System;
using System.Collections.Generic;

namespace BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels
{
    public class GeneralRiskAssessmentArchiveIndexViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Ref { get; set; }
        public DateTime CreatedOn { get; set; }
        public IEnumerable<GeneralRiskAssessmentArchiveSummaryViewModel> Archive { get; set; } 

        public GeneralRiskAssessmentArchiveIndexViewModel()
        {
            Archive = new List<GeneralRiskAssessmentArchiveSummaryViewModel>();
        }
    }
}