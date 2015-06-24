using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToCompleteTaskThatIsCompletedException : Exception
    {
        public AttemptingToCompleteTaskThatIsCompletedException(long taskId)
            : base(string.Format("Trying to complete task that is marked as completed. Task Id {0}", taskId))
        { }

        public AttemptingToCompleteTaskThatIsCompletedException()
        { }
    }
}