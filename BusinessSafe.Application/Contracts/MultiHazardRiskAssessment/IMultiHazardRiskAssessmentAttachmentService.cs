using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Contracts.MultiHazardRiskAssessment
{
    public interface IMultiHazardRiskAssessmentAttachmentService
    {
        void AttachHazardsToRiskAssessment(AttachHazardsToRiskAssessmentRequest request);
    }
}