using BusinessSafe.Application.Request.FireRiskAssessments;
using FluentValidation.Results;

namespace BusinessSafe.Application.Contracts.FireRiskAssessments
{
    public interface IFireRiskAssessmentChecklistService
    {
        void Save(SaveFireRiskAssessmentChecklistRequest request);
        ValidationResult ValidateFireRiskAssessmentChecklist(ValidateCompleteFireRiskAssessmentChecklistRequest request);
        void MarkChecklistWithCompleteFailureAttempt(MarkChecklistWithCompleteFailureAttemptRequest request);
    }
}
