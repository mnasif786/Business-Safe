using System;

namespace BusinessSafe.Application.Request
{
    public class MarkRiskAssessmentDraftStatusRequestBase
    {
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
        public long RiskAssessmentId { get; set; }
    }

    public class ReinstateRiskAssessmentAsDeletedRequest : MarkRiskAssessmentDraftStatusRequestBase { }

    public class MarkRiskAssessmentAsDeletedRequest: MarkRiskAssessmentDraftStatusRequestBase { }
    
    public class MarkRiskAssessmentAsLiveRequest : MarkRiskAssessmentDraftStatusRequestBase { }

    public class MarkRiskAssessmentAsDraftRequest : MarkRiskAssessmentDraftStatusRequestBase { }

    public class MarkRiskAssessmentAsArchivedRequest : MarkRiskAssessmentDraftStatusRequestBase { }

    public class MarkRiskAssessmentTasksAsNoLongerRequiredRequest : MarkRiskAssessmentDraftStatusRequestBase { }
}