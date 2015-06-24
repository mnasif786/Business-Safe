using System.Collections.Generic;
using System.Security.Principal;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels
{
    public class FireRiskAssessmentChecklistViewModel
    {
        public long RiskAssessmentId { get; set; }
        public long FireRiskAssessmentChecklistId { get; set; }
        public long CompanyId { get; set; }
        public List<SectionViewModel> Sections { get; set; }
        public bool IsValid { get; private set; }

        public FireRiskAssessmentChecklistViewModel()
        {
            IsValid = true;
        }

        public void MarkAsInvalid()
        {
            IsValid = false;
        }

        public bool IsSaveButtonEnabled(IPrincipal user)
        {
            return user.IsInRole(Permissions.EditGeneralandHazardousSubstancesRiskAssessments.ToString());
        }
    }
}