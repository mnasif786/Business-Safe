using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToDeleteRiskAssessmentReviewTaskException : Exception
    {
        public AttemptingToDeleteRiskAssessmentReviewTaskException(long taskId)
            : base(string.Format("Trying to delete risk assessment review task that is marked as completed. Risk Assessment Review Task Id {0}", taskId))
        { }
    }
}