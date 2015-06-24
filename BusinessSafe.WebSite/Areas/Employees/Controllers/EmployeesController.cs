using System;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Employees.Controllers
{
    public class EmployeesController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public JsonResult GetEmployees(string filter, long companyId, int pageLimit)
        {
            var employees = _employeeService.GetEmployeeNames(companyId);
            var result = employees.Select(AutoCompleteViewModel.ForEmployee)
                .OrderBy(x => x.label)
                .ToList();
                
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PermissionFilter(Permissions.ViewEmployeeRecords)]
        public JsonResult GetEmployeesWithNoJobTitle(string filter, long companyId, int pageLimit)
        {
            var employees = _employeeService.GetEmployeesForSearchTerm(filter, companyId, pageLimit);
            var result = employees.Select(AutoCompleteViewModel.ForEmployeeNoJobTitle).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PermissionFilter(Permissions.ViewEmployeeRecords)]
        public JsonResult GetEmployeesWithSite(string filter, long companyId, int pageLimit)
        {
            var employees = _employeeService.GetEmployeesForSearchTerm(filter, companyId, pageLimit);
            var result = employees.Select(AutoCompleteViewModel.ForEmployeeWithSite).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult IsEmployeeAUser(Guid employeeId, long companyId)
        {
            var employee = _employeeService.GetEmployee(employeeId, companyId);
            var isBusinessSafeUser = IsBusinessSafeUser(employee);
            return Json(new { IsBusinessSafeUser = isBusinessSafeUser }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult IsEmployeeAbleToCompleteReviewTask(Guid employeeId, long companyId)
        {
            if (employeeId == Guid.Empty)
            {
                return Json(new
                {
                    CanCompleteReviewTask = false,
                    IsUser = false
                }, JsonRequestBehavior.AllowGet); 
            }

            var employee = _employeeService.GetEmployee(employeeId, companyId);

            bool canCompleteReviewTask;
            var isUser = true;

            if (!IsBusinessSafeUser(employee))
            {
                canCompleteReviewTask = false;
                isUser = false;
            }
            else
            {
                canCompleteReviewTask = employee.User.Role.Name != "GeneralUser";
            }

            return Json(new
                        {
                            CanCompleteReviewTask = canCompleteReviewTask,
                            IsUser = isUser
                        }, JsonRequestBehavior.AllowGet);
        }

        private static bool IsBusinessSafeUser(EmployeeDto employee)
        {
            return employee.User != null && employee.User.Id != Guid.Empty;
        }
    }
}