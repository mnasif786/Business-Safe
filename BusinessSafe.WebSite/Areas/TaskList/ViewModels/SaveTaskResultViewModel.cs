namespace BusinessSafe.WebSite.Areas.TaskList.ViewModels
{
    public class SaveTaskResultViewModel
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; }
        public long Id { get; set; }
        public long RiskAssessmentHazardId { get; set; }
    }
}