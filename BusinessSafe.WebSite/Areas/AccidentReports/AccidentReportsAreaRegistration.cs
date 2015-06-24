using System.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.AccidentReports
{
    public class AccidentReportsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AccidentReports";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AccidentReports_default",
                "AccidentReports/{controller}/{action}/{id}",
                new {action = "Index", controller = "Search", id = UrlParameter.Optional}
                );

            context.MapRoute(
                "AccidentReports_Index",
                "AccidentReports",
                new {action = "Index", controller = "Search"}
                );

            context.MapRoute(
                "AccidentRecords_Index",
                "AccidentRecords",
                new {action = "Index", controller = "Search"}
                );
        }
    }
}
