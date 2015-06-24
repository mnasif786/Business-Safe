namespace BusinessSafe.Application.DataTransferObjects
{
    public class FireRiskAssessmentControlMeasureDto
    {
        public FireRiskAssessmentDto RiskAssessment { get; set; }
        public FireSafetyControlMeasureDto ControlMeasure { get; set; }
    }
}