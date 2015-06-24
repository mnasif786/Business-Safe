using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToMarkAsNoLongerRequiredFurtherActionTaskThatIsCompletedException : Exception
    {
        public AttemptingToMarkAsNoLongerRequiredFurtherActionTaskThatIsCompletedException(long taskId)
            : base(string.Format("Trying to mark as no longer required further action task that is marked as completed. Further Action Task Id {0}", taskId))
        { }

        public AttemptingToMarkAsNoLongerRequiredFurtherActionTaskThatIsCompletedException()
        { }
    }
}