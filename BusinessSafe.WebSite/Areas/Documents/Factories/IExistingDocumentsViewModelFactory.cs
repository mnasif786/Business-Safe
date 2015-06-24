using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;

namespace BusinessSafe.WebSite.Areas.Documents.Factories
{
    public interface IExistingDocumentsViewModelFactory
    {
        IExistingDocumentsViewModelFactory WithCanDeleteDocuments(bool canDeleteDocuments);
        IExistingDocumentsViewModelFactory WithCanEditDocumentType(bool canEditDocumentType);
        IExistingDocumentsViewModelFactory WithDefaultDocumentType(DocumentTypeEnum  defaultDocumentType);
        IExistingDocumentsViewModelFactory WithDocumentOriginType(DocumentOriginType documentOriginType);
        ExistingDocumentsViewModel GetViewModel(IEnumerable<TaskDocumentDto> documents);
    }
}