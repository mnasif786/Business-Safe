using System.Collections.Generic;

using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Contracts.Documents
{

    public interface IDocHandlerDocumentTypeService
    {
        IEnumerable<DocHandlerDocumentTypeDto> GetForDocumentGroup(
            DocHandlerDocumentTypeGroup docHandlerDocumentTypeGroup);
    }

}