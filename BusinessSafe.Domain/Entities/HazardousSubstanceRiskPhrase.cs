using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class HazardousSubstanceRiskPhrase: Entity<long>
    {
        public virtual RiskPhrase RiskPhrase { get; set; }
        public virtual HazardousSubstance HazardousSubstance { get; set; }
        public virtual string AdditionalInformation { get; set; }
    }
}