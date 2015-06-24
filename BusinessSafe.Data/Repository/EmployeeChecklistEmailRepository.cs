using System;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class EmployeeChecklistEmailRepository : Repository<EmployeeChecklistEmail, Guid>, IEmployeeChecklistEmailRepository
    {
        public EmployeeChecklistEmailRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

    }
}