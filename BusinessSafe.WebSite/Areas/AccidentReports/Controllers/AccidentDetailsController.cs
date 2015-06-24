using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.WebSite.Areas.AccidentReports.Factories;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.Tests.Controllers.AccidentRecording.AccidentDetails;
using FluentValidation;

namespace BusinessSafe.WebSite.Areas.AccidentReports.Controllers
{
    [AccidentRecordCurrentTabActionFilter(AccidentRecordTabs.Accident)]
    [AccidentRecordContextFilter]
    public class AccidentDetailsController : BaseController
    {
        private readonly IAccidentDetailsViewModelFactory _accidentDetailsViewModelFactory;
        private readonly IAccidentRecordService _accidentRecordService;

        public AccidentDetailsController(IAccidentDetailsViewModelFactory accidentDetailsViewModelFactory, IAccidentRecordService accidentRecordService)
        {
            _accidentDetailsViewModelFactory = accidentDetailsViewModelFactory;
            _accidentRecordService = accidentRecordService;
        }

        [PermissionFilter(Permissions.ViewAccidentRecords)]
        public ActionResult Index(long accidentRecordId, long companyId)
        {
            var model = _accidentDetailsViewModelFactory
                .WithCompanyId(CurrentUser.CompanyId)
                .WithAccidentRecordId(accidentRecordId)
                .WithSites(CurrentUser.GetSitesFilter())
                .GetViewModel();

            return View(model);
        }

        [PermissionFilter(Permissions.ViewAccidentRecords)]
        public ActionResult View(long accidentRecordId, long companyId)
        {
            var model = _accidentDetailsViewModelFactory
                .WithCompanyId(CurrentUser.CompanyId)
                .WithAccidentRecordId(accidentRecordId)
                .WithSites(CurrentUser.GetSitesFilter())
                .GetViewModel();

            IsReadOnly = true;
            return View("Index", model);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditAccidentRecords)]
        public JsonResult SaveAndNext(AccidentDetailsViewModel model)
        {
            Validate(model);

            if (ModelState.IsValid)
            {
                UpdateAccidentDetails(model);
                return Json(new { Success = true });
            }
            else
            {
                return ModelStateErrorsAsJson();
            }
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditAccidentRecords)]
        public ActionResult Save(AccidentDetailsViewModel model)
        {
            Validate(model);

            if (!ModelState.IsValid)
            {
                return InvalidCreateAccidentDetailsViewResult(model);
            }
            else
            {
                try
                {
                    UpdateAccidentDetails(model);
                    TempData["Notice"] = "Accident details successfully updated";
                    return RedirectToIndexAction(model);
                }
                catch (ValidationException validationException)
                {
                    ModelState.AddValidationErrors(validationException);
                }
            }

            return View("Index", model);
        }

        private void UpdateAccidentDetails(AccidentDetailsViewModel model)
        {
            var request = UpdateAccidentRecordAccidentDetailsRequest.Create(CurrentUser.CompanyId,
                                                                            model.AccidentRecordId,
                                                                            model.DateOfAccident,
                                                                            FormatTime(model.TimeOfAccident),
                                                                            model.SiteId.HasValue && model.SiteId != AccidentDetailsViewModel.OFF_SITE ? model.SiteId : null,
                                                                            model.SiteId == AccidentDetailsViewModel.OFF_SITE ? model.OffSiteName : string.Empty, model.Location,
                                                                            model.AccidentTypeId,
                                                                            model.AccidentTypeId ==AccidentDetailsViewModel.OTHER_ACCIDENT_TYPE ? model.OtherAccidentType : string.Empty,
                                                                            model.AccidentCauseId,
                                                                            model.AccidentCauseId == AccidentDetailsViewModel.OTHER_ACCIDENT_CAUSE ?  model.OtherAccidentCause : string.Empty,
                                                                            model.FirstAidAdministered,
                                                                            model.FirstAiderEmployeeId,
                                                                            model.FirstAiderEmployeeId == Guid.Empty ? model.NonEmployeeFirstAiderName : string.Empty,
                                                                            model.DetailsOfFirstAid,
                                                                            CurrentUser.UserId
                                                                            
                );

            _accidentRecordService.UpdateAccidentRecordAccidentDetails(request);
        }

        private ActionResult RedirectToIndexAction(AccidentDetailsViewModel model)
        {
            return RedirectToAction("Index", new {model.AccidentRecordId, CurrentUser.CompanyId});
        }

        private ActionResult InvalidCreateAccidentDetailsViewResult(AccidentDetailsViewModel model)
        {
            var viewModel = _accidentDetailsViewModelFactory
             .WithCompanyId(CurrentUser.CompanyId)
             .WithAccidentRecordId(model.AccidentRecordId)
             .WithSites(CurrentUser.GetSitesFilter())
             .GetViewModel();

            return View("Index", viewModel);
        }


        private void Validate(AccidentDetailsViewModel model)
        {
            if (string.IsNullOrEmpty(model.DateOfAccident))
            {
                ModelState.AddModelError("DateOfAccident", "Date of accident is required.");
            }

            if (string.IsNullOrEmpty(model.DateOfAccident))
            {
                ModelState.AddModelError("DateOfAccident", "Date of accident is required.");
            }
            else
            {
                DateTime result;
                if (!DateTime.TryParse(model.DateOfAccident, out result))
                {
                    ModelState.AddModelError("DateOfAccident", "Please specify date of accident in correct format (dd/mm/yyyy).");
                }
            }

            if (string.IsNullOrEmpty(model.TimeOfAccident))
            {
                ModelState.AddModelError("TimeOfAccident", "Time of accident is required.");
            }
            else
            {
                var isInvalid = false;
                var rxTimeOfAccident = new Regex(@"^\d{2}[\s:;,\.]?\d{2}$");

                if (!rxTimeOfAccident.IsMatch(model.TimeOfAccident))
                {
                    isInvalid = true;
                }

                DateTime result;
                if (!DateTime.TryParse(FormatTime(model.TimeOfAccident), out result))
                {
                    isInvalid = true;
                }

                if(isInvalid)
                {
                    ModelState.AddModelError("TimeOfAccident", "Please specify time of accident in correct format (HH:MM).");
                }
            }

            if (model.SiteId == null || model.SiteId ==default(long))
            {
                ModelState.AddModelError("SiteId", "Please select a Site.");
            }
            else if (model.SiteId == AccidentDetailsViewModel.OFF_SITE && string.IsNullOrEmpty(model.OffSiteName))
            {
                ModelState.AddModelError("OffSiteName", "Please specify an off-site name.");
            }

            if (string.IsNullOrEmpty(model.Location))
            {
                ModelState.AddModelError("Location", "Location of accident is required.");
            }

            if (model.AccidentTypeId == null || model.AccidentTypeId == default(long))
            {
                ModelState.AddModelError("AccidentTypeId", "Please select the kind of accident.");
            }
            else if (model.AccidentTypeId == AccidentDetailsViewModel.OTHER_ACCIDENT_TYPE && string.IsNullOrEmpty(model.OtherAccidentType))
            {
                ModelState.AddModelError("OtherAccidentType", "Please describe the kind of accident.");
            }

            if (model.AccidentCauseId ==null | model.AccidentCauseId== default(long))
            {
                ModelState.AddModelError("AccidentCauseId", "Please select the cause of the accident.");
            }
            else if (model.AccidentCauseId == AccidentDetailsViewModel.OTHER_ACCIDENT_CAUSE && string.IsNullOrEmpty(model.OtherAccidentCause))
            {
                ModelState.AddModelError("OtherAccidentCause", "Please describe the cause of the accident.");
            }

            if (model.FirstAidAdministered)
            {
                if (model.FirstAiderEmployeeId == null ||
                    model.FirstAiderEmployeeId == Guid.Empty && string.IsNullOrEmpty(model.NonEmployeeFirstAiderName))
                {
                    ModelState.AddModelError("NonEmployeeFirstAiderName", "Please enter the name of the first aider.");
                }
            }
        }

        private string FormatTime(string inputTime)
        {
            var formattedTime = inputTime
                .Replace(' ', ':')
                .Replace(';', ':')
                .Replace(',', ':')
                .Replace('.', ':');

            if(formattedTime.Length == 4)
            {
                formattedTime = formattedTime.Substring(0, 2) + ":" + formattedTime.Substring(2, 2);
            }

            return formattedTime;
        }
    }
}
