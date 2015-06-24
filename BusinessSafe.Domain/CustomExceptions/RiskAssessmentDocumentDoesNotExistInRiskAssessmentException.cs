using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class RiskAssessmentDocumentDoesNotExistInRiskAssessmentException : ApplicationException
    {
        public RiskAssessmentDocumentDoesNotExistInRiskAssessmentException(RiskAssessment riskAssessment, long documentId)
            : base(string.Format("Could not find risk assessment document for Risk Assessment Id {0}. Risk Assessment Document Id {1}", riskAssessment.Id, documentId))
        { }
    }
}