using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class SiteGroupNotFoundException : ArgumentNullException
    {
        public SiteGroupNotFoundException(long siteGroup)
            : base(string.Format("Site group not found for site group id {0}", siteGroup))
        {
        }
    }
}