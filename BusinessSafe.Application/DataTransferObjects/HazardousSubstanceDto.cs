using BusinessSafe.Domain.Entities;
using System;
using System.Collections.Generic;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class HazardousSubstanceDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public SupplierDto Supplier { get; set; }
        public DateTime SdsDate { get; set; }
        public HazardousSubstanceStandard Standard { get; set; }
        public IList<PictogramDto> Pictograms { get; set; }
        public IList<RiskPhraseDto> RiskPhrases { get; set; }
        public IEnumerable<HazardousSubstanceSafetyPhraseDto> HazardousSubstanceSafetyPhrases { get; set; }
        public string DetailsOfUse { get; set; }
        public bool AssessmentRequired { get; set; }
        public long CompanyId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public IEnumerable<HazardousSubstanceLinkedRiskAssessmentDto> LinkedRiskAsessments { get; set; }

        public HazardousSubstanceDto()
        {
            Standard = HazardousSubstanceStandard.Global;
            Pictograms = new List<PictogramDto>();
            RiskPhrases = new List<RiskPhraseDto>();
            HazardousSubstanceSafetyPhrases = new List<HazardousSubstanceSafetyPhraseDto>();
            LinkedRiskAsessments = new List<HazardousSubstanceLinkedRiskAssessmentDto>();
        }
    }
}