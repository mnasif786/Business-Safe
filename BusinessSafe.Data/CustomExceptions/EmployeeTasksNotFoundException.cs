using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class EmployeeTasksNotFoundException : ArgumentException
    {
        public EmployeeTasksNotFoundException()
        { }

        public EmployeeTasksNotFoundException(Guid employeeId)
            : base(string.Format("Employee tasks not for found. Employee Id requested {0}", employeeId))
        {

        }
    }
}