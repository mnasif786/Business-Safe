using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;

using NHibernate.Linq;

using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class StatutoryResponsibilityTemplateRepository : Repository<StatutoryResponsibilityTemplate, long>, IStatutoryResponsibilityTemplateRepository
    {
        public StatutoryResponsibilityTemplateRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager) { }
    }
}
