using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Tests.Builder
{
    public class DeleteSiteGroupRequestBuilder
    {
        private static DeleteSiteGroupRequest _delinkSiteViewModel;
        private long _siteGroupId = 1;
        private long _companyId = 1;

        public static DeleteSiteGroupRequestBuilder Create()
        {
            _delinkSiteViewModel = new DeleteSiteGroupRequest();
            return new DeleteSiteGroupRequestBuilder();
        }

        public DeleteSiteGroupRequest Build()
        {
            _delinkSiteViewModel.CompanyId = _companyId;
            _delinkSiteViewModel.GroupId = _siteGroupId;
            return _delinkSiteViewModel;
        }

        public DeleteSiteGroupRequestBuilder WithCompanyId(int companyId)
        {
            _companyId = companyId;
            return this;
        }

        public DeleteSiteGroupRequestBuilder WithSiteGroupId(int siteGroupId)
        {
            _siteGroupId = siteGroupId;
            return this;
        }
    }
}