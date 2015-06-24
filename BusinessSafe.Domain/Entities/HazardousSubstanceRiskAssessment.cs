using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.CustomExceptions;

namespace BusinessSafe.Domain.Entities
{
    public class HazardousSubstanceRiskAssessment : RiskAssessment
    {
        public virtual bool? IsInhalationRouteOfEntry { get; set; }
        public virtual bool? IsIngestionRouteOfEntry { get; set; }
        public virtual bool? IsAbsorptionRouteOfEntry { get; set; }
        public virtual string WorkspaceExposureLimits { get; set; }
        public virtual HazardousSubstance HazardousSubstance { get; set; }
        public virtual Quantity? Quantity { get; set; }
        public virtual MatterState? MatterState { get; set; }
        public virtual DustinessOrVolatility? DustinessOrVolatility { get; set; }
        public virtual IList<HazardousSubstanceRiskAssessmentControlMeasure> ControlMeasures { get; protected set; }
        public virtual IList<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask> FurtherControlMeasureTasks { get; protected set; }
        public virtual bool? HealthSurveillanceRequired { get; set; }
        public virtual ControlSystem LastRecommendedControlSystem { get; protected set; }

        public HazardousSubstanceRiskAssessment()
            : base()
        {
            ControlMeasures = new List<HazardousSubstanceRiskAssessmentControlMeasure>();
            FurtherControlMeasureTasks = new List<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>();
        }

        //This is a derived field - if this causes performace issues, add 'HazardousSubstanceGroupId' to the HazardousSubstanceRiskAssessment
        //table, make this a persistent field, calculate the value in the Create method and then persist it. WARNING: Changing a hazardous substance
        //risk phrase group must automatically change the hazardous substance risk assessment group, so BE VERY CAREFUL if going for this option.
        public virtual HazardousSubstanceGroup Group
        {
            get
            {
                if (HazardousSubstance == null)
                {
                    return null;
                }

                return HazardousSubstance
                    .HazardousSubstanceRiskPhrases
                    .Select(riskPhrase => riskPhrase.RiskPhrase.Group)
                    .OrderByDescending(riskPhrase => riskPhrase.Code)
                    .FirstOrDefault();
            }
        }

        public static HazardousSubstanceRiskAssessment Create(string title, string reference, long clientId, UserForAuditing currentUser, HazardousSubstance hazardousSubstance)
        {
            var riskAssessment = new HazardousSubstanceRiskAssessment
            {
                CompanyId = clientId,
                Reference = reference,
                Title = title,
                CreatedBy = currentUser,
                CreatedOn = DateTime.Now,
                Status = RiskAssessmentStatus.Draft,
                HazardousSubstance = hazardousSubstance
            };
            return riskAssessment;
        }

        public virtual void Update(UserForAuditing user, bool isInhalationRouteOfEntry, bool isIngestionRouteOfEntry, bool isAbsorptionRouteOfEntry, string workspaceExposureLimits)
        {
            IsInhalationRouteOfEntry = isInhalationRouteOfEntry;
            IsIngestionRouteOfEntry = isIngestionRouteOfEntry;
            IsAbsorptionRouteOfEntry = isAbsorptionRouteOfEntry;
            WorkspaceExposureLimits = workspaceExposureLimits;
            SetLastModifiedDetails(user);
        }

        public virtual void UpdateAssessmentDetails(
            Quantity? quantity,
            MatterState? matterState,
            DustinessOrVolatility? dustinessOrVolatility,
            bool healthSurveillanceRequired,
            UserForAuditing currentUser)
        {
            Quantity = quantity;
            MatterState = matterState;
            DustinessOrVolatility = dustinessOrVolatility;
            HealthSurveillanceRequired = healthSurveillanceRequired;
            SetLastModifiedDetails(currentUser);
        }

        public virtual void AddControlMeasure(HazardousSubstanceRiskAssessmentControlMeasure controlMeasure, UserForAuditing user)
        {
            ControlMeasures.Add(controlMeasure);
            SetLastModifiedDetails(user);
        }

        public virtual void UpdateControlMeasure(long controlMeasureId, string updatedControlMeasure, UserForAuditing user)
        {
            var controlMeasure = FindHazardousSubstanceRiskAssessmentControlMeasure(controlMeasureId);
            controlMeasure.UpdateControlMeasure(updatedControlMeasure, user);

            SetLastModifiedDetails(user);
        }

        public virtual void RemoveControlMeasure(long controlMeasureId, UserForAuditing user)
        {
            var controlMeasure = FindHazardousSubstanceRiskAssessmentControlMeasure(controlMeasureId);
            controlMeasure.MarkForDelete(user);

            SetLastModifiedDetails(user);
        }

        public virtual void AddFurtherControlMeasureTask(HazardousSubstanceRiskAssessmentFurtherControlMeasureTask furtherControlMeasureTask, UserForAuditing user)
        {
            FurtherControlMeasureTasks.Add(furtherControlMeasureTask);
            SetLastModifiedDetails(user);
        }

        private HazardousSubstanceRiskAssessmentControlMeasure FindHazardousSubstanceRiskAssessmentControlMeasure(
            long controlMeasureId)
        {
            if (ControlMeasures.Count(x => x.Id == controlMeasureId) == 0)
            {
                throw new ControlMeasureDoesNotExistOnRiskAssessmentException(this, controlMeasureId);
            }

            var controlMeasure = ControlMeasures.Single(x => x.Id == controlMeasureId);
            return controlMeasure;
        }

        public override string PreFix
        {
            get { return "HSRA"; }
        }


        public virtual void UpdateSummary(string title, string reference, DateTime? assessmentDate, RiskAssessor riskAssessor, HazardousSubstance hazardousSubstance, Site site, UserForAuditing currentUser)
        {
            if (IsDifferentRiskAssessor(riskAssessor))
            {
                if (AreThereAnyFurtherControlMeasureTasks())
                {
                    FurtherControlMeasureTasks
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
            HazardousSubstance = hazardousSubstance;
            RiskAssessmentSite = site;
            SetLastModifiedDetails(currentUser);
        }

        protected bool AreThereAnyFurtherControlMeasureTasks()
        {
            return FurtherControlMeasureTasks != null && FurtherControlMeasureTasks.Any();
        }


        public override bool HasUndeletedTasks()
        {
            if (FurtherControlMeasureTasks.Any(x => x.Deleted == false))
            {
                return true;
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
            if (FurtherControlMeasureTasks.Any(x => x.TaskStatus == TaskStatus.Outstanding && x.Deleted == false))
            {
                return true;
            }
            return false;
        }

        public override IEnumerable<FurtherControlMeasureTask> GetAllUncompleteFurtherControlMeasureTasks()
        {
            var furtherControlMeasureTasks = FurtherControlMeasureTasks.Where(x => x.TaskStatus != TaskStatus.Completed && x.Deleted == false);
            return furtherControlMeasureTasks;
        }

        public override RiskAssessment Copy(string title, string reference, UserForAuditing user)
        {
            var clone = Create(title, reference, CompanyId, user, HazardousSubstance);
            clone.AssessmentDate = AssessmentDate;
            clone.CreatedOn = DateTime.Now;

            foreach (var employee in Employees)
            {
                clone.AttachEmployeeToRiskAssessment(employee.Employee, user);
            }

            foreach (var nonEmployee in NonEmployees)
            {
                clone.AttachNonEmployeeToRiskAssessment(nonEmployee.NonEmployee, user);
            }

            clone.WorkspaceExposureLimits = WorkspaceExposureLimits;
            clone.IsInhalationRouteOfEntry = IsInhalationRouteOfEntry;
            clone.IsIngestionRouteOfEntry = IsIngestionRouteOfEntry;
            clone.IsAbsorptionRouteOfEntry = IsAbsorptionRouteOfEntry;

            clone.Quantity = Quantity;
            clone.MatterState = MatterState;
            clone.DustinessOrVolatility = DustinessOrVolatility;
            clone.HealthSurveillanceRequired = HealthSurveillanceRequired;

            foreach (var document in Documents)
            {
                var clonedDocument = document.CloneForRiskAssessmentTemplating(user);
                clone.AttachDocumentToRiskAssessment(clonedDocument, user);
            }

            foreach (var controlMeasure in ControlMeasures)
            {
                var clonedControlMeasure = controlMeasure.CloneForRiskAssessmentTemplating(clone, user);
                clone.AddControlMeasure(clonedControlMeasure, user);
            }

            return clone;
        }

        public virtual void SetLastRecommendedControlSystem(ControlSystem controlSystem, UserForAuditing userForAuditing)
        {
            LastRecommendedControlSystem = controlSystem;
            SetLastModifiedDetails(userForAuditing);
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
                return FurtherControlMeasureTasks
                    .Where(task => task.Deleted == false 
                        && task.TaskStatus != TaskStatus.Completed 
                        && task.TaskStatus != TaskStatus.NoLongerRequired)
                    .Min(task => task.TaskCompletionDueDate);
            }
        }

        public override bool HasAnyReviews()
        {
            if (Reviews.Any(x => x.Deleted == false))
            {
                return true;
            }
            return false;
        }
    }
}