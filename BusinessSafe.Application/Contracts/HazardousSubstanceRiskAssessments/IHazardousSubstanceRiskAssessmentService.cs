using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using System.Collections.Generic;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;

namespace BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments
{
    public interface IHazardousSubstanceRiskAssessmentService
    {
        long CreateRiskAssessment(SaveHazardousSubstanceRiskAssessmentRequest request);
        void UpdateRiskAssessmentDescription(SaveHazardousSubstanceRiskAssessmentRequest request);
        HazardousSubstanceRiskAssessmentDto GetRiskAssessment(long hazardousSubstanceAssessmentId, long companyId);
        IEnumerable<HazardousSubstanceRiskAssessmentDto> Search(SearchHazardousSubstanceRiskAssessmentsRequest request);

        void UpdateHazardousSubstanceRiskAssessmentAssessmentDetails(
            UpdateHazardousSubstanceRiskAssessmentAssessmentDetailsRequest request);

        long AddControlMeasureToRiskAssessment(AddControlMeasureRequest request);
        void UpdateControlMeasure(UpdateControlMeasureRequest request);
        void RemoveControlMeasureFromRiskAssessment(RemoveControlMeasureRequest request);

        void UpdateRiskAssessmentSummary(SaveHazardousSubstanceRiskAssessmentRequest isAny);

        long CopyRiskAssessment(CopyRiskAssessmentRequest copyRiskAssessmentRequest);
        void SaveLastRecommendedControlSystem(SaveLastRecommendedControlSystemRequest request);
        void CopyForMultipleSites(CopyRiskAssessmentForMultipleSitesRequest request);
        int Count(SearchHazardousSubstanceRiskAssessmentsRequest request);
    }
}