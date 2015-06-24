using System;

namespace BusinessSafe.WebSite.Custom_Exceptions
{
    public class SiteNotFoundForEmployeeException : ArgumentNullException
    {
        public SiteNotFoundForEmployeeException(Guid employeeId, long siteId)
            : base(string.Format("Site not found for Employee. Site Id {0}. Employee Id {1}", siteId, employeeId.ToString()))
        {
        }
    }
}