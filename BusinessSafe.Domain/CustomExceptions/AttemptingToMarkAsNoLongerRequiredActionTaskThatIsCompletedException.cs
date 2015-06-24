using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToMarkAsNoLongerRequiredActionTaskThatIsCompletedException : Exception
    {
        public AttemptingToMarkAsNoLongerRequiredActionTaskThatIsCompletedException(long taskId)
            : base(string.Format("Trying to mark as no longer required action task that is already marked as no longer required. Action Task Id {0}", taskId))
        { }

        public AttemptingToMarkAsNoLongerRequiredActionTaskThatIsCompletedException()
        { }
    }
}