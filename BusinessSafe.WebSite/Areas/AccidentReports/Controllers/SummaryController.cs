using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.AccidentReports.Factories;
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
    [AccidentRecordContextFilter]
    public class SummaryController : BaseController
    {
        private readonly IAccidentRecordService _accidentRecordService;
        private readonly IAccidentSummaryViewModelFactory _summaryViewModelFactory;
        public SummaryController(IAccidentRecordService accidentRecordService, IAccidentSummaryViewModelFactory summaryViewModelFactory)
        {
            _accidentRecordService = accidentRecordService;
            _summaryViewModelFactory = summaryViewModelFactory;
        }
        // todo: add responsibilities permission
        [PermissionFilter(Permissions.ViewAccidentRecords)]
        public ActionResult Index(long accidentRecordId, long companyId)
        {
            var model =
                _summaryViewModelFactory
                .WithCompanyId(CurrentUser.CompanyId)
                .WithAccidentRecordId(accidentRecordId)
                .GetViewModel();

            ViewBag.NextStepsVisible = model.NextStepsVisible;
            return View(model);
        }

        [PermissionFilter(Permissions.ViewAccidentRecords)]
        public ActionResult View(long accidentRecordId, long companyId)
        {
            var model =
                _summaryViewModelFactory
                .WithCompanyId(CurrentUser.CompanyId)
                .WithAccidentRecordId(accidentRecordId)
                .GetViewModel();

            IsReadOnly = true;
            ViewBag.NextStepsVisible = model.NextStepsVisible;
            return View("Index", model);
        }


        [HttpPost]
        [PermissionFilter(Permissions.EditAccidentRecords)]
        public JsonResult SaveAndNext(AccidentSummaryViewModel model)
        {
            if (ModelState.IsValid)
            {
                UpdateAccidentRecordSummary(model);
                return Json(new { Success = true });
            }
            else
            {
                return ModelStateErrorsAsJson();
            }
        }


        [HttpPost]
        [PermissionFilter(Permissions.EditAccidentRecords)]
        public ActionResult Save(AccidentSummaryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            try
            {
                UpdateAccidentRecordSummary(model);
            }
            catch (ValidationException validationException)
            {
                ModelState.AddValidationErrors(validationException);
                return View("Index", model);
            }
            TempData["Notice"] = "Summary details successfully updated";
            return RedirectToAction("Index", "Summary", new { accidentRecordId = model.AccidentRecordId, companyId = CurrentUser.CompanyId });
        }

        private void UpdateAccidentRecordSummary(AccidentSummaryViewModel model)
        {
            _accidentRecordService.SaveAccidentRecordSummary(new SaveAccidentRecordSummaryRequest
                                                          {
                                                              CompanyId = CurrentUser.CompanyId,
                                                              AccidentRecordId = model.AccidentRecordId,
                                                              UserId = CurrentUser.UserId,
                                                              Title = model.Title,
                                                              Reference = model.Reference,
                                                              JurisdictionId = model.JurisdictionId
                                                          });
        }
    }
}
