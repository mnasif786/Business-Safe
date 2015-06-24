using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.Factories
{
    public class HazardousSubstanceDescriptionViewModelFactory: IHazardousSubstanceDescriptionViewModelFactory
    {
        private long _riskAssessmentId;
        private long _companyId;
        private readonly IHazardousSubstanceRiskAssessmentService _hazardousSubstanceRiskAssessmentService;
        
        public HazardousSubstanceDescriptionViewModelFactory(IHazardousSubstanceRiskAssessmentService hazardousSubstanceRiskAssessmentService)
        {
            _hazardousSubstanceRiskAssessmentService = hazardousSubstanceRiskAssessmentService;
        }

        public IHazardousSubstanceDescriptionViewModelFactory WithRiskAssessmentId(long riskAssessmentId)
        {
            _riskAssessmentId = riskAssessmentId;
            return this;
        }

        public IHazardousSubstanceDescriptionViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public DescriptionViewModel GetViewModel()
        {
            var riskAssessment = _hazardousSubstanceRiskAssessmentService.GetRiskAssessment(_riskAssessmentId, _companyId);
            return DescriptionViewModel.CreateFrom(riskAssessment);
        }
    }
}