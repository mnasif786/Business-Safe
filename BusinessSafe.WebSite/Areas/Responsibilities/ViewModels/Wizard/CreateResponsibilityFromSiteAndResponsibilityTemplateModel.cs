using System;
using System.Collections.Generic;

using BusinessSafe.Domain.Entities;

namespace BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard
{
    public class CreateResponsibilityFromSiteAndResponsibilityTemplateModel
    {
        public long[] SiteIds { get; set; }
        public IEnumerable<ConfiguredResponsibilityFromTemplate> Responsibilities { get; set; }
    }

    public class ConfiguredResponsibilityFromTemplate
    {
        public long ResponsibilityTemplateId { get; set; }
        public TaskReoccurringType FrequencyId { get; set; }
        public Guid? ResponsiblePersonEmployeeId { get; set; }
    }
}