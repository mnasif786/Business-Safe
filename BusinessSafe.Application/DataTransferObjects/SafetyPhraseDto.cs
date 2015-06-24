using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class SafetyPhraseDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ReferenceNumber { get; set; }
        public HazardousSubstanceStandard HazardousSubstanceStandard { get; set; }
        public bool RequiresAdditionalInformation { get; set; }
    }
}
