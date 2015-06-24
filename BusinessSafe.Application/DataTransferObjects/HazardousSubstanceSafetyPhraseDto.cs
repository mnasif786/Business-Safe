namespace BusinessSafe.Application.DataTransferObjects
{
    public class HazardousSubstanceSafetyPhraseDto
    {
        public long Id { get; set; }
        public SafetyPhraseDto SafetyPhase { get; set; }
        public string AdditionalInformation { get; set; }
    }
}