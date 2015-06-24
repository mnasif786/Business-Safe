namespace BusinessSafe.Application.DataTransferObjects
{
    public class MultiHazardRiskAssessmentFurtherControlMeasureTaskDto : FurtherControlMeasureTaskDto
    {
        public RiskAssessmentHazardDto RiskAssessmentHazard { get; set; }
    }
}