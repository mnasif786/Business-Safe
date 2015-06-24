namespace BusinessSafe.Application.Request.HazardousSubstanceInventory
{
    public class SafetyPhraseAdditionalInformationRequest
    {
        public string ReferenceNumber { get; set; }
        public long SafetyPhaseId { get; set; }
        public string AdditionalInformation { get; set; }
    }
}