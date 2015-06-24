using System;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Users.ViewModels;

namespace BusinessSafe.WebSite.Controllers.AutoMappers
{
    public class RegisterEmployeeAsUserRequestMapper
    {
        public CreateEmployeeAsUserRequest Map(AddUsersViewModel viewModel, Guid actioningUserId)
        {
            var siteId = default(long);
            if(viewModel.SiteGroupId.HasValue) siteId = viewModel.SiteGroupId.Value;
            if(viewModel.SiteId.HasValue) siteId = viewModel.SiteId.Value;

            return new CreateEmployeeAsUserRequest
                       {
                           EmployeeId = viewModel.EmployeeId,
                           NewUserId = viewModel.UserId,
                           CompanyId = viewModel.CompanyId,
                           RoleId = viewModel.RoleId,
                           SiteId = siteId,
                           ActioningUserId = actioningUserId,
                           MainSiteId = viewModel.MainSiteId
                       };
        }
    }
}