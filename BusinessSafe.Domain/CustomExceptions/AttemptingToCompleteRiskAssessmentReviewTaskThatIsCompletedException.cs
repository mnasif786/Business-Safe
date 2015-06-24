using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToCompleteRiskAssessmentReviewTaskThatIsCompletedException : Exception
    {
        public AttemptingToCompleteRiskAssessmentReviewTaskThatIsCompletedException(long taskId)
            : base(string.Format("Trying to complete risk assessment review task that is marked as completed. Risk Assessment Review Id {0}", taskId))
        { }

        public AttemptingToCompleteRiskAssessmentReviewTaskThatIsCompletedException()
        { }
    }
}