using System;
using System.Collections.Generic;

using BusinessSafe.Application.Request.Documents;

namespace BusinessSafe.Application.Request
{
    public class AttachDocumentsToRiskAssessmentRequest
    {
        public Guid UserId { get; set; }
        public long CompanyId { get; set; }
        public long RiskAssessmentId { get; set; }
        public List<CreateDocumentRequest> DocumentsToAttach { get; set; }
    }
}