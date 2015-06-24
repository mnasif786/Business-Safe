using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.AccidentReports.Factories;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.ViewModels;
using FluentValidation;
using System.Linq;

namespace BusinessSafe.WebSite.Areas.AccidentReports.Controllers
{
    [AccidentRecordCurrentTabActionFilter(AccidentRecordTabs.Overview)]
    [AccidentRecordContextFilter]
    public class OverviewController : BaseController
    {
        private readonly IAccidentRecordService _accidentRecordService;
        private readonly IAccidentRecordOverviewViewModelFactory _accidentRecordOverviewViewModelFactory;
        
        public OverviewController(IAccidentRecordOverviewViewModelFactory accidentRecordOverviewViewModelFactory, IAccidentRecordService accidentRecordService)
        {
            _accidentRecordOverviewViewModelFactory = accidentRecordOverviewViewModelFactory;
            _accidentRecordService = accidentRecordService;
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

        public OverviewViewModel GetViewModel(long accidentRecordId, long companyId)
        {
            var model = _accidentRecordOverviewViewModelFactory
               .WithAccidentRecordId(accidentRecordId)
               .WithCompanyId(CurrentUser.CompanyId)
               .GetViewModel();

            ViewBag.NextStepsVisible = model.NextStepsVisible;
            return model;
        }

        [PermissionFilter(Permissions.EditAccidentRecords)]
        [HttpPost]
        public ActionResult Save(long accidentRecordId, long companyId, OverviewViewModel overviewModel, DocumentsToSaveViewModel documentsToSaveViewModel)
        {
            Validate(overviewModel);

            if (!ModelState.IsValid)
            {
                return InvalidAddAccidentDocumentsViewResult(overviewModel);
            }

            UpdateAccidentRecordOverview(overviewModel, documentsToSaveViewModel);
            TempData["Notice"] = "Accident overview successfully updated";

            return View("Index", GetViewModel(accidentRecordId, CurrentUser.CompanyId));

        }

        [HttpPost]
        [PermissionFilter(Permissions.ViewAccidentRecords)]
        public JsonResult SaveAndNext(OverviewViewModel overviewModel,DocumentsToSaveViewModel documentsToSaveViewModel)
        {
            Validate(overviewModel);

            if (ModelState.IsValid)
            {
                UpdateAccidentRecordOverview(overviewModel, documentsToSaveViewModel);
                return Json(new { Success = true });
            }
            else
            {   
                return ModelStateErrorsAsJson();
            }
        }

        public void UpdateAccidentRecordOverview(OverviewViewModel overviewModel,DocumentsToSaveViewModel documentsToSaveViewModel)
        {
            if (documentsToSaveViewModel.CreateDocumentRequests.Count > 0)
            {
                var request = new AddDocumentToAccidentReportRequest
                {
                    AccidentRecordId = overviewModel.AccidentRecordId
                    ,
                    UserId = CurrentUser.UserId,
                    CompanyId = CurrentUser.CompanyId
                    ,
                    DocumentLibraryIds = documentsToSaveViewModel.CreateDocumentRequests.Select(x => new DocumentLibraryFile() { Description = x.Description, Filename = x.Filename, Id = x.DocumentLibraryId }).ToList()
                };
                
                _accidentRecordService.AddAccidentRecordDocument(request);
            }

            if (documentsToSaveViewModel.DeleteDocumentRequests.Count > 0)
            {
                var request = new RemoveDocumentsFromAccidentRecordRequest()
                {
                    AccidentRecordId = overviewModel.AccidentRecordId,
                    UserId = CurrentUser.UserId,
                    CompanyId = CurrentUser.CompanyId,
                    DocumentLibraryIds = documentsToSaveViewModel.DeleteDocumentRequests
                };

                _accidentRecordService.RemoveAccidentRecordDocuments(request);
            }

            var accidentRecordOverviewRequest = new AccidentRecordOverviewRequest()
                              {
                                  AccidentRecordId = overviewModel.AccidentRecordId,
                                  CompanyId = CurrentUser.CompanyId,
                                  UserId = CurrentUser.UserId,
                                  Description = overviewModel.DescriptionHowAccidentHappened,
                                  DoNotSendEmailNotification = overviewModel.DoNotSendEmailNotification
                              };


            _accidentRecordService.SetAccidentRecordOverviewDetails(accidentRecordOverviewRequest);
            
            if (!overviewModel.DoNotSendEmailNotification)
            {
                _accidentRecordService.SendAccidentRecordEmails(overviewModel.AccidentRecordId, CurrentUser.CompanyId, CurrentUser.UserId);
            }
        }

        private ActionResult InvalidAddAccidentDocumentsViewResult(OverviewViewModel model)
        {
            return View("Index", GetViewModel(model.AccidentRecordId, CurrentUser.CompanyId));
        }
        
        private void Validate(OverviewViewModel viewModel)
        {
            if (String.IsNullOrEmpty(viewModel.DescriptionHowAccidentHappened))
            {
                ModelState.AddModelError("EventsDescription", "Briefly describe how the accident happened.");
            }
        }
    }
}
