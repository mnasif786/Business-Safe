using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class QuestionDtoMapper
    {
        public QuestionDto Map(Question entity)
        {
            var dto = new QuestionDto
                          {
                              Id = entity.Id,
                              QuestionType = entity.QuestionType,
                              ListOrder = entity.ListOrder,
                              IsRequired = entity.IsRequired,
                              Text = entity.Text,
                              Information = entity.Information
                          };

            return dto;
        }

        public IEnumerable<QuestionDto> Map(IEnumerable<Question> entities)
        {
            return entities.Select(Map);
        }
    }
}
