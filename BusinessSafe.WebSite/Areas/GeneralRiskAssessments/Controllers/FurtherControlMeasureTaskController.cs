using System.Web.Mvc;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Controllers
{
    public class FurtherControlMeasureTaskController : BaseController
    {
        private readonly IEditFurtherControlMeasureTaskViewModelFactory _editTaskViewModelFactory;
        private readonly IAddFurtherControlMeasureTaskViewModelFactory _addTaskViewModelFactory;
        private readonly ICompleteFurtherControlMeasureTaskWithHazardSummaryViewModelFactory _completeTaskWithHazardSummaryViewModelFactory;
        private readonly IReassignFurtherControlMeasureTaskWithHazardSummaryViewModelFactory _reassignTaskWithHazardSummaryViewModelFactory;
        private readonly IViewRiskAssessmentFurtherControlMeasureTaskViewModelFactory _viewTaskViewModelFactory;

        public FurtherControlMeasureTaskController(
            IEditFurtherControlMeasureTaskViewModelFactory editTaskViewModelFactory,
            IAddFurtherControlMeasureTaskViewModelFactory addTaskViewModelFactory,
            ICompleteFurtherControlMeasureTaskWithHazardSummaryViewModelFactory completeTaskWithHazardSummaryViewModelFactory,
            IReassignFurtherControlMeasureTaskWithHazardSummaryViewModelFactory reassignTaskWithHazardSummaryViewModelFactory,
            IViewRiskAssessmentFurtherControlMeasureTaskViewModelFactory viewTaskViewModelFactory)
        {
            _editTaskViewModelFactory = editTaskViewModelFactory;
            _addTaskViewModelFactory = addTaskViewModelFactory;
            _completeTaskWithHazardSummaryViewModelFactory = completeTaskWithHazardSummaryViewModelFactory;
            _reassignTaskWithHazardSummaryViewModelFactory = reassignTaskWithHazardSummaryViewModelFactory;
            _viewTaskViewModelFactory = viewTaskViewModelFactory;
        }

        [PermissionFilter(Permissions.ViewRiskAssessmentTasks)]
        public PartialViewResult Print(long companyId, long furtherControlMeasureTaskId)
        {
            var viewModel = _viewTaskViewModelFactory
                .WithCompanyId(companyId)
                .WithFurtherControlMeasureTaskId(furtherControlMeasureTaskId)
                .GetViewModel();

            return PartialView("_PrintRiskAssessmentFurtherControlMeasureTask", viewModel);
        }

        [PermissionFilter(Permissions.AddRiskAssessmentTasks)]
        public PartialViewResult View(long companyId, long furtherControlMeasureTaskId)
        {
            IsReadOnly = true;

            var viewModel = _viewTaskViewModelFactory
                .WithCompanyId(companyId)
                .WithFurtherControlMeasureTaskId(furtherControlMeasureTaskId)
                .GetViewModel();

            return PartialView("_ViewRiskAssessmentFurtherControlMeasureTask", viewModel);
        }

        [PermissionFilter(Permissions.AddRiskAssessmentTasks)]
        public PartialViewResult New(long companyId, long riskAssessmentHazardId)
        {
            var viewModel = _addTaskViewModelFactory
                .WithCompanyId(companyId)
                .WithRiskAssessmentHazardId(riskAssessmentHazardId)
                .WithFurtherControlMeasureTaskCategory(FurtherControlMeasureTaskCategoryEnum.GeneralRiskAssessments)
                .GetViewModel();

            return PartialView("_AddRiskAssessmentFurtherControlMeasureTask", viewModel);
        }

        [PermissionFilter(Permissions.EditRiskAssessmentTasks)]
        public PartialViewResult Edit(long companyId, long furtherControlMeasureTaskId)
        {
            var viewModel = _editTaskViewModelFactory
                    .WithCompanyId(companyId)
                    .WithFurtherControlMeasureTaskId(furtherControlMeasureTaskId)
                    .WithCanDeleteDocuments(true)
                    .GetViewModel();

            return PartialView("_EditRiskAssessmentFurtherControlMeasureTask", viewModel);
        }

        [PermissionFilter(Permissions.ViewRiskAssessmentTasks)]
        public PartialViewResult Complete(long companyId, long furtherControlMeasureTaskId)
        {
            var viewModel = _completeTaskWithHazardSummaryViewModelFactory
                .WithCompanyId(companyId)
                .WithFurtherControlMeasureTaskId(furtherControlMeasureTaskId)
                .GetViewModel();

            return PartialView("_CompleteRiskAssessmentFurtherControlMeasureTask", viewModel);
        }

        [PermissionFilter(Permissions.EditRiskAssessmentTasks)]
        public PartialViewResult Reassign(long companyId, long furtherControlMeasureTaskId)
        {
            var viewModel = _reassignTaskWithHazardSummaryViewModelFactory
                .WithCompanyId(companyId)
                .WithFurtherControlMeasureTaskId(furtherControlMeasureTaskId)
                .GetViewModel();

            return PartialView("_ReassignRiskAssessmentFurtherControlMeasureTask", viewModel);
        }
    }
}