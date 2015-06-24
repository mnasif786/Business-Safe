using BusinessSafe.WebSite.Models;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public class CompleteReviewViewModelFactory : ICompleteReviewViewModelFactory
    {
        private long _riskAssessmentReviewId;
        private long _companyId;
        private bool _hasUncompletedTasks;
        private RiskAssessmentType _riskAssessmentType;

        public ICompleteReviewViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public ICompleteReviewViewModelFactory WithReviewId(long reviewId)
        {
            _riskAssessmentReviewId = reviewId;
            return this;
        }

        public ICompleteReviewViewModelFactory WithHasUncompletedTasks(bool hasUncompletedTasks)
        {
            _hasUncompletedTasks = hasUncompletedTasks;
            return this;
        }

        public ICompleteReviewViewModelFactory WithRiskAssessmentType(Models.RiskAssessmentType riskAssessmentType)
        {
            _riskAssessmentType = riskAssessmentType;
            return this;
        }

        public CompleteReviewViewModel GetViewModel()
        {
            return new CompleteReviewViewModel()
            {
                CompanyId = _companyId,
                RiskAssessmentReviewId = _riskAssessmentReviewId,
                HasUncompletedTasks = _hasUncompletedTasks,
                RiskAssessmentType = _riskAssessmentType
            };
        }
    }
}