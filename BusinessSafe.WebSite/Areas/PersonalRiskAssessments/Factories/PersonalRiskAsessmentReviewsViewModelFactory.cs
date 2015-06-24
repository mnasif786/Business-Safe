using System.Security.Principal;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.Models;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories
{
    public class PersonalRiskAsessmentReviewsViewModelFactory : IPersonalRiskAsessmentReviewsViewModelFactory
    {
        private long _companyId;
        private long _riskAssessmentId;
        private IPrincipal _user;
        private readonly IRiskAssessmentReviewsViewModelFactory _riskAssessmentReviewsViewModelFactory;

        public PersonalRiskAsessmentReviewsViewModelFactory(IRiskAssessmentReviewsViewModelFactory riskAssessmentReviewsViewModelFactory)
        {
            _riskAssessmentReviewsViewModelFactory = riskAssessmentReviewsViewModelFactory;
        }

        public IPersonalRiskAsessmentReviewsViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IPersonalRiskAsessmentReviewsViewModelFactory WithRiskAssessmentId(long riskAssessmentId)
        {
            _riskAssessmentId = riskAssessmentId;
            return this;
        }

        public IPersonalRiskAsessmentReviewsViewModelFactory WithUser(IPrincipal user)
        {
            _user = user;
            return this;
        }

        public PersonalRiskAssessmentReviewsViewModel GetViewModel()
        {
            var riskAssessmentReviewsViewModel = _riskAssessmentReviewsViewModelFactory
                .WithCompanyId(_companyId)
                .WithRiskAssessmentId(_riskAssessmentId)
                .WithUser(_user)
                .GetViewModel();

            riskAssessmentReviewsViewModel.RiskAssessmentType = RiskAssessmentType.PRA;

            return new PersonalRiskAssessmentReviewsViewModel
                       {
                           ReviewViewModel = riskAssessmentReviewsViewModel
                       };
        }
    }
}