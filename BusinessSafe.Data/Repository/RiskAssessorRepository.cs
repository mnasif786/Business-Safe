using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;

using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class RiskAssessorRepository : Repository<RiskAssessor, long>, IRiskAssessorRepository
    {
        public RiskAssessorRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public RiskAssessor GetByIdAndCompanyId(long riskAssessorId, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<RiskAssessor>()
                .CreateAlias("Employee", "employee")
                .Add(Restrictions.Eq("Id", riskAssessorId))
                .Add(Restrictions.Eq("employee.CompanyId", companyId))
                .Add(Restrictions.Eq("Deleted", false))
                .SetMaxResults(1)
                .UniqueResult<RiskAssessor>();

            if (result != null && result.Site != null)
            {
                //load site to prevent the error, "collection [BusinessSafe.Domain.Entities.SiteStructureElement.Children] was not processed by flush() " 
                NHibernateUtil.Initialize(result.Site);
            }

            return result;
        }

        public RiskAssessor GetByIdAndCompanyIdIncludingDeleted(long riskAssessorId, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<RiskAssessor>()
                .CreateAlias("Employee", "employee")
                .Add(Restrictions.Eq("Id", riskAssessorId))
                .Add(Restrictions.Eq("employee.CompanyId", companyId))
                .SetMaxResults(1)
                .UniqueResult<RiskAssessor>();

            if (result.Site != null)
            {
                //load site to prevent the error, "collection [BusinessSafe.Domain.Entities.SiteStructureElement.Children] was not processed by flush() " 
                NHibernateUtil.Initialize(result.Site);
            }

            return result;
        }

        public IEnumerable<RiskAssessor> GetByCompanyId(long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<RiskAssessor>()
                .CreateAlias("Employee", "employee")
                .Add(Restrictions.Eq("employee.CompanyId", companyId))
                .Add(Restrictions.Eq("Deleted", false))
                .List<RiskAssessor>();

            return result;
        }

        public IEnumerable<RiskAssessor> Search(
            string searchTerm,
            long siteId,
            long companyId,
            int pageLimit,
            bool includeDeleted,
            bool excludeActive,
            bool showDistinct = true
            )
        {
            return Search(
                searchTerm,
                new []{siteId},
                companyId,
                pageLimit,
                includeDeleted,
                excludeActive,
                showDistinct);
        }


        public IEnumerable<RiskAssessor> Search(
            string searchTerm, 
            long[] siteIds, 
            long companyId, 
            int pageLimit, 
            bool includeDeleted, 
            bool excludeActive,
            bool showDistinct = true
        )
        {

            //Where(siteId=> siteId != 0)
            var formattedSearchTerm = searchTerm ?? string.Empty;

            var query = SessionManager.Session.Query<RiskAssessor>()
                .Where(x => x.Employee.CompanyId == companyId);

            if (siteIds.Any())
            {
                query = query.Where(x => x.HasAccessToAllSites ||  siteIds.Contains(x.Site.Id));
            }

            if (!includeDeleted)
            {
                query = query.Where(x => !x.Deleted);
            }

            if(excludeActive)
            {
                query = query.Where(x => x.Deleted);
            }

            // Employee filtering
            if (!string.IsNullOrEmpty(formattedSearchTerm))
            {
                query = query
                    .Where(x => (x.Employee.Forename.Contains(formattedSearchTerm) || x.Employee.Surname.Contains(formattedSearchTerm)));
            }


            query = query
                .OrderBy(x => x.Employee.Surname);

            if (pageLimit != default(int))
            {
                query = query.Take(pageLimit);
            }

            if(showDistinct)
            {
                query = query.Distinct(new RiskAssessorEqualityComparer());
            }

            return query.ToList();
        }
    }

    public class RiskAssessorEqualityComparer : IEqualityComparer<RiskAssessor>
    {
        public bool Equals(RiskAssessor x, RiskAssessor y)
        {
            return x.Employee.Id == y.Employee.Id;
        }

        public int GetHashCode(RiskAssessor obj)
        {
            return obj.Employee.GetHashCode();
        }
    }
}