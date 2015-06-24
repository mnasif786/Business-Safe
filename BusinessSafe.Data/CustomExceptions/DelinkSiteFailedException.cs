using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class DelinkSiteFailedException : ApplicationException
    {
        public DelinkSiteFailedException(long siteId, long companyId)
            : base (string.Format("Delink site command failed. No rows deleted for site id {0} and company id {1}",siteId,companyId))
        {
        }
    }
}
