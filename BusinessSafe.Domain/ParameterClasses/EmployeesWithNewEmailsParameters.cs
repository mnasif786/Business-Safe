using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.ParameterClasses
{
    public class EmployeesWithNewEmailsParameters
    {
        public Employee Employee { get; set; }
        public string NewEmail { get; set; }
    }
}
