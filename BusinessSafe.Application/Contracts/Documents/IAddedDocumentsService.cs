using System;
using System.Collections.Generic;
using BusinessSafe.Application.Request.Documents;

namespace BusinessSafe.Application.Contracts.Documents
{
    public interface IAddedDocumentsService
    {
        void Add(IEnumerable<CreateDocumentRequest> createDocumentRequests, Guid userId, long companyId);
    }
}