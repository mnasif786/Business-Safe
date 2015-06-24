using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using System.Linq;
using BusinessSafe.Domain.CustomExceptions;

namespace BusinessSafe.Domain.Entities
{
    public class MultiHazardRiskAssessmentHazard: Entity<long>
    {
        public virtual string Description { get; protected set; }
        public virtual Hazard Hazard { get; protected set; }
        public virtual MultiHazardRiskAssessment MultiHazardRiskAssessment { get; protected set; }
        public virtual IList<MultiHazardRiskAssessmentControlMeasure> ControlMeasures { get; protected set; }
        public virtual IList<MultiHazardRiskAssessmentFurtherControlMeasureTask> FurtherControlMeasureTasks { get; protected set; }
        public virtual int? OrderNumber { get; set; }

        public MultiHazardRiskAssessmentHazard()
        {
            ControlMeasures = new List<MultiHazardRiskAssessmentControlMeasure>();
            FurtherControlMeasureTasks = new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>();
        }

        public static MultiHazardRiskAssessmentHazard Create(MultiHazardRiskAssessment riskAssessment, Hazard hazard, UserForAuditing user)
        {
            return new MultiHazardRiskAssessmentHazard()
                       {
                           Hazard = hazard,
                           MultiHazardRiskAssessment = riskAssessment,
                           CreatedBy = user,
                           CreatedOn = DateTime.Now,
                           LastModifiedBy = user,
                           LastModifiedOn = DateTime.Now
                       };
        }

        public virtual void UpdateDescription(string description, UserForAuditing user)
        {
            Description = description;
            SetLastModifiedDetails(user);
        }

        public virtual void UpdateTitle(string title, UserForAuditing user)
        {
            if (Hazard.RiskAssessmentId != MultiHazardRiskAssessment.Id)
            {
                throw new Exception("Cannot edit title because hazard is not unique to this risk assessment.");
            }

            Hazard.Name = title;
            Hazard.SetLastModifiedDetails(user);
            SetLastModifiedDetails(user);
        }     

        public virtual void AddControlMeasure(MultiHazardRiskAssessmentControlMeasure controlMeasure, UserForAuditing user)
        {
            ControlMeasures.Add(controlMeasure);
            SetLastModifiedDetails(user);
        }

        public virtual void RemoveControlMeasure(long controlMeasureId, UserForAuditing user)
        {
            if (ControlMeasures.Count(c => c.Id == controlMeasureId) == 0)
            {
                throw new ControlMethodDoesNotExistInHazard(this, controlMeasureId);
            }

            var controlMeasure = ControlMeasures.Single(c => c.Id == controlMeasureId);
            controlMeasure.MarkAsDeleted(user);   
            SetLastModifiedDetails(user);
        }

        public virtual void UpdateControlMeasure(long controlMeasureId, string controlMeasure, UserForAuditing user)
        {
            if (ControlMeasures.Count(c => c.Id == controlMeasureId) == 0)
            {
                throw new ControlMethodDoesNotExistInHazard(this, controlMeasureId);
            }

            var controlMeasureToUpdate = ControlMeasures.Single(c => c.Id == controlMeasureId);

            controlMeasureToUpdate.UpdateControlMeasure(controlMeasure, user);
            SetLastModifiedDetails(user);
        }
        
        public virtual void AddFurtherActionTask(MultiHazardRiskAssessmentFurtherControlMeasureTask multiHazardRiskAssessmentFurtherControlMeasureTask, UserForAuditing user)
        {
            FurtherControlMeasureTasks.Add(multiHazardRiskAssessmentFurtherControlMeasureTask);
            multiHazardRiskAssessmentFurtherControlMeasureTask.MultiHazardRiskAssessmentHazard = this;
            SetLastModifiedDetails(user);
        }
        
        public virtual bool CanDeleteHazard()
        {
            return !(FurtherControlMeasureTasks.Any(x => x.Deleted == false) || ControlMeasures.Any(x => x.Deleted == false));
        }


        public virtual MultiHazardRiskAssessmentHazard CloneForRiskAssessmentTemplating(UserForAuditing user, MultiHazardRiskAssessment riskAssessment)
        {
            var result = new MultiHazardRiskAssessmentHazard
                             {
                                 MultiHazardRiskAssessment = riskAssessment,
                                 Description = Description,
                                 CreatedOn = DateTime.Now,
                                 CreatedBy = user
                             };

            if (Hazard.RiskAssessment != null)
            {
                var hazardTypes = new List<HazardType>();
                
                foreach (var hazardType in Hazard.HazardTypes)
                {
                    hazardTypes.Add(hazardType);
                }

                result.Hazard = Hazard.Create(
                    Hazard.Name,
                    Hazard.CompanyId.Value,
                    user,
                    hazardTypes,
                    riskAssessment);
            }
            else
            {   
                result.Hazard = Hazard;
            }


            foreach (var controlMeasure in ControlMeasures)
            {
                var clonedControlMeasure = controlMeasure.CloneForRiskAssessmentTemplating(result, user);
                result.AddControlMeasure(clonedControlMeasure, user);
            }

            return result;
        }
    }
}