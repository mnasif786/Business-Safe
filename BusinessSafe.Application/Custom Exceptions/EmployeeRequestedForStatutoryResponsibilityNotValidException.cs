using System;

namespace BusinessSafe.Application.Custom_Exceptions
{
    public class EmployeeRequestedForStatutoryResponsibilityNotValidException : Exception
    {
        public EmployeeRequestedForStatutoryResponsibilityNotValidException() : base(string.Format("Employee requested when creating a Statutory Responsibility not valid")) { }
    }
}