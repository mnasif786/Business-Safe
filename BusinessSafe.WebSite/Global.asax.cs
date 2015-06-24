using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Areas.TaskList.ViewModels;
using BusinessSafe.WebSite.Areas.Users.ViewModels;
using BusinessSafe.WebSite.CustomModelBinders;
using BusinessSafe.WebSite.Custom_Exceptions;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.ViewModels;
using FluentValidation.Mvc;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using System;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap;

namespace BusinessSafe.WebSite
{
    public class MvcApplication : HttpApplication
    {
        
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              "UserTimesOut", // Route name
              "Account/TimedOut/{returnUrl}",
               new { controller = "Account", action = "TimedOut", returnUrl = UrlParameter.Optional }); // Parameter defaults);

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { area = string.Empty, action = "Index", id = UrlParameter.Optional } // Parameter defaults
                );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.Add(typeof(SaveUserRoleViewModel), new UserRolePermissionsRequestBinder());
            ModelBinders.Binders.Add(typeof(IEnumerable<ReassignTaskViewModel>), new BulkReassignRequestBinder());
            ModelBinders.Binders.Add(typeof(DocumentsToSaveViewModel), new SaveDocumentViewModelBinder());
            ModelBinders.Binders.Add(typeof(EmployeeChecklistGeneratorViewModel), new ChecklistGeneratorViewModelBinder());
            ModelBinders.Binders.Add(typeof(FireRiskAssessmentChecklistViewModel), new FireRiskAssessmentChecklistViewModelBinder());
            ModelBinders.Binders.Add(typeof(CopyRiskAssessmentForMultipleSitesViewModel), new CopyRiskAssessmentForMultipleSitesViewModelBinder());

            Bootstrapper.Run();

            if (Environment.MachineName != "PBSBSOSTAGE1"
                && Environment.MachineName != "PBSBS01"
                && Environment.MachineName != "PBSBS02"
                && Environment.MachineName != "PBSBS03"
                && Environment.MachineName != "PBSBS04"
                && Environment.MachineName != "PBSBS05")
            {
                HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            }

            FluentValidationModelValidatorProvider.Configure();

            Log4NetHelper.Logger.Info("Application_Start Complete");

        }

        void FormsAuthentication_OnAuthenticate(Object sender, FormsAuthenticationEventArgs e)
        {
            Log4NetHelper.Logger.Debug("Application_PostAuthenticateRequest called.");

            // Does the request require authentication
            if (RequestDoesNotRequireAuthentication())
                return;

            HttpContext httpContext = HttpContext.Current;

            string cookieName = FormsAuthentication.FormsCookieName;

            Log4NetHelper.Logger.Debug("CookieName=" + (cookieName ?? ""));

            HttpCookie authCookie = httpContext.Request.Cookies[cookieName];
            if (authCookie == null)
            {
                Log4NetHelper.Logger.Debug("AuthCookie was null");
                return;
            }

            Log4NetHelper.Logger.Debug("CookieValue=" + authCookie.Value);

            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            Log4NetHelper.Logger.Debug("Got authTicket");

            int companyId;
            if (!Int32.TryParse(authTicket.UserData, out companyId))
            {
                throw new CompanyIdNotFoundOnAuthenticationCookieException(authTicket.Name);
            }

            var userId = Guid.Parse(authTicket.Name);

            Log4NetHelper.Logger.Debug("Got userId=" + userId);

            try
            {
                Log4NetHelper.Logger.Debug("Loading user.");

                var customPrincipalFactory = ObjectFactory.GetInstance<ICustomPrincipalFactory>();
                var customPrincipal = customPrincipalFactory.Create(companyId, userId);


                // Are we impersonating the user??
                if (httpContext.Request.Cookies["Impersonate"] != null)
                {
                    customPrincipal.MarkAsImpersonatingUser();
                }

                Context.User = customPrincipal;
                Thread.CurrentPrincipal = customPrincipal;

                Log4NetHelper.Logger.Debug("User loaded.");

                // Sliding expiration on the authentication ticker
                if (FormsAuthentication.SlidingExpiration)
                {
                    var newTicket = FormsAuthentication.RenewTicketIfOld(authTicket);
                    if (newTicket != null && newTicket != authTicket)
                    {
                        Context.Response.Cookies.Remove(authTicket.Name);

                        string encryptedTicket = FormsAuthentication.Encrypt(newTicket);

                        var httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                        {
                            Domain = FormsAuthentication.CookieDomain,
                            Path = FormsAuthentication.FormsCookiePath,
                            Expires = newTicket.Expiration
                        };

                        HttpContext.Current.Response.Cookies.Add(httpCookie);
                    }
                }
            }
            catch (BusinessSafeUnauthorisedException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                Log.Add(2, string.Format("Business Safe Unauthorised Exception Encountered: {0}", ex.Message), ex);
                new BusinessSafeLogOut().LogOut(userId);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                Log.Add(2, string.Format("Post Authentication Exception Encountered: {0}", ex.Message), ex);
                throw new HttpException((int)HttpStatusCode.Unauthorized, "Unauthorized",
                                        new UserAuthenticationFilterException(
                                            string.Format("User could not be loaded successfully. User id was {0}", userId)));
            }
        }
        
        void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();
            Log.Add(ex);

            var compilationSection = (CompilationSection)System.Configuration.ConfigurationManager.GetSection(@"system.web/compilation");
            if (!compilationSection.Debug)
            {
                Response.Clear();
                var httpException = ex as HttpException;
                if (httpException != null)
                {
                    switch (httpException.GetHttpCode())
                    {
                        case (int)HttpStatusCode.NotFound:
                            Response.Redirect("~/Error404.aspx");
                            break;
                        case (int)HttpStatusCode.Unauthorized:
                            Response.Redirect("~/Error401.aspx");
                            break;
                        default:
                            Response.Redirect("~/Error.aspx");
                            break;
                    }
                }
                else
                {
                    Response.Redirect("~/Error.aspx");
                }
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));

            // hack to allow IE8 to download the help files stored as embedded resources - vl 020813
            if (RouteRequiresPublicCaching(routeData))
            {
                Response.Cache.SetCacheability(HttpCacheability.Public);
            }else {
                // Cache prevention because of back button/ajax problem.
                // amended to get ie8 https download pdf working - vl 100713
                // see: http://stackoverflow.com/questions/12840883/is-it-possible-to-remove-a-pragma-no-cache-response-header-once-it-has-been-set
                Response.Cache.SetCacheability(HttpCacheability.Private);
                Response.Cache.SetExpires(DateTime.UtcNow.AddSeconds(1));
                Response.Cache.SetNoStore();

            }

            System.Web.HttpContext.Current.Items["RequestId"] = Guid.NewGuid();
        }

        private bool RouteRequiresPublicCaching(RouteData routeData)
        {
            if (routeData == null)
            {
                return false;
            }
            
            try
            {
                var controller = routeData.GetRequiredString("controller");
                var action = routeData.GetRequiredString("action");
                return (controller == "Help" && action == "Index");
            }
            catch
            {
                // request may be for asset
                return false;
            }
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            var sessionManager = ObjectFactory.GetInstance<IBusinessSafeSessionManager>();
            sessionManager.CloseSession();
        }

        private static bool RequestDoesNotRequireAuthentication()
        {
            return IsTelerikAjaxGrid() || 
                   IsLoadBalancerPing() || 
                   IsImpersonationTrigger();
        }

        private static bool IsTelerikAjaxGrid()
        {
            return HttpContext.Current.Request.FilePath.EndsWith(".axd");
        }

        private static bool IsLoadBalancerPing()
        {
            return HttpContext.Current.Request.UserHostAddress == "172.16.1.10";
        }

        private static bool IsImpersonationTrigger()
        {
            return HttpContext.Current.Request.Path == "/AutoLogInFromPeninsula/Index";
        }
    }
}