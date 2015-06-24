using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Areas.AccidentReports.Factories;

namespace BusinessSafe.WebSite.Areas.AccidentReports.Controllers
{
    [AccidentRecordCurrentTabActionFilter(AccidentRecordTabs.NextSteps)]
    [AccidentRecordContextFilter]
    public class NextStepsController : BaseController
    {
        private readonly INextStepsViewModelFactory _nextStepsViewModelFactory;

        public NextStepsController(INextStepsViewModelFactory nextStepsViewModelFactory)
        {
            _nextStepsViewModelFactory = nextStepsViewModelFactory;
        }

        [PermissionFilter(Permissions.ViewAccidentRecords)]
        public ActionResult Index(long accidentRecordId, long companyId)
        {
            return View(GetViewModel(accidentRecordId, CurrentUser.CompanyId));
        }

        [PermissionFilter(Permissions.ViewAccidentRecords)]
        public ActionResult View(long accidentRecordId, long companyId)
        {
            IsReadOnly = true;
            return View("Index", GetViewModel(accidentRecordId, CurrentUser.CompanyId));
        }

        public NextStepsViewModel GetViewModel(long accidentRecordId, long companyId)
        {
            var viewModel = _nextStepsViewModelFactory
               .WithAccidentRecordId(accidentRecordId)
               .WithCompanyId(CurrentUser.CompanyId)
               .GetViewModel();

            ViewBag.NextStepsVisible = viewModel.NextStepsVisible;
			return viewModel;
        }
		
    }
}
