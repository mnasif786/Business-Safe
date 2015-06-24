using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public class DocumentsViewModelFactory : IDocumentsViewModelFactory
    {
        private readonly IRiskAssessmentAttachmentService _riskAssessmentAttachmentService;
        private IEnumerable<string> _displayMessages = new string[] { };
        private long _riskAssessmentId;
        private long _companyId;
        private DocumentTypeEnum _documentType;


        public DocumentsViewModelFactory(IRiskAssessmentAttachmentService riskAssessmentAttachmentService)
        {
            _riskAssessmentAttachmentService = riskAssessmentAttachmentService;
        }

        public IDocumentsViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IDocumentsViewModelFactory WithRiskAssessmentId(long riskAssessmentId)
        {
            _riskAssessmentId = riskAssessmentId;
            return this;
        }

        public IDocumentsViewModelFactory WithDocumentDisplayMessage(IEnumerable<string> displayMessages)
        {
            _displayMessages = displayMessages;
            return this;
        }

        public IDocumentsViewModelFactory WithDocumentDefaultType(DocumentTypeEnum documentType)
        {
            _documentType = documentType;
            return this;
        }

        public DocumentsViewModel GetViewModel()
        {
            var existingDocumentsViewModel = new ExistingDocumentsViewModel { CanDeleteDocuments = true, CanEditDocumentType = true, DocumentTypeId = (int)_documentType };
            
            var attachedDocuments = _riskAssessmentAttachmentService.GetRiskAssessmentAttachedDocuments(_riskAssessmentId, _companyId);
            foreach (var attachedDocument in attachedDocuments.Where(document => !document.Deleted))
            {
                existingDocumentsViewModel.PreviouslyAddedDocuments.Add(new PreviouslyAddedDocumentGridRowViewModel()
                {
                    Description = attachedDocument.Description,
                    DocumentTypeName = attachedDocument.DocumentType.Name,
                    Id = attachedDocument.Id,
                    DocumentLibraryId = attachedDocument.DocumentLibraryId,
                    Filename = attachedDocument.Filename,
                });
            };
            
            var model = new DocumentsViewModel()
            {
                CompanyId = _companyId,
                RiskAssessmentId = _riskAssessmentId,
                ExistingDocumentsViewModel = existingDocumentsViewModel,
                DocumentDisplayMessages = _displayMessages.Select(x => new HtmlString(x))
            };

            return model;
        }
    }
}