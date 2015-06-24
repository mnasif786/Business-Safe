using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class HazardousSubstanceSafetyPhrase : Entity<long>
    {
        public virtual SafetyPhrase SafetyPhrase { get; set; }
        public virtual HazardousSubstance HazardousSubstance { get; set; }
        public virtual string AdditionalInformation { get; set; }
    }
}