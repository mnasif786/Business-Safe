using System.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceInventory
{
    public class HazardousSubstanceInventoryAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "HazardousSubstanceInventory";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HazardousSubstanceInventory_default",
                "HazardousSubstanceInventory/{controller}/{action}/{id}",
                new { action = "Index", controller = "Inventory", id = UrlParameter.Optional }
            );
        }
    }
}