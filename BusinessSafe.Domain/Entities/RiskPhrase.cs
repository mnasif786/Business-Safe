using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class RiskPhrase : Entity<long>
    {
        public virtual string Title { get; set; }
        public virtual string ReferenceNumber { get; set; }
        public virtual HazardousSubstanceStandard HazardousSubstanceStandard { get; set; }
        public virtual HazardousSubstanceGroup Group { get; set; }
    }
}