using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class SafetyPhrase : Entity<long>
    {
        public virtual string Title { get; set; }
        public virtual string ReferenceNumber { get; set; }
        public virtual HazardousSubstanceStandard HazardousSubstanceStandard { get; set; }
        public virtual bool? RequiresAdditionalInformation { get; set; }
    }
}
