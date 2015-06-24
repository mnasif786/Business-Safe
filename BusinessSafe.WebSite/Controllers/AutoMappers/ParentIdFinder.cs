namespace BusinessSafe.WebSite.Controllers.AutoMappers
{
    public class ParentIdFinder
    {
        private readonly long? _linkToSiteId;
        private readonly long? _linkToSiteGroupId;

        public ParentIdFinder(long? linkToSiteId, long? linkToSiteGroupId)
        {
            _linkToSiteId = linkToSiteId;
            _linkToSiteGroupId = linkToSiteGroupId;
        }

        public long? GetLinkId()
        {
            if (_linkToSiteId.HasValue && _linkToSiteId.Value > 0)
                return _linkToSiteId.Value;

            if (_linkToSiteGroupId.HasValue && _linkToSiteGroupId.Value > 0)
                return _linkToSiteGroupId.Value;

            return null;
        }

    }
}