using System;
using System.Web;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.Helpers
{
    public interface ILogOut
    {
        void LogOut(Guid userId);
    }

    public class BusinessSafeLogOut: ILogOut
    {

        public void LogOut(Guid userId)
        {
            RemoveImpersonateIndicatorCookie();
            RemoveUserFromCache(userId);
            SignOutFromFormsAuthentication();
        }

        private void RemoveImpersonateIndicatorCookie()
        {
            if (HttpContext.Current.Request.Cookies["Impersonate"] != null)
            {
                var impersonateDisplayIndicatorCookie = new HttpCookie("Impersonate");
                impersonateDisplayIndicatorCookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(impersonateDisplayIndicatorCookie);
            }
        }

        private void RemoveUserFromCache(Guid userId)
        {
            var key = "User:" + userId;
            var cacheHelper = new CacheHelper();
            cacheHelper.Clear(key);
        }

        private static void SignOutFromFormsAuthentication()
        {
            var formsAuthenticationService = new FormsAuthenticationService();
            formsAuthenticationService.SignOut();
        }

    }
}