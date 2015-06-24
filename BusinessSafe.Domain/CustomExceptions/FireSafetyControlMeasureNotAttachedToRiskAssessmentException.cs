using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class FireSafetyControlMeasureNotAttachedToRiskAssessmentException : ApplicationException
    {
        public FireSafetyControlMeasureNotAttachedToRiskAssessmentException(long riskAssessmentId, long fireSafetyControlMeasureId)
            : base(string.Format("Trying to detach fire safety control measure from risk assessment. Risk Assessment Id {0}. Fire Safety Control Measure Id {1}", riskAssessmentId, fireSafetyControlMeasureId))
        { }
    }
}