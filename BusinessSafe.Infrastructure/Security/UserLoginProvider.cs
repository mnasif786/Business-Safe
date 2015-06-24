using System.Security.Principal;
using System.ServiceModel;
using System.Web;

using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Infrastructure.Security
{
    public class UserLoginProvider : IUserLoginProvider
    {
        [CoverageExclude]
        public string GetUserLogin()
        {
            if (OperationContext.Current != null
                && OperationContext.Current.IncomingMessageHeaders != null
                && OperationContext.Current.IncomingMessageHeaders.FindHeader("Username", "Peninsula.Common") > -1
                && OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("Username", "Peninsula.Common") != null)
            {
                return OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("Username", "Peninsula.Common");
            }
            if (HttpContext.Current != null
                && HttpContext.Current.User != null
                    && HttpContext.Current.User.Identity.Name != null)
            {
                return HttpContext.Current.User.Identity.Name;
            }
            return WindowsIdentity.GetCurrent().Name;
        }
    }
}
