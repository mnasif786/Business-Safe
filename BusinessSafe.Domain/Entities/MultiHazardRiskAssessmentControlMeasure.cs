using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class MultiHazardRiskAssessmentControlMeasure : Entity<long>
    {
        public virtual string ControlMeasure { get; protected set; }
        public virtual MultiHazardRiskAssessmentHazard MultiHazardRiskAssessmentHazard { get; protected set; }

        public static MultiHazardRiskAssessmentControlMeasure Create(string controlMeasure, MultiHazardRiskAssessmentHazard riskAssessmentHazard, UserForAuditing user)
        {
            return new MultiHazardRiskAssessmentControlMeasure()
            {
                ControlMeasure = controlMeasure,
                MultiHazardRiskAssessmentHazard = riskAssessmentHazard,
                CreatedBy = user,
                CreatedOn = DateTime.Now
            };
        }

        public virtual void UpdateControlMeasure(string controlMeasure, UserForAuditing user)
        {
            ControlMeasure = controlMeasure;
            SetLastModifiedDetails(user);
        }

        public virtual void MarkAsDeleted(UserForAuditing user)
        {
            Deleted = true;
            SetLastModifiedDetails(user);
        }

        public virtual MultiHazardRiskAssessmentControlMeasure CloneForRiskAssessmentTemplating(MultiHazardRiskAssessmentHazard riskAssessmentHazard, UserForAuditing user)
        {
            var result = new MultiHazardRiskAssessmentControlMeasure()
                             {
                                 MultiHazardRiskAssessmentHazard = riskAssessmentHazard,
                                 ControlMeasure = ControlMeasure,
                                 CreatedBy = user,
                                 CreatedOn = DateTime.Now
                             };
            return result;
        }
    }
}