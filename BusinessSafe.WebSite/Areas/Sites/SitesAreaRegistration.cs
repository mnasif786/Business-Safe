using System.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.Sites
{
    public class SitesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Sites";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               "Sites_default",
               "Sites/{controller}/{action}/{id}",
                new { action = "Index", controller = "SitesStructure", id = UrlParameter.Optional }
           );

            context.MapRoute(
                "Sites_index",
                "Sites",
                 new { controller = "SitesStructure", action = "Index", id = UrlParameter.Optional });     
        }
    }
}
