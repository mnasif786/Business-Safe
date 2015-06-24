using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class HazardousSubstanceRiskAssessmentNotFoundException : ArgumentNullException
    {
        public HazardousSubstanceRiskAssessmentNotFoundException(long hazardousSubstanceRiskAssessmentId)
            : base(string.Format("Hazardous Substance Risk Assessment Not Found. Hazardous Substance Risk Assessment not found for hazardous substance risk assessment id {0}", hazardousSubstanceRiskAssessmentId))
        {
        }
    }
}