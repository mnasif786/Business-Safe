using System;

namespace BusinessSafe.WebSite.Custom_Exceptions
{
    public class CompanyIdNotFoundOnAuthenticationCookieException : ApplicationException
    {
        public CompanyIdNotFoundOnAuthenticationCookieException(string userId)
            : base(string.Format("No Company Id found in Authentication Cookie. User Id {0}.", userId))
        {
        }
    }
}