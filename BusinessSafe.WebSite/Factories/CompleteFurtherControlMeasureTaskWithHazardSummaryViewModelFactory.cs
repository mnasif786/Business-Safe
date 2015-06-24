using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public class CompleteFurtherControlMeasureTaskWithHazardSummaryViewModelFactory : ICompleteFurtherControlMeasureTaskWithHazardSummaryViewModelFactory
    {
        private readonly IRiskAssessmentHazardSummaryViewModelFactory _riskAssessmentHazardSummaryViewModelFactory;
        private readonly ICompleteFurtherControlMeasureTaskViewModelFactory _completeFurtherControlMeasureTaskViewModelFactory;
        private readonly IFurtherControlMeasureTaskService _furtherControlMeasureTaskService;
        private long _companyId;
        private long _furtherControlMeasureTaskId;

        public CompleteFurtherControlMeasureTaskWithHazardSummaryViewModelFactory(
            IRiskAssessmentHazardSummaryViewModelFactory riskAssessmentHazardSummaryViewModelFactory,
            ICompleteFurtherControlMeasureTaskViewModelFactory completeFurtherControlMeasureTaskViewModelFactory,
            IFurtherControlMeasureTaskService furtherControlMeasureTaskService)
        {
            _riskAssessmentHazardSummaryViewModelFactory = riskAssessmentHazardSummaryViewModelFactory;
            _completeFurtherControlMeasureTaskViewModelFactory = completeFurtherControlMeasureTaskViewModelFactory;
            _furtherControlMeasureTaskService = furtherControlMeasureTaskService;
        }

        public ICompleteFurtherControlMeasureTaskWithHazardSummaryViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public ICompleteFurtherControlMeasureTaskWithHazardSummaryViewModelFactory WithFurtherControlMeasureTaskId(long furtherControlMeasureTaskId)
        {
            _furtherControlMeasureTaskId = furtherControlMeasureTaskId;
            return this;
        }

        public CompleteRiskAssessmentFurtherControlMeasureTaskViewModel GetViewModel()
        {
            var furtherControlMeasure =
                _furtherControlMeasureTaskService.GetByIdAndCompanyId(_furtherControlMeasureTaskId, _companyId) as
                MultiHazardRiskAssessmentFurtherControlMeasureTaskDto;

            return new CompleteRiskAssessmentFurtherControlMeasureTaskViewModel
            {
                HazardSummary = _riskAssessmentHazardSummaryViewModelFactory
                    .WithRiskAssessmentHazardId(furtherControlMeasure.RiskAssessmentHazard.Id)
                    .WithCompanyId(_companyId)
                    .GetViewModel(),

                CompleteFurtherControlMeasureTaskViewModel = _completeFurtherControlMeasureTaskViewModelFactory
                    .WithFurtherControlMeasureTaskId(_furtherControlMeasureTaskId)
                    .WithCompanyId(_companyId)
                    .GetViewModel()
            };
        }
    }
}