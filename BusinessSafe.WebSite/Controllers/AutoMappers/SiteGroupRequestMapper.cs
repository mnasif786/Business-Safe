using AutoMapper;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using System;

namespace BusinessSafe.WebSite.Controllers.AutoMappers
{
    public class SiteGroupRequestMapper
    {
        private readonly AutoMapperConfiguration _autoMapperConfiguration = AutoMapperConfiguration.Instance;

        public CreateUpdateSiteGroupRequest Map(SiteGroupDetailsViewModel siteGroupDetailsViewModel, Guid userId)
        {
            var request = Mapper.Map<SiteGroupDetailsViewModel, CreateUpdateSiteGroupRequest>(siteGroupDetailsViewModel);
            request.CurrentUserId = userId;
            return request;
        }
    }
}