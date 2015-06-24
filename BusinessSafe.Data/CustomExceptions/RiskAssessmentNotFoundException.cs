using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class RiskAssessmentNotFoundException : ArgumentNullException
    {
        public RiskAssessmentNotFoundException(long riskAssessmentId)
            : base(string.Format("Risk Assessment Not Found. Risk Assessment not found for risk assessment id {0}", riskAssessmentId))
        {
        }
    }
}