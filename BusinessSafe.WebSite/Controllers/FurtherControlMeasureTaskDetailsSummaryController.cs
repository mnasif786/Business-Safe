using System.Web.Mvc;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Controllers
{
    public class FurtherControlMeasureTaskDetailsSummaryController:BaseController
    {
        private readonly ITaskService _taskService;

        public FurtherControlMeasureTaskDetailsSummaryController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [PermissionFilter(Permissions.ViewGeneralandHazardousSubstancesRiskAssessments)]
        public PartialViewResult Index(long furtherControlMeasureTaskId)
        {
            var taskDetailsSummaryDto = _taskService.GetTaskDetailsSummary(new TaskDetailsSummaryRequest
                                                                               {
                                                                                   RiskAssessmentFurtherControlMeasureId = furtherControlMeasureTaskId
                                                                               });

            var viewModel = TaskDetailsSummaryViewModel.CreateFrom(taskDetailsSummaryDto);

            return PartialView("_TaskDetailsSummary", viewModel);
        }
    }
}