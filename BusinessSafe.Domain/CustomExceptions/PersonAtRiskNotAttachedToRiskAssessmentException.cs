using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class PersonAtRiskNotAttachedToRiskAssessmentException: ApplicationException
    {
        public PersonAtRiskNotAttachedToRiskAssessmentException(long riskAssessmentId, long personAtRiskId)
            : base(string.Format("Trying to detach person at risk from risk assessment. Risk Assessment Id {0}. Person At Risk Id {1}", riskAssessmentId, personAtRiskId))
        {}
    }
}