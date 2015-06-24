using System;
using System.Collections.Generic;

using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Request.Responsibilities
{
    public class CreateResponsibilityFromWizardRequest
    {
        public Guid UserId { get; set; }
        public long CompanyId { get; set; }
        public long[] SiteIds { get; set; }
        public List<ResponsibilityFromTemplateDetail> ResponsibilityFromTemplateDetails { get; set; }
    }

    public class ResponsibilityFromTemplateDetail
    {
        public Guid ResponsiblePersonEmployeeId { get; set; }
        public long ResponsibilityTemplateId { get; set; }
        public TaskReoccurringType FrequencyId { get; set; }
    }
}