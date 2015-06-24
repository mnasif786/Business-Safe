namespace BusinessSafe.Application.DataTransferObjects
{
    public class ControlMeasureDto
    {
        public long Id { get; set; }
        public long RiskAssessmentHazardId { get; set; }
        public string ControlMeasure { get; set; }
    }
}