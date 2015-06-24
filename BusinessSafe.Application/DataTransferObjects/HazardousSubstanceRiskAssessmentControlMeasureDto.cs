namespace BusinessSafe.Application.DataTransferObjects
{
    public class HazardousSubstanceRiskAssessmentControlMeasureDto
    {
        public long Id { get; set; }
        public long HazardousSubstanceRiskAssessmentId { get; set; }
        public string ControlMeasure { get; set; }
    }
}