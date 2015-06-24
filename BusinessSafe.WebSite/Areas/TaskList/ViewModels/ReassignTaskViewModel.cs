using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.WebSite.Areas.TaskList.ViewModels
{
    public class ReassignTaskViewModel
    {
        [Required]
        public long CompanyId { get; set; }
        [Required]
        public long FurtherControlMeasureTaskId { get; set; }
        [Required]
        public Guid ReassignTaskToId { get; set; }
        [Required]
        public Guid TaskGuid { get; set; }
    }
}