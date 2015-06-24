using System.Security.Principal;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.Models;

namespace BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Factories
{
    public class GeneralRiskAssessmentReviewsViewModelFactory : IGeneralRiskAssessmentReviewsViewModelFactory
    {        
        private long _companyId;
        private long _riskAssessmentId;
        private IPrincipal _user;
        private readonly IRiskAssessmentReviewsViewModelFactory _riskAssessmentReviewsViewModelFactory;

        public GeneralRiskAssessmentReviewsViewModelFactory(IRiskAssessmentReviewsViewModelFactory riskAssessmentReviewsViewModelFactory)
        {
            _riskAssessmentReviewsViewModelFactory = riskAssessmentReviewsViewModelFactory;
        }

        public IGeneralRiskAssessmentReviewsViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IGeneralRiskAssessmentReviewsViewModelFactory WithRiskAssessmentId(long riskAssessmentId)
        {
            _riskAssessmentId = riskAssessmentId;
            return this;
        }

        public IGeneralRiskAssessmentReviewsViewModelFactory WithUser(IPrincipal user)
        {
            _user = user;
            return this;
        }

        public GeneralRiskAssessmentReviewsViewModel GetViewModel()
        {
            var riskAssessmentReviewsViewModel = _riskAssessmentReviewsViewModelFactory
                                                                .WithCompanyId(_companyId)
                                                                .WithRiskAssessmentId(_riskAssessmentId)
                                                                .WithUser(_user)
                                                                .GetViewModel();

            riskAssessmentReviewsViewModel.RiskAssessmentType = RiskAssessmentType.GRA;
            
            return new GeneralRiskAssessmentReviewsViewModel
                       {
                            ReviewViewModel = riskAssessmentReviewsViewModel
                       };
        }
    }
}