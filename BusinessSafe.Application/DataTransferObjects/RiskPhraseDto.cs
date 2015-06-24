using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class RiskPhraseDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ReferenceNumber { get; set; }
        public HazardousSubstanceStandard HazardousSubstanceStandard { get; set; }
    }
}
