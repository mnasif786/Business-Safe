using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class EmployeeChecklistNotFoundException : ArgumentNullException
    {
        public EmployeeChecklistNotFoundException(Guid employeeChecklistId)
            : base(string.Format("EmployeeChecklist Not Found. EmployeeChecklist not found for id {0}", employeeChecklistId))
        {
        }
    }
}