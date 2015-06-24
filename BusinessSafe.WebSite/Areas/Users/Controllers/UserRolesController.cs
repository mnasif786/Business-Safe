using System;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.WebSite.Areas.Users.Factories;
using BusinessSafe.WebSite.Areas.Users.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Areas.Users.Controllers
{
    public class UserRolesController : BaseController
    {
        private readonly IUserRolesViewModelFactory _userRolesViewModelFactory;
        private readonly IUserRolePermissionsViewModelFactory _userRolePermissionsViewModelFactory;
        private readonly IRolesService _rolesService;

        public UserRolesController(IRolesService rolesService, IUserRolesViewModelFactory userRolesViewModelFactory, IUserRolePermissionsViewModelFactory userRolePermissionsViewModelFactory)
        {
            _userRolesViewModelFactory = userRolesViewModelFactory;
            _userRolePermissionsViewModelFactory = userRolePermissionsViewModelFactory;
            _rolesService = rolesService;
        }

        [PermissionFilter(Permissions.ViewUsers)]
        public ActionResult Index(long companyId, string roleId)
        {
            var viewModel = _userRolesViewModelFactory
                                .WithCompanyId(companyId)
                                .WithRoleId(roleId)
                                .GetViewModel();
            return View(viewModel);
        }

        [PermissionFilter(Permissions.AddUsers)]
        public ActionResult New(long companyId)
        {
            var viewModel = _userRolesViewModelFactory
                                .WithCompanyId(companyId)
                                .GetViewModelForNewUseRole();
            return View("Index", viewModel);
        }

        [PermissionFilter(Permissions.ViewUsers)]
        public PartialViewResult GetUserRolePermissions(long companyId, string roleId, bool enableCustomRoleEditing)
        {
            var viewModel = _userRolePermissionsViewModelFactory
                                .WithCompanyId(companyId)
                                .WithRoleId(roleId)
                                .WithEnableCustomRoleEditing(enableCustomRoleEditing)
                                .GetViewModel();
            return PartialView("_UserRolePermissions", viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddUsers)]
        public JsonResult CreateUserRole(SaveUserRoleViewModel viewModel)
        {

            var result = _rolesService.Add(new AddUserRoleRequest()
                                   {
                                       CompanyId = viewModel.CompanyId,
                                       RoleName = viewModel.RoleName,
                                       Permissions = viewModel.Permisssions.Select(int.Parse).ToArray(),
                                       UserId = CurrentUser.UserId
                                   });

            if (!result.Success)
            {
                return Json(new SaveUserRoleResultViewModel { Success = false });
            }

            return Json(new SaveUserRoleResultViewModel { Success = true, RoleId = result.RoleId.ToString() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditUsers)]
        public JsonResult UpdateUserRole(SaveUserRoleViewModel viewModel)
        {
            _rolesService.Update(new UpdateUserRoleRequest()
            {
                CompanyId = viewModel.CompanyId,
                Permissions = viewModel.Permisssions.Select(int.Parse).ToArray(),
                UserId = CurrentUser.UserId,
                RoleId = Guid.Parse(viewModel.RoleId),
                RoleName = viewModel.RoleName
            });

            return Json(new SaveUserRoleResultViewModel { Success = true }, JsonRequestBehavior.AllowGet); 
        }

        [HttpPost]
        [PermissionFilter(Permissions.DeleteUsers)]
        public JsonResult MarkUserRoleAsDeleted(long companyId, string userRoleId)
        {
            if (companyId == 0 || string.IsNullOrEmpty(userRoleId))
            {
                return Json(new DeleteUserRoleResultViewModel
                {
                    Success = false,
                    Message = "Invalid UserRoleId and CompanyId"
                });
            }

            try
            {
                _rolesService.MarkUserRoleAsDeleted(new MarkUserRoleAsDeletedRequest()
                {
                    CompanyId = companyId,
                    UserRoleId = Guid.Parse(userRoleId),
                    UserId = CurrentUser.UserId
                });

                return Json(new DeleteUserRoleResultViewModel { Success = true });
            }
            catch (AttemptingToDeleteRoleCurrentlyUsedByUsersException)
            {
                return Json(new DeleteUserRoleResultViewModel
                {
                    Success = false,
                    Message = "Role is currently in use and can not be deleted."
                });
            }
        }

        [PermissionFilter(Permissions.ViewUsers)]
        public JsonResult GetUsersWithRole(long companyId, string roleId)
        {
            var usersWithRole = _rolesService.GetUsersWithRole(Guid.Parse(roleId), companyId);
            return Json(new UsersWithUserRoleResultViewModel
                            {
                                Users = usersWithRole.Select(x => x.Employee.FullName).ToList()
                            }, JsonRequestBehavior.AllowGet); 
        }
    }
}