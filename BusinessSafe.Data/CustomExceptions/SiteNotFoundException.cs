using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class SiteNotFoundException : ArgumentNullException
    {
        public SiteNotFoundException(long siteId)
            : base(string.Format("Site Not Found. Site not found for site id {0}", siteId))
        {
        }
    }
}