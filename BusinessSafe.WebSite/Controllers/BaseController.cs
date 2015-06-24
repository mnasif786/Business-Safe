using System.Linq;
using System.Web.Mvc;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Controllers
{
    [Authorize]
    [UrlHackingFilter]
    public class BaseController : Controller
    {
        public CustomPrincipal CurrentUser
        {
            get
            {
                if (User == null)
                {
                    throw new CurrentUserNotSetException();
                }
                return User as CustomPrincipal;
            }
        }

        protected bool IsReadOnly
        {
            get
            {
                if (ViewBag.IsReadOnly == null)
                    return false;

                return ViewBag.IsReadOnly;
            }

            set { ViewBag.IsReadOnly = value; }
        }

        protected JsonResult ModelStateErrorsAsJson()
        {
            return Json(new { Success = "false", Errors = ModelState.GetErrorMessages().Distinct().ToArray() });
        }

        protected JsonResult ModelStateErrorsWithKeysAsJson()
        {
            var errorList = ModelState.Keys.ToList()
                .Select(k => new { PropertyName = k, Errors = ModelState[k].Errors })
                .Where(e => e.Errors.Count > 0);
            return Json(new { Success = "false", Errors = errorList });
        }
    }
}