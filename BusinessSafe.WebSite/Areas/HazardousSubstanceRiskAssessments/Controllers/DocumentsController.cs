﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers
{
    [HazardousSubstanceRiskAssessmentCurrentTabActionFilter(HazardousSubstancesTabs.Documents)]
    [HazardousSubstanceRiskAssessmentContextFilter]
    public class DocumentsController : BaseController
    {
        private readonly IRiskAssessmentAttachmentService _riskAssessmentAttachmentService;
        private readonly IDocumentsViewModelFactory _viewModelFactory;
        
        public DocumentsController(IDocumentsViewModelFactory viewModelFactory, IRiskAssessmentAttachmentService riskAssessmentAttachmentService)
        {
            _viewModelFactory = viewModelFactory;
            _riskAssessmentAttachmentService = riskAssessmentAttachmentService;
        }

        [PermissionFilter(Permissions.ViewGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult View(long riskAssessmentId, long companyId)
        {
            IsReadOnly = true;

            return Index(riskAssessmentId, companyId);
        }        

        [PermissionFilter(Permissions.ViewGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult Index(long riskAssessmentId, long companyId)
        {
            var viewModel = _viewModelFactory
                                    .WithCompanyId(companyId)
                                    .WithRiskAssessmentId(riskAssessmentId)
                                    .WithDocumentDefaultType(DocumentTypeEnum.HSRADocumentType)
                                    .GetViewModel();

            return View("Index", viewModel);
        }

        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        [HttpPost]
        public ActionResult Save(long companyId, long riskAssessmentId, DocumentsToSaveViewModel documentsToSaveViewModel)
        {
            SaveDocuments(companyId, riskAssessmentId, documentsToSaveViewModel);

            return RedirectToAction("Index", "Documents", new { riskAssessmentId = riskAssessmentId, companyId });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
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

        private void DetachDocumentsFromRiskAssessment(long companyId, long riskAssessmentId, List<long> generalRiskAssessmentDettachDocumentRequests)
        {
            if (generalRiskAssessmentDettachDocumentRequests.Any())
            {
                _riskAssessmentAttachmentService.DetachDocumentsToRiskAssessment(new DetachDocumentsFromRiskAssessmentRequest
                {
                    CompanyId = companyId,
                    UserId = CurrentUser.UserId,
                    RiskAssessmentId = riskAssessmentId,
                    DocumentsToDetach = generalRiskAssessmentDettachDocumentRequests
                });
            }
        }

        private void AttachDocumentsToRiskAssessment(long companyId, long riskAssessmentId, List<CreateDocumentRequest> generalRiskAssessmentAttachDocumentRequests)
        {
            if (generalRiskAssessmentAttachDocumentRequests.Any())
            {
                _riskAssessmentAttachmentService.AttachDocumentsToRiskAssessment(new AttachDocumentsToRiskAssessmentRequest
                {
                    CompanyId = companyId,
                    UserId = CurrentUser.UserId,
                    RiskAssessmentId = riskAssessmentId,
                    DocumentsToAttach = generalRiskAssessmentAttachDocumentRequests
                });
            }
        }
    }
}