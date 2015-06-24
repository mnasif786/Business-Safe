using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class ContactDetailsAlreadyAttachedToEmployeeException : ApplicationException
    {
        public ContactDetailsAlreadyAttachedToEmployeeException(Employee employee, EmployeeContactDetail contactDetails)
            : base(string.Format("Trying to attach contact details to employee. Employee Id {0}. Contact Details Id {1}", employee.Id, contactDetails.Id))
        { }
    }
}