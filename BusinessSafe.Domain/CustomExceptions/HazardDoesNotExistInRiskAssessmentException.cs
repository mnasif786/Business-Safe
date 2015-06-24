using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class HazardDoesNotExistInRiskAssessmentException : ApplicationException
    {
        public HazardDoesNotExistInRiskAssessmentException(long riskAssessmentId, long riskAssessmentHazardId)
            : base(string.Format("Could not find risk assessment hazard for Risk Assessment Id {0}. Risk Assessment Hazard Id {1}", riskAssessmentId, riskAssessmentHazardId))
        { }
    }
}