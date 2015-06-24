using System;
using System.Net;
using System.Web;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Custom_Exceptions
{
    public class BusinessSafeUnauthorisedException : HttpException
    {
        public BusinessSafeUnauthorisedException(Guid userId):base((int)HttpStatusCode.Unauthorized,"Unauthorized",new UserAuthenticationFilterException(string.Format("User marked as deleted. User id was {0}", userId))){ }
        
    }
}