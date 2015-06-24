using System;
using System.Web;
using System.Web.Mvc;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.Models;
using System.Text;

namespace BusinessSafe.WebSite.Controllers
{

    public class AccountController : BaseController
    {
        private readonly BusinessSafeSignOutLinkBaseUrlConfiguration _urlConfigurations;
        private readonly ILogOut _logout;

        public AccountController(BusinessSafeSignOutLinkBaseUrlConfiguration urlConfigurations, ILogOut logout)
        {
            _urlConfigurations = urlConfigurations;
            _logout = logout;
        }

        public ActionResult LogOut()
        {
            _logout.LogOut(CurrentUser.UserId);
            return RedirectToLogIn();
        }

        private ActionResult RedirectToLogIn()
        {
            var url = string.Format("{0}/SignIn", _urlConfigurations.GetBaseUrl());
            return Redirect(url);
        }

        public ActionResult TimedOut(string returnUrl)
        {
            var requestUrl = Request.Url;
            var destination = new StringBuilder(_urlConfigurations.GetBaseUrl());
            destination.Append("/SignIn/TimedOut");
            destination.Append("?returnUrl=");
            destination.Append(HttpUtility.UrlEncode(string.Format("http://{0}:{1}{2}",
                                                                  requestUrl.Host,
                                                                  requestUrl.Port,
                                                                  HttpUtility.UrlDecode(returnUrl))));
            return Redirect(destination.ToString());
        }

    }
}