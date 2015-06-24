using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using ControlMeasuresViewModel = BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels.ControlMeasuresViewModel;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories
{
    public class ControlMeasuresViewModelFactory: IControlMeasuresViewModelFactory
    {
        private readonly IHazardousSubstanceRiskAssessmentService _hazardousSubstanceRiskAssessmentService;
        private long _companyId;
        private long _hazardousSubstanceRiskAssessmentId;
        
        public ControlMeasuresViewModelFactory(IHazardousSubstanceRiskAssessmentService hazardousSubstanceRiskAssessmentService)
        {
            _hazardousSubstanceRiskAssessmentService = hazardousSubstanceRiskAssessmentService;
        }

        public IControlMeasuresViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IControlMeasuresViewModelFactory WithHazardousSubstanceRiskAssessmentId(long hazardousSubstanceRiskAssessmentId)
        {
            _hazardousSubstanceRiskAssessmentId = hazardousSubstanceRiskAssessmentId;
            return this;
        }

        public ControlMeasuresViewModel GetViewModel()
        {
            var hazardousSubstanceDto = _hazardousSubstanceRiskAssessmentService.GetRiskAssessment(_hazardousSubstanceRiskAssessmentId, _companyId);
            return new ControlMeasuresViewModel()
                       {
                           CompanyId = _companyId,
                           RiskAssessmentId = _hazardousSubstanceRiskAssessmentId,
                           ControlMeasures = HazardousSubstanceRiskAssessmentControlMeasureViewModel.CreateFrom(hazardousSubstanceDto.ControlMeasures),
                           FurtherControlMeasures = HazardousSubstanceRiskAssessmentFurtherControlMeasureViewModel.CreateFrom(hazardousSubstanceDto.FurtherControlMeasureTasks)
                       };
        }
    }
}