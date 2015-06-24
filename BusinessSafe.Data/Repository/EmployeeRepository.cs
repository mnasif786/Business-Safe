using System;
using System.Collections.Generic;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class EmployeeRepository : Repository<Employee, Guid>, IEmployeeRepository
    {
        public EmployeeRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IEnumerable<Employee> Search(long companyId, string employeeReferenceLike, string fornameLike,
                                            string surnameLike, long[] siteIds, bool showDeleted, int maximumResults,
                                            bool includeSiteless, bool excludeWithActiveUser, string orderBy, bool @ascending)
        {
            var filterQuery = CreateEmployeeSearchFilterQuery(companyId, employeeReferenceLike, fornameLike, surnameLike,
                                                              siteIds, showDeleted, maximumResults, includeSiteless,excludeWithActiveUser,
                                                              orderBy, ascending);
            return filterQuery.GetExecutableCriteria(SessionManager.Session).List<Employee>();
        }

        public Employee GetByIdAndCompanyId(Guid employeeId, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<Employee>()
                .Add(Restrictions.Eq("Id", employeeId))
                .Add(Restrictions.Eq("CompanyId", companyId))
                .SetMaxResults(1)
                .UniqueResult<Employee>();

            if (result == null)
                throw new EmployeeNotFoundException(employeeId);

            return result;

        }

        private DetachedCriteria CreateEmployeeSearchFilterQuery(long companyId, string employeeReferenceLike,
                                                                 string forenameLike, string surnameLike, long[] siteIds,
                                                                 bool showDeleted, int maximumResults,
                                                                 bool includeSiteless, bool excludeWithActiveUser,
                                                                 string orderBy, bool @ascending)
        {
            var result = DetachedCriteria.For(typeof(Employee), "employee");

            result.SetFetchMode("ContactDetails", FetchMode.Eager);
            result.SetFetchMode("EmergencyContactDetails", FetchMode.Eager);

            if (companyId != default(long))
            {
                result.Add(Restrictions.Eq("CompanyId", companyId));
            }

            result.Add(Restrictions.Eq("Deleted", showDeleted));

            if (!string.IsNullOrEmpty(employeeReferenceLike))
            {
                result.Add(Restrictions.InsensitiveLike("EmployeeReference", employeeReferenceLike));
            }

            if (!string.IsNullOrEmpty(forenameLike))
            {
                result.Add(Restrictions.InsensitiveLike("Forename", forenameLike));
            }

            if (!string.IsNullOrEmpty(surnameLike))
            {
                result.Add(Restrictions.InsensitiveLike("Surname", surnameLike));
            }

            if (siteIds != null && siteIds.Length > 0)
            {
                var siteRestriction = Restrictions.Disjunction()
                    .Add(Restrictions.In("Site.Id", siteIds));

                if (includeSiteless)
                    siteRestriction.Add(Restrictions.IsNull("Site.Id"));
                
                result.Add(siteRestriction);
            }

            if (excludeWithActiveUser)
            {
                result.CreateAlias("User", "user", JoinType.LeftOuterJoin);
                result.Add(Restrictions.Or
                    (
                        Restrictions.IsNull("user.Id"),
                        Restrictions.Eq("user.Deleted", true)
                    ));
            }

            if(!string.IsNullOrEmpty(orderBy))
            {
                result.AddOrder(new Order(orderBy, ascending));
            }

            if (maximumResults != default(long))
            {
                result.SetMaxResults(maximumResults);
            }
            else
            {
                result.SetMaxResults(1500);
            }

            result.SetResultTransformer(new DistinctRootEntityResultTransformer());

            return result;
        }

        public IEnumerable<Employee> GetByTermSearch(string searchTerm, long companyId, int pageLimit)
        {
            var foreNameCriteria = Restrictions.Or(
                                                   Restrictions.InsensitiveLike("Forename", searchTerm + "%"),
                                                   Restrictions.InsensitiveLike("Forename", "%" + searchTerm));
            var surenameNameCriteria = Restrictions.Or(
                                                   Restrictions.InsensitiveLike("Surname", searchTerm + "%"),
                                                   Restrictions.InsensitiveLike("Surname", "%" + searchTerm));
            return SessionManager
                           .Session
                           .CreateCriteria<Employee>()
                           .Add(Restrictions.Eq("CompanyId", companyId))
                           .Add(Restrictions.Eq("Deleted", false))
                           .Add(
                              Expression.Disjunction()
                                .Add(foreNameCriteria)
                                .Add(surenameNameCriteria)
                            )
                            .AddOrder(new Order("Surname", true))
                           .SetMaxResults(pageLimit)
                           .List<Employee>();
        }

        public IEnumerable<Employee> GetBySite(long siteId)
        {
            return SessionManager
                           .Session
                           .CreateCriteria<Employee>()
                           .Add(Restrictions.Eq("Site.Id", siteId))
                           .Add(Restrictions.Eq("Deleted", false))
                           .List<Employee>();
        }
    }
}