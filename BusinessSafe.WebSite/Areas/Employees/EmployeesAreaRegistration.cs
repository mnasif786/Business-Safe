using System.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.Employees
{
    public class EmployeesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Employees"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Employees_default", "Employees/{controller}/{action}/{id}", new { action = "Index", controller = "Employees", id = UrlParameter.Optional });
        }
    }
}