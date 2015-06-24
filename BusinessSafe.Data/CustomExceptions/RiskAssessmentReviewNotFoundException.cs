using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class RiskAssessmentReviewNotFoundException : NullReferenceException
    {
        public RiskAssessmentReviewNotFoundException(long riskAssessmentReviewId)
            : base(string.Format("Risk Assessment Review Not Found. Risk Assessment Review not found for id {0}", riskAssessmentReviewId))
        {
        }
    }
}