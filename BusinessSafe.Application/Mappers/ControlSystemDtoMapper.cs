using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class ControlSystemDtoMapper
    {
        public ControlSystemDto Map (ControlSystem entity)
        {
            return new ControlSystemDto
                       {
                           Id = entity.Id,
                           Description = entity.Description,
                           DocumentLibraryId = entity.DocumentLibraryId.GetValueOrDefault()
                       };
        }
    }
}
