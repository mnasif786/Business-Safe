using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using System.Security.Principal;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories
{
    public interface IHazardousSubstanceRiskAssessmentReviewsViewModelFactory
    {
        IHazardousSubstanceRiskAssessmentReviewsViewModelFactory WithCompanyId(long companyId);
        IHazardousSubstanceRiskAssessmentReviewsViewModelFactory WithRiskAssessmentId(long riskAssessmentId);
        IHazardousSubstanceRiskAssessmentReviewsViewModelFactory WithUser(IPrincipal user);
        HazardousSubstanceRiskAssessmentReviewsViewModel GetViewModel();
    }
}