using System;
using BusinessSafe.Domain.Entities;
using NHibernate;
using NHibernate.Criterion;

namespace BusinessSafe.EscalationService.Queries
{
    public class GetEmployeeQuery : IGetEmployeeQuery
    {
        private Guid _employeeId;
        private long _companyId;

        public Employee Execute(ISession session)
        {
            var result = session
                .CreateCriteria<Employee>()
                .Add(Restrictions.Eq("Id", _employeeId))
                .Add(Restrictions.Eq("CompanyId", _companyId))
                .SetMaxResults(1)
                .UniqueResult<Employee>();

            return result;
        }

        public IGetEmployeeQuery WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IGetEmployeeQuery WithEmployeeId(Guid employeeId)
        {
            _employeeId = employeeId;
            return this;
        }
    }
}