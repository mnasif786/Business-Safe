using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Entities
{
    public class FireRiskAssessment : RiskAssessment
    {
        public virtual string Location { get; set; }
        public virtual string BuildingUse { get; set; }
        public virtual string ElectricityEmergencyShutOff { get; set; }
        public virtual string GasEmergencyShutOff { get; set; }
        public virtual string WaterEmergencyShutOff { get; set; }
        public virtual string OtherEmergencyShutOff { get; set; }
        public virtual string PersonAppointed { get; set; }
        public virtual bool? PremisesProvidesSleepingAccommodation { get; set; }
        public virtual bool? PremisesProvidesSleepingAccommodationConfirmed { get; set; }
        public virtual int? NumberOfFloors { get; set; }
        public virtual int? NumberOfPeople { get; set; }
        public virtual IList<RiskAssessmentPeopleAtRisk> PeopleAtRisk { get; set; }
        public virtual IList<FireRiskAssessmentControlMeasure> FireSafetyControlMeasures { get; set; }
        public virtual IList<FireRiskAssessmentChecklist> FireRiskAssessmentChecklists { get; set; }
        public virtual IList<FireRiskAssessmentSourceOfIgnition> FireRiskAssessmentSourcesOfIgnition { get; set; }
        public virtual IList<FireRiskAssessmentSourceOfFuel> FireRiskAssessmentSourcesOfFuel { get; set; }

        public FireRiskAssessment()
        {
            PeopleAtRisk = new List<RiskAssessmentPeopleAtRisk>();
            FireSafetyControlMeasures = new List<FireRiskAssessmentControlMeasure>();
            FireRiskAssessmentSourcesOfIgnition = new List<FireRiskAssessmentSourceOfIgnition>();
            FireRiskAssessmentSourcesOfFuel = new List<FireRiskAssessmentSourceOfFuel>();
            FireRiskAssessmentChecklists = new List<FireRiskAssessmentChecklist>();
        }

        public override string PreFix
        {
            get { return "FRA"; }
        }

        public virtual FireRiskAssessmentChecklist LatestFireRiskAssessmentChecklist
        {
            get
            {
                if (FireRiskAssessmentChecklists == null || FireRiskAssessmentChecklists.Count == 0) return null;

                return FireRiskAssessmentChecklists
                    .OrderBy(fireRiskAssessmentChecklist => fireRiskAssessmentChecklist.CreatedOn)
                    .Last();
            }
        }

        public override bool HasUndeletedTasks()
        {
            // TODO: What should the logic be here for the fire risk assessment has undeleted tasks

            if (Reviews != null)
            {
                foreach (var review in Reviews.Where(x => x.Deleted == false))
                {
                    if (!review.RiskAssessmentReviewTask.Deleted)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override bool HasUncompletedTasks()
        {
            // TODO: What should the logic be here for the fire risk assessment has uncompleted tasks
            return false;
        }

        public static FireRiskAssessment Create(string title, string reference, long companyId, Checklist checklist, UserForAuditing currentUser)
        {
            var now = DateTime.Now;

            var fireRiskAssessment = new FireRiskAssessment
            {
                CompanyId = companyId,
                Reference = reference,
                Title = title,
                CreatedBy = currentUser,
                CreatedOn = now,
                Status = RiskAssessmentStatus.Draft
            };

            var fireRiskAssessmentChecklist = new FireRiskAssessmentChecklist
                                                  {
                                                      Checklist = checklist,
                                                      FireRiskAssessment = fireRiskAssessment,
                                                      CreatedBy = currentUser,
                                                      CreatedOn = now
                                                  };

            fireRiskAssessment.FireRiskAssessmentChecklists.Add(fireRiskAssessmentChecklist);
            return fireRiskAssessment;
        }

        public virtual void UpdateSummary(string title, string reference, string personAppointed, DateTime? assessmentDate, RiskAssessor riskAssessor, Site site, UserForAuditing user)
        {
            if (IsDifferentRiskAssessor(riskAssessor))
            {
                if (AreThereAnyFurtherControlMeasureTasks())
                {
                    LatestFireRiskAssessmentChecklist.SignificantFindings
                        .Where(finding => finding.FurtherControlMeasureTasks != null)
                        .SelectMany(finding => finding.FurtherControlMeasureTasks)
                        .ToList()
                        .ForEach(task =>
                        {
                            task.SendTaskCompletedNotification = riskAssessor == null ? true : !riskAssessor.DoNotSendTaskCompletedNotifications;
                            task.SendTaskOverdueNotification = riskAssessor == null ? true : !riskAssessor.DoNotSendTaskOverdueNotifications;
                            task.SetLastModifiedDetails(user);
                        });
                }
            }

            Title = title;
            Reference = reference;
            RiskAssessor = riskAssessor;
            AssessmentDate = assessmentDate;
            PersonAppointed = personAppointed;
            RiskAssessmentSite = site;
            SetLastModifiedDetails(user);

        }

        private bool AreThereAnyFurtherControlMeasureTasks()
        {
            return LatestFireRiskAssessmentChecklist != null
                   && LatestFireRiskAssessmentChecklist.SignificantFindings != null && LatestFireRiskAssessmentChecklist.SignificantFindings.Any()
                   && LatestFireRiskAssessmentChecklist.SignificantFindings.Any(x => x.FurtherControlMeasureTasks != null);
        }

        public virtual void UpdatePremisesInformation(bool? premisesProvidesSleepingAccommodation, bool? premisesProvidesSleepingAccommodationConfirmed, string location, string buildingUse, int? numberOfFloors, int? numberOfPeople, EmergencyShutOffParameters emergencyShutOffParameters, UserForAuditing currentUser)
        {
            PremisesProvidesSleepingAccommodation = premisesProvidesSleepingAccommodation;
            PremisesProvidesSleepingAccommodationConfirmed = premisesProvidesSleepingAccommodationConfirmed;
            Location = location;
            BuildingUse = buildingUse;
            NumberOfFloors = numberOfFloors;
            NumberOfPeople = numberOfPeople;
            ElectricityEmergencyShutOff = emergencyShutOffParameters.ElectricityEmergencyShutOff;
            GasEmergencyShutOff = emergencyShutOffParameters.GasEmergencyShutOff;
            WaterEmergencyShutOff = emergencyShutOffParameters.WaterEmergencyShutOff;
            OtherEmergencyShutOff = emergencyShutOffParameters.OtherEmergencyShutOff;
            SetLastModifiedDetails(currentUser);
        }

        public virtual void CompleteFireRiskAssessmentChecklist(IList<SubmitFireAnswerParameters> answerParameterClasses, UserForAuditing submittingUser)
        {
            LatestFireRiskAssessmentChecklist.Complete(answerParameterClasses, submittingUser);
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

        public virtual void SaveFireRiskAssessmentChecklist(IList<SubmitFireAnswerParameters> answerParameterClasses, UserForAuditing currentUser)
        {
            LatestFireRiskAssessmentChecklist.Save(answerParameterClasses, currentUser);
            SetLastModifiedDetails(currentUser);
        }

        private IEnumerable<PeopleAtRisk> GetExistingPeopleAtRiskToDetach(IEnumerable<PeopleAtRisk> peopleAtRisk)
        {
            var peopleAtRiskToDetach = new List<PeopleAtRisk>();

            foreach (var riskAssessmentPeopleAtRisk in PeopleAtRisk)
            {
                //if (!peopleAtRisk.Contains(riskAssessmentPeopleAtRisk.PeopleAtRisk))
                if (!peopleAtRisk.Contains(riskAssessmentPeopleAtRisk.PeopleAtRisk))
                {
                    peopleAtRiskToDetach.Add(riskAssessmentPeopleAtRisk.PeopleAtRisk);
                }
            }

            return peopleAtRiskToDetach;
        }

        private IEnumerable<PeopleAtRisk> GetPeopleAtRiskNeedToAttach(IEnumerable<PeopleAtRisk> peopleAtRisk)
        {
            var peopleAtRiskToAttach = new List<PeopleAtRisk>();

            foreach (var currentPeopleAtRisk in peopleAtRisk)
            {
                if (!PeopleAtRisk.Select(x => x.PeopleAtRisk).Contains(currentPeopleAtRisk))
                {
                    peopleAtRiskToAttach.Add(currentPeopleAtRisk);
                }
            }

            return peopleAtRiskToAttach;
        }

        private bool PersonAtRiskNotAttachedToRiskAssessment(PeopleAtRisk peopleAtRisk)
        {
            return PeopleAtRisk.Count(x => x.Id == peopleAtRisk.Id) == 0;
        }

        public virtual void AttachPersonAtRiskToRiskAssessment(PeopleAtRisk peopleAtRisk, UserForAuditing user)
        {
            if (PeopleAtRisk.Count(x=> x.PeopleAtRisk.Id == peopleAtRisk.Id) > 0)
            {
                throw new PersonAtRiskAlreadyAttachedToRiskAssessmentException(Id, peopleAtRisk.Id);
            }

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
            if (PeopleAtRisk.Count(x => x.PeopleAtRisk == peopleAtRisk) == 0)
            {
                throw new PersonAtRiskNotAttachedToRiskAssessmentException(Id, peopleAtRisk.Id);
            }

            var riskAssessmentPeopleAtRisk = PeopleAtRisk.Single(x => x.PeopleAtRisk == peopleAtRisk);
            riskAssessmentPeopleAtRisk.Deleted = true;
            riskAssessmentPeopleAtRisk.LastModifiedBy = user;
            riskAssessmentPeopleAtRisk.LastModifiedOn = DateTime.Now;
            SetLastModifiedDetails(user);
        }

        public virtual void AttachFireSafetyControlMeasuresToRiskAssessment(IEnumerable<FireSafetyControlMeasure> fireSafetyControlMeasures, UserForAuditing user)
        {
            var existingPeopleAtRiskNeedToDetach = GetExistingFireSafetyControlMeasuresToDetach(fireSafetyControlMeasures);
            DetachFireSafetyControlMeasuresFromRiskAssessment(existingPeopleAtRiskNeedToDetach, user);

            var fireSafetyControlMeasuresToAttach = GetFireSafetyControlMeasuresNeedToAttach(fireSafetyControlMeasures);
            foreach (var fireSafetyControlMeasure in fireSafetyControlMeasuresToAttach)
            {
                AttachFireSafetyControlMeasureToRiskAssessment(fireSafetyControlMeasure, user);
            }
        }

        private IEnumerable<FireSafetyControlMeasure> GetFireSafetyControlMeasuresNeedToAttach(IEnumerable<FireSafetyControlMeasure> fireSafetyControlMeasures)
        {
            return fireSafetyControlMeasures.Where(FireSafetyControlMeasureNotAttachedToRiskAssessment);
        }

        private bool FireSafetyControlMeasureNotAttachedToRiskAssessment(FireSafetyControlMeasure fireSafetyControlMeasure)
        {
            return FireSafetyControlMeasures.Count(x => x.FireSafetyControlMeasure == fireSafetyControlMeasure) == 0;
        }

        private IEnumerable<FireSafetyControlMeasure> GetExistingFireSafetyControlMeasuresToDetach(IEnumerable<FireSafetyControlMeasure> fireSafetyControlMeasures)
        {
            return FireSafetyControlMeasures
                .Where(fireSafetyControlMeasure => fireSafetyControlMeasures.Count(x => x == fireSafetyControlMeasure.FireSafetyControlMeasure) == 0)
                .Select(x => x.FireSafetyControlMeasure)
                .ToList();
        }

        private void DetachFireSafetyControlMeasuresFromRiskAssessment(IEnumerable<FireSafetyControlMeasure> fireSafetyControlMeasures, UserForAuditing user)
        {
            foreach (var fireSafetyControlMeasure in fireSafetyControlMeasures)
            {
                DetachFireSafetyControlMeasureFromRiskAssessment(fireSafetyControlMeasure, user);
            }
        }

        private void DetachFireSafetyControlMeasureFromRiskAssessment(FireSafetyControlMeasure fireSafetyControlMeasure, UserForAuditing user)
        {
            if (FireSafetyControlMeasures.Count(x => x.FireSafetyControlMeasure == fireSafetyControlMeasure) == 0)
            {
                throw new FireSafetyControlMeasureNotAttachedToRiskAssessmentException(Id, fireSafetyControlMeasure.Id);
            }
            var fireRiskAssessmentControlMeasure =
                FireSafetyControlMeasures.Single(x => x.FireSafetyControlMeasure == fireSafetyControlMeasure);

            fireRiskAssessmentControlMeasure.MarkForDelete(user);
            SetLastModifiedDetails(user);
        }

        private void AttachFireSafetyControlMeasureToRiskAssessment(FireSafetyControlMeasure fireSafetyControlMeasure, UserForAuditing user)
        {
            if (!FireSafetyControlMeasures.Any(x => x.FireSafetyControlMeasure.Id == fireSafetyControlMeasure.Id))
            {
                FireSafetyControlMeasures.Add(new FireRiskAssessmentControlMeasure
                {
                    FireSafetyControlMeasure = fireSafetyControlMeasure,
                    RiskAssessment = this,
                    CreatedBy = user,
                    CreatedOn = DateTime.Now,
                    Deleted = false
                });

                SetLastModifiedDetails(user);
            }

        }

        public virtual void AttachSourceOfIgnitionsToRiskAssessment(IEnumerable<SourceOfIgnition> sourceOfIgnitions, UserForAuditing user)
        {
            var existingSourcesOfIgnitionNeedToDetach = GetExistingSourcesOfIgnitionToDetach(sourceOfIgnitions);
            DetachSourcesOfIgnitionFromRiskAssessment(existingSourcesOfIgnitionNeedToDetach, user);

            var sourcesOfIgnitionNeedToAttach = GetSourcesOfIgnitionNeedToAttach(sourceOfIgnitions);
            foreach (var sourceOfIgnition in sourcesOfIgnitionNeedToAttach)
            {
                AttachSourceOfIgnitionToRiskAssessment(sourceOfIgnition, user);
            }
        }

        private IEnumerable<SourceOfIgnition> GetExistingSourcesOfIgnitionToDetach(IEnumerable<SourceOfIgnition> sourceOfIgnitions)
        {
            return FireRiskAssessmentSourcesOfIgnition
                .Where(exisitingFireRiskAssessmentSourceOfIgnition => sourceOfIgnitions.Count(x => x == exisitingFireRiskAssessmentSourceOfIgnition.SourceOfIgnition) == 0)
                .Select(x => x.SourceOfIgnition)
                .ToList();
        }

        private void DetachSourcesOfIgnitionFromRiskAssessment(IEnumerable<SourceOfIgnition> sourceOfIgnitions, UserForAuditing user)
        {
            foreach (var sourceOfIgnition in sourceOfIgnitions)
            {
                DetachSourceOfIgnitionFromRiskAssessment(sourceOfIgnition, user);
            }
        }

        private void DetachSourceOfIgnitionFromRiskAssessment(SourceOfIgnition sourceOfIgnition, UserForAuditing user)
        {
            if (FireRiskAssessmentSourcesOfIgnition.Count(x => x.SourceOfIgnition == sourceOfIgnition) == 0)
            {
                throw new SourceOfIgnitionNotAttachedToRiskAssessmentException(Id, sourceOfIgnition.Id);
            }

            var fireRiskAssessmentSourceOfIgnition =
                FireRiskAssessmentSourcesOfIgnition.Single(x => x.SourceOfIgnition == sourceOfIgnition);

            fireRiskAssessmentSourceOfIgnition.MarkForDelete(user);
            SetLastModifiedDetails(user);
        }

        private IEnumerable<SourceOfIgnition> GetSourcesOfIgnitionNeedToAttach(IEnumerable<SourceOfIgnition> sourceOfIgnitions)
        {
            return sourceOfIgnitions.Where(SourceOfIgnitionNotAttachedToRiskAssessment);
        }

        private bool SourceOfIgnitionNotAttachedToRiskAssessment(SourceOfIgnition sourceOfIgnition)
        {
            return FireRiskAssessmentSourcesOfIgnition.Count(x => x.SourceOfIgnition == sourceOfIgnition) == 0;
        }

        private void AttachSourceOfIgnitionToRiskAssessment(SourceOfIgnition sourceOfIgnition, UserForAuditing user)
        {
            if (FireRiskAssessmentSourcesOfIgnition.Count(x => x.SourceOfIgnition == sourceOfIgnition) > 0)
            {
                throw new SourceOfIgnitionAlreadyAttachedToRiskAssessmentException(Id, sourceOfIgnition.Id);
            }

            FireRiskAssessmentSourcesOfIgnition.Add(new FireRiskAssessmentSourceOfIgnition
                                                        {
                                                            SourceOfIgnition = sourceOfIgnition,
                                                            FireRiskAssessment = this,
                                                            CreatedBy = user,
                                                            CreatedOn = DateTime.Now
                                                        });

            SetLastModifiedDetails(user);
        }

        public virtual void AttachSourceOfFuelsToRiskAssessment(IEnumerable<SourceOfFuel> sourceOfFuels, UserForAuditing user)
        {
            var existingSourcesOfFuelNeedToDetach = GetExistingSourcesOfFuelToDetach(sourceOfFuels);
            DetachSourcesOfFuelFromRiskAssessment(existingSourcesOfFuelNeedToDetach, user);

            var sourcesOfFuelNeedToAttach = GetSourcesOfFuelNeedToAttach(sourceOfFuels);
            foreach (var sourceOfFuel in sourcesOfFuelNeedToAttach)
            {
                AttachSourceOfFuelToRiskAssessment(sourceOfFuel, user);
            }
        }

        private IEnumerable<SourceOfFuel> GetExistingSourcesOfFuelToDetach(IEnumerable<SourceOfFuel> sourceOfFuel)
        {
            return FireRiskAssessmentSourcesOfFuel
                .Where(exisitingFireRiskAssessmentSourceOfFuel => sourceOfFuel.Count(x => x == exisitingFireRiskAssessmentSourceOfFuel.SourceOfFuel) == 0)
                .Select(x => x.SourceOfFuel)
                .ToList();
        }

        private void DetachSourcesOfFuelFromRiskAssessment(IEnumerable<SourceOfFuel> sourceOfFuels, UserForAuditing user)
        {
            foreach (var sourceOfFuel in sourceOfFuels)
            {
                DetachSourceOfFuelFromRiskAssessment(sourceOfFuel, user);
            }
        }

        private void DetachSourceOfFuelFromRiskAssessment(SourceOfFuel sourceOfFuel, UserForAuditing user)
        {
            if (FireRiskAssessmentSourcesOfFuel.Count(x => x.SourceOfFuel == sourceOfFuel) == 0)
            {
                throw new SourceOfFuelNotAttachedToRiskAssessmentException(Id, sourceOfFuel.Id);
            }

            var fireRiskAssessmentSourceOfFuel =
                FireRiskAssessmentSourcesOfFuel.Single(x => x.SourceOfFuel == sourceOfFuel);

            fireRiskAssessmentSourceOfFuel.MarkForDelete(user);
            SetLastModifiedDetails(user);
        }

        private IEnumerable<SourceOfFuel> GetSourcesOfFuelNeedToAttach(IEnumerable<SourceOfFuel> sourceOfFuels)
        {
            return sourceOfFuels.Where(SourceOfIgnitionNotAttachedToRiskAssessment);
        }

        private bool SourceOfIgnitionNotAttachedToRiskAssessment(SourceOfFuel sourceOfFuel)
        {
            return FireRiskAssessmentSourcesOfFuel.Count(x => x.SourceOfFuel == sourceOfFuel) == 0;
        }

        private void AttachSourceOfFuelToRiskAssessment(SourceOfFuel sourceOfFuel, UserForAuditing user)
        {
            if (FireRiskAssessmentSourcesOfFuel.Count(x => x.SourceOfFuel == sourceOfFuel) > 0)
            {
                throw new SourceOfIgnitionAlreadyAttachedToRiskAssessmentException(Id, sourceOfFuel.Id);
            }

            FireRiskAssessmentSourcesOfFuel.Add(new FireRiskAssessmentSourceOfFuel
            {
                SourceOfFuel = sourceOfFuel,
                FireRiskAssessment = this,
                CreatedBy = user,
                CreatedOn = DateTime.Now
            });

            SetLastModifiedDetails(user);
        }

        public override RiskAssessment Copy(string newTitle, string newReference, UserForAuditing user)
        {
            var clone = FireRiskAssessment.Create(newTitle, newReference, CompanyId, null, user);

            //summary information
            clone.AssessmentDate = AssessmentDate;
            clone.RiskAssessor = RiskAssessor;
            clone.PersonAppointed = PersonAppointed;

            //copy hazards
            FireSafetyControlMeasures.ToList()
                .ForEach(x => clone.FireSafetyControlMeasures.Add(x));

            PeopleAtRisk.ToList()
                .ForEach(x => clone.PeopleAtRisk.Add(x));

            FireRiskAssessmentSourcesOfFuel.ToList()
                .ForEach(x => clone.FireRiskAssessmentSourcesOfFuel.Add(x));

            FireRiskAssessmentSourcesOfIgnition.ToList()
                .ForEach(x => clone.FireRiskAssessmentSourcesOfIgnition.Add(x));

            clone.Documents = Documents.Select(x =>
            {
                var clonedDocument = x.CloneForRiskAssessmentTemplating(user);
                clonedDocument.RiskAssessment = clone;
                return clonedDocument;
            }).ToList();

            //Copy latest FRA checklists
            clone.FireRiskAssessmentChecklists.Clear();
            clone.FireRiskAssessmentChecklists.Add(LatestFireRiskAssessmentChecklist.CopyWithYesAnswers(user));

            return clone;
        }

        public virtual FireRiskAssessment Copy(UserForAuditing user)
        {
            return Copy(Title, Reference, user) as FireRiskAssessment;
        }

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
            return new List<FurtherControlMeasureTask>();
        }

        protected virtual DateTime? NextFurtherControlMeasureTaskCompletionDueDate
        {
            get
            {
                if (LatestFireRiskAssessmentChecklist == null)
                    return null;
                return LatestFireRiskAssessmentChecklist
                    .SignificantFindings.Where(sf => sf.Deleted == false)
                    .SelectMany(sf => sf.FurtherControlMeasureTasks)
                    .Where(task => task.Deleted == false && task.TaskStatus != TaskStatus.Completed && task.TaskStatus != TaskStatus.NoLongerRequired).Min(task => task.TaskCompletionDueDate);
            }
        }

    }
}
