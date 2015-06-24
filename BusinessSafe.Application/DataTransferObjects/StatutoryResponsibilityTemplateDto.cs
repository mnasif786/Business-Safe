using System.Collections.Generic;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class StatutoryResponsibilityTemplateDto
    {
        public long Id { get; set; } 
        public ResponsibilityCategoryDto ResponsibilityCategory { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string GuidanceNote { get; set; }
        public ResponsibilityReasonDto ResponsibilityReason { get; set; }
        public TaskReoccurringType TaskReoccurringType { get; set; }
        public IEnumerable<StatutoryResponsibilityTaskTemplateDto> ResponsibilityTasks { get; set; }
    }
}
