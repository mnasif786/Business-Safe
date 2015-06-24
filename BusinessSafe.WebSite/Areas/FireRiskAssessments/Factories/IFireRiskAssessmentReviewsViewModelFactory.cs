using System.Security.Principal;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories
{
    public interface IFireRiskAssessmentReviewsViewModelFactory
    {
        IFireRiskAssessmentReviewsViewModelFactory WithCompanyId(long companyId);
        IFireRiskAssessmentReviewsViewModelFactory WithRiskAssessmentId(long riskAssessmentId);
        IFireRiskAssessmentReviewsViewModelFactory WithUser(IPrincipal user);
        FireRiskAssessmentReviewsViewModel GetViewModel();
    }
}