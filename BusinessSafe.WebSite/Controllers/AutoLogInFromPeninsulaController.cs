using System;
using System.Configuration;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.Domain.InfrastructureContracts.Logging;

namespace BusinessSafe.WebSite.Controllers
{
    public class AutoLogInFromPeninsulaController : Controller
    {
        
        public ActionResult Index(string companyId, string userId)
        {
            Log.Add("Impersonate Logging in with companyId=" + (companyId ?? "") + ", userId=" + (userId ?? ""));

            var formsAuthenticationService = new FormsAuthenticationService();
            formsAuthenticationService.SignOut();

            var impersonateSettings = CreateImpersonateGuardSettings();
            var allowedToImpersonate= new ImpersonateGuard(impersonateSettings).IsAllowed(Request.UrlReferrer);
            if (allowedToImpersonate == false)
                return null;

            AddCookieToShowWeAreInImpersonateMode();

            var formsauthenticationService = new FormsAuthenticationService();
            formsauthenticationService.SignIn(userId, true, companyId.ToString(CultureInfo.InvariantCulture));

            new CacheHelper().RemoveUser(Guid.Parse(userId));

            Log.Add("Impersonate Logged in.");
            
            // Hack for general user testing because not got home page for this user
            // This gapping hole has to change anyway 
            if (userId == "E7385B71-ABFC-400A-8FB0-CC58ACA78E38")
            {
                return RedirectToAction("Index", "Company", new { id = companyId, area = "Company" });    
            }

            return RedirectToAction("Index", "TaskList", new { area = "TaskList" });
        }
        
        private void AddCookieToShowWeAreInImpersonateMode()
        {
            var impersonateHttpCookie = new HttpCookie("Impersonate", Guid.NewGuid().ToString());
            Response.Cookies.Add(impersonateHttpCookie);
        }

        public ActionResult Test()
        {
            return View();
        }

        private ImpersonateGuardSettings CreateImpersonateGuardSettings()
        {
            return new ImpersonateGuardSettings()
                       {
                           IsImpersonateOn = ConfigurationManager.AppSettings["AllowAutoLogin"],
                           Environment = ConfigurationManager.AppSettings["config_file"],
                           AllowedUrlReferrerHost = ConfigurationManager.AppSettings["Impersonate_Allowed_Url_Referrer_Host"]
                       };
        }
    }
}