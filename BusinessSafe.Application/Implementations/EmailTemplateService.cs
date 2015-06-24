using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Implementations
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;

        public EmailTemplateService(IEmailTemplateRepository emailTemplateRepository)
        {
            _emailTemplateRepository = emailTemplateRepository;
        }

        public EmailTemplateDto GetByEmailTemplateName(EmailTemplateName name)
        {
            var emailTemplate = _emailTemplateRepository.GetByEmailTemplateName(name);
            return new EmailTemplateDtoMapper().Map(emailTemplate);
        }
    }
}
