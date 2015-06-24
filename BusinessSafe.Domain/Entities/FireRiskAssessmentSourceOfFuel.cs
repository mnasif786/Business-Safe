using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class FireRiskAssessmentSourceOfFuel : Entity<long>
    {
        public virtual FireRiskAssessment FireRiskAssessment { get; set; }
        public virtual SourceOfFuel SourceOfFuel { get; set; }
    }
}
