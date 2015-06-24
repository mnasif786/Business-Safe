using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public class CompleteFurtherControlMeasureTaskViewModelFactory : ICompleteFurtherControlMeasureTaskViewModelFactory
    {
        private readonly IFurtherControlMeasureTaskService _furtherControlMeasureTaskService;
        private readonly IViewFurtherControlMeasureTaskViewModelFactory _viewFurtherControlMeasureTaskViewModelFactory;
        private long _companyId;
        private long _furtherControlMeasureTaskId;

        public CompleteFurtherControlMeasureTaskViewModelFactory(
            IFurtherControlMeasureTaskService furtherControlMeasureTaskService,
            IViewFurtherControlMeasureTaskViewModelFactory viewFurtherControlMeasureTaskViewModelFactory)
        {
            _furtherControlMeasureTaskService = furtherControlMeasureTaskService;
            _viewFurtherControlMeasureTaskViewModelFactory = viewFurtherControlMeasureTaskViewModelFactory;
        }

        public ICompleteFurtherControlMeasureTaskViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public ICompleteFurtherControlMeasureTaskViewModelFactory WithFurtherControlMeasureTaskId(long furtherControlMeasureTaskId)
        {
            _furtherControlMeasureTaskId = furtherControlMeasureTaskId;
            return this;
        }

        public CompleteFurtherControlMeasureTaskViewModel GetViewModel()
        {
            var furtherControlMeasureTask =
                   _furtherControlMeasureTaskService.GetByIdAndCompanyId(_furtherControlMeasureTaskId, _companyId);

            var viewModel = new CompleteFurtherControlMeasureTaskViewModel
                                {
                                    CompanyId = _companyId,
                                    FurtherControlMeasureTaskId = _furtherControlMeasureTaskId,
                                    ViewFurtherControlMeasureTaskViewModel = _viewFurtherControlMeasureTaskViewModelFactory
                                        .GetViewModel(furtherControlMeasureTask)
                                };

            return viewModel;
        }
    }
}