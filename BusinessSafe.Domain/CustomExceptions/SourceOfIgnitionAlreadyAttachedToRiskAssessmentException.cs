using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class SourceOfIgnitionAlreadyAttachedToRiskAssessmentException : ApplicationException
    {
        public SourceOfIgnitionAlreadyAttachedToRiskAssessmentException(long riskAssessmentId, long sourceOfIgnitionId)
            : base(string.Format("Trying to attach source of ignition to risk assessment. Risk Assessment Id {0}. Source of ignition Id {1}", riskAssessmentId, sourceOfIgnitionId))
        { }
    }
}