using System.Web.Mvc;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Company.Tasks;
using BusinessSafe.WebSite.Areas.Company.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Controllers
{
    public class NonEmployeeDefaultsController : BaseController
    {
        private readonly INonEmployeeService _nonEmployeeService;
        private readonly INonEmployeeSaveTask _nonEmployeeSaveTask;

        public NonEmployeeDefaultsController(INonEmployeeService nonEmployeeService, INonEmployeeSaveTask nonEmployeeSaveTask)
        {
            _nonEmployeeService = nonEmployeeService;
            _nonEmployeeSaveTask = nonEmployeeSaveTask;
        }

        [HttpPost]
        [PermissionFilter(Permissions.DeleteCompanyDefaults)]
        public JsonResult MarkNonEmployeeAsDeleted(long companyDefaultId, long companyId)
        {
            _nonEmployeeService.MarkNonEmployeeAsDeleted(new MarkNonEmployeeAsDeletedRequest(companyDefaultId, companyId, CurrentUser.UserId));
            return Json(new { Success = true, Id = companyDefaultId });
        }

        [PermissionFilter(Permissions.ViewCompanyDefaults)]
        public PartialViewResult EditNonEmployee(long nonEmployeeId, long companyId)
        {
            var nonEmployeeDto = _nonEmployeeService.GetNonEmployee(nonEmployeeId, companyId);
            return PartialView("_AddNonEmployee", new NonEmployeeViewModel(nonEmployeeDto, companyId));
        }


        [HttpPost]
        public JsonResult CreateNonEmployee(long companyIdLink, string name, string position, string companyName, bool runMatchCheck)
        {
            var createNonEmployeeRequest = new SaveNonEmployeeRequest(0, companyIdLink, name, position, companyName, runMatchCheck, CurrentUser.UserId);
            return SaveNonEmployee(createNonEmployeeRequest);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditCompanyDefaults)]
        public JsonResult UpdateNonEmployee(long nonEmployeeId, long companyIdLink, string name, string position, string companyName, bool runMatchCheck)
        {
            var updateNonEmployeeRequest = new SaveNonEmployeeRequest(nonEmployeeId, companyIdLink, name, position, companyName, runMatchCheck, CurrentUser.UserId);
            return SaveNonEmployee(updateNonEmployeeRequest);
        }

        private JsonResult SaveNonEmployee(SaveNonEmployeeRequest saveNonEmployeeRequest)
        {
            var result = _nonEmployeeSaveTask.Execute(saveNonEmployeeRequest);
            if (!result.Success)
            {
                return Json(new { Success = false, result.Message, result.Matches });
            }

            return Json(new { Success = true, NonEmployeeId = result.Id });
        }
    }
}
