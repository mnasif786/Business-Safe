using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.ViewModels
{
    public class DocumentsViewModel
    {
        public long CompanyId { get; set; }
        public long RiskAssessmentId { get; set; }
        public ExistingDocumentsViewModel ExistingDocumentsViewModel { get; set; }
        public IEnumerable<HtmlString> DocumentDisplayMessages { get; set; }

        public bool IsSaveButtonEnabled(IPrincipal user)
        {
            return user.IsInRole(RiskAssessmentId == 0 ? Permissions.AddGeneralandHazardousSubstancesRiskAssessments.ToString() : Permissions.EditGeneralandHazardousSubstancesRiskAssessments.ToString());
        }
    }
}