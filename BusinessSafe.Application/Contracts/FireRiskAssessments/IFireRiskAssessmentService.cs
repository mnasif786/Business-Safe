using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.FireRiskAssessments;

namespace BusinessSafe.Application.Contracts.FireRiskAssessments
{
    public interface IFireRiskAssessmentService
    {
        long CreateRiskAssessment(CreateRiskAssessmentRequest request);
        void UpdateRiskAssessmentSummary(SaveRiskAssessmentSummaryRequest request);
        FireRiskAssessmentDto GetRiskAssessment(long riskAssessmentId, long companyId);
        FireRiskAssessmentDto GetRiskAssessmentWithFireSafetyControlMeasuresAndPeopleAtRisk(long riskAssessmentId, long companyId);
        void UpdatePremisesInformation(UpdateFireRiskAssessmentPremisesInformationRequest request);
        IEnumerable<FireRiskAssessmentDto> Search(SearchRiskAssessmentsRequest riskAssessmentsSearchRequest);
        int Count(SearchRiskAssessmentsRequest riskAssessmentsSearchRequest);
        FireRiskAssessmentDto GetWithLatestFireRiskAssessmentChecklist(long riskAssessmentId, long companyId);
        void SaveFireRiskAssessmentChecklist(SaveFireRiskAssessmentChecklistRequest request);
        void CompleteFireRiskAssessmentChecklist(CompleteFireRiskAssessmentChecklistRequest request);
        FireRiskAssessmentDto GetRiskAssessmentWithSignificantFindings(long riskAssessmentId, long companyId);
        long CopyRiskAssessment(CopyRiskAssessmentRequest copyRiskAssessmentRequest);
        void CompleteFireRiskAssessementReview(CompleteRiskAssessmentReviewRequest request);
        void CopyForMultipleSites(CopyRiskAssessmentForMultipleSitesRequest request);
    }
}