using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Models;

namespace BusinessSafe.WebSite.Helpers
{
    public interface IReviewAuditDocumentHelper
    {
        ReviewAuditDocumentResult CreateReviewAuditDocument(RiskAssessmentType riskAssessmentType, RiskAssessmentDto riskAssessment);
    }
}