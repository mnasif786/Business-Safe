using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class EmployeeNotFoundException : ArgumentException
    {
        public EmployeeNotFoundException()
        { }

        public EmployeeNotFoundException(Guid employeeId)
            : base(string.Format("Employee not for found. Employee Id requested {0}", employeeId))
        {

        }
    }
}