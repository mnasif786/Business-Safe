using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToCompleteRiskAssessmentReviewThatIsCompletedException : Exception
    {
        public AttemptingToCompleteRiskAssessmentReviewThatIsCompletedException(long reviewId)
            : base(string.Format("Trying to complete risk assessment review that is marked as completed. Risk Assessment Review Id {0}", reviewId))
        { }
    }
}