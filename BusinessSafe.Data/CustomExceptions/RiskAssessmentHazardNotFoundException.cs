using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class RiskAssessmentHazardNotFoundException : ArgumentNullException
    {
        public RiskAssessmentHazardNotFoundException(long riskAssessmentHazardId)
            : base(string.Format("Risk Assessment Hazard Not Found. Risk Assessment Hazard not found for risk assessment hazard id {0}", riskAssessmentHazardId))
        {
        }
    }
}