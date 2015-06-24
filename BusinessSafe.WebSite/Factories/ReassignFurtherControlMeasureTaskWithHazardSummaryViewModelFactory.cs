using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public class ReassignFurtherControlMeasureTaskWithHazardSummaryViewModelFactory : IReassignFurtherControlMeasureTaskWithHazardSummaryViewModelFactory
    {
        private readonly IRiskAssessmentHazardSummaryViewModelFactory _riskAssessmentHazardSummaryViewModelFactory;
        private readonly IReassignFurtherControlMeasureTaskViewModelFactory _reassignFurtherControlMeasureTaskViewModelFactory;
        private readonly IFurtherControlMeasureTaskService _furtherControlMeasureTaskService;
        private long _companyId;
        private long _furtherControlMeasureTaskId;

        public ReassignFurtherControlMeasureTaskWithHazardSummaryViewModelFactory(
            IRiskAssessmentHazardSummaryViewModelFactory riskAssessmentHazardSummaryViewModelFactory,
            IReassignFurtherControlMeasureTaskViewModelFactory reassignFurtherControlMeasureTaskViewModelFactory,
            IFurtherControlMeasureTaskService furtherControlMeasureTaskService)
        {
            _riskAssessmentHazardSummaryViewModelFactory = riskAssessmentHazardSummaryViewModelFactory;
            _reassignFurtherControlMeasureTaskViewModelFactory = reassignFurtherControlMeasureTaskViewModelFactory;
            _furtherControlMeasureTaskService = furtherControlMeasureTaskService;
        }

        public IReassignFurtherControlMeasureTaskWithHazardSummaryViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IReassignFurtherControlMeasureTaskWithHazardSummaryViewModelFactory WithFurtherControlMeasureTaskId(long furtherControlMeasureTaskId)
        {
            _furtherControlMeasureTaskId = furtherControlMeasureTaskId;
            return this;
        }

        public ReassignRiskAssessmentFurtherControlMeasureTaskViewModel GetViewModel()
        {
            var furtherControlMeasure = _furtherControlMeasureTaskService.GetByIdAndCompanyId(_furtherControlMeasureTaskId, _companyId) as MultiHazardRiskAssessmentFurtherControlMeasureTaskDto;

            return new ReassignRiskAssessmentFurtherControlMeasureTaskViewModel
            {
                HazardSummary = _riskAssessmentHazardSummaryViewModelFactory
                    .WithRiskAssessmentHazardId(furtherControlMeasure.RiskAssessmentHazard.Id)
                    .WithCompanyId(_companyId)
                    .GetViewModel(),

                ReassignFurtherControlMeasureTaskViewModel = _reassignFurtherControlMeasureTaskViewModelFactory
                    .WithFurtherControlMeasureTaskId(_furtherControlMeasureTaskId)
                    .WithCompanyId(_companyId)
                    .GetViewModel()
            };
        }
    }
}