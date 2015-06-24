using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using NHibernate.Linq;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class AccidentTypeRepository : Repository<AccidentType, long>, IAccidentTypeRepository
    {
        public AccidentTypeRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IEnumerable<AccidentType> GetAllForCompany(long companyId)
        {
            return SessionManager
                .Session
                .CreateCriteria<AccidentType>()
                .Add(Restrictions.Or(Restrictions.Eq("CompanyId", companyId), Restrictions.IsNull("CompanyId")))
                .Add(Restrictions.Eq("Deleted", false))
                .AddOrder(new Order("Description", true))
                .List<AccidentType>();
        }
    }
}