using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToCompleteFurtherControlMeasureTaskThatIsCompletedException : Exception
    {
        public AttemptingToCompleteFurtherControlMeasureTaskThatIsCompletedException(long taskId)
            : base(string.Format("Trying to complete further action task that is marked as completed. Further Action Task Id {0}", taskId))
        { }

        public AttemptingToCompleteFurtherControlMeasureTaskThatIsCompletedException()
        { }
    }
}