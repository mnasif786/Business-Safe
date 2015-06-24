using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.CustomExceptions;

namespace BusinessSafe.Domain.Entities
{
    public class GeneralRiskAssessment : MultiHazardRiskAssessment
    {
        public virtual IList<RiskAssessmentPeopleAtRisk> PeopleAtRisk { get; set; }

        public static GeneralRiskAssessment Create(
            string title,
            string reference,
            long clientId,
            UserForAuditing currentUser, 
            string location = null,
            string taskProcessDescription = null,
            Site site = null
            )
        {
            var riskAssessment = new GeneralRiskAssessment
                                     {
                                         CompanyId = clientId,
                                         Reference = reference,
                                         Title = title,
                                         CreatedBy = currentUser,
                                         CreatedOn = DateTime.Now,
                                         Status = RiskAssessmentStatus.Draft,
                                         PeopleAtRisk = new List<RiskAssessmentPeopleAtRisk>(),
                                         Location = location,
                                         TaskProcessDescription = taskProcessDescription,
                                         RiskAssessmentSite = site
                                     };

            return riskAssessment;
        }

        public static GeneralRiskAssessment Create(
            string title,
            string reference,
            long clientId,
            UserForAuditing currentUser,
            string location,
            string taskProcessDescription,
            Site site,
            DateTime? assessmentDate,
            RiskAssessor riskAssessor
           )
        {
            var riskAssessment = new GeneralRiskAssessment
                                     {
                                         Title = title,
                                         Reference = reference,
                                         AssessmentDate = assessmentDate,
                                         RiskAssessmentSite = site,
                                         RiskAssessor = riskAssessor,
                                         Location = location,
                                         TaskProcessDescription = taskProcessDescription,
                                         CompanyId = clientId,
                                         CreatedBy = currentUser,
                                         CreatedOn = DateTime.Now,
                                         Status = RiskAssessmentStatus.Draft,
                                         PeopleAtRisk = new List<RiskAssessmentPeopleAtRisk>(),
                                     };

            return riskAssessment;
        }


        public virtual void AttachPeopleAtRiskToRiskAssessment(IEnumerable<PeopleAtRisk> peopleAtRisk, UserForAuditing user)
        {
            var existingPeopleAtRiskNeedToDetach = GetExistingPeopleAtRiskToDetach(peopleAtRisk);
            DetachPeopleAtRiskFromRiskAssessment(existingPeopleAtRiskNeedToDetach, user);

            var peopeAtRiskNeedToAttach = GetPeopleAtRiskNeedToAttach(peopleAtRisk);
            foreach (var personAtRisk in peopeAtRiskNeedToAttach)
            {
                AttachPersonAtRiskToRiskAssessment(personAtRisk, user);
            }
        }

        private IEnumerable<PeopleAtRisk> GetExistingPeopleAtRiskToDetach(IEnumerable<PeopleAtRisk> peopleAtRisk)
        {
            return PeopleAtRisk.Select(x => x.PeopleAtRisk).Where(existingPersonAtRisk => peopleAtRisk.Count(x => x.Id == existingPersonAtRisk.Id) == 0).ToList();
        }

        private IEnumerable<PeopleAtRisk> GetPeopleAtRiskNeedToAttach(IEnumerable<PeopleAtRisk> peopleAtRisk)
        {
            return peopleAtRisk.Where(PersonAtRiskNotAttachedToRiskAssessment);
        }

        private bool PersonAtRiskNotAttachedToRiskAssessment(PeopleAtRisk peopleAtRisk)
        {
            return PeopleAtRisk.Count(x => x.PeopleAtRisk == peopleAtRisk) == 0;
        }

        public virtual void AttachPersonAtRiskToRiskAssessment(PeopleAtRisk peopleAtRisk, UserForAuditing user)
        {
            if (PeopleAtRisk.Count(x => x.PeopleAtRisk == peopleAtRisk) > 0)
            {
                throw new PersonAtRiskAlreadyAttachedToRiskAssessmentException(Id, peopleAtRisk.Id);
            }

            //PeopleAtRisk.Add(peopleAtRisk);

            PeopleAtRisk.Add(new RiskAssessmentPeopleAtRisk
                                 {
                                     PeopleAtRisk = peopleAtRisk,
                                     RiskAssessment = this,
                                     CreatedBy = user,
                                     CreatedOn = DateTime.Now,
                                     Deleted = false
                                 });

            SetLastModifiedDetails(user);
        }

        private void DetachPeopleAtRiskFromRiskAssessment(IEnumerable<PeopleAtRisk> peopleAtRisk, UserForAuditing user)
        {
            foreach (var personAtRisk in peopleAtRisk)
            {
                DetachPersonAtRiskFromRiskAssessment(personAtRisk, user);
            }
        }

        private void DetachPersonAtRiskFromRiskAssessment(PeopleAtRisk peopleAtRisk, UserForAuditing user)
        {
            if (PeopleAtRisk.Select(y => y.PeopleAtRisk).Count(x => x == peopleAtRisk) == 0)
            {
                throw new PersonAtRiskNotAttachedToRiskAssessmentException(Id, peopleAtRisk.Id);
            }

            //peopleAtRisk.SetLastModifiedDetails(user);
            //PeopleAtRisk.Remove(peopleAtRisk);
            var riskAssessmentPeopleAtRisk = PeopleAtRisk.Single(x => x.PeopleAtRisk == peopleAtRisk);
            riskAssessmentPeopleAtRisk.Deleted = true;
            riskAssessmentPeopleAtRisk.SetLastModifiedDetails(user);
            SetLastModifiedDetails(user);
        }

        public override string PreFix
        {
            get { return "GRA"; }
        }

        public override void MarkAsArchived(UserForAuditing user)
        {
            SetLastModifiedDetails(user);
            Status = RiskAssessmentStatus.Archived;

            foreach (var hazard in Hazards.Where(x => x.Deleted == false))
            {
                foreach (MultiHazardRiskAssessmentFurtherControlMeasureTask task in hazard.FurtherControlMeasureTasks.Where(x => x.TaskStatus == TaskStatus.Outstanding && x.Deleted == false))
                {
                    task.MarkAsNoLongerRequired(user);
                }
            }

            foreach (var review in Reviews)
            {
                if (review.RiskAssessmentReviewTask.TaskStatus == TaskStatus.Outstanding)
                    review.RiskAssessmentReviewTask.MarkAsNoLongerRequired(user);
            }
        }

        public override RiskAssessment Copy(string title, string reference, UserForAuditing user)
        {
            //TODO: when copying people at risk that are bespoke to this risk assessment, need to recreate it and add the new one
            //The same has been done for hazards so look at what has been done there. Once this is done, needs a script to go round 
            //cleaning up any existing entires.

            var clone = Create(title, reference, this.CompanyId, this.CreatedBy);
            clone.CreatedOn = DateTime.Now;
            clone.Location = this.Location;
            clone.TaskProcessDescription = this.TaskProcessDescription;
            clone.Status = RiskAssessmentStatus.Draft;
            clone.RiskAssessmentSite = this.RiskAssessmentSite;

            foreach (var employee in Employees)
            {
                clone.AttachEmployeeToRiskAssessment(employee.Employee, user);
            }

            foreach (var nonEmployee in NonEmployees)
            {
                clone.AttachNonEmployeeToRiskAssessment(nonEmployee.NonEmployee, user);
            }

            foreach (var personAtRisk in PeopleAtRisk)
            {
                clone.AttachPersonAtRiskToRiskAssessment(personAtRisk.PeopleAtRisk, user);
            }

            foreach (var hazard in Hazards)
            {
                var clonedHazard = hazard.CloneForRiskAssessmentTemplating(user, clone);
                clone.AttachClonedRiskAssessmentHazardToRiskAssessment(clonedHazard);
            }

            foreach (var document in Documents)
            {
                var clonedDocument = document.CloneForRiskAssessmentTemplating(user);
                clone.AttachDocumentToRiskAssessment(clonedDocument, user);
            }

            return clone;
        }

        public override bool HasAnyReviews()
        {
            if (Reviews.Any(x => x.Deleted == false))
            {
                return true;
            }
            return false;
        }

        public override IEnumerable<FurtherControlMeasureTask> GetAllUncompleteFurtherControlMeasureTasks()
        {
            var furtherControlMeasureTasks = new List<FurtherControlMeasureTask>();
            foreach (var hazard in Hazards.Where(x => x.Deleted == false))
            {
                foreach (MultiHazardRiskAssessmentFurtherControlMeasureTask task in hazard.FurtherControlMeasureTasks.Where(x => x.TaskStatus != TaskStatus.Completed && x.Deleted == false))
                {
                    furtherControlMeasureTasks.Add(task);
                }
            }

            return furtherControlMeasureTasks;
        }

        private void AttachClonedRiskAssessmentHazardToRiskAssessment(MultiHazardRiskAssessmentHazard clonedHazard)
        {
            if (!Hazards.Any(x => clonedHazard.Hazard.Id > 0 && x.Hazard.Id == clonedHazard.Hazard.Id))
            {
                Hazards.Add(clonedHazard);
            }
            
        }
    }
}