using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToMarkAsNoLongerRequiredResponsibilityTaskThatIsCompletedException : Exception
    {
        public AttemptingToMarkAsNoLongerRequiredResponsibilityTaskThatIsCompletedException(long taskId)
            : base(string.Format("Trying to mark as no longer required responsibility task that is already marked as no longer required. Responsibility Task Id {0}", taskId))
        { }

        public AttemptingToMarkAsNoLongerRequiredResponsibilityTaskThatIsCompletedException()
        { }
    }
}