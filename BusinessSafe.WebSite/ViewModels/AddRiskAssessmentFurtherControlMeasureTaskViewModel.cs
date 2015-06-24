using BusinessSafe.Domain.Entities;

namespace BusinessSafe.WebSite.ViewModels
{
    public class AddRiskAssessmentFurtherControlMeasureTaskViewModel : AddEditFurtherControlMeasureTaskViewModel
    {
        public RiskAssessmentHazardSummaryViewModel HazardSummary { get; set; }
        public long RiskAssessmentHazardId { get; set; }
        public FurtherControlMeasureTaskCategoryEnum FurtherControlMeasureTaskCategory { get; set; }
    }
}