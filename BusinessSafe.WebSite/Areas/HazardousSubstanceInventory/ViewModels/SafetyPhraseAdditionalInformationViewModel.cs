using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.ViewModels
{
    public class SafetyPhraseAdditionalInformationViewModel
    {
        public string ReferenceNumber { get; set; }
        [StringLength(200, ErrorMessage = "Max 200 Characters")]
        public string AdditionalInformation { get; set; }
        public long SafetyPhaseId { get; set; }
    }
}