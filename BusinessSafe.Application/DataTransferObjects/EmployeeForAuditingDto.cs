using System;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class EmployeeForAuditingDto
    {
        public Guid Id;
        public string Forename;
        public string Surname;
        public string FullName;
    }
}