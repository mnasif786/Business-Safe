using System.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.Company
{
    public class CompanyAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Company"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Company_default", "Company/{controller}/{action}/{id}", new { action = "Index", controller = "Company", id = UrlParameter.Optional });
        }
    }
}