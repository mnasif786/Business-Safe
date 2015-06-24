using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Entities
{
    public class HazardousSubstance : Entity<long>
    {
        public virtual string Name { get; set; }
        public virtual string Reference { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual HazardousSubstanceStandard Standard { get; set; }
        public virtual DateTime SdsDate { get; set; }
        public virtual string DetailsOfUse { get; set; }
        public virtual bool AssessmentRequired { get; set; }
        public virtual long CompanyId { get; set; }
        
        public virtual IList<HazardousSubstancePictogram> HazardousSubstancePictograms { get; set; }
        public virtual IList<HazardousSubstanceRiskPhrase> HazardousSubstanceRiskPhrases { get; set; }
        public virtual IList<HazardousSubstanceSafetyPhrase> HazardousSubstanceSafetyPhrases { get; set; }
        public virtual IList<HazardousSubstanceRiskAssessment> HazardousSubstanceRiskAssessments { get; set; }

        public HazardousSubstance()
        {
            HazardousSubstanceRiskAssessments = new List<HazardousSubstanceRiskAssessment>();
            HazardousSubstanceSafetyPhrases = new List<HazardousSubstanceSafetyPhrase>();
            HazardousSubstanceRiskPhrases = new List<HazardousSubstanceRiskPhrase>();
            HazardousSubstancePictograms = new List<HazardousSubstancePictogram>();
        }

        public static HazardousSubstance Add(long companyId, UserForAuditing user, string name, string reference, Supplier supplier, DateTime sdsDate, IList<Pictogram> pictograms, IList<RiskPhrase> riskPhrases, IEnumerable<SafetyPhraseParameters> safetyPhrases, HazardousSubstanceStandard standard, string detailsOfUse, bool assessmentRequired)
        {
            var hazardousSubstance = new HazardousSubstance
            {
                Name = name,
                CreatedOn = DateTime.Now,
                CompanyId = companyId,
                CreatedBy = user,
                Reference = reference,
                SdsDate = sdsDate,
                DetailsOfUse = detailsOfUse,
                Supplier = supplier,
                Standard = standard,
                AssessmentRequired = assessmentRequired
            };

            hazardousSubstance.AddSafetyPhrases(safetyPhrases, user);
            hazardousSubstance.AddRiskPhrases(riskPhrases, user);
            hazardousSubstance.AddPictograms(pictograms, user);

            return hazardousSubstance;
        }

        public virtual void Update(UserForAuditing user, string name, string reference, Supplier supplier, DateTime sdsDate, IList<Pictogram> pictograms, IList<RiskPhrase> riskPhrases, IEnumerable<SafetyPhraseParameters> safetyPhrases, HazardousSubstanceStandard standard, string detailsOfUse, bool assessmentRequired)
        {
            Name = name;
            Reference = reference;
            Supplier = supplier;
            SdsDate = sdsDate;
            Standard = standard;
            DetailsOfUse = detailsOfUse;
            AssessmentRequired = assessmentRequired;

            // todo: refactor this to delegate the management of pictograms/phrases or genericise it somehow
            RemoveUnneededPictograms(GetPictogramsThatAreNoLongerRequired(pictograms), user);
            AddPictograms(pictograms, user);
            RemoveUnneededRiskPhrases(GetRiskPhrasesThatAreNoLongerRequired(riskPhrases), user);
            AddRiskPhrases(riskPhrases, user);
            RemoveUnneededSafetyPhrases(GetSafetyPhrasesThatAreNoLongerRequired(safetyPhrases), user);
            AddSafetyPhrases(safetyPhrases, user);

            SetLastModifiedDetails(user);
        }


        private List<Pictogram> GetPictogramsThatAreNoLongerRequired(IList<Pictogram> pictogramsToAdd )
        {
            if (pictogramsToAdd != null)
            {
                return HazardousSubstancePictograms.Select(hsp => hsp.Pictogram)
                    .ToList()
                    .Where(p => !pictogramsToAdd.Contains(p))
                    .ToList();
            }
            return HazardousSubstancePictograms.Select(x => x.Pictogram).ToList();
        }

        private void RemoveUnneededPictograms(ICollection<Pictogram> pictogramsToRemove, UserForAuditing user)
        {
            if (pictogramsToRemove == null || pictogramsToRemove.Count <= 0)
            {
                return;
            }

            foreach (var pictogram in pictogramsToRemove)
            {
                var hazardousSubstancePictogramToRemove = HazardousSubstancePictograms.FirstOrDefault(hsp => hsp.Pictogram.Id == pictogram.Id);
                if (hazardousSubstancePictogramToRemove != null)
                {
                    hazardousSubstancePictogramToRemove.MarkForDelete(user);
                }
            }
        }

        private void AddPictograms(IEnumerable<Pictogram> pictograms, UserForAuditing user)
        {

            if (pictograms == null)
            {
                return;
            }

            if (HazardousSubstancePictograms == null)
            {
                HazardousSubstancePictograms = new List<HazardousSubstancePictogram>();
            }

            pictograms.ToList()
                .ForEach(p =>
                {
                    if (!HazardousSubstancePictograms.Any(currentList => currentList.Pictogram.Id == p.Id))
                    {
                        HazardousSubstancePictograms.Add(HazardousSubstancePictogram.Create(this, p, user ));
                    }
                });
        }

        private void RemoveUnneededRiskPhrases(IEnumerable<RiskPhrase> riskPhrasesToRemove, UserForAuditing user)
        {
            if (riskPhrasesToRemove == null || !riskPhrasesToRemove.Any())
            {
                return;
            }

            foreach (var phrase in riskPhrasesToRemove)
            {
                var toRemove = HazardousSubstanceRiskPhrases.FirstOrDefault(hsp => hsp.RiskPhrase.Id == phrase.Id);
                if (toRemove != null)
                {
                    toRemove.MarkForDelete(user);
                }
            }
        }

        private void AddRiskPhrases(IEnumerable<RiskPhrase> riskPhrases, UserForAuditing user)
        {
            if (riskPhrases == null) return;

            foreach (var riskPhrase in riskPhrases)
            {
                if (HazardousSubstanceRiskPhrases.Select(x => x.RiskPhrase).All(y => y.Id != riskPhrase.Id))
                {
                    HazardousSubstanceRiskPhrases.Add(new HazardousSubstanceRiskPhrase()
                    {
                        HazardousSubstance = this,
                        RiskPhrase = riskPhrase,
                        //AdditionalInformation = safetyPhraseParameterAdding.Information,
                        CreatedBy = user,
                        CreatedOn = DateTime.Now
                    });
                }
            }
        }

        private IEnumerable<RiskPhrase> GetRiskPhrasesThatAreNoLongerRequired(IEnumerable<RiskPhrase> riskPhrasesBeingAdded)
        {
            if (riskPhrasesBeingAdded != null)
            {
                return HazardousSubstanceRiskPhrases.Select(x => x.RiskPhrase)
                    .Where(y => !riskPhrasesBeingAdded.Contains(y))
                    .ToList();
            }
            return HazardousSubstanceRiskPhrases.Select(x => x.RiskPhrase).ToList();
        }


        private List<SafetyPhrase> GetSafetyPhrasesThatAreNoLongerRequired(IEnumerable<SafetyPhraseParameters> safetyPhrasesBeingAdded)
        {
            if (safetyPhrasesBeingAdded != null)
            {
                return HazardousSubstanceSafetyPhrases.Select(x => x.SafetyPhrase)
                    .ToList()
                    .Where(y => !safetyPhrasesBeingAdded.Select(z => z.Phrase).Contains(y))
                    .ToList();
            }
            return HazardousSubstanceSafetyPhrases.Select(x => x.SafetyPhrase).ToList();
        }

        private void RemoveUnneededSafetyPhrases(ICollection<SafetyPhrase> safetyPhrasesToRemove, UserForAuditing user)
        {
            if (safetyPhrasesToRemove == null || safetyPhrasesToRemove.Count <= 0)
            {
                return;
            }

            foreach (var phrase in safetyPhrasesToRemove)
            {
                var toRemove = HazardousSubstanceSafetyPhrases.First(hsp => hsp.SafetyPhrase.Id == phrase.Id);
                toRemove.MarkForDelete(user);
            }
        }

        private void AddSafetyPhrases(IEnumerable<SafetyPhraseParameters> safetyPhraseParametersAdding, UserForAuditing user)
        {
            if (safetyPhraseParametersAdding == null) return;

            foreach (var safetyPhraseParameterAdding in safetyPhraseParametersAdding)
            {
                if (!HazardousSubstanceSafetyPhrases.Select(x => x.SafetyPhrase).Any(y => y.Id == safetyPhraseParameterAdding.Phrase.Id))
                {
                    HazardousSubstanceSafetyPhrases.Add(new HazardousSubstanceSafetyPhrase()
                                                        {
                                                            HazardousSubstance = this,
                                                            SafetyPhrase = safetyPhraseParameterAdding.Phrase,
                                                            AdditionalInformation = safetyPhraseParameterAdding.Information,
                                                            CreatedBy = user,
                                                            CreatedOn = DateTime.Now
                                                        });
                }
            }
        }


    }
}