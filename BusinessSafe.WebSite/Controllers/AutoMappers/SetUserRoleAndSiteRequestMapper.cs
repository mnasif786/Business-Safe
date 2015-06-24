using System;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Users.ViewModels;

namespace BusinessSafe.WebSite.Controllers.AutoMappers
{
    public class SetUserRoleAndSiteRequestMapper
    {
        public SetUserRoleAndSiteRequest Map(AddUsersViewModel viewModel, Guid actioningUserId)
        {
            var siteId = default(long);
            if(viewModel.SiteGroupId.HasValue) siteId = viewModel.SiteGroupId.Value;
            if(viewModel.SiteId.HasValue) siteId = viewModel.SiteId.Value;
            if(viewModel.PermissionsApplyToAllSites)
            {
                siteId = viewModel.MainSiteId;
            }
            return new SetUserRoleAndSiteRequest
                       {
                           UserToUpdateId = viewModel.UserId,
                           RoleId = viewModel.RoleId,
                           SiteId = siteId,
                           ActioningUserId = actioningUserId,
                           CompanyId = viewModel.CompanyId,
                           PermissionsApplyToAllSites = viewModel.PermissionsApplyToAllSites,
                       };
        }
    }
}