using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class PersonAtRiskAlreadyAttachedToRiskAssessmentException : ApplicationException
    {
        public PersonAtRiskAlreadyAttachedToRiskAssessmentException(long riskAssessmentId, long personAtRiskId)
            : base(string.Format("Trying to attach person at risk to risk assessment. Risk Assessment Id {0}. Person At Risk Id {1}", riskAssessmentId, personAtRiskId))
        { }
    }
}