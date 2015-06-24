using System.Collections.Generic;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using NHibernate.Criterion;

namespace BusinessSafe.Data.Repository
{
    public class SupplierRepository : Repository<Supplier, long>, ISupplierRepository
    {
        public SupplierRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        { }

        public IEnumerable<Supplier> GetByCompanyId(long companyId)
        {
            return SessionManager
                .Session
                .CreateCriteria<Supplier>()
                .Add(Restrictions.Eq("CompanyId", companyId))
                .Add(Restrictions.Eq("Deleted", false))
                .List<Supplier>();
        }

        public Supplier GetByIdAndCompanyId(long id, long companyId)
        {
            var result = SessionManager
                    .Session
                    .CreateCriteria<Supplier>()
                    .Add(Restrictions.Eq("Id", id))
                    .Add(Restrictions.Or(Restrictions.Eq("CompanyId", companyId), Restrictions.IsNull("CompanyId")))
                    .Add(Restrictions.Eq("Deleted", false))
                    .SetMaxResults(200)
                    .UniqueResult<Supplier>();

            if (result == null)
                throw new SupplierNotFoundException(id);
            return result;
        }

        public IEnumerable<Supplier> GetAllByNameSearch(string nameToSearch, long excludeSupplierId, long companyId, int pageLimit)
        {
            ICriteria executableCriteria = CompanyDefaultsRepositoryCriteriaHelper
                                            .GetCompanyDefaultExistsDetachedCriteria<Supplier>(excludeSupplierId, companyId, nameToSearch)
                                            .SetMaxResults(pageLimit)
                                            .GetExecutableCriteria(SessionManager.Session);
            return executableCriteria.List<Supplier>();
        }
      
    }
}
