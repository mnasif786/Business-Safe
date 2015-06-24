using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        
        public IEnumerable<User> GetAllByCompanyId(long companyId)
        {
            return SessionManager
                .Session
                .CreateCriteria<User>()
                .Add(Restrictions.Or(Restrictions.Eq("CompanyId", companyId), Restrictions.Eq("CompanyId", (long)0)))
                .Add(Restrictions.Eq("Deleted", false))
                //Todo: order by Employee.Surname
                //.AddOrder(Property.ForName("Surname").Asc())
                .List<User>();
        }

        public User GetByIdAndCompanyId(Guid userId, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<User>()
                .Add(Restrictions.Eq("Id", userId))
                .Add(Restrictions.Eq("Deleted", false))
                .Add(Restrictions.Eq("CompanyId", companyId))
                //.CreateAlias("Site", "site", JoinType.LeftOuterJoin)
                //.SetFetchMode("site", FetchMode.Eager)
                .SetMaxResults(1)
                .UniqueResult<User>();

            //if (result == null)
            //{
            //    throw new UserNotFoundException(userId);
            //}

            if (result != null && result.Site != null && result.Site.Id == 0)
            {
                //load site to prevent the error, "collection [BusinessSafe.Domain.Entities.SiteStructureElement.Children] was not processed by flush() " 
                NHibernateUtil.Initialize(result.Site);
            }

            return result;
        }

        public IEnumerable<User> GetByIdsAndCompanyId(Guid[] ids, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<User>()
                .Add(Restrictions.In("Id", ids))
                .Add(Restrictions.Eq("Deleted", false))
                .Add(Restrictions.Eq("CompanyId", companyId))
                .List<User>();

            return result;
        }

        public User GetByIdAndCompanyIdIncludeDeleted(Guid userId, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<User>()
                .Add(Restrictions.Eq("Id", userId))
                .Add(Restrictions.Eq("CompanyId", companyId))
                .SetFetchMode("Employee", FetchMode.Eager)
                .SetFetchMode("Site", FetchMode.Eager)
                .SetFetchMode("Role", FetchMode.Eager)
                .CreateAlias("Role", "r")
                .SetFetchMode("r.Permissions", FetchMode.Eager)
                .SetMaxResults(1)
                .UniqueResult<User>();

            if (result == null)
            {
                throw new UserNotFoundException(userId);
            }

            return result;
        }

        public User GetSystemUser()
        {
            var user = GetById(new Guid("B03C83EE-39F2-4F88-B4C4-7C276B1AAD99"));

            if (user.Site != null && user.Site.Id==0)
            {
                //load site to prevent the error, "collection [BusinessSafe.Domain.Entities.SiteStructureElement.Children] was not processed by flush() " 
                NHibernateUtil.Initialize(user.Site);
            }
            return user;
        }

        public IEnumerable<User> Search(
            long companyId, 
            string forenameLike, 
            string surnameLike, 
            IList<long> allowableSites, 
            long siteId,
            bool showDeleted, 
            int maximumResults)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<User>();

            if (companyId != default(long))
            {
                result.Add(Restrictions.Eq("CompanyId", companyId));
            }

            result.Add(Restrictions.Eq("Deleted", showDeleted));

            if (allowableSites != null && allowableSites.Count > 0)
            {
                result.CreateCriteria("Site", "site");
                result.Add(Restrictions.In("site.Id", allowableSites.ToArray()));
            }

            if (siteId != default(long))
            {
                result.Add(Restrictions.Eq("site.Id", siteId));
            }

            result.CreateCriteria("Employee", "employee");

            if (!string.IsNullOrEmpty(forenameLike))
            {
                result.Add(Restrictions.InsensitiveLike("employee.Forename", forenameLike));
            }

            if (!string.IsNullOrEmpty(surnameLike))
            {
                result.Add(Restrictions.InsensitiveLike("employee.Surname", surnameLike));
            }

            result.SetMaxResults(maximumResults != default(long) ? maximumResults : 1000);

            return result.List<User>();
        }

        public IEnumerable<User> GetAllByCompanyIdAndRole(long companyId, Guid roleId)
        {
            return SessionManager
                .Session
                .CreateCriteria<User>()
                .Add(Restrictions.Or(Restrictions.Eq("CompanyId", companyId), Restrictions.Eq("CompanyId", (long)0)))
                .Add(Restrictions.Eq("Deleted", false))
                .Add(Restrictions.Eq("Role.Id", roleId))
                .List<User>();
        }
    }
}