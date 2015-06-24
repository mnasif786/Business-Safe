using System;
using BusinessSafe.Domain.Entities;
using NHibernate;

namespace BusinessSafe.EscalationService.Queries
{
    public  interface IGetEmployeeQuery
    {
        Employee Execute(ISession sessionManager);
        IGetEmployeeQuery WithCompanyId(long companyId);
        IGetEmployeeQuery WithEmployeeId(Guid employeeId);
    }
}