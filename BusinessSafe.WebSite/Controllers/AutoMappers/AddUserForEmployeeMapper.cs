using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Employees.ViewModels;

namespace BusinessSafe.WebSite.Controllers.AutoMappers
{
    public class AddUserForEmployeeMapper
    {
        public static CreateEmployeeAsUserRequest Map(EmployeeViewModel viewModel, Guid actioningUserId)
        {
            var siteId = default(long);
            if (viewModel.UserSiteGroupId.HasValue) siteId = viewModel.UserSiteGroupId.Value;
            if (viewModel.UserSiteId.HasValue) siteId = viewModel.UserSiteId.Value;

            if (!viewModel.EmployeeId.HasValue)
                throw new Exception("Invalid Field - EmployeeId undefined");

            if (!viewModel.UserRoleId.HasValue)
                throw new Exception("Invalid Field - UserRoleId undefined");

            return new CreateEmployeeAsUserRequest
            {
                EmployeeId = viewModel.EmployeeId.Value,
                NewUserId = viewModel.UserId,
                CompanyId = viewModel.CompanyId,
                RoleId = viewModel.UserRoleId.Value,
                SiteId = siteId,
                ActioningUserId = actioningUserId,
                MainSiteId = siteId, // ?? Is this right??
                EmployeeContact = new EmployeeContactDetail() { Telephone1 = viewModel.Telephone, Email = viewModel.Email },
                PermissionsForAllSites = viewModel.UserPermissionsApplyToAllSites
            };
        }     
    }
}