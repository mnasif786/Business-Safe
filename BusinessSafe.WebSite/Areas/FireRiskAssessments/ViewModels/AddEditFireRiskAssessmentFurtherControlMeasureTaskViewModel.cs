namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels
{
    public class AddEditFireRiskAssessmentFurtherControlMeasureTaskViewModel : WebSite.ViewModels.AddEditFurtherControlMeasureTaskViewModel
    {
        public long SignificantFindingId { get; set; }
        public long RiskAssessmentId { get; set; }
    }
}