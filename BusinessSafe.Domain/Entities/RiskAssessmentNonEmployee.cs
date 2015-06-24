using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class RiskAssessmentNonEmployee : Entity<long>
    {
        public virtual RiskAssessment RiskAssessment { get; set; }
        public virtual NonEmployee NonEmployee { get; set; }
    }
}
