using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class FireAnswerNotFoundException : ArgumentNullException
    {
        public FireAnswerNotFoundException(long fireRiskAssessmentChecklistId, long questionId)
            : base(string.Format("Fire Answer Not Found. Searching answer for Fire Risk Assessment Checklist Id is {0} and Question Id is {1}", fireRiskAssessmentChecklistId, questionId))
        {
        }
    }
}