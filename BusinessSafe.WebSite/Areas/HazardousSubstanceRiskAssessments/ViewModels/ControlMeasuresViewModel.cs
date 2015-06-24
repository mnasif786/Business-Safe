using System.Collections.Generic;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels
{
    public class ControlMeasuresViewModel
    {
        public long RiskAssessmentId { get; set; }
        public long CompanyId { get; set; }
        public IEnumerable<HazardousSubstanceRiskAssessmentControlMeasureViewModel> ControlMeasures { get; set; }
        public IEnumerable<HazardousSubstanceRiskAssessmentFurtherControlMeasureViewModel> FurtherControlMeasures { get; set; }
    }
}