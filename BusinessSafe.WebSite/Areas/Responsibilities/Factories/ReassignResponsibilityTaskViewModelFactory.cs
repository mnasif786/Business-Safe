using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels;

namespace BusinessSafe.WebSite.Areas.Responsibilities.Factories
{
    public class ReassignResponsibilityTaskViewModelFactory : IReassignResponsibilityTaskViewModelFactory
    {
        private IResponsibilityTaskService _responsibilityTaskService;

        private long _companyId;
        private long _responsibilityTaskId;
        private IViewResponsibilityTaskViewModelFactory _viewResponsibilityTaskViewModelFactory;

        public ReassignResponsibilityTaskViewModelFactory(IResponsibilityTaskService responsibilityTaskService,
                                                          IViewResponsibilityTaskViewModelFactory
                                                              viewResponsibilityTaskViewModelFactory)
        {
            _responsibilityTaskService = responsibilityTaskService;
            _viewResponsibilityTaskViewModelFactory = viewResponsibilityTaskViewModelFactory;
        }

        public IReassignResponsibilityTaskViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IReassignResponsibilityTaskViewModelFactory WithResponsibilityTaskId(long responsibilityTaskId)
        {
            _responsibilityTaskId = responsibilityTaskId;
            return this;
        }

        public ReassignResponsibilityTaskViewModel GetViewModel()
        {
            var task = _responsibilityTaskService.GetByIdAndCompanyId(_responsibilityTaskId, _companyId);
            return new ReassignResponsibilityTaskViewModel
                       {
                           CompanyId = _companyId,
                           ResponsibilityTaskId = _responsibilityTaskId,
                           ResponsibilitySummary = new ResponsibilitySummaryViewModel
                                                       {
                                                           Id = task.Responsibility.Id,
                                                           Title = task.Responsibility.Title,
                                                           Description = task.Responsibility.Description
                                                       },
                           ResponsibilityTask = _viewResponsibilityTaskViewModelFactory
                               .WithCompanyId(_companyId)
                               .WithResponsibilityTaskId(_responsibilityTaskId)
                               .GetViewModel()
                       };
        }
    }
}