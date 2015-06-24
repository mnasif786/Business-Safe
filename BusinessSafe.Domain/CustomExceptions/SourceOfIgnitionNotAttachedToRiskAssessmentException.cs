using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class SourceOfIgnitionNotAttachedToRiskAssessmentException : ApplicationException
    {
        public SourceOfIgnitionNotAttachedToRiskAssessmentException(long riskAssessmentId, long sourceOfIgnition)
            : base(string.Format("Trying to detach source of ignition from risk assessment. Risk Assessment Id {0}. Source Of Ignition Id {1}", riskAssessmentId, sourceOfIgnition))
        { }
    }
}