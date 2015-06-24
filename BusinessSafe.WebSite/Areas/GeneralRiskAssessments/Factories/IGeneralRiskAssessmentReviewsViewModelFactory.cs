using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using System.Security.Principal;

namespace BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Factories
{
    public interface IGeneralRiskAssessmentReviewsViewModelFactory
    {
        IGeneralRiskAssessmentReviewsViewModelFactory WithCompanyId(long companyId);
        IGeneralRiskAssessmentReviewsViewModelFactory WithRiskAssessmentId(long riskAssessmentId);
        IGeneralRiskAssessmentReviewsViewModelFactory WithUser(IPrincipal user);
        GeneralRiskAssessmentReviewsViewModel GetViewModel();
    }
}