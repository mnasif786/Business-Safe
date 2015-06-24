using System.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.SqlReports
{
    public class SqlReportsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "SqlReports"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("ResponsibilityReports_default", "SqlReports/Responsibilities/Index/{filename}", new { action = "Index", controller = "Responsibilities", filename = UrlParameter.Optional });
            context.MapRoute("Reports_default", "SqlReports/{controller}/{action}/{id}", new { action = "Index", id = UrlParameter.Optional });

        }
    }
}


      