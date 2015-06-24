using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Contracts.GeneralRiskAssessments
{
    public interface IGeneralRiskAssessmentService
    {
        long CreateRiskAssessment(CreateRiskAssessmentRequest request);
        GeneralRiskAssessmentDto GetRiskAssessment(long riskAssessmentId, long companyId);
        GeneralRiskAssessmentDto GetRiskAssessmentWithHazards(long riskAssessmentId, long companyId);
        GeneralRiskAssessmentDto GetRiskAssessmentWithHazardsAndPeopleAtRisk(long riskAssessmentId, long companyId);
        IEnumerable<GeneralRiskAssessmentDto> Search(SearchRiskAssessmentsRequest riskAssessmentsSearchRequest);
        int Count(SearchRiskAssessmentsRequest riskAssessmentsSearchRequest);
        IEnumerable<string> GetPeopleAtRiskDisplayWarningMessages(long riskAssessmentId, long companyId);
        long CopyRiskAssessment(CopyRiskAssessmentRequest copyRiskAssessmentRequest);
        void UpdateRiskAssessmentSummary(SaveRiskAssessmentSummaryRequest request);
        void UpdateRiskAssessment(SaveGeneralRiskAssessmentRequest request);
        void CopyForMultipleSites(CopyRiskAssessmentForMultipleSitesRequest request);
    }
}