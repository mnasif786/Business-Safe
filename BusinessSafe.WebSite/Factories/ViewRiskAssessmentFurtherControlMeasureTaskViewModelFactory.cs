using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public class ViewRiskAssessmentFurtherControlMeasureTaskViewModelFactory : IViewRiskAssessmentFurtherControlMeasureTaskViewModelFactory
    {
        private readonly IRiskAssessmentHazardSummaryViewModelFactory _riskAssessmentHazardSummaryViewModelFactory;
        private readonly IViewFurtherControlMeasureTaskViewModelFactory _viewFurtherControlMeasureTaskViewModelFactory;
        private readonly IFurtherControlMeasureTaskService _furtherControlMeasureTaskService;
        private long _companyId;
        private long _furtherControlMeasureTaskId;

        public ViewRiskAssessmentFurtherControlMeasureTaskViewModelFactory(
            IRiskAssessmentHazardSummaryViewModelFactory riskAssessmentHazardSummaryViewModelFactory,
            IViewFurtherControlMeasureTaskViewModelFactory viewFurtherControlMeasureTaskViewModelFactory,
            IFurtherControlMeasureTaskService furtherControlMeasureTaskService)
        {
            _riskAssessmentHazardSummaryViewModelFactory = riskAssessmentHazardSummaryViewModelFactory;
            _viewFurtherControlMeasureTaskViewModelFactory = viewFurtherControlMeasureTaskViewModelFactory;
            _furtherControlMeasureTaskService = furtherControlMeasureTaskService;
        }

        public IViewRiskAssessmentFurtherControlMeasureTaskViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IViewRiskAssessmentFurtherControlMeasureTaskViewModelFactory WithFurtherControlMeasureTaskId(long furtherControlMeasureTaskId)
        {
            _furtherControlMeasureTaskId = furtherControlMeasureTaskId;
            return this;
        }

        public ViewRiskAssessmentFurtherControlMeasureTaskViewModel GetViewModel()
        {
            var furtherControlMeasure =
                _furtherControlMeasureTaskService.GetByIdIncludeDeleted(_furtherControlMeasureTaskId) as
                MultiHazardRiskAssessmentFurtherControlMeasureTaskDto;

            return new ViewRiskAssessmentFurtherControlMeasureTaskViewModel
            {
                HazardSummary = _riskAssessmentHazardSummaryViewModelFactory
                    .WithRiskAssessmentHazardId(furtherControlMeasure.RiskAssessmentHazard.Id)
                    .WithCompanyId(_companyId)
                    .GetViewModel(),

                FurtherControlMeasureTask = _viewFurtherControlMeasureTaskViewModelFactory
                    .WithFurtherControlMeasureTaskId(_furtherControlMeasureTaskId)
                    .WithCompanyId(_companyId)
                    .GetViewModel()
            };
        }
    }
}