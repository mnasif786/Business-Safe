using System;
using System.Collections.Generic;
using BusinessSafe.Application.Request.Attributes;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Request.HazardousSubstanceInventory
{
    public class AddHazardousSubstanceRequest
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public DateTime SdsDate { get; set; }
        public string DetailsOfUse { get; set; }
        public long SupplierId { get; set; }
        public long[] PictogramIds { get; set; }
        public long[] RiskPhraseIds { get; set; }
        public long[] SafetyPhraseIds { get; set; }
        [EnumValueMustBeSet("Hazardous Substance Standard")]
        public HazardousSubstanceStandard HazardousSubstanceStandard { get; set; }
        
        [BooleanRequired(ErrorMessage = "Assessment required must be selected")]
        public bool? AssessmentRequired { get; set; }
        public bool? PreviousAssessmentRequired { get; set; }

        public List<SafetyPhraseAdditionalInformationRequest> AdditionalInformation { get; set; }

        public bool IsAssessmentRequired()
        {
            if (PreviousAssessmentRequired == false && AssessmentRequired.GetValueOrDefault())
                return true;

            return false;
        }

        public AddHazardousSubstanceRequest()
        {
            AdditionalInformation =new List<SafetyPhraseAdditionalInformationRequest>();
        }
    }

}