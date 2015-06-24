namespace BusinessSafe.Application.Request.FireRiskAssessments
{
    public class GetFurtherControlMeasureTaskCountsForAnswerRequest
    {
        public long FireChecklistId { get; set; }
        public long FireQuestionId { get; set; }
    }
}