using System.Linq;
using System.Security.Principal;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public class RiskAssessmentReviewsViewModelFactory : IRiskAssessmentReviewsViewModelFactory
    {
        private readonly IRiskAssessmentReviewService _riskAssessmentReviewService;
        private long _companyId;
        private long _riskAssessmentId;
        private IPrincipal _user;

        public RiskAssessmentReviewsViewModelFactory(IRiskAssessmentReviewService riskAssessmentReviewService)
        {
            _riskAssessmentReviewService = riskAssessmentReviewService;
        }

        public IRiskAssessmentReviewsViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IRiskAssessmentReviewsViewModelFactory WithRiskAssessmentId(long riskAssessmentId)
        {
            _riskAssessmentId = riskAssessmentId;
            return this;
        }

        public IRiskAssessmentReviewsViewModelFactory WithUser(IPrincipal user)
        {
            _user = user;
            return this;
        }

        public RiskAssessmentReviewsViewModel GetViewModel()
        {
            var reviews = _riskAssessmentReviewService.Search(_riskAssessmentId);

            bool canUserAddReviews = !reviews.Any(r => r.CompletedDate == null || r.CompletedDate.Equals(string.Empty));
            
            return new RiskAssessmentReviewsViewModel
                       {
                           CompanyId = _companyId,
                           RiskAssessmentId = _riskAssessmentId,
                           RiskAssessmentReviews = reviews,
                           CanAddReview = canUserAddReviews
                       };
        }
    }
}