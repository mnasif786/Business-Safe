using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class ChecklistGeneratorEmployee : Entity<long>
    {
        public virtual PersonalRiskAssessment PersonalRiskAssessment { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
