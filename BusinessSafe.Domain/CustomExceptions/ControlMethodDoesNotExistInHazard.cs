using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class ControlMethodDoesNotExistInHazard : ApplicationException
    {
        public ControlMethodDoesNotExistInHazard(MultiHazardRiskAssessmentHazard riskAssessmentHazard, long controlMeasureId)
            : base(string.Format("Could not find control measure for Risk Assessment Hazard Id {0}. Control Measure Id {1}", riskAssessmentHazard.Id, controlMeasureId))
        { }
    }
}