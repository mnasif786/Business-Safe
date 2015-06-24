using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToReassignFurtherActionTaskThatIsCompletedException : Exception
    {
        public AttemptingToReassignFurtherActionTaskThatIsCompletedException(long taskId)
            : base(string.Format("Trying to reassign further action task that is marked as completed. Further Action Task Id {0}", taskId))
        { }

        public AttemptingToReassignFurtherActionTaskThatIsCompletedException()
        { }
    }
}