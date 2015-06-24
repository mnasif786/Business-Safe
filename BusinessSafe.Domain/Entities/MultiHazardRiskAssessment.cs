using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BusinessSafe.Domain.CustomExceptions;

namespace BusinessSafe.Domain.Entities
{
    public abstract class MultiHazardRiskAssessment : RiskAssessment
    {
        public virtual string Location { get; protected set; }
        public virtual string TaskProcessDescription { get; protected set; }
        public virtual IList<MultiHazardRiskAssessmentHazard> Hazards { get; protected set; }

        protected MultiHazardRiskAssessment()
        {
            Hazards = new List<MultiHazardRiskAssessmentHazard>();
        }

        public virtual void UpdateSummary(string title, string reference, DateTime? assessmentDate, RiskAssessor riskAssessor, Site site, UserForAuditing currentUser)
        {
            if (IsDifferentRiskAssessor(riskAssessor))
            {
                if (AreThereAnyFurtherControlMeasureTasks())
                {
                    Hazards
                        .Where(h => h.FurtherControlMeasureTasks != null)
                        .SelectMany(h => h.FurtherControlMeasureTasks)
                        .ToList()
                        .ForEach(task =>
                                     {
                                         task.SendTaskCompletedNotification = riskAssessor == null ? true : !riskAssessor.DoNotSendTaskCompletedNotifications;
                                         task.SendTaskOverdueNotification = riskAssessor == null ? true : !riskAssessor.DoNotSendTaskOverdueNotifications;
                                         task.SetLastModifiedDetails(currentUser);
                                     });
                }
            }

            Title = title;
            Reference = reference;
            RiskAssessor = riskAssessor;
            AssessmentDate = assessmentDate;
            RiskAssessmentSite = site;
            SetLastModifiedDetails(currentUser);

        }

        protected bool AreThereAnyFurtherControlMeasureTasks()
        {
            return Hazards != null && Hazards.Any()
                   && Hazards.Any(h => h.FurtherControlMeasureTasks != null);
        }


        public virtual void AttachHazardsToRiskAssessment(IEnumerable<Hazard> hazards, UserForAuditing user)
        {
            var existingHazardsNeedToDetach = GetExistingHazardsToDetach(hazards);
            DetachHazardsFromRiskAssessment(existingHazardsNeedToDetach, user);

            var hazardsNeedToAttach = GetHazardsNeedToAttach(hazards);
            foreach (var hazard in hazardsNeedToAttach)
            {
                AttachHazardToRiskAssessment(hazard, user);
            }
        }

        private IEnumerable<Hazard> GetExistingHazardsToDetach(IEnumerable<Hazard> hazards)
        {
            return Hazards.Select(x => x.Hazard).Where(existingHazard => hazards.Count(x => x.Id == existingHazard.Id) == 0).ToList();
        }

        private IEnumerable<Hazard> GetHazardsNeedToAttach(IEnumerable<Hazard> hazards)
        {
            return hazards.Where(HazardNotAttachedToRiskAssessment);
        }

        private bool HazardNotAttachedToRiskAssessment(Hazard hazard)
        {
            return Hazards.Count(x => x.Hazard.Id == hazard.Id) == 0;
        }

        private bool HazardAttachedToRiskAssessment(Hazard hazard)
        {
            return Hazards.Any(x => x.Hazard.Id == hazard.Id);
        }

        public virtual void AttachHazardToRiskAssessment(Hazard hazard, UserForAuditing user)
        {
            if (!HazardAttachedToRiskAssessment(hazard))
            {
                var multiHazardRiskAssessmentHazard = MultiHazardRiskAssessmentHazard.Create(this, hazard, user);
                Hazards.Add(multiHazardRiskAssessmentHazard);
                SetLastModifiedDetails(user);
            }
        }

        public virtual void DetachHazardsFromRiskAssessment(IEnumerable<Hazard> hazards, UserForAuditing user)
        {
            foreach (var hazard in hazards)
            {
                DetachHazardFromRiskAssessment(hazard, user);
            }
        }

        private void DetachHazardFromRiskAssessment(Hazard hazard, UserForAuditing user)
        {
            if (Hazards.Count(x => x.Hazard.Id == hazard.Id) == 0)
            {
                throw new HazardNotAttachedToRiskAssessmentException(Id, hazard.Id);
            }

            Hazards.Where(x => x.Hazard.Id == hazard.Id)
                .ToList()
                .ForEach(multiHazardRiskAssessmentHazard =>
                             {
                                 if (!multiHazardRiskAssessmentHazard.CanDeleteHazard())
                                 {
                                     throw new Exception("MultiHazardRiskAssessmentHazard with Id " + multiHazardRiskAssessmentHazard.Id.ToString(CultureInfo.InvariantCulture) +
                                                         " cannot be removed from risk assessment because it contains Control Measures or Further Control Measures.");
                                 }

                                 if (multiHazardRiskAssessmentHazard.ControlMeasures != null)
                                 {
                                     multiHazardRiskAssessmentHazard.ControlMeasures.ToList().ForEach(x => x.SetLastModifiedDetails(user));
                                 }

                                 multiHazardRiskAssessmentHazard.Deleted = true;
                                 multiHazardRiskAssessmentHazard.SetLastModifiedDetails(user);
                             });

            SetLastModifiedDetails(user);
        }

        public virtual MultiHazardRiskAssessmentHazard FindRiskAssessmentHazard(long riskAssessmentHazardId)
        {
            if (Hazards.Count(h => h.Id == riskAssessmentHazardId) == 0)
                throw new HazardDoesNotExistInRiskAssessmentException(Id, riskAssessmentHazardId);

            return Hazards.Single(x => x.Id == riskAssessmentHazardId);
        }

        public virtual MultiHazardRiskAssessmentHazard FindRiskAssessmentHazardByHazardId(long hazardId)
        {
            if (Hazards.Count(h => h.Hazard.Id == hazardId) == 0)
                throw new HazardDoesNotExistInRiskAssessmentException(Id, hazardId);

            return Hazards.Single(x => x.Hazard.Id == hazardId);
        }

        public override bool HasUndeletedTasks()
        {
            foreach (var hazard in Hazards.Where(x => x.Deleted == false))
            {
                if (hazard.FurtherControlMeasureTasks.Any(x => x.Deleted == false))
                {
                    return true;
                }
            }

            foreach (var review in Reviews.Where(x => x.Deleted == false))
            {
                if (!review.RiskAssessmentReviewTask.Deleted)
                {
                    return true;
                }
            }

            return false;
        }

        public override bool HasUncompletedTasks()
        {
            foreach (var hazard in Hazards.Where(x => x.Deleted == false))
            {
                if (hazard.FurtherControlMeasureTasks.Any(x => x.TaskStatus == TaskStatus.Outstanding))
                {
                    return true;
                }
            }

            return false;
        }

        public virtual void UpdatePremisesInformation(string locationAreaDepartment, string taskProcessDescription, UserForAuditing currentUser)
        {
            Location = locationAreaDepartment;
            TaskProcessDescription = taskProcessDescription;
            SetLastModifiedDetails(currentUser);
        }

        public virtual void UpdateControlMeasureForRiskAssessmentHazard(long riskAssessmentHazardId, long controlMeasureId, string controlMeasure, UserForAuditing user)
        {
            if (Hazards.Count(h => h.Id == riskAssessmentHazardId) == 0)
            {
                throw new HazardDoesNotExistInRiskAssessmentException(Id, riskAssessmentHazardId);
            }

            var riskAssessmentHazard = Hazards.Single(h => h.Id == riskAssessmentHazardId);

            riskAssessmentHazard.UpdateControlMeasure(controlMeasureId, controlMeasure, user);
        }

        /// <summary>
        /// The next completion date of a further control measure task or review task
        /// </summary>
        public virtual DateTime? CompletionDueDate
        {
            get
            {
                var nextFCMCompletionDueDate = NextFurtherControlMeasureTaskCompletionDueDate;

                if (!nextFCMCompletionDueDate.HasValue && !NextReviewDate.HasValue)
                {
                    return null;
                }
                else if (nextFCMCompletionDueDate.HasValue && !NextReviewDate.HasValue)
                {
                    return nextFCMCompletionDueDate;
                }
                else if (!nextFCMCompletionDueDate.HasValue && NextReviewDate.HasValue)
                {
                    return NextReviewDate;
                }
                else if (nextFCMCompletionDueDate < NextReviewDate)
                {
                    return nextFCMCompletionDueDate;
                }
                else
                {
                    return NextReviewDate;
                }

            }
        }


        protected virtual DateTime? NextFurtherControlMeasureTaskCompletionDueDate
        {
            get
            {
                return Hazards
                    .Where(hazard => hazard.Deleted == false)
                    .SelectMany(hazard => hazard.FurtherControlMeasureTasks)
                    .Where(task => task.Deleted == false && task.TaskStatus != TaskStatus.Completed && task.TaskStatus != TaskStatus.NoLongerRequired)
                    .Min(task => task.TaskCompletionDueDate);
            }
        }

        public virtual void UpdateHazardOrder(Hazard hazard, int orderNumber, UserForAuditing user)
        {
            if (HazardAttachedToRiskAssessment(hazard))
            {
                var riskAssessmentHazard = Hazards.First(x => x.Hazard.Id == hazard.Id);
                riskAssessmentHazard.OrderNumber = orderNumber;
                riskAssessmentHazard.SetLastModifiedDetails(user);
                SetLastModifiedDetails(user);
            }
        }
    }
}