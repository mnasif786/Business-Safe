using System.Web.Mvc;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.Filters
{
    public class PermissionFilterAttribute : ActionFilterAttribute
    {
        private readonly Permissions[] _permissions;

        public PermissionFilterAttribute(params Permissions[] permissions)
        {
            _permissions = permissions;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.User as ICustomPrincipal;
            foreach (var permission in _permissions)
            {
                if (user == null || !user.IsInRole(permission.ToString()))
                {
                    filterContext.Result = new HttpUnauthorizedResult("User is not authorised to access controller.");    
                    break;
                }    
            }

            base.OnActionExecuting(filterContext);
        }
    }
}