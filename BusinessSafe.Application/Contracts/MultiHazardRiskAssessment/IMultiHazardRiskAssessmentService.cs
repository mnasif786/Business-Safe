using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Contracts.MultiHazardRiskAssessment
{
    public interface IMultiHazardRiskAssessmentService
    {
        bool CanRemoveRiskAssessmentHazard(long companyId, long riskAssessmentId, long riskAssessmentHazardId);
        void UpdateRiskAssessmentPremisesInformation(SaveRiskAssessmentPremisesInformationRequest request);
    }
}