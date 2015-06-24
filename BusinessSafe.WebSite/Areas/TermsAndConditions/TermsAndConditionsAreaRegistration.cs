using System.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.TermsAndConditions
{
    public class TermsAndConditionsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "TermsAndConditions";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "TermsAndConditions_default",
                "TermsAndConditions/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
