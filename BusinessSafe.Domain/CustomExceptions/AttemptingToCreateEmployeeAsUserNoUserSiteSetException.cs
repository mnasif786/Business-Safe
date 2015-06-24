using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToCreateEmployeeAsUserNoUserSiteSetException : Exception
    {
        public AttemptingToCreateEmployeeAsUserNoUserSiteSetException(Employee employee, long newCompanyId)
            : base("Employee + (FullName: " + employee.FullName + ", Id: " + employee.Id.ToString() + ", CompanyId: " + employee.CompanyId.ToString() + " cannot be registered as user for company " + newCompanyId.ToString() + " no user site has been specified")
        {
        }

        public AttemptingToCreateEmployeeAsUserNoUserSiteSetException()
        {
        }
    }
}