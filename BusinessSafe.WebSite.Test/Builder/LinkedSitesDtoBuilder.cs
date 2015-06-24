using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.WebSite.Tests.Builder
{
    public class LinkedSitesDtoBuilder
    {
        private static long _siteId;
        private static string _name;

        public static LinkedSitesDtoBuilder Create()
        {
            _siteId = 0;
            _name = default(string);
            return new LinkedSitesDtoBuilder();
        }

        public SiteDto Build()
        {
            var linkedSitesDto = new SiteDto { Id = _siteId, Name = _name };

            return linkedSitesDto;
        }

        public LinkedSitesDtoBuilder WithId(long id)
        {
            _siteId = id;
            return this;
        }

        public LinkedSitesDtoBuilder WithName(string name)
        {
            _name = name;
            return this;
        }
    }
}