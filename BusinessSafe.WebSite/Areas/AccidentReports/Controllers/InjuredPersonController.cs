using System;
using System.Linq;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.ViewModels;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.AccidentReports.Factories;

namespace BusinessSafe.WebSite.Areas.AccidentReports.Controllers
{
    [AccidentRecordCurrentTabActionFilter(AccidentRecordTabs.InjuredPerson)]
    [AccidentRecordContextFilter]
    public class InjuredPersonController : BaseController
    {
        private readonly IAccidentRecordService _accidentRecordService;
        private readonly IInjuredPersonViewModelFactory _injuredPersonViewModelFactory;

        public InjuredPersonController(
            IAccidentRecordService accidentRecordService,
            IInjuredPersonViewModelFactory injuredPersonViewModelFactory)
        {
            _accidentRecordService = accidentRecordService;
            _injuredPersonViewModelFactory = injuredPersonViewModelFactory;
        }

        [PermissionFilter(Permissions.ViewAccidentRecords)]
        public ActionResult Index(long accidentRecordId, long companyId)
        {
            var model = _injuredPersonViewModelFactory
                .WithCompanyId(CurrentUser.CompanyId)
                .WithAccidentRecordId(accidentRecordId)
                .GetViewModel();

            ViewBag.NextStepsVisible = model.NextStepsVisible;
            return View(model);
        }

        [PermissionFilter(Permissions.ViewAccidentRecords)]
        public ActionResult View(long accidentRecordId, long companyId)
        {
            var model = _injuredPersonViewModelFactory
                .WithCompanyId(CurrentUser.CompanyId)
                .WithAccidentRecordId(accidentRecordId)
                .GetViewModel();

            ViewBag.NextStepsVisible = model.NextStepsVisible;
            IsReadOnly = true;
            return View("Index", model);
        }

        [HttpPost]
        [PermissionFilter(Permissions.ViewAccidentRecords)]
        public JsonResult SaveAndNext(InjuredPersonViewModel model)
        {
            if (ModelState.IsValid)
            {
                UpdateInjuredPerson(model);
                return Json(new {Success = true});
            }
            else
            {
                return ModelStateErrorsAsJson();
            }
        }

        [PermissionFilter(Permissions.ViewAccidentRecords)]
        public ActionResult Save(InjuredPersonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var returnViewModel = _injuredPersonViewModelFactory
                    .WithCompanyId(model.CompanyId)
                    .WithAccidentRecordId(model.AccidentRecordId)
                    .GetViewModel(model);

                ViewBag.NextStepsVisible = model.NextStepsVisible;
                return View("Index", returnViewModel);
            }

           UpdateInjuredPerson(model);
           TempData["Notice"] = "Details of injured person successfully updated";
           return RedirectToAction("Index", new { accidentRecordId = model.AccidentRecordId, companyId = model.CompanyId });
           //return View("Index", model);
        }

        private void UpdateInjuredPerson(InjuredPersonViewModel model)
        {
            UpdateInjuredPersonRequest request = new UpdateInjuredPersonRequest()
            {
                AccidentRecordId = model.AccidentRecordId,
                CurrentUserId = CurrentUser.UserId,
                CompanyId = CurrentUser.GetUsersCompanyId(),
                PersonInvolved = model.PersonInvolvedType,
                PersonInvolvedOtherDescription = model.PersonInvolvedOtherDescription,
                PersonInvolvedOtherDescriptionId = model.PersonInvolvedOtherDescriptionId,
                PersonInvolvedOtherDescriptionOther = model.PersonInvolvedOtherDescriptionOther,
                EmployeeInjuredId = model.EmployeeId,
                NonEmployeeInjuredForename = model.Forename,
                NonEmployeeInjuredSurname = model.Surname,
                NonEmployeeInjuredAddress1 = model.AddressLine1,
                NonEmployeeInjuredAddress2 = model.AddressLine2,
                NonEmployeeInjuredAddress3 = model.AddressLine3,
                NonEmployeeInjuredCountyState = model.County,
                NonEmployeeInjuredCountry = model.CountryId,
                NonEmployeeInjuredPostcode = model.Postcode,
                NonEmployeeInjuredContactNumber = model.ContactNo,
                NonEmployeeInjuredOccupation = model.Occupation
            };

            _accidentRecordService.UpdateInjuredPerson(request);
        }

    }
}
