using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.ActionPlans
{
    public class ActionPlanAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ActionPlans";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
             "ActionPlans_root",
             "ActionPlans/",
             new { action = "Index", controller = "ActionPlan" }
             );

            context.MapRoute(
                "ActionPlans_default",
                "ActionPlans/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}