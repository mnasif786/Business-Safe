using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.AccidentReports.Factories;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using System.Linq;
using NHibernate.Hql.Ast.ANTLR.Tree;

namespace BusinessSafe.WebSite.Areas.AccidentReports.Controllers
{
    [AccidentRecordCurrentTabActionFilter(AccidentRecordTabs.Injury)]
    [AccidentRecordContextFilter]
    public class InjuryDetailsController : BaseController
    {
        private readonly IInjuryDetailsViewModelFactory _injuryDetailsViewModelFactory;
        private readonly IAccidentRecordService _accidentRecordService;

        public InjuryDetailsController(IInjuryDetailsViewModelFactory injuryDetailsViewModelFactory, IAccidentRecordService accidentRecordService)
        {
            _injuryDetailsViewModelFactory = injuryDetailsViewModelFactory;
            _accidentRecordService = accidentRecordService;
        }

        [PermissionFilter(Permissions.ViewAccidentRecords)]
        public ActionResult Index(long accidentRecordId, long companyId)
        {
            //TODO: Why are we passing in a companyId which doesn't get used?
            var model = _injuryDetailsViewModelFactory.GetViewModel(accidentRecordId,CurrentUser.CompanyId);
            ViewBag.NextStepsVisible = model.NextStepsVisible;
            return View("Index", model);
        }

        [PermissionFilter(Permissions.ViewAccidentRecords)]
        public ActionResult View(long accidentRecordId, long companyId)
        {
            IsReadOnly = true;
            var model = _injuryDetailsViewModelFactory.GetViewModel(accidentRecordId,CurrentUser.CompanyId);
            ViewBag.NextStepsVisible = model.NextStepsVisible;
            return View("Index", model);
        }

        [PermissionFilter(Permissions.EditAccidentRecords)]
        [HttpPost]
        public ActionResult Save(InjuryDetailsViewModel model)
        {
            Validate(model);

            if (!ModelState.IsValid)
            {
                return InvalidIjnuryDetailsViewResult(model);
            }

            UpdateAccidentRecordInjuryDetailsOverview(model);
            TempData["Notice"] = "Injury details successfully updated";
   
            return RedirectToAction("Index", new { AccidentRecordId = model.AccidentRecordId , CompanyId = CurrentUser.CompanyId});

        }

        private void UpdateAccidentRecordInjuryDetailsOverview(InjuryDetailsViewModel model)
        {
            var request = new UpdateInjuryDetailsRequest()
                              {
                                  AccidentRecordId = model.AccidentRecordId
                                  , BodyPartsInjured = model.SelectedBodyPartIds == null ? new List<long>() : model.SelectedBodyPartIds.ToList()
                                  , CompanyId = CurrentUser.CompanyId
                                  , CurrentUserId = CurrentUser.UserId
                                  , WasTheInjuredPersonBeenTakenToHospital = model.InjuredPersonWasTakenToHospital
                                  , HasTheInjuredPersonBeenAbleToCarryOutTheirNormalWorkActivity = model.InjuredPersonAbleToCarryOutWork
                                  , Injuries = model.SelectedInjuryIds == null ? new List<long>() : model.SelectedInjuryIds.ToList()
                                  , SeverityOfInjury = model.SeverityOfInjury
                                  , LengthOfTimeUnableToCarryOutWork = model.LengthOfTimeUnableToCarryOutWork
                                  , CustomInjuryDescription = model.CustomInjuryDescription
                                  , CustomBodyPartDescription = model.CustomBodyPartyDescription
                              };
            _accidentRecordService.UpdateInjuryDetails(request);
        }

        private void Validate(InjuryDetailsViewModel model)
        {
            var severityTypesThatRequireInjuryAndBodyParts = new [] {SeverityOfInjuryEnum.Fatal, SeverityOfInjuryEnum.Major, SeverityOfInjuryEnum.Minor};

            if (!model.SeverityOfInjury.HasValue)
            {
                ModelState.AddModelError("SeverityOfInjury", "Severity of injury is required.");
            }
            else
            {
                ValidateInjurySelection(model, severityTypesThatRequireInjuryAndBodyParts);
                ValidateBodyPartSelection(model, severityTypesThatRequireInjuryAndBodyParts);
                ValidateTakenToHospital(model);
                ValidateAbleToCarryOutWork(model);
                ValidateTimeOff(model);
            }
        }

        private void ValidateTimeOff(InjuryDetailsViewModel model)
        {
            if (model.SeverityOfInjury.Value != SeverityOfInjuryEnum.Fatal
                && model.InjuredPersonAbleToCarryOutWork.HasValue
                && model.InjuredPersonAbleToCarryOutWork.Value == YesNoUnknownEnum.No
                && !model.LengthOfTimeUnableToCarryOutWork.HasValue)
            {
                ModelState.AddModelError("LengthOfTimeUnableToCarryOutWork", "Please select the length of time the injured person has been unable to carry out their normal work activity.");
            }
        }

        private void ValidateAbleToCarryOutWork(InjuryDetailsViewModel model)
        {
            if (model.SeverityOfInjury.Value != SeverityOfInjuryEnum.Fatal && !model.InjuredPersonAbleToCarryOutWork.HasValue && model.ShowInjuredPersonAbleToCarryOutWorkSection)
            {
                ModelState.AddModelError("InjuredPersonAbleToCarryOutWork", "Please select an answer for the able to carry out normal work activity question.");
            }
        }

        private void ValidateTakenToHospital(InjuryDetailsViewModel model)
        {
            if (model.SeverityOfInjury.Value != SeverityOfInjuryEnum.Fatal && !model.InjuredPersonWasTakenToHospital.HasValue)
            {
                ModelState.AddModelError("TakenToHospitalSection", "Please select an answer for the taken to hospital question.");
            }
        }

        private void ValidateBodyPartSelection(InjuryDetailsViewModel model, SeverityOfInjuryEnum[] severityTypesThatRequireInjuryAndBodyParts)
        {
            if (severityTypesThatRequireInjuryAndBodyParts.Contains(model.SeverityOfInjury.Value)
                && (model.SelectedBodyPartIds == null || !model.SelectedBodyPartIds.Any()))
            {
                ModelState.AddModelError("BodyPartsSection", "Please select body parts injured.");
            }

            if (model.SelectedBodyPartIds != null
                        && model.SelectedBodyPartIds.Contains(BodyPart.UNKOWN_BODY_PART)
                        && string.IsNullOrEmpty(model.CustomBodyPartyDescription))
            {
                ModelState.AddModelError("CustomBodyPartyDescription", "Please provide description of unknown location.");
            }
        }

        private void ValidateInjurySelection(InjuryDetailsViewModel model, SeverityOfInjuryEnum[] severityTypesThatRequireInjuryAndBodyParts)
        {
            if (severityTypesThatRequireInjuryAndBodyParts.Contains(model.SeverityOfInjury.Value)
                && (model.SelectedInjuryIds == null || !model.SelectedInjuryIds.Any()))
            {
                ModelState.AddModelError("Injury", "Please select injuries suffered.");
            }

            if (model.SelectedInjuryIds != null
                        && model.SelectedInjuryIds.Contains(Injury.UNKOWN_INJURY)
                        && string.IsNullOrEmpty(model.CustomInjuryDescription))
            {
                ModelState.AddModelError("CustomInjuryDescription", "Please provide description of unknown injury.");
            }
        }

        private ActionResult InvalidIjnuryDetailsViewResult(InjuryDetailsViewModel model)
        {
            var viewModel = _injuryDetailsViewModelFactory.GetViewModel(model.AccidentRecordId, CurrentUser.CompanyId);

            
            var selectedBodyParts = from selectedBodyPartIds in (model.SelectedBodyPartIds ?? new long[0])
                                    join bodyPart in viewModel.BodyParts on selectedBodyPartIds equals bodyPart.Id
                                    select bodyPart;

            var selectedInjuries = from selectedInjuryIds in (model.SelectedInjuryIds ?? new long[0])
                                   join injury in (viewModel.Injuries ?? new List<LookupDto>()) on selectedInjuryIds equals injury.Id
                                 select injury;


            viewModel.SelectedInjuries = selectedInjuries.ToList();
            viewModel.SelectedBodyParts = selectedBodyParts.ToList();
            return View("Index", viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.ViewAccidentRecords)]
        public JsonResult SaveAndNext(InjuryDetailsViewModel model)
        {
            Validate(model);

            if (ModelState.IsValid)
            {
                UpdateAccidentRecordInjuryDetailsOverview(model);
                return Json(new {Success = true});
            }
            else
            {
                return ModelStateErrorsAsJson();
            }
        }
    }


}
