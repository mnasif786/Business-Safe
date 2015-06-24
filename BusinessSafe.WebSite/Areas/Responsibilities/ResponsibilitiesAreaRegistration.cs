using System.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.Responsibilities
{
    public class ResponsibilitiesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Responsibilities";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Responsibilities_default",
                "Responsibilities/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
