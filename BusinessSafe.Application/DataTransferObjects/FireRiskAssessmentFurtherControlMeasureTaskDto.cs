namespace BusinessSafe.Application.DataTransferObjects
{
    public class FireRiskAssessmentFurtherControlMeasureTaskDto : FurtherControlMeasureTaskDto
    {
        public SignificantFindingDto SignificantFinding { get; set; }
    }
}