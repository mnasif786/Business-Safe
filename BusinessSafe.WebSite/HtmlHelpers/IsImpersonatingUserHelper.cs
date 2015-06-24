using System.Web;
using System.Web.Mvc;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.HtmlHelpers
{
    public static class IsImpersonatingUserHelper
    {
        public static bool IsImpersonatingUser(this HtmlHelper helper)
        {
            var user = HttpContext.Current.User as CustomPrincipal;

            if (user != null)
            {
                return user.IsImpersonatingUser;
            }

            return false;
        }
    }
}