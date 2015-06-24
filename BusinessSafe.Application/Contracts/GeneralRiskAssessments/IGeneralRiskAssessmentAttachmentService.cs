using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Contracts.GeneralRiskAssessments
{
    public interface IGeneralRiskAssessmentAttachmentService
    {
        void AttachPeopleAtRiskToRiskAssessment(AttachPeopleAtRiskToRiskAssessmentRequest request);
    }
}


