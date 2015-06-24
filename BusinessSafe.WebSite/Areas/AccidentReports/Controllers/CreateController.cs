using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using FluentValidation;

namespace BusinessSafe.WebSite.Areas.AccidentReports.Controllers
{
    [AccidentRecordCurrentTabActionFilter(AccidentRecordTabs.Summary)]
    public class CreateController : BaseController
    {
        private readonly IAccidentRecordService _accidentRecordService;

        public CreateController(IAccidentRecordService accidentRecordService)
        {
            _accidentRecordService = accidentRecordService;
        }

        [PermissionFilter(Permissions.ViewAccidentRecords)]
        public ActionResult Index(long companyId)
        {
            var accidentRecordSummaryViewModel = new CreateAccidentRecordSummaryViewModel
            {
                CompanyId = companyId
            };
            return View("Index",accidentRecordSummaryViewModel);
        }


        [HttpPost]
        [PermissionFilter(Permissions.AddAccidentRecords)]
        public ActionResult Save(CreateAccidentRecordSummaryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var accidentRecordId = default(long);

            try
            {
                accidentRecordId = _accidentRecordService.CreateAccidentRecord(new SaveAccidentRecordSummaryRequest
                {
                    CompanyId = CurrentUser.CompanyId,
                    UserId = CurrentUser.UserId,
                    Title = model.Title,
                    Reference = model.Reference,
                    JurisdictionId = model.JurisdictionId.Value
                });

            }
            catch (ValidationException validationException)
            {
                ModelState.AddValidationErrors(validationException);
                return View("Index", model);
            }

            return RedirectToAction("Index", "InjuredPerson", new { accidentRecordId, companyId = CurrentUser.CompanyId });
        }
    }
}
