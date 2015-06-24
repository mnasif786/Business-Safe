using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using NHibernate.Criterion;

namespace BusinessSafe.Data.Repository
{
    public class EmployeeEmergencyContactDetailsRepository : Repository<EmployeeEmergencyContactDetail, int>, IEmployeeEmergencyContactDetailsRepository
    {
        public EmployeeEmergencyContactDetailsRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        { }

        public EmployeeEmergencyContactDetail GetByIdAndCompanyId(int id, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<EmployeeEmergencyContactDetail>()
                .CreateAlias("Employee", "employee")
                .Add(Restrictions.Eq("Id", id))
                .Add(Restrictions.Eq("employee.CompanyId", companyId))
                .Add(Restrictions.Eq("Deleted", false))
                .SetMaxResults(1)
                .UniqueResult<EmployeeEmergencyContactDetail>();

            return result;
        }
    }
}