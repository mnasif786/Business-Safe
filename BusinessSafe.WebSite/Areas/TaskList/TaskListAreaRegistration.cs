using System.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.TaskList
{
    public class TaskListAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "TaskList";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               "TaskList_default",
               "TaskList/{controller}/{action}/{id}",
               new { action = "Index", controller = "TaskList", id = UrlParameter.Optional }
           );

            context.MapRoute(
                "Website_Default",
                "",
                new { controller = "TaskList", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
