using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToCompleteRiskAssessmentReviewWithoutArchiveOrNextReviewDateSetException : Exception
    {
        public AttemptingToCompleteRiskAssessmentReviewWithoutArchiveOrNextReviewDateSetException(long reviewId)
            : base(string.Format("Trying to complete risk assessment review without either Archive or NextReviewDate supplied. Risk Assessment Review Id {0}", reviewId))
        { }
    }
}