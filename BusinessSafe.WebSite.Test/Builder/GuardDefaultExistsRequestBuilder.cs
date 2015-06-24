using BusinessSafe.Application.Implementations;
using BusinessSafe.Application.Request;

namespace BusinessSafe.WebSite.Tests.Builder
{
    public class GuardDefaultExistsRequestBuilder
    {
        private string _name;
        private long _companyId;
        private long _companyDefaultId;

        public static GuardDefaultExistsRequestBuilder Create()
        {
            return new GuardDefaultExistsRequestBuilder();
        }

        public GuardDefaultExistsRequest Build()
        {
            return new GuardDefaultExistsRequest(_name, _companyDefaultId, _companyId);
        }

        public GuardDefaultExistsRequestBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public GuardDefaultExistsRequestBuilder WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public GuardDefaultExistsRequestBuilder WithCompanyDefaultId(long companyDefaultId)
        {
            _companyDefaultId = companyDefaultId;
            return this;
        }
    }
}