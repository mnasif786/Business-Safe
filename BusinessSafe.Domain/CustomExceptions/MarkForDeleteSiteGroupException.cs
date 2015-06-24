using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class MarkForDeleteSiteGroupException: ApplicationException
    {
        public MarkForDeleteSiteGroupException(long siteGroupId)
            : base(string.Format("Trying to mark site group for delete. Site group has children sites address or site groups. Mark for delete attempt with Site group id {0}", siteGroupId))
        {}
    }
}
