using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class TaskDocumentDtoMapper
    {
        public IEnumerable<TaskDocumentDto> Map(IEnumerable<TaskDocument> entities)
        {
            return entities.Select(entity => new DocumentDtoMapper().Map(entity) as TaskDocumentDto);
        }
    }
}
