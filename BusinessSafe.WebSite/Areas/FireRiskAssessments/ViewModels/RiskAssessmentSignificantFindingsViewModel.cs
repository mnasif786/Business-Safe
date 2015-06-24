using System.Collections.Generic;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels
{
    public class RiskAssessmentSignificantFindingsViewModel
    {
        public IEnumerable<SignificantFindingViewModel> SignificantFindings { get; set; }
        public long CompanyId { get; set; }
        public long RiskAssessmentId { get; set; }
    }
}