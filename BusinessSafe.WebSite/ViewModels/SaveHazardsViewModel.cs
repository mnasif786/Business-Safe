using System.Collections.Generic;
using BusinessSafe.Application.Request;

namespace BusinessSafe.WebSite.ViewModels
{
    public class SaveHazardsViewModel
    {
        public long RiskAssessmentId { get; set; }
        public long CompanyId { get; set; }
        public long[] HazardIds { get; set; }
        public long[] PeopleAtRiskIds { get; set; }
        public long[] FireSafetyControlMeasureIds { get; set; }
        public long[] SourceOfFuelsIds { get; set; }
        public long[] SourceOfIgnitionIds { get; set; }
        
        public SaveHazardsViewModel()
        {
            HazardIds = new long[] {};
            PeopleAtRiskIds = new long[]{};
            FireSafetyControlMeasureIds = new long[]{};
            SourceOfFuelsIds = new long[]{};
            SourceOfIgnitionIds = new long[]{};
        }
    }
}