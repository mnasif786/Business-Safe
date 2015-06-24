using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessSafe.WebSite.Areas.ActionPlans.Factories;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Areas.ActionPlans.Controllers
{
    public class ActionsController :  BaseController
    {
        private readonly ISearchActionViewModelFactory _searchActionViewModelFactory;

        public ActionsController(ISearchActionViewModelFactory searchActionViewModelFactory)
        {
            _searchActionViewModelFactory = searchActionViewModelFactory;
        }

        //
        // GET: /ActionPlans/Actions/
        [PermissionFilter(Permissions.ViewActionPlan)]
        public ActionResult Index(long actionPlanId)
        {
            var viewModel = _searchActionViewModelFactory
                            .WithActionPlanId(actionPlanId)
                            .GetViewModel();

            return View(viewModel);
        }

    }
}
