using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;

namespace BusinessSafe.WebSite.Areas.Documents.Factories
{
    public class ExistingDocumentsViewModelFactory : IExistingDocumentsViewModelFactory
    {
        private bool _canDeleteDocuments;
        private bool _canEditDocumentType;
        private DocumentTypeEnum _defaultDocumentType;
        private DocumentOriginType _documentOriginType = DocumentOriginType.TaskCompleted;

        public IExistingDocumentsViewModelFactory WithCanDeleteDocuments(bool canDeleteDocuments)
        {
            _canDeleteDocuments = canDeleteDocuments;
            return this;
        }

        public IExistingDocumentsViewModelFactory WithCanEditDocumentType(bool canEditDocumentType)
        {
            _canEditDocumentType = canEditDocumentType;
            return this;
        }

        public IExistingDocumentsViewModelFactory WithDefaultDocumentType(DocumentTypeEnum defaultDocumentType)
        {
            _defaultDocumentType = defaultDocumentType;
            return this;
        }

        public IExistingDocumentsViewModelFactory WithDocumentOriginType(DocumentOriginType documentOriginType)
        {
            _documentOriginType = documentOriginType;
            return this;
        }

        public ExistingDocumentsViewModel GetViewModel(IEnumerable<TaskDocumentDto> documents)
        {
            var viewModel = ExistingDocumentsViewModel.CreateFrom(documents);
            viewModel.CanDeleteDocuments = _canDeleteDocuments;
            viewModel.CanEditDocumentType = _canEditDocumentType;
            viewModel.DocumentTypeId = (int) _defaultDocumentType;
            viewModel.DocumentOriginTypeId = (int) _documentOriginType;
        
            return viewModel;
        }

    }
}