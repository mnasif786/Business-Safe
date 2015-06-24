using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.ViewModels
{
    public class InventorySubstanceViewModel
    {
        public long Id;
        public string Name { get; set; }
        public string Reference { get; set; }
        public string Supplier { get; set; }
        public string SdsDate { get; set; }
        public string Standard { get; set; }
        public string DetailsOfUse { get; set; }
        public bool AssessmentRequired { get; set; }
        public long CompanyId { get; set; }
        public string CreatedOn { get; set; }
        public string RiskPhraseReferences { get; set; }
        public string SafetyPhraseReferences { get; set; }
        public IEnumerable<long> LinkedRiskAssessmentsIds { get; set; }

        public InventorySubstanceViewModel()
        {
            LinkedRiskAssessmentsIds = new long[] { };
        }

        public bool HasLinkedRiskAssessments
        {
            get
            {
                return LinkedRiskAssessmentsIds.Any();
            }
        }

        public bool HasOneLinkredRiskAssessment
        {
            get
            {
                return LinkedRiskAssessmentsIds.Count() == 1;
            }
        }
        
        public string GetRiskAssessmentId()
        {
            if (LinkedRiskAssessmentsIds.Count() == 1)
            {
                return LinkedRiskAssessmentsIds.First().ToString();
            }

            throw new SystemException("Should not call GetRiskAssessmentId if got more than one risk assessment linked to hazardous substance");
            
        }
    }
}