using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSafe.Application.Request
{
    public class EditRiskAssessmentReviewRequest
    {
        public long RiskAssessmentReviewId { get; set; }
        public long CompanyId { get; set; }
        public DateTime ReviewDate { get; set; }
        public Guid ReviewingEmployeeId { get; set; }
        public Guid AssigningUserId { get; set; }
    }
}
