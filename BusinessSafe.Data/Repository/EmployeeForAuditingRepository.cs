using System;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class EmployeeForAuditingRepository : Repository<EmployeeForAuditing, Guid>, IEmployeeForAuditingRepository
    {
        public EmployeeForAuditingRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public EmployeeForAuditing GetByIdAndCompanyId(Guid employeeForAuditingId, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<EmployeeForAuditing>()
                .Add(Restrictions.Eq("Id", employeeForAuditingId))
                .Add(Restrictions.Eq("Deleted", false))
                .Add(Restrictions.Eq("CompanyId", companyId))
                .SetMaxResults(1)
                .UniqueResult<EmployeeForAuditing>();

            if (result == null)
            {
                throw new EmployeeForAuditingNotFoundException(employeeForAuditingId);
            }
            
            return result;
        }

        public EmployeeForAuditing GetByIdAndCompanyIdWithoutChecking(Guid employeeForAuditingId, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<EmployeeForAuditing>()
                .Add(Restrictions.Eq("Id", employeeForAuditingId))
                .Add(Restrictions.Eq("Deleted", false))
                .Add(Restrictions.Eq("CompanyId", companyId))
                .SetMaxResults(1)
                .UniqueResult<EmployeeForAuditing>();

            return result;
        }
    }
}