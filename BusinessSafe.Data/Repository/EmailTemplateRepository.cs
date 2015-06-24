using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Data.Common;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class EmailTemplateRepository : Repository<EmailTemplate, long>, IEmailTemplateRepository
    {
        public EmailTemplateRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager)
        {}

        public EmailTemplate GetByEmailTemplateName(EmailTemplateName templateName)
        {
            return SessionManager
                        .Session
                       .CreateCriteria<EmailTemplate>()
                       .Add(Restrictions.Eq("Name", templateName.ToString()))
                       .UniqueResult<EmailTemplate>();
        }
    }
}
