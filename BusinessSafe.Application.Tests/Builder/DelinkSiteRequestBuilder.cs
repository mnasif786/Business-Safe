using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Tests.Builder
{
    public class DelinkSiteRequestBuilder
    {
        private static DelinkSiteRequest _delinkSiteViewModel;
        private long _siteId = 1;
        private long _companyId = 1;

        public static DelinkSiteRequestBuilder Create()
        {
            _delinkSiteViewModel = new DelinkSiteRequest();
            return new DelinkSiteRequestBuilder();
        }

        public DelinkSiteRequest Build()
        {
            _delinkSiteViewModel.CompanyId = _companyId;
            _delinkSiteViewModel.SiteId = _siteId;
            return _delinkSiteViewModel;
        }

        public DelinkSiteRequestBuilder WithCompanyId(int companyId)
        {
            _companyId = companyId;
            return this;
        }

        public DelinkSiteRequestBuilder WithSiteId(int siteId)
        {
            _siteId = siteId;
            return this;
        }
    }
}