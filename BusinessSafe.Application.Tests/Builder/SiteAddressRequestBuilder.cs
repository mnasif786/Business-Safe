using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Tests.Builder
{
    public class SiteAddressRequestBuilder
    {
        public static long Id { get; private set; }
        public static long? SiteId { get; private set; }
        public static long? ParentId { get; private set; }
        public static long ClientId { get; private set; }
        public static string Name { get; private set; }
        public static string Reference { get; private set; }
        public static string AddreessLine1 { get; private set; }
        public static string AddreessLine2 { get; private set; }
        public static string AddreessLine3 { get; private set; }
        public static string AddreessLine4 { get; private set; }
        public static string AddreessLine5 { get; private set; }
        public static string County { get; private set; }
        public static long LinkToSiteGroupId { get; set; }
        public static long LinkToSiteId { get; set; }

        public static SiteAddressRequestBuilder Create()
        {
            Id = 0;
            SiteId = 0;
            ParentId = null;
            ClientId = 0;
            LinkToSiteId = 0;
            LinkToSiteGroupId = 0;
            Name = default(string);
            Reference = default(string);
            AddreessLine1 = default(string);
            AddreessLine2 = default(string);
            AddreessLine3 = default(string);
            AddreessLine4 = default(string);
            AddreessLine5 = default(string);
            County = default(string);

            return new SiteAddressRequestBuilder();
        }

        public CreateUpdateSiteRequest Build()
        {
            return new CreateUpdateSiteRequest(Id, SiteId, ParentId, ClientId, Name, Reference, AddreessLine1, AddreessLine2, AddreessLine3, AddreessLine4,AddreessLine5, County, false) { LinkToSiteGroupId = LinkToSiteGroupId, LinkToSiteId = LinkToSiteId };
        }

        public SiteAddressRequestBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public SiteAddressRequestBuilder WithAddressLine1(string addreessLine1)
        {
            AddreessLine1 = addreessLine1;
            return this;
        }

        public SiteAddressRequestBuilder WithId(long id)
        {
            Id = id;
            return this;
        }

        public SiteAddressRequestBuilder WithClientId(long clientId)
        {
            ClientId = clientId;
            return this;
        }

        public SiteAddressRequestBuilder WithSiteId(long siteId)
        {
            SiteId = siteId;
            return this;
        }

        public SiteAddressRequestBuilder WithParentId(long parentId)
        {
            ParentId = parentId;
            return this;
        }

        public SiteAddressRequestBuilder WithReference(string reference)
        {
            Reference = reference;
            return this;
        }

        public SiteAddressRequestBuilder WithLinkToSiteGroupId(int linkToSiteGroupId)
        {
            LinkToSiteGroupId = linkToSiteGroupId;
            return this;
        }

        public SiteAddressRequestBuilder WithLinkToSiteId(int linkToSiteId)
        {
            LinkToSiteId = linkToSiteId;
            return this;
        }
    }
}