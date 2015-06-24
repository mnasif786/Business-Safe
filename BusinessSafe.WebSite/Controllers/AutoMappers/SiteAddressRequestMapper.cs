using AutoMapper;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using System;

namespace BusinessSafe.WebSite.Controllers.AutoMappers
{
    public class SiteAddressRequestMapper
    {
        private readonly AutoMapperConfiguration _autoMapperConfiguration = AutoMapperConfiguration.Instance;

        public CreateUpdateSiteRequest Map(SiteDetailsViewModel siteDetailsViewModel, Guid currentUserId)
        {
            var request = Mapper.Map<SiteDetailsViewModel, CreateUpdateSiteRequest>(siteDetailsViewModel);
            request.CurrentUserId = currentUserId;
            return request;
        }

        public static long? GetParentId(SiteDetailsViewModel siteDetailsViewModel)
        {
            return
                new ParentIdFinder(siteDetailsViewModel.LinkToSiteId, siteDetailsViewModel.LinkToGroupId).GetLinkId();
        }
    }

}