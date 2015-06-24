using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Contracts.FireRiskAssessments
{
    public interface IFireRiskAssessmentAttachmentService
    {
        void AttachPeopleAtRiskToRiskAssessment(AttachPeopleAtRiskToRiskAssessmentRequest request);
        void AttachFireSafetyControlMeasuresToRiskAssessment(AttachFireSafetyControlMeasuresToRiskAssessmentRequest request);
        void AttachSourcesOfIgnitionToRiskAssessment(AttachSourceOfIgnitionToRiskAssessmentRequest request);
        void AttachSourcesOfFuelToRiskAssessment(AttachSourceOfFuelToRiskAssessmentRequest request);
    }
}