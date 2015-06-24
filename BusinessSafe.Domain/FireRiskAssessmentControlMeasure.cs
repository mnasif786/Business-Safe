using System;

using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class FireRiskAssessmentControlMeasure : Entity<long>
    {
        public virtual FireRiskAssessment RiskAssessment { get; set; }
        public virtual FireSafetyControlMeasure FireSafetyControlMeasure { get; set; }

        public static FireRiskAssessmentControlMeasure Create(FireRiskAssessment riskAssessment, FireSafetyControlMeasure controlMeasure, UserForAuditing user)
        {
            return new FireRiskAssessmentControlMeasure
                   {
                       RiskAssessment = riskAssessment,
                       FireSafetyControlMeasure = controlMeasure,
                       CreatedOn = DateTime.Now,
                       CreatedBy = user
                   };
        }
    }
}
