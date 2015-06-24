using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.ViewModels
{
    public class HazardsViewModel
    {
        public long CompanyId { get; set; }
        public long RiskAssessmentId { get; set; }
        public HtmlString DisplayWarningMessageForPeopleAtRisk { get; set; }
        public IEnumerable<LookupDto> Hazards { get;  set; }
        public IEnumerable<LookupDto> SelectedHazards { get; set; }
        public IEnumerable<LookupDto> PeopleAtRisk { get; set; }
        public IEnumerable<LookupDto> SelectedPeopleAtRisk { get; set; }
        public IEnumerable<LookupDto> FireSafetyControlMeasures { get; set; }
        public IEnumerable<LookupDto> SelectedFireSafetyControlMeasures { get; set; }
        public IEnumerable<LookupDto> SourceOfFuels { get; set; }
        public IEnumerable<LookupDto> SelectedSourceOfFuels { get; set; }
        public IEnumerable<LookupDto> SourceOfIgnitions { get; set; }
        public IEnumerable<LookupDto> SelectedSourceOfIgnitions { get; set; }

        public bool IsSaveButtonEnabled(IPrincipal user)
        {
            return user.IsInRole(RiskAssessmentId == 0 ? Permissions.AddGeneralandHazardousSubstancesRiskAssessments.ToString() : Permissions.EditGeneralandHazardousSubstancesRiskAssessments.ToString());
        }

        public HazardsViewModel()
        {
            Hazards = new List<LookupDto>();
            SelectedHazards = new List<LookupDto>();
            PeopleAtRisk = new List<LookupDto>();
            SelectedPeopleAtRisk = new List<LookupDto>();
            FireSafetyControlMeasures = new List<LookupDto>();
            SelectedFireSafetyControlMeasures = new List<LookupDto>();
            SourceOfFuels = new List<LookupDto>();
            SelectedSourceOfFuels = new List<LookupDto>();
            SourceOfIgnitions = new List<LookupDto>();
            SelectedSourceOfIgnitions = new List<LookupDto>();
        }
    }
}