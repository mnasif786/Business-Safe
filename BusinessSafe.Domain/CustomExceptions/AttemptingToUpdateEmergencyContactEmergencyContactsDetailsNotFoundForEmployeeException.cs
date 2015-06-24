using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToUpdateEmergencyContactEmergencyContactsDetailsNotFoundForEmployeeException : Exception
    {
        public AttemptingToUpdateEmergencyContactEmergencyContactsDetailsNotFoundForEmployeeException(Guid employeeId)
            : base(string.Format("Trying to update emergency contact. Employee emergency contact detail not found. Employee Id {0}", employeeId))
        { }

        public AttemptingToUpdateEmergencyContactEmergencyContactsDetailsNotFoundForEmployeeException()
        { }
    }
}