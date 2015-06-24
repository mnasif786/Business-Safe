using BusinessSafe.WebSite.ViewModels;
using System.Security.Principal;

namespace BusinessSafe.WebSite.Factories
{
    public interface IRiskAssessmentReviewsViewModelFactory
    {
        IRiskAssessmentReviewsViewModelFactory WithCompanyId(long companyId);
        IRiskAssessmentReviewsViewModelFactory WithRiskAssessmentId(long riskAssessmentId);
        IRiskAssessmentReviewsViewModelFactory WithUser(IPrincipal user);
        RiskAssessmentReviewsViewModel GetViewModel();
    }
}