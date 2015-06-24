using System;
using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Contracts.PersonalRiskAssessments
{
    public interface IPersonalRiskAssessmentService
    {
        long CreateRiskAssessment(CreatePersonalRiskAssessmentRequest request);
        long CreateRiskAssessmentWithChecklist(CreateRiskAssessmentRequest createRiskAssessmentRequest, Guid checklistId);
        PersonalRiskAssessmentDto GetRiskAssessment(long riskAssessmentId, long companyId, Guid currentUserId);
        PersonalRiskAssessmentDto GetWithReviews(long riskAssessmentId, long companyId, Guid currentUserId);
        PersonalRiskAssessmentDto GetRiskAssessmentWithHazards(long riskAssessmentId, long companyId, Guid currentUserId);
        PersonalRiskAssessmentDto GetWithChecklistGeneratorEmployeesAndChecklists(long riskAssessmentId, long companyId, Guid currentUserId);
        PersonalRiskAssessmentDto GetWithEmployeeChecklists(long riskAssessmentId, long companyId, Guid currentUserId);
        IEnumerable<PersonalRiskAssessmentDto> Search(SearchRiskAssessmentsRequest riskAssessmentsSearchRequest);
        int Count(SearchRiskAssessmentsRequest riskAssessmentsSearchRequest);
        void UpdateRiskAssessmentSummary(UpdatePersonalRiskAssessmentSummaryRequest request);
        void SaveChecklistGenerator(SaveChecklistGeneratorRequest request);
        void AddEmployeesToChecklistGenerator(AddEmployeesToChecklistGeneratorRequest request);
        void ResetChecklistAfterGenerate(ResetAfterChecklistGenerateRequest request);
        void SetAsGenerating(long personalRiskAssessmentId);
        void UpdateRiskAssessment(SavePersonalRiskAssessmentRequest request);
        void RemoveEmployeeFromCheckListGenerator(long riskAssessmentId, long companyId, Guid employeeId, Guid userId);
        long CreateRiskAssessmentFromChecklist(Guid employeeChecklistId, Guid userId);
		bool CanUserAccess(long riskAssessmentId, long companyId, Guid employeeId);
    }
}