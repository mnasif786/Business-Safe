using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.Extensions
{
    public static class CustomPrincipalExtensions
    {
        public static ICustomPrincipal GetUser(this HttpContextBase httpContextBase)
        {
            return httpContextBase.User as ICustomPrincipal;
        }

        public static ICustomPrincipal GetCustomPrinciple(this IPrincipal principal)
        {
            return principal as ICustomPrincipal;
        }

        public static long GetUsersCompanyId(this IPrincipal principal)
        {
            return ((ICustomPrincipal)principal).CompanyId;
        }

        public static long GetUsersCompanyId(this HtmlHelper helper, IPrincipal user)
        {
            return GetUsersCompanyId(user);
        }
    }
}