using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class SourceOfFuelNotAttachedToRiskAssessmentException : ApplicationException
    {
        public SourceOfFuelNotAttachedToRiskAssessmentException(long riskAssessmentId, long sourceOfFuelId)
            : base(string.Format("Trying to detach source of fuel from risk assessment. Risk Assessment Id {0}. Source Of Fuel Id {1}", riskAssessmentId, sourceOfFuelId))
        { }
    }
}