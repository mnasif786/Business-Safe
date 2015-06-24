using System;
using System.Collections.Generic;
using System.ComponentModel;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class ResponsibilityDto
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public ResponsibilityCategoryDto ResponsibilityCategory { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public SiteStructureElementDto Site { get; set; }
        public ResponsibilityReasonDto ResponsibilityReason { get; set; }
        public EmployeeDto Owner { get; set; }
        public TaskReoccurringType InitialTaskReoccurringType { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Deleted { get; set; }
        public DateTime? NextDueDate { get; set; }
        public DerivedTaskStatusForDisplay StatusDerivedFromTasks { get; set; }
        public bool HasMultipleFrequencies { get; set; }
        public IEnumerable<ResponsibilityTaskDto> ResponsibilityTasks { get; set; }
        public List<StatutoryResponsibilityTaskTemplateDto> UncreatedStatutoryResponsibilityTaskTemplates { get; set; }
        public List<StatutoryResponsibilityTaskTemplateDto> StatutoryResponsibilityTaskTemplates { get; set; }

        public ResponsibilityDto()
        {
            ResponsibilityTasks = new List<ResponsibilityTaskDto>();
            UncreatedStatutoryResponsibilityTaskTemplates = new List<StatutoryResponsibilityTaskTemplateDto>();
            StatutoryResponsibilityTaskTemplates = new List<StatutoryResponsibilityTaskTemplateDto>();
  }
    }
}