using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BusinessSafe.Checklists.CustomModelBinders;
using BusinessSafe.Checklists.ViewModels;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap;

namespace BusinessSafe.Checklists
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "DefaultEmployeeChecklist", // Route name
                "{employeeChecklistId}", // URL with parameters
                new { controller = "Home", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{employeeChecklistId}", // URL with parameters
                new { controller = "Home", action = "Index", employeeChecklistId = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            Bootstrapper.Run();

            ModelBinders.Binders.Add(typeof(EmployeeChecklistViewModel), new EmployeeChecklistViewModelBinder());

            if (Environment.MachineName != "PBSBSOSTAGE1"
               && Environment.MachineName != "PBSBS01"
               && Environment.MachineName != "PBSBS02"
               && Environment.MachineName != "PBSBS03"
               && Environment.MachineName != "PBSBS04"
               && Environment.MachineName != "PBSBS05"
               && Environment.MachineName != "PBS42576")
            {
                HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            }

        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();
            Log.Add(ex);

            //Server.ClearError();
            
#if !DEBUG
            
            Response.Clear();
            var httpException = ex as System.Web.HttpException;
            if (httpException != null)
            {
                switch (httpException.GetHttpCode())
                {
                    case (int) System.Net.HttpStatusCode.NotFound:
                        Response.Redirect("~/Error404.htm");
                        break;
                    case (int) System.Net.HttpStatusCode.Unauthorized:
                        Response.Redirect("~/Error401.htm");
                        break;

                    default:
                        Response.Redirect("~/Error.htm");
                        break;
                }
            }
#endif

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var sessionManager = ObjectFactory.GetInstance<IBusinessSafeSessionManager>();
            var session = sessionManager.Session;
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            var sessionManager = ObjectFactory.GetInstance<IBusinessSafeSessionManager>();
            sessionManager.CloseSession();
        } 
    }
}