using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers
{
    [PersonalRiskAssessmentCurrentTabActionFilter(PersonalRiskAssessmentTabs.Documents)]
    [PersonalRiskAssessmentContextFilter]
    public class DocumentsController : BaseController
    {
        private readonly IDocumentsViewModelFactory _viewModelFactory;
        private readonly IRiskAssessmentAttachmentService _riskAssessmentAttachmentService;

        public DocumentsController(IDocumentsViewModelFactory viewModelFactory, IRiskAssessmentAttachmentService riskAssessmentAttachmentService)
        {
            _viewModelFactory = viewModelFactory;
            _riskAssessmentAttachmentService = riskAssessmentAttachmentService;
        }


        [PermissionFilter(Permissions.ViewPersonalRiskAssessments)]
        public ActionResult View(long riskAssessmentId, long companyId)
        {
            IsReadOnly = true;
            return Index(riskAssessmentId, companyId);
        }

        [PermissionFilter(Permissions.ViewPersonalRiskAssessments)]
        public ActionResult Index(long riskAssessmentId, long companyId)
        {
            var viewModel = _viewModelFactory
                                    .WithCompanyId(companyId)
                                    .WithRiskAssessmentId(riskAssessmentId)
                                    .WithDocumentDefaultType(DocumentTypeEnum.PRADocumentType)
                                    .GetViewModel();

            return View("Index", viewModel);
        }

        [PermissionFilter(Permissions.EditPersonalRiskAssessments)]
        [HttpPost]
        public ActionResult Save(long companyId, long riskAssessmentId, DocumentsToSaveViewModel documentsToSaveViewModel)
        {
            SaveDocuments(companyId, riskAssessmentId, documentsToSaveViewModel);

            return RedirectToAction("Index", "Documents", new { riskAssessmentId, companyId });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditPersonalRiskAssessments)]
        public JsonResult SaveAndNext(long companyId, long riskAssessmentId, DocumentsToSaveViewModel documentsToSaveViewModel)
        {
            SaveDocuments(companyId, riskAssessmentId, documentsToSaveViewModel);

            return Json(new { Success = true });
        }

        private void SaveDocuments(long companyId, long riskAssessmentId, DocumentsToSaveViewModel documentsToSaveViewModel)
        {
            AttachDocumentsToRiskAssessment(companyId, riskAssessmentId, documentsToSaveViewModel.CreateDocumentRequests);
            DetachDocumentsFromRiskAssessment(companyId, riskAssessmentId, documentsToSaveViewModel.DeleteDocumentRequests);
        }

        private void DetachDocumentsFromRiskAssessment(long companyId, long riskAssessmentId, List<long> dettachDocumentRequests)
        {
            if (dettachDocumentRequests.Any())
            {
                _riskAssessmentAttachmentService.DetachDocumentsToRiskAssessment(new DetachDocumentsFromRiskAssessmentRequest
                {
                    CompanyId = companyId,
                    UserId = CurrentUser.UserId,
                    RiskAssessmentId = riskAssessmentId,
                    DocumentsToDetach = dettachDocumentRequests
                });
            }
        }

        private void AttachDocumentsToRiskAssessment(long companyId, long riskAssessmentId, List<CreateDocumentRequest> attachDocumentRequests)
        {
            if (attachDocumentRequests.Any())
            {
                _riskAssessmentAttachmentService.AttachDocumentsToRiskAssessment(new AttachDocumentsToRiskAssessmentRequest
                {
                    CompanyId = companyId,
                    UserId = CurrentUser.UserId,
                    RiskAssessmentId = riskAssessmentId,
                    DocumentsToAttach = attachDocumentRequests
                });
            }
        }
    }
}