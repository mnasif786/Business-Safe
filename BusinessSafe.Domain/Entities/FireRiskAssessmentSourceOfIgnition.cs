using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class FireRiskAssessmentSourceOfIgnition : Entity<long>
    {
        public virtual FireRiskAssessment FireRiskAssessment { get; set; }
        public virtual SourceOfIgnition SourceOfIgnition { get; set; }
    }
}
