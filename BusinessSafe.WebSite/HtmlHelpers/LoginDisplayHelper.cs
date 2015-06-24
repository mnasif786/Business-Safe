using System;
using System.Web;
using System.Web.Mvc;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.HtmlHelpers
{
    public static class LoginDisplayHelper
    {
        public static MvcHtmlString GetLoginName(this HtmlHelper helper)
        {
            var user = HttpContext.Current.User as CustomPrincipal;

            if(user != null)
            {
                return new MvcHtmlString(user.FullName);
            }
            
            return new MvcHtmlString(String.Empty);
        }

        public static MvcHtmlString GetLoginCompanyName(this HtmlHelper helper)
        {
            var user = HttpContext.Current.User as CustomPrincipal;

            if (user != null)
            {
                return new MvcHtmlString(user.CompanyName);
            }

            return new MvcHtmlString(String.Empty);
        }
    }
}