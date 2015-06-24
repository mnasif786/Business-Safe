using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{    
    public class ResponsibilityTaskDto : TaskDto
    {
        public ResponsibilityDto Responsibility { get; set; }
        public StatutoryResponsibilityTaskTemplateDto StatutoryResponsibilityTaskTemplateCreatedFrom { get; set; }
    }
}