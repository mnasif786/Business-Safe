using System;
using System.Collections.Generic;

namespace BusinessSafe.Application.Request
{
    public class AddEmployeesToChecklistGeneratorRequest
    {
        public long PersonalRiskAssessmentId { get; set; }
        public long CompanyId { get; set; }
        public IList<Guid> EmployeeIds { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}
