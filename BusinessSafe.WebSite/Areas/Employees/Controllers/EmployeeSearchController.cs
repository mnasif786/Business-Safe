using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.WebSite.Areas.Employees.Factories;
using BusinessSafe.WebSite.Areas.Employees.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Areas.Employees.Controllers
{
    public class EmployeeSearchController : BaseController
    {
        private readonly IEmployeeSearchViewModelFactory _employeeSearchViewModelFactory;
        private readonly ITaskService _taskService;

        public EmployeeSearchController(IEmployeeSearchViewModelFactory employeeSearchViewModelFactory, ITaskService taskService)
        {
            _employeeSearchViewModelFactory = employeeSearchViewModelFactory;
            _taskService = taskService;
        }

        [PermissionFilter(Permissions.ViewEmployeeRecords)]
        public ActionResult Index(long companyId, string employeeReference = "", string forename = "", string surname = "", long siteId = 0, bool showDeleted = false)
        {
            var viewModel = _employeeSearchViewModelFactory
                                .WithCompanyId(companyId)
                                .WithAllowedSites(CurrentUser.GetSitesFilter())
                                .WithEmployeeReference(employeeReference)
                                .WithForeName(forename)
                                .WithSurname(surname)
                                .WithSiteId(siteId)
                                .WithShowDeleted(showDeleted)
                                .WithCurrentUser(CurrentUser)
                                .GetViewModel();
            return View(viewModel);
        }

        [PermissionFilter(Permissions.DeleteEmployeeRecords)]
        public JsonResult CanDeleteEmployee(CanDeleteEmployeeViewModel viewModel)
        {
            if (viewModel.CompanyId == 0 || viewModel.EmployeeId == Guid.Empty)
            {
                throw new ArgumentException("Invalid employeeId and companyId");
            }

            var result = _taskService.HasEmployeeGotOutstandingTasks(viewModel.EmployeeId, viewModel.CompanyId);

            if (result)
            {
                return Json(new { CanDeleteEmployee = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { CanDeleteEmployee = true }, JsonRequestBehavior.AllowGet);
        }
    }
}