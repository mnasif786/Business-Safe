using System;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using NHibernate.Linq;
using System.Linq;

namespace BusinessSafe.Data.Repository.SafeCheck
{
    public class ChecklistTemplateRepository : Repository<ChecklistTemplate, Guid>, IChecklistTemplateRepository
    {
        public ChecklistTemplateRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public override System.Collections.Generic.IEnumerable<ChecklistTemplate> GetAll()
        {
            return SessionManager.Session.Query<ChecklistTemplate>()
                .FetchMany(x => x.Questions)
                .ThenFetch(x=> x.Question)
                .ToList();
        }

        public bool DoesChecklistTemplateExistWithTheSameName(string name, Guid templateId)
        {
            var count = SessionManager.Session.Query<ChecklistTemplate>().Count(x => x.Name == name && x.Id != templateId);
            return count > 0;
        }

        public override ChecklistTemplate GetById(Guid id)
        {
            var checklistTemplates = SessionManager.Session.Query<ChecklistTemplate>()
                .Where(x => x.Id == id)
                .FetchMany(x => x.Questions)
                .ThenFetch(x => x.Question)
                .ToList();

            return checklistTemplates.Any() ? checklistTemplates[0] : null;

        }
    }
}
