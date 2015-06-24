using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToDeleteRiskAssessmentReviewTaskWithNoRiskAssessmentReviewSetException : Exception
    {
        public AttemptingToDeleteRiskAssessmentReviewTaskWithNoRiskAssessmentReviewSetException(long taskId)
            : base(string.Format("Trying to delete risk assessment review task which has not risk assessment review. Risk Assessment Review Task Id {0}", taskId))
        { }
    }
}