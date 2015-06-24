using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToCompleteFurtherControlMeasureTaskThatIsNotRequiredException : Exception
    {
        public AttemptingToCompleteFurtherControlMeasureTaskThatIsNotRequiredException(long taskId)
            : base(string.Format("Trying to complete further action task that is marked as not required. Further Action Task Id {0}", taskId))
        { }

        public AttemptingToCompleteFurtherControlMeasureTaskThatIsNotRequiredException()
        { }
    }
}