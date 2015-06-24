using System.Security.Principal;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories
{
    public interface IPersonalRiskAsessmentReviewsViewModelFactory
    {
        IPersonalRiskAsessmentReviewsViewModelFactory WithCompanyId(long companyId);
        IPersonalRiskAsessmentReviewsViewModelFactory WithRiskAssessmentId(long riskAssessmentId);
        IPersonalRiskAsessmentReviewsViewModelFactory WithUser(IPrincipal user);
        PersonalRiskAssessmentReviewsViewModel GetViewModel();
    }
}