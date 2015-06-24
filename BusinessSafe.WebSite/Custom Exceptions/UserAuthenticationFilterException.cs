using System;

namespace BusinessSafe.WebSite.Filters
{
    public class UserAuthenticationFilterException : ApplicationException
    {
        public UserAuthenticationFilterException(string message): base(message)
        {}
    }
}