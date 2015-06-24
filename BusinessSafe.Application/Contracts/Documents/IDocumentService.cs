using System.Collections.Generic;

using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Documents;

namespace BusinessSafe.Application.Contracts.Documents
{
    public interface IDocumentService
    {
        DocumentDto GetDocument(long documentId, long companyId);
        IEnumerable<DocumentDto> Search(SearchDocumentRequest request);
        void MarkDocumentAsDeleted(MarkDocumentAsDeletedRequest request);
        void ValidateDocumentForCompany(long documentLibraryId, long companyId);
    }
}