namespace BusinessSafe.Application.DataTransferObjects
{
    public class PersonalRiskAssessmentFurtherControlMeasureTaskDto : FurtherControlMeasureTaskDto
    {
        public RiskAssessmentHazardDto RiskAssessmentHazard { get; set; }
    }
}