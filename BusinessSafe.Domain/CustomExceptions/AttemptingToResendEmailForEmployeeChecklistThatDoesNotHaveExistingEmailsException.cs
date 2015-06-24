using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToResendEmailForEmployeeChecklistThatDoesNotHaveExistingEmailsException : Exception
    {
        public AttemptingToResendEmailForEmployeeChecklistThatDoesNotHaveExistingEmailsException(Guid employeeChecklistId)
            : base(string.Format("Trying to resend checklist email for emails that do not exist. Employee Checklist Id {0}", employeeChecklistId))
        { }

        public AttemptingToResendEmailForEmployeeChecklistThatDoesNotHaveExistingEmailsException()
        { }
    }
}