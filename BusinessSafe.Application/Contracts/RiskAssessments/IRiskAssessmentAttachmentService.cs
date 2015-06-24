using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Contracts.RiskAssessments
{
    public interface IRiskAssessmentAttachmentService
    {
        List<DocumentDto> AttachDocumentsToRiskAssessment(AttachDocumentsToRiskAssessmentRequest request);
        void DetachDocumentsToRiskAssessment(DetachDocumentsFromRiskAssessmentRequest request);
        void AttachNonEmployeeToRiskAssessment(AttachNonEmployeeToRiskAssessmentRequest request);
        void DetachNonEmployeeFromRiskAssessment(DetachNonEmployeeFromRiskAssessmentRequest request);
        void AttachEmployeeToRiskAssessment(AttachEmployeeRequest request);
        void DetachEmployeeFromRiskAssessment(DetachEmployeeRequest request);
        IEnumerable<DocumentDto> GetRiskAssessmentAttachedDocuments(long riskAssessmentId, long companyId);
    }
}