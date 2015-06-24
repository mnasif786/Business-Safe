using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class SourceOfFuelAlreadyAttachedToRiskAssessmentException : ApplicationException
    {
        public SourceOfFuelAlreadyAttachedToRiskAssessmentException(long riskAssessmentId, long sourceOfFuelId)
            : base(string.Format("Trying to attach source of fuel to risk assessment. Risk Assessment Id {0}. Source of fuel Id {1}", riskAssessmentId, sourceOfFuelId))
        { }
    }
}