using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class DocHandlerDocumentTypeDtoMapper
    {
        public IEnumerable<DocHandlerDocumentTypeDto> Map(IEnumerable<DocHandlerDocumentType> entities)
        {
            return entities.Select(Map);
        }

        public DocHandlerDocumentTypeDto Map(DocHandlerDocumentType entity)
        {
            return new DocHandlerDocumentTypeDto()
                          {
                              Id = entity.Id,
                              DocHandlerDocumentTypeGroup = entity.DocHandlerDocumentTypeGroup
                          };
        }
    }
}
