using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using System;
using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Contracts.RiskAssessments
{
    public interface IRiskAssessmentService
    {
        RiskAssessmentDto GetByIdAndCompanyId(long riskAssessmentId, long companyId);
        bool HasUncompletedTasks(long companyId, long riskAssessmentId);
        bool HasUndeletedTasks(long companyId, long riskAssessmentId);
        DateTime? GetDefaultDateOfNextReviewById(long riskAssessmentId);
        void MarkRiskAssessmentAsDraft(MarkRiskAssessmentAsDraftRequest request);
        void MarkRiskAssessmentAsLive(MarkRiskAssessmentAsLiveRequest request);
        void MarkRiskAssessmentAsDeleted(MarkRiskAssessmentAsDeletedRequest request);
        void ReinstateRiskAssessmentAsNotDeleted(ReinstateRiskAssessmentAsDeletedRequest request);
        bool CanMarkRiskAssessmentAsLive(long companyId, long riskAssessmentId);
        void MarkAllRealtedUncompletedTasksAsNoLongerRequired(MarkRiskAssessmentTasksAsNoLongerRequiredRequest request);
    }
}
