using System;

namespace BusinessSafe.WebSite.Custom_Exceptions
{
    public class EmploymentStatusNotFoundForEmployeeException : ArgumentNullException
    {
        public EmploymentStatusNotFoundForEmployeeException(Guid employeeId, long siteId)
            : base(string.Format("Employment Status not found for Employee. Employment Status Id {0}. Employee Id {1}", siteId, employeeId.ToString()))
        {
        }
    }
}