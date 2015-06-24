using System.Collections.Generic;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class NonEmployeeRepository : Repository<NonEmployee, long>, INonEmployeeRepository
    {  
        public NonEmployeeRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager)
        {
        }

        public IEnumerable<NonEmployee> GetByTermSearch(string termToSearch, long companyId, int pageLimit)
        {
            return SessionManager
                            .Session
                            .CreateCriteria<NonEmployee>()
                            .Add(Restrictions.Eq("LinkToCompanyId", companyId))
                            .Add(Restrictions.Eq("Deleted", false))
                            .Add(Restrictions.Disjunction()
                                .Add(Restrictions.InsensitiveLike ("Name", termToSearch + "%"))
                                .Add(Restrictions.InsensitiveLike("Position", termToSearch + "%"))
                                .Add(Restrictions.InsensitiveLike("Company", termToSearch + "%")
                                ))
                            .SetMaxResults(pageLimit)
                            .AddOrder(Order.Asc("Name"))
                            .List<NonEmployee>();
        }

        public IEnumerable<NonEmployee> GetAllByNameSearch(string nameToSearch, long nonEmployeeId, long companyId)
        {
            return SessionManager
                            .Session
                            .CreateCriteria<NonEmployee>()
                            .Add(Restrictions.Not(Restrictions.Eq("Id", nonEmployeeId)))
                            .Add(Restrictions.Eq("LinkToCompanyId", companyId))
                            .Add(Restrictions.Eq("Deleted", false))
                            .Add(Restrictions.Or(
                                                    Restrictions.InsensitiveLike("Name", nameToSearch + "%"), 
                                                    Restrictions.InsensitiveLike("Name", "%" + nameToSearch))
                                                )
                            .SetMaxResults(100)
                            .List<NonEmployee>();
        }

        public NonEmployee GetByIdAndCompanyId(long nonEmployeeId, long companyId)
        {
            var result = SessionManager
                           .Session
                           .CreateCriteria<NonEmployee>()
                           .Add(Restrictions.Eq("LinkToCompanyId", companyId))
                           .Add(Restrictions.Eq("Deleted", false))
                           .Add(Restrictions.Eq("Id", nonEmployeeId))
                           .UniqueResult<NonEmployee>();
            if (result == null)
                throw new NonEmployeeNotFoundException(nonEmployeeId);
            return result;
        }

        public IEnumerable<NonEmployee> GetAllNonEmployeesForCompany(long companyId)
        {
            return SessionManager
                            .Session
                            .CreateCriteria<NonEmployee>()
                            .Add(Restrictions.Eq("LinkToCompanyId", companyId))
                            .Add(Restrictions.Eq("Deleted", false))
                            .SetMaxResults(500)
                            .AddOrder(Order.Asc("Name"))
                            .List<NonEmployee>();
        }

    }
}