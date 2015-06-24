using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class RiskAssessmentPeopleAtRisk : Entity<long>
    {
        public virtual RiskAssessment RiskAssessment { get; set; }
        public virtual PeopleAtRisk PeopleAtRisk { get; set; }
    }
}
