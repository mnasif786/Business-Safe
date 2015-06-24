using System;
using System.Collections.Generic;

namespace BusinessSafe.Application.Request
{
    public class MarkTaskDocumentsAsDeletedRequest
    {
        public List<long> TaskDoumentsToDelteIds { get; set; }
        public long RiskAssessmentId { get; set; }
        public long RiskAssessmentHazardId { get; set; }
        public long FurtherControlMeasureTaskId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
}