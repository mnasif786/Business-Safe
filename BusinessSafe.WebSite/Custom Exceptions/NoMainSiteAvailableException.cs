using System;

namespace BusinessSafe.WebSite.Custom_Exceptions
{
    public class NoMainSiteAvailableException : ApplicationException
    {
        public NoMainSiteAvailableException(long companyId)
            : base(string.Format("No MainSite setup for company {0}.", companyId))
        {
        }
    }
}