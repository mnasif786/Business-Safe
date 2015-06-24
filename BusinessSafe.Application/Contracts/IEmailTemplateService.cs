using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Contracts
{
    public interface IEmailTemplateService
    {
        EmailTemplateDto GetByEmailTemplateName(EmailTemplateName name);
    }
}
