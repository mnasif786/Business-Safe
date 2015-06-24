using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class EmailTemplateDtoMapper
    {
        public EmailTemplateDto Map(EmailTemplate entity)
        {
            return new EmailTemplateDto
                       {
                           Id = entity.Id,
                           Body = entity.Body,
                           Name = entity.Name,
                           Subject = entity.Subject
                       };
        }
    }
}
