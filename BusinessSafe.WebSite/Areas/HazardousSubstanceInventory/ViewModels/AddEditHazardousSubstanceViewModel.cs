using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.ViewModels
{
    public class AddEditHazardousSubstanceViewModel
    {
        public AddEditHazardousSubstanceViewModel()
        {
            SelectedHazardousSubstanceSymbols = new List<long>();
            SelectedRiskPhrases = new List<long>();
            SelectedSafetyPhrases = new List<long>();
            SafetyPhrasesAdditionalInformation = new List<SafetyPhraseAdditionalInformationViewModel>();
        }

        public long Id { get; set; }
        public long CompanyId { get; set; }

        [Required(ErrorMessage = "Please enter a substance name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter details of use")]
        [StringLength(500)]
        public string DetailsOfUse { get; set; }
        
        
        public bool? AssessmentRequired { get; set; }

        public string Reference { get; set; }
        public long SupplierId { get; set; }

        [Required(ErrorMessage = "Please enter a date")]
        [DataType(DataType.Date)]
        public DateTime? SdsDate { get; set; }

        public HazardousSubstanceStandard HazardousSubstanceStandard { get; set; }

        public List<long> SelectedHazardousSubstanceSymbols { get; set; }
        public List<long> SelectedRiskPhrases { get; set; }
        public List<long> SelectedSafetyPhrases { get; set; }
        public IEnumerable<PictogramDto> Pictograms { get; set; }
        public IEnumerable<RiskPhraseDto> RiskPhrases { get; set; }
        public IEnumerable<SafetyPhraseDto> SafetyPhrases { get; set; }
        public IEnumerable<AutoCompleteViewModel> Suppliers { get; set; }
        public bool HasLinkedRiskAssessments { get; set; }
        public IEnumerable<SafetyPhraseAdditionalInformationViewModel> SafetyPhrasesAdditionalInformation { get; set; }
        public string SupplierName { get; set; }

        public string GetSdsDate()
        {
           return SdsDate.HasValue ? SdsDate.Value.ToShortDateString() : string.Empty;            
        }

        public bool ShowAdditionalInformation()
        {
            return SafetyPhrasesAdditionalInformation.Any();
        }

        public bool IsSaveButtonEnabled(IPrincipal user)
        {
            return user.IsInRole(Id == 0 ? Permissions.AddHazardousSubstanceInventory.ToString() : Permissions.EditHazardousSubstanceInventory.ToString());
        }
    }
}