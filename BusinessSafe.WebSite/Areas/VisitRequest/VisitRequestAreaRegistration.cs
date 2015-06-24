using System.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.VisitRequest
{
    public class VisitRequestAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "VisitRequest";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute( "VisitRequest_default", "VisitRequest/{controller}/{action}/{id}", new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}