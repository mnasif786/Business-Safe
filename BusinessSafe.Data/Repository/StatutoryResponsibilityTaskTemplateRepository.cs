using System.Collections.Generic;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using System.Linq;

namespace BusinessSafe.Data.Repository
{
    public class StatutoryResponsibilityTaskTemplateRepository : Repository<StatutoryResponsibilityTaskTemplate, long>, IStatutoryResponsibilityTaskTemplateRepository
    {
        public StatutoryResponsibilityTaskTemplateRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager) { }
    }
}
