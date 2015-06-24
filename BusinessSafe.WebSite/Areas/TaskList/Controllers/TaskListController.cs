using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.Implementations.Users;
using BusinessSafe.WebSite.Areas.TaskList.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Contracts;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using System;
using System.Linq;
using BusinessSafe.WebSite.ViewModels;
using BusinessSafe.Domain.Entities;
using StructureMap;
using Telerik.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.TaskList.Controllers
{
    public class TaskListController : BaseController
    {
        private readonly ITaskListViewModelFactory _taskListViewModelFactory;
        private readonly IUserService _userService;

        public TaskListController(ITaskListViewModelFactory taskListViewModelFactory, IUserService userService)
        {
            _taskListViewModelFactory = taskListViewModelFactory;
            _userService = userService;
        }

        [PermissionFilter(Permissions.ViewRiskAssessmentTasks)]
        public ActionResult Index()
        {
            Guid employeeId = EmploeeIdForUser();

            return RedirectToAction("Find", new { employeeId });

        }

        [GridAction(EnableCustomBinding = true, GridName = "TaskGrid")]
        [PermissionFilter(Permissions.ViewRiskAssessmentTasks)]
        public ViewResult Find(
            Guid? employeeId,
            string createdFrom,
            string createdTo,
            string completedFrom,
            string completedTo,
            string title,
            long taskCategoryId = 0,
            int taskStatusId = 0,
            bool showDeleted = false,
            bool showCompleted = false,
            bool isBulkReassign= false,
            long? siteId = null,
            long? siteGroupId = null,
            int page = 0, int size = 0, string orderBy = ""
            )
        {
            var viewModel = _taskListViewModelFactory
                                .WithCompanyId(CurrentUser.CompanyId)
                                .WithEmployeeId(employeeId)
                                .WithCreatedFrom(createdFrom)
                                .WithCreatedTo(createdTo)
                                .WithCompletedFrom(completedFrom)
                                .WithCompletedTo(completedTo)
                                .WithTaskCategoryId(taskCategoryId)
                                .WithTaskStatusId(taskStatusId)
                                .WithUser(CurrentUser)
                                .WithShowDeleted(showDeleted)
                                .WithShowCompleted(showCompleted)
                                .WithBulkReassignMode(isBulkReassign)
                                .WithSiteId(siteId)
                                .WithSiteGroupId(siteGroupId)
                                .WithAllowedSiteIds(CurrentUser.GetSitesFilter())
                                .WithTitle(title)
                                .WithPage(page)
                                .WithPageSize(size)
                                .WithOrderBy(orderBy)
                                .GetViewModel();

            return View("Index", viewModel);
        }

        [PermissionFilter(Permissions.ViewRiskAssessmentTasks)]
        public JsonResult GetTaskListSummary(long? siteGroupId = null, long? siteId = null, long taskCategoryId = 0, Guid? employeeId = null)
        {
            var viewModel = _taskListViewModelFactory
                .WithCompanyId(CurrentUser.CompanyId)
                .WithSiteGroupId(siteGroupId)
                .WithSiteId(siteId)
                .WithTaskCategoryId(taskCategoryId)
                .WithEmployeeId(employeeId)
                .WithAllowedSiteIds(CurrentUser.GetSitesFilter())
                .GetSummaryViewModel();

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PermissionFilter(Permissions.ViewRiskAssessmentTasks)]
        public JsonResult GetTaskReoccuringTypes(string filter, long companyId, int pageLimit)
        {
            var taskReoccuringTypes = Enum.GetValues(typeof (TaskReoccurringType)).Cast<TaskReoccurringType>().ToList();
            var result = taskReoccuringTypes.Select(AutoCompleteViewModel.ForTaskReoccurringType).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private Guid EmploeeIdForUser()
        {
            Guid employeeId = Guid.Empty;
            var user = _userService.GetIncludingEmployeeAndSiteByIdAndCompanyId(CurrentUser.UserId, CurrentUser.CompanyId);
            if (user != null && user.Employee != null)
            {
                employeeId = user.Employee.Id;
            }
            return employeeId;
        }
    }
}