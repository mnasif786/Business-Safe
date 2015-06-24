using System;
using System.Collections.Generic;

using BusinessSafe.Application.Request.Documents;

namespace BusinessSafe.Application.Request
{
    public class CompleteRiskAssessmentReviewRequest
    {
        public long ClientId { get; set; }

        //todo: ptd - is this needed? don't see why it should be here because we have riskassessmentreviewId - remove if not needed.,
        public long RiskAssessmentId { get; set; }

        public long RiskAssessmentReviewId { get; set; }
        public Guid ReviewingUserId { get; set; }
        public bool IsComplete { get; set; }
        public string CompletedComments { get; set; }
        public DateTime? NextReviewDate { get; set; }
        public bool Archive { get; set; }
        public IEnumerable<CreateDocumentRequest> CreateDocumentRequests { get; set; }
    }
}
