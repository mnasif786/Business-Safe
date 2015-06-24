using System;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public class ReassignFurtherControlMeasureTaskViewModelFactory : IReassignFurtherControlMeasureTaskViewModelFactory
    {
        private readonly IFurtherControlMeasureTaskService _furtherControlMeasureTaskService;
        private readonly IViewFurtherControlMeasureTaskViewModelFactory _viewFurtherControlMeasureTaskViewModelFactory;
        private long _companyId;
        private long _furtherControlMeasureTaskId;

        public ReassignFurtherControlMeasureTaskViewModelFactory(
            IFurtherControlMeasureTaskService furtherControlMeasureTaskService,
            IViewFurtherControlMeasureTaskViewModelFactory viewFurtherControlMeasureTaskViewModelFactory)
        {
            _furtherControlMeasureTaskService = furtherControlMeasureTaskService;
            _viewFurtherControlMeasureTaskViewModelFactory = viewFurtherControlMeasureTaskViewModelFactory;
        }

        public IReassignFurtherControlMeasureTaskViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IReassignFurtherControlMeasureTaskViewModelFactory WithFurtherControlMeasureTaskId(long furtherControlMeasureTaskId)
        {
            _furtherControlMeasureTaskId = furtherControlMeasureTaskId;
            return this;
        }

        public ReassignFurtherControlMeasureTaskViewModel GetViewModel()
        {
            var furtherControlMeasureTask = _furtherControlMeasureTaskService.GetByIdAndCompanyId(_furtherControlMeasureTaskId, _companyId);

            var viewModel = new ReassignFurtherControlMeasureTaskViewModel
            {
                CompanyId = _companyId,
                FurtherControlMeasureTaskId = _furtherControlMeasureTaskId,
                ReassignTaskToId =  furtherControlMeasureTask.TaskAssignedTo != null ? furtherControlMeasureTask.TaskAssignedTo.Id : new Guid(),
                ReassignTaskTo = furtherControlMeasureTask.TaskAssignedTo != null ? furtherControlMeasureTask.TaskAssignedTo.FullName : null,
                ViewFurtherControlMeasureTaskViewModel = _viewFurtherControlMeasureTaskViewModelFactory.GetViewModel(furtherControlMeasureTask),
                TaskGuid = furtherControlMeasureTask.TaskGuid
            };

            return viewModel;
        }
    }
}