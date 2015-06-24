using System.Security.Principal;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.Models;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories
{
    public class FireRiskAssessmentReviewsViewModelFactory: IFireRiskAssessmentReviewsViewModelFactory
    {
        private long _companyId;
        private long _riskAssessmentId;
        private IPrincipal _user;
        private readonly IRiskAssessmentReviewsViewModelFactory _riskAssessmentReviewsViewModelFactory;

        public FireRiskAssessmentReviewsViewModelFactory(IRiskAssessmentReviewsViewModelFactory riskAssessmentReviewsViewModelFactory)
        {
            _riskAssessmentReviewsViewModelFactory = riskAssessmentReviewsViewModelFactory;
        }

        public IFireRiskAssessmentReviewsViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IFireRiskAssessmentReviewsViewModelFactory WithRiskAssessmentId(long riskAssessmentId)
        {
            _riskAssessmentId = riskAssessmentId;
            return this;
        }

        public IFireRiskAssessmentReviewsViewModelFactory WithUser(IPrincipal user)
        {
            _user = user;
            return this;
        }

        public FireRiskAssessmentReviewsViewModel GetViewModel()
        {
            var riskAssessmentReviewsViewModel = _riskAssessmentReviewsViewModelFactory
                .WithCompanyId(_companyId)
                .WithRiskAssessmentId(_riskAssessmentId)
                .WithUser(_user)
                .GetViewModel();

            riskAssessmentReviewsViewModel.RiskAssessmentType = RiskAssessmentType.FRA;

            return new FireRiskAssessmentReviewsViewModel
                       {
                           ReviewViewModel = riskAssessmentReviewsViewModel
                       };
        }
    }
}