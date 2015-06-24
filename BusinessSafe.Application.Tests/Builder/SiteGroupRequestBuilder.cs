using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Tests.Builder
{
    public class SiteGroupRequestBuilder
    {
        private static long _groupId;
        private static long _clientId;
        private static string _name;
        private static long? _linkToSiteId;
        private static long? _linkToGroupId;

        public static SiteGroupRequestBuilder Create()
        {
            _groupId = 0;
            _name = default(string);
            return new SiteGroupRequestBuilder();
        }

        public CreateUpdateSiteGroupRequest Build()
        {
            return new CreateUpdateSiteGroupRequest()
            {
                GroupId = _groupId,
                Name = _name, 
                LinkToSiteId = _linkToSiteId, 
                LinkToGroupId = _linkToGroupId
            };
        }

        public SiteGroupRequestBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public SiteGroupRequestBuilder WithGroupId(long groupId)
        {
            _groupId = groupId;
            return this;
        }

        public SiteGroupRequestBuilder WithLinkToSiteId(long? linkToSiteId)
        {
            _linkToSiteId = linkToSiteId;
            return this;
        }

        public SiteGroupRequestBuilder WithLinkToGroupId(long? linkToGroupId)
        {
            _linkToGroupId = linkToGroupId;
            return this;
        }

        public SiteGroupRequestBuilder WithClientId(long clientId)
        {
            _clientId = clientId;
            return this;
        }
    }
}