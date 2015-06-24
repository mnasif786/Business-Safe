using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using EvaluationChecklist.App_Start;
using EvaluationChecklist.Helpers;
using NServiceBus;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using SafeCheckSpike;
using StructureMap;
using log4net;

namespace EvaluationChecklist
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static IBus Bus;

        protected void Application_Start()
        {
            Bus = Configure.With()
                .DefaultBuilder()
                .DBSubcriptionStorage()
                .Log4Net()
                .UnicastBus()
                .MsmqTransport()
                .XmlSerializer()
                .DisableRavenInstall()
                .CreateBus()
                .Start(
                    () =>
                    Configure.Instance.ForInstallationOn<NServiceBus.Installation.Environments.Windows>().Install());

            AreaRegistration.RegisterAllAreas();
            IocConfig.Setup();
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            if (Environment.MachineName.ToUpper() == "PBS42691")
            {
                HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            }


            log4net.Config.XmlConfigurator.Configure();

            LogManager.GetLogger(typeof(MvcApplication)).Info("Application Started");
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { area = string.Empty, action = "Index", id = UrlParameter.Optional } // Parameter defaults
                );
        }

        void Application_EndRequest(object sender, EventArgs e)
        {
            var sessionManager = ObjectFactory.GetInstance<IBusinessSafeSessionManager>();
            sessionManager.CloseSession();
        }
    }
}