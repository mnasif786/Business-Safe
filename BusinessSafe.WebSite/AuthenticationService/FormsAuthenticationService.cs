using System;
using System.Globalization;
using System.Web;
using System.Web.Security;
using BusinessSafe.Domain.InfrastructureContracts.Logging;

namespace BusinessSafe.WebSite.AuthenticationService
{
    public sealed class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            SignIn(userName, createPersistentCookie, String.Empty);
        }

        public void SignIn(string userName, bool createPersistentCookie, string userData)
        {
            Log.Add("Logging in with userName " + (userName ?? "") + ", createPersistentCookie=" +
                    createPersistentCookie.ToString(CultureInfo.InvariantCulture) + ", userData=" + (userData ?? ""));

            var cookieExpiration = DateTime.Now.AddMinutes(HttpContext.Current.Session.Timeout); //.AddSeconds(30);

            var ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, cookieExpiration,
                                                       createPersistentCookie, userData);
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            string domain = GetCookieDomain();

            var httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                                 {
                                     Domain = domain,
                                     Path = FormsAuthentication.FormsCookiePath,
                                     Expires = cookieExpiration,
                                 };
            HttpContext.Current.Response.Cookies.Add(httpCookie);

            Log.Add("Cookie added with name=" + (FormsAuthentication.FormsCookieName ?? ""));
        }

        private static string GetCookieDomain()
        {
            return FormsAuthentication.CookieDomain;
        }

        public string LoginUrl
        {
            get { return FormsAuthentication.LoginUrl; }
        }

        public void RedirectToLoginPage()
        {
            FormsAuthentication.RedirectToLoginPage();
        }

        public void RedirectToLoginPage(string extraQueryString)
        {
            FormsAuthentication.RedirectToLoginPage(extraQueryString);
        }

        public string DefaultUrl
        {
            get { return FormsAuthentication.DefaultUrl; }
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}