using System;
using System.Collections.Generic;

namespace BusinessSafe.Application.Request.Responsibilities
{
    public class CreateManyResponsibilityTaskFromWizardRequest
    {
        public List<CreateResponsibilityTasksFromWizardRequest> TaskDetails { get; set; }
        public long CompanyId { get; set; }
        public Guid CreatingUserId { get; set; }
    }
}