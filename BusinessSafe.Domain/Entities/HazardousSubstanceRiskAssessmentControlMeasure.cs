using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class HazardousSubstanceRiskAssessmentControlMeasure : Entity<long>
    {
        public virtual string ControlMeasure { get; protected set; }
        public virtual HazardousSubstanceRiskAssessment HazardousSubstanceRiskAssessment { get; protected set; }

        public static HazardousSubstanceRiskAssessmentControlMeasure Create(string controlMeasure, HazardousSubstanceRiskAssessment hazardousSubstanceRiskAssessment, UserForAuditing user)
        {
            return new HazardousSubstanceRiskAssessmentControlMeasure()
                       {
                           ControlMeasure = controlMeasure,
                           HazardousSubstanceRiskAssessment = hazardousSubstanceRiskAssessment,
                           CreatedBy = user,
                           CreatedOn = DateTime.Now
                       };
        }

        public virtual void UpdateControlMeasure(string controlMeasure, UserForAuditing user)
        {
            ControlMeasure = controlMeasure;
            SetLastModifiedDetails(user);
        }

        public virtual HazardousSubstanceRiskAssessmentControlMeasure CloneForRiskAssessmentTemplating(HazardousSubstanceRiskAssessment riskAssessment, UserForAuditing user)
        {
            var result = new HazardousSubstanceRiskAssessmentControlMeasure()
            {
                HazardousSubstanceRiskAssessment = riskAssessment,
                ControlMeasure = ControlMeasure,
                CreatedBy = user,
                CreatedOn = DateTime.Now,
                LastModifiedBy = LastModifiedBy,
                LastModifiedOn = LastModifiedOn
            };

            return result;
        }
    }
}