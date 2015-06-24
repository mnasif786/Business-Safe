using System;

namespace BusinessSafe.Application.Implementations
{
    public class TryingToDeleteEmployeeWithOutstandingTasksException : ArgumentException
    {
        public TryingToDeleteEmployeeWithOutstandingTasksException(Guid employeeId)
            : base(string.Format("Trying to delete employee with outstanding tasks. Employee Id is {0}", employeeId))
        {}
    }
}