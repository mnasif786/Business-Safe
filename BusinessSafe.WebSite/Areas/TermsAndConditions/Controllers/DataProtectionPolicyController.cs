using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Areas.TermsAndConditions.Controllers
{
    public class DataProtectionPolicyController : BaseController
    {        
        [PermissionFilter(Permissions.ViewSiteDetails)]
        public ActionResult Index()
        {
            return View();
        }

    }
}
