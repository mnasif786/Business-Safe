using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class DocumentAlreadyAttachedToRiskAssessmentException: ApplicationException
    {
        public DocumentAlreadyAttachedToRiskAssessmentException(RiskAssessment riskAssessment, RiskAssessmentDocument document)
            : base(string.Format("Trying to attach document to risk assessment. Risk Assessment Id {0}. Document Id {1}", riskAssessment.Id, document.Id))
        {}
    }
}