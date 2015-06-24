using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BusinessSafe.Application.Mappers
{
    public class DocumentTypeDtoMapper
    {
        public DocumentTypeDto Map(DocumentType entity)
        {
            return new DocumentTypeDto
                       {
                           Id = entity.Id,
                           Name = entity.Name
                       };
        }

        public IEnumerable<DocumentTypeDto> Map(IEnumerable<DocumentType> entities)
        {
            return entities.Select(Map);
        }
    }
}
