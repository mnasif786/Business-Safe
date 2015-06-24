using System.Web.Mvc;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.Filters
{
    public class UrlHackingFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionParameters = filterContext.ActionParameters;

            if (actionParameters.ContainsKey("companyId"))
            {
                var user = filterContext.HttpContext.User as ICustomPrincipal;
                if (user == null)
                {
                    filterContext.Result = new HttpUnauthorizedResult("CustomPrinciple is currently null.");
                    return;
                }
                
                var urlCompanyId = long.Parse(actionParameters["companyId"].ToString());
                if (urlCompanyId != 0 && user.CompanyId != urlCompanyId) //this check should be redundant because the company id should be obtained from the user and unauthorisation must be checked in the application layer
                {
                    filterContext.Result = new HttpUnauthorizedResult("CustomPrinciple's company id is different to the company id specified in the requesting url.");
                    return;
                }
            }
        }
    }
}