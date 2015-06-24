using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class HazardNotAttachedToRiskAssessmentException : ApplicationException
    {
        public HazardNotAttachedToRiskAssessmentException(long riskAssessmentId, long hazardId)
            : base(string.Format("Trying to detach hazard from risk assessment. Risk Assessment Id {0}. Hazard Id {1}", riskAssessmentId, hazardId))
        { }
    }
}