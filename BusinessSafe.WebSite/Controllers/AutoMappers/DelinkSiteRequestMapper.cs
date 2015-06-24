using AutoMapper;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Sites.ViewModels;

namespace BusinessSafe.WebSite.Controllers.AutoMappers
{
    public class DelinkSiteRequestMapper
    {
        private readonly AutoMapperConfiguration _autoMapperConfiguration = AutoMapperConfiguration.Instance;
        
        public DelinkSiteRequest Map(DelinkSiteViewModel deleteSiteGroupViewModel)
        {
            return Mapper.Map<DelinkSiteViewModel, DelinkSiteRequest>(deleteSiteGroupViewModel);
        }
    }
}