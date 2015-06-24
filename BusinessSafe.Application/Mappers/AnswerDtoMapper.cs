using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class AnswerDtoMapper
    {
        public AnswerDto Map(Answer entity)
        {
            AnswerDto dto = null;

            if(entity == null)
            {
                return null;
            }

            if (entity.Self as PersonalAnswer != null)
            {
                dto = new PersonalAnswerDto();
                var personalAnswer = entity.Self as PersonalAnswer;
                var personalAnswertDto = dto as PersonalAnswerDto;
                personalAnswertDto.BooleanResponse = personalAnswer.BooleanResponse;
            }

            if(entity.Self as FireAnswer != null)
            {
                dto = new FireAnswerDto();
                var fireAnswer = entity.Self as FireAnswer;
                var fireAnswerDto = dto as FireAnswerDto;
                fireAnswerDto.YesNoNotApplicableResponse = fireAnswer.YesNoNotApplicableResponse;
            }

            dto.Id = entity.Id;
            dto.Question = new QuestionDtoMapper().Map(entity.Question);
            dto.AdditionalInfo = entity.AdditionalInfo;

            return dto;
        }

        public IEnumerable<AnswerDto> Map(IList<Answer> entities)
        {
            return entities.Select(Map);
        }
    }
}