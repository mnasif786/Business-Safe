using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class SectionDtoMapper
    {
        public SectionDto Map(Section entity)
        {
            var dto = new SectionDto
                          {
                              Id = entity.Id,
                              Title = entity.Title,
                              ShortTitle = entity.ShortTitle,
                              ListOrder = entity.ListOrder,
                              Questions = new QuestionDtoMapper().Map(entity.Questions)
                          };

            return dto;
        }

        public IEnumerable<SectionDto> Map(IEnumerable<Section> entities)
        {
            return entities.Select(Map);
        }
    }
}
