namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels
{
    public class ValidateCompleteFireRiskAssessmentChecklistViewModel
    {
        public long ChecklistId { get; set; }
        public long[] AllNoAnswerQuestionIds { get; set; }
    }
}