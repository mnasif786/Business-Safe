using System.Security.Principal;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.Models;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories
{
    public class HazardousSubstanceRiskAssessmentReviewsViewModelFactory : IHazardousSubstanceRiskAssessmentReviewsViewModelFactory
    {        
        private long _companyId;
        private long _riskAssessmentId;
        private IPrincipal _user;
        private readonly IRiskAssessmentReviewsViewModelFactory _riskAssessmentReviewsViewModelFactory;

        public HazardousSubstanceRiskAssessmentReviewsViewModelFactory(IRiskAssessmentReviewsViewModelFactory riskAssessmentReviewsViewModelFactory)
        {
            _riskAssessmentReviewsViewModelFactory = riskAssessmentReviewsViewModelFactory;
        }

        public IHazardousSubstanceRiskAssessmentReviewsViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IHazardousSubstanceRiskAssessmentReviewsViewModelFactory WithRiskAssessmentId(long riskAssessmentId)
        {
            _riskAssessmentId = riskAssessmentId;
            return this;
        }

        public IHazardousSubstanceRiskAssessmentReviewsViewModelFactory WithUser(IPrincipal user)
        {
            _user = user;
            return this;
        }

        public HazardousSubstanceRiskAssessmentReviewsViewModel GetViewModel()
        {
            var riskAssessmentReviewsViewModel = _riskAssessmentReviewsViewModelFactory
                .WithCompanyId(_companyId)
                .WithRiskAssessmentId(_riskAssessmentId)
                .WithUser(_user)
                .GetViewModel();

            riskAssessmentReviewsViewModel.RiskAssessmentType = RiskAssessmentType.HSRA;

            return new HazardousSubstanceRiskAssessmentReviewsViewModel
                       {
                            ReviewViewModel = riskAssessmentReviewsViewModel
                       };
        }
    }
}