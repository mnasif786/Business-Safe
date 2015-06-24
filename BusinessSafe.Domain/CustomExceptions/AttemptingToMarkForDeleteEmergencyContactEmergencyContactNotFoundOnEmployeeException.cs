using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToMarkForDeleteEmergencyContactEmergencyContactNotFoundOnEmployeeException : Exception
    {
        public AttemptingToMarkForDeleteEmergencyContactEmergencyContactNotFoundOnEmployeeException(Guid id, long emergencyContactId)
            : base(string.Format("Trying to mark emergency contact for delete. Emegency contact not found on employee. Employee Id {0}. Emergency Contact Id {1}", id, emergencyContactId))
        {}
    }
}