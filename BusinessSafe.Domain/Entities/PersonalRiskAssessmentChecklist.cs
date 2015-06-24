using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class PersonalRiskAssessmentChecklist : Entity<long>
    {
        public virtual PersonalRiskAssessment PersonalRiskAssessment { get; set; }
        public virtual Checklist Checklist { get; set; }
    }
}
