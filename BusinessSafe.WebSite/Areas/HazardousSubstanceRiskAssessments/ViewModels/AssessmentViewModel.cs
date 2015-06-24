using System.Security.Principal;
using BusinessSafe.Application.Request.Attributes;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels
{
    public class AssessmentViewModel
    {
        public long CompanyId { get; set; }
        public long RiskAssessmentId{ get; set; }
        public string HazardGroup { get; set; }
        public Quantity? Quantity { get; set; }
        public MatterState? MatterState { get; set; }
        public DustinessOrVolatility? DustinessOrVolatility { get; set; }
        public string WorkApproach { get; set; }
        public string Url { get; set; }
        [BooleanRequired(ErrorMessage = "Please select if Health Surveillance is required")]
        public bool? HealthSurveillanceRequired { get; set; }
        public long ControlSystemId { get; set; }

        public bool IsSaveButtonEnabled(IPrincipal user)
        {
            return user.IsInRole(RiskAssessmentId == 0 ? Permissions.AddGeneralandHazardousSubstancesRiskAssessments.ToString() : Permissions.EditGeneralandHazardousSubstancesRiskAssessments.ToString());
        }
    }
}