using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public class EditFurtherControlMeasureTaskViewModelFactory : IEditFurtherControlMeasureTaskViewModelFactory
    {
        private readonly IRiskAssessmentHazardSummaryViewModelFactory _riskAssessmentHazardSummaryViewModelFactory;
        private readonly IEditHazardousSubstanceFurtherControlMeasureTaskViewModelFactory _editHazardousSubstanceFurtherControlMeasureTaskViewModelFactory;
        private readonly IFurtherControlMeasureTaskService _furtherControlMeasureTaskService;
        private long _companyId;
        private long _furtherControlMeasureTaskId;
        private bool _canDeleteDocuments;

        public EditFurtherControlMeasureTaskViewModelFactory(
            IRiskAssessmentHazardSummaryViewModelFactory riskAssessmentHazardSummaryViewModelFactory, 
            IEditHazardousSubstanceFurtherControlMeasureTaskViewModelFactory editHazardousSubstanceFurtherControlMeasureTaskViewModelFactory, 
            IFurtherControlMeasureTaskService furtherControlMeasureTaskService)
        {
            _riskAssessmentHazardSummaryViewModelFactory = riskAssessmentHazardSummaryViewModelFactory;
            _editHazardousSubstanceFurtherControlMeasureTaskViewModelFactory = editHazardousSubstanceFurtherControlMeasureTaskViewModelFactory;
            _furtherControlMeasureTaskService = furtherControlMeasureTaskService;
        }

        public IEditFurtherControlMeasureTaskViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IEditFurtherControlMeasureTaskViewModelFactory WithFurtherControlMeasureTaskId(long furtherControlMeasureTaskId)
        {
            _furtherControlMeasureTaskId = furtherControlMeasureTaskId;
            return this;
        }

        public IEditFurtherControlMeasureTaskViewModelFactory WithCanDeleteDocuments(bool canDeleteDocuments)
        {
            _canDeleteDocuments = canDeleteDocuments;
            return this;
        }

        public EditRiskAssessmentFurtherControlMeasureTaskViewModel GetViewModel()
        {
            var furtherControlMeasure =
                _furtherControlMeasureTaskService.GetByIdAndCompanyId(_furtherControlMeasureTaskId, _companyId) as
                MultiHazardRiskAssessmentFurtherControlMeasureTaskDto;

            return new EditRiskAssessmentFurtherControlMeasureTaskViewModel
                       {
                           HazardSummary = _riskAssessmentHazardSummaryViewModelFactory
                               .WithRiskAssessmentHazardId(furtherControlMeasure.RiskAssessmentHazard.Id)
                               .WithCompanyId(_companyId)
                               .GetViewModel(),

                           FurtherControlMeasureTask = _editHazardousSubstanceFurtherControlMeasureTaskViewModelFactory
                               .WithFurtherControlMeasureTaskId(_furtherControlMeasureTaskId)
                               .WithCompanyId(_companyId)
                               .WithCanDeleteDocuments(_canDeleteDocuments)
                               .GetViewModel()
                       };
        }
    }
}