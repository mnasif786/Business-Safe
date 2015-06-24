using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories
{
    public class SignificantFindingViewModelFactory : ISignificantFindingViewModelFactory
    {
        private readonly IFireRiskAssessmentService _fireRiskAssessmentService;
        private long _companyId;
        private long _riskAssessmentId;
        private IPrincipal _user;

        public SignificantFindingViewModelFactory(IFireRiskAssessmentService fireRiskAssessmentService)
        {
            _fireRiskAssessmentService = fireRiskAssessmentService;
        }

        public ISignificantFindingViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public ISignificantFindingViewModelFactory WithRiskAssessmentId(long riskAssessmentId)
        {
            _riskAssessmentId = riskAssessmentId;
            return this;
        }

        public ISignificantFindingViewModelFactory WithUser(IPrincipal user)
        {
            _user = user;
            return this;
        }

        public RiskAssessmentSignificantFindingsViewModel GetViewModel()
        {
            var riskAssessmentDto = _fireRiskAssessmentService.GetRiskAssessmentWithSignificantFindings(_riskAssessmentId, _companyId);

            var fireRiskAssessmentChecklistViewModel = new RiskAssessmentSignificantFindingsViewModel
                                                           {
                                                               RiskAssessmentId = _riskAssessmentId,
                                                               CompanyId = _companyId,
                                                               SignificantFindings = SignificantFindingViewModel.CreateFrom(riskAssessmentDto.SignificantFindings)
                                                           };

            return fireRiskAssessmentChecklistViewModel;
        }

        
    }
}