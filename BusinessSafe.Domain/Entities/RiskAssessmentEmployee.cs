using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class RiskAssessmentEmployee : Entity<long>
    {
        public virtual RiskAssessment RiskAssessment { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
