using BusinessSafe.Application.Request;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.Application.Contracts.RiskAssessments
{
    public interface IRiskAssessmentHazardService
    {
        void UpdateRiskAssessmentHazardDescription(UpdateRiskAssessmentHazardDescriptionRequest request);
        void UpdateRiskAssessmentHazardTitle(UpdateRiskAssessmentHazardTitleRequest request);
        long AddControlMeasureToRiskAssessmentHazard(AddControlMeasureRequest request);
        void RemoveControlMeasureFromRiskAssessmentHazard(RemoveControlMeasureRequest request);
        void UpdateControlMeasureForRiskAssessmentHazard(UpdateControlMeasureRequest request);
        RiskAssessmentHazardDto GetByIdAndCompanyId(long id, long companyId);
    }
}