using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class EmployeeForAuditingNotFoundException : ArgumentNullException
    {
        public EmployeeForAuditingNotFoundException(Guid employeeForAuditingId)
            : base(string.Format("Employee For Auditing Not Found with id {0}", employeeForAuditingId))
        {
        }
    }
}