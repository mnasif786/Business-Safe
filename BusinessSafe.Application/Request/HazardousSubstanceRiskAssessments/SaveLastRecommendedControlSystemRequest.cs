using System;

namespace BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments
{
    public class SaveLastRecommendedControlSystemRequest
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
        public long ControlSystemId { get; set; }
    }
}
