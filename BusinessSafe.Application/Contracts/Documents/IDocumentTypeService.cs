using System.Collections.Generic;

using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.Application.Contracts.Documents
{
    public interface IDocumentTypeService
    {
        IEnumerable<DocumentTypeDto> GetAll();
    }
}