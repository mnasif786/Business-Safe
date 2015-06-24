using System.Collections.Generic;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class StatutoryResponsibilityTaskTemplateDto
    {
        public long Id { get; set; }
        public StatutoryResponsibilityTemplateDto StatutoryResponsibilityTemplate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskReoccurringType TaskReoccurringType { get; set; }
    }

    public class StatutoryResponsibilityTaskNotCreatedDto
    {
        /// <summary>
        /// list of tasks that haven't been created
        /// </summary>
        public IEnumerable<StatutoryResponsibilityTaskTemplateDto> StatutoryResponsibilityTaskTemplates { get; set; }
        /// <summary>
        /// the responsibility the tasks relate to
        /// </summary>
        public ResponsibilityDto Responsibility { get; set; }
        /// <summary>
        /// Site that the responsibility is asssociated with
        /// </summary>
        public SiteDto Site { get; set; }


    }

}