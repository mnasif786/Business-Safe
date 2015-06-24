using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToCreateEmployeeAsUserWhenUserExistsException: Exception
    {
        public AttemptingToCreateEmployeeAsUserWhenUserExistsException(Employee employee)
            : base(string.Format("A user for Employee + (FullName: {0}, Id: {1}, CompanyId: {2} cannot be created because the user has already been created.", employee.FullName, employee.Id.ToString(), employee.CompanyId.ToString()))
        {
        }
    }
}
