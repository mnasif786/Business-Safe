using System;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class UserEmployeeDto
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeReference { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string JobTitle { get; set; }
        public string SiteName { get; set; }
        public string Role { get; set; }
        public bool Deleted { get; set; }

        public string FullName { get { return string.Format(Forename + " " + Surname); } }
    }
}