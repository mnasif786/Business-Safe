using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToDeleteFurtherControlMeasureTaskThatIsCompletedException : Exception
    {
        public AttemptingToDeleteFurtherControlMeasureTaskThatIsCompletedException(long taskId)
            : base(string.Format("Trying to delete further control measure task that is marked as completed. Further Control Measure Task Id {0}", taskId))
        { }

        public AttemptingToDeleteFurtherControlMeasureTaskThatIsCompletedException()
        { }
    }
}