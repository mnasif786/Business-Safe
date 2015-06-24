using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels;

namespace BusinessSafe.WebSite.Areas.Responsibilities.Factories
{
    public class CompleteResponsibilityTaskViewModelFactory : ICompleteResponsibilityTaskViewModelFactory
    {
        private readonly IResponsibilityTaskService _responsibilityTaskService;
        private readonly IViewResponsibilityTaskViewModelFactory _viewResponsibilityTaskViewModelFactory;
        private long _companyId;
        private long _responsibilityTaskId;
        
        public CompleteResponsibilityTaskViewModelFactory(
            IResponsibilityTaskService responsibilityTaskService,
            IViewResponsibilityTaskViewModelFactory viewResponsibilityTaskViewModelFactory)
        {
            _responsibilityTaskService = responsibilityTaskService;
            _viewResponsibilityTaskViewModelFactory = viewResponsibilityTaskViewModelFactory;
        }

        public ICompleteResponsibilityTaskViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public ICompleteResponsibilityTaskViewModelFactory WithResponsibilityTaskId(long responsibilityTaskId)
        {
            _responsibilityTaskId = responsibilityTaskId;
            return this;
        }

        public CompleteResponsibilityTaskViewModel GetViewModel()
        {
            var task = _responsibilityTaskService.GetByIdAndCompanyId(_responsibilityTaskId, _companyId);

            var viewModel = new CompleteResponsibilityTaskViewModel
                       {
                           CompanyId = _companyId,
                           ResponsibilityTaskId = _responsibilityTaskId,
                           ResponsibilitySummary = new ResponsibilitySummaryViewModel
                                                       {
                                                           Id = task.Responsibility.Id,
                                                           Title = task.Responsibility.Title,
                                                           Description = task.Responsibility.Description
                                                       }
                       };

            viewModel.ResponsibilityTask = ViewResponsibilityTaskViewModel.CreateFrom(task);
            return viewModel;

        }
    }
}