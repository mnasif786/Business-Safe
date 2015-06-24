using BusinessSafe.WebSite.Models;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public interface ICompleteReviewViewModelFactory
    {
        ICompleteReviewViewModelFactory WithCompanyId(long companyId);
        ICompleteReviewViewModelFactory WithReviewId(long reviewId);
        ICompleteReviewViewModelFactory WithHasUncompletedTasks(bool hasUncompletedTasks);
        ICompleteReviewViewModelFactory WithRiskAssessmentType(RiskAssessmentType riskAssessmentType);
        CompleteReviewViewModel GetViewModel();
    }
}