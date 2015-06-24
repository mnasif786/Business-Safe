using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToMarkAsNoLongerRequiredRiskAssessmentReviewTaskThatIsNoLongerRequiredException : Exception
    {
        public AttemptingToMarkAsNoLongerRequiredRiskAssessmentReviewTaskThatIsNoLongerRequiredException(long taskId)
            : base(string.Format("Trying to mark as no longer required risk assessment review task that is already marked as no longer required. Risk Assessment Review Task Id {0}", taskId))
        { }

        public AttemptingToMarkAsNoLongerRequiredRiskAssessmentReviewTaskThatIsNoLongerRequiredException()
        { }
    }
}