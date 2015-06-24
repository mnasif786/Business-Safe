using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public class RiskAssessmentHazardSummaryViewModelFactory : IRiskAssessmentHazardSummaryViewModelFactory
    {
        private readonly IRiskAssessmentHazardService _riskAssessmentHazardService;
        private long _generalRiskAssessmentHazardId;
        private long _companyId;

        public RiskAssessmentHazardSummaryViewModelFactory(IRiskAssessmentHazardService riskAssessmentHazardService)
        {
            _riskAssessmentHazardService = riskAssessmentHazardService;
        }

        public IRiskAssessmentHazardSummaryViewModelFactory WithRiskAssessmentHazardId(long riskAssessmentHazardId)
        {
            _generalRiskAssessmentHazardId = riskAssessmentHazardId;
            return this;
        }

        public IRiskAssessmentHazardSummaryViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public RiskAssessmentHazardSummaryViewModel GetViewModel()
        {
            var generalRiskAssessmentHazard =
                _riskAssessmentHazardService.GetByIdAndCompanyId(_generalRiskAssessmentHazardId, _companyId);

            return new RiskAssessmentHazardSummaryViewModel
                       {
                           Id = _generalRiskAssessmentHazardId,
                           RiskAssessmentHazardDescription = generalRiskAssessmentHazard.Description,
                           RiskAssessmentHazardName = generalRiskAssessmentHazard.Hazard.Name,
                           RiskAssessmentTitle = generalRiskAssessmentHazard.RiskAssessment.Title
                       };
        }
    }
}