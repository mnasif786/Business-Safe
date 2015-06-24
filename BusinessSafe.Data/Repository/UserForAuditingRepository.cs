using System;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class UserForAuditingRepository : Repository<UserForAuditing, Guid>, IUserForAuditingRepository
    {
        public UserForAuditingRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public UserForAuditing GetByIdAndCompanyId(Guid userId, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<UserForAuditing>()
                .Add(Restrictions.Eq("Id", userId))
                .Add(Restrictions.Eq("Deleted", false))
                .Add(Restrictions.Eq("CompanyId", companyId))
                .SetMaxResults(1)
                .UniqueResult<UserForAuditing>();

            if (result == null)
            {
                throw new UserNotFoundException(userId);
            }
            
            return result;
        }

        public UserForAuditing GetSystemUser()
        {
            var user = GetById(new Guid("B03C83EE-39F2-4F88-B4C4-7C276B1AAD99"));
            return user;
        }
    }
}