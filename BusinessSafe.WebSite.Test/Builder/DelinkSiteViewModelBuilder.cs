using BusinessSafe.WebSite.Areas.Sites.ViewModels;

namespace BusinessSafe.WebSite.Tests.Builder
{
    public class DelinkSiteViewModelBuilder
    {
        private static DelinkSiteViewModel _delinkSiteViewModel;
        private long _siteId  = 1;
        private long _companyId = 1;

        public static DelinkSiteViewModelBuilder Create()
        {
            _delinkSiteViewModel = new DelinkSiteViewModel();
            return new DelinkSiteViewModelBuilder();
        }

        public DelinkSiteViewModel Build()
        {
            _delinkSiteViewModel.ClientId = _companyId;
            _delinkSiteViewModel.SiteId = _siteId;
            return _delinkSiteViewModel;
        }

        public DelinkSiteViewModelBuilder WithCompanyId(int companyId)
        {
            _companyId = companyId;
            return this;
        }

        public DelinkSiteViewModelBuilder WithSiteId(int siteId)
        {
            _siteId = siteId;
            return this;
        }
    }
}