namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels
{
    public class MarkSignificantFindingAsDeletedViewModel
    {
        public long CompanyId { get; set; }
        public long FireChecklistId { get; set; }
        public long FireQuestionId { get; set; }
    }
}