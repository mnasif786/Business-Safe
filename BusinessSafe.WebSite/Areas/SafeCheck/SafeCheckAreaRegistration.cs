using System.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.SafeCheck
{
    public class SafeCheckAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SafeCheck";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SafeCheck_default",
                "SafeCheck/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            ); 
            
            context.MapRoute(
               "SafeCheck_Index",
               "SafeCheck",
               new { action = "Index", controller = "SafeCheck" }
               );
        }
    }
}
