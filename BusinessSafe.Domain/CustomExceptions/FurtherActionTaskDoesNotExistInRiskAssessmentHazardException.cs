using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class FurtherActionTaskDoesNotExistInRiskAssessmentHazardException : ApplicationException
    {
        public FurtherActionTaskDoesNotExistInRiskAssessmentHazardException(MultiHazardRiskAssessmentHazard riskAssessmentHazard, long riskAssessmentFutherActionTaskId)
            : base(string.Format("Could not find risk assessment further action task for Risk Assessment Hazard Id {0}. Further Action Task Id {1}", riskAssessmentHazard.Id, riskAssessmentFutherActionTaskId))
        { }
    }
}