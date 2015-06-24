using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.WebSite.ViewModels
{
    public class ReassignFurtherControlMeasureTaskViewModel
    {
        public long FurtherControlMeasureTaskId { get; set; }
        public long CompanyId { get; set; }
        public ViewFurtherControlMeasureTaskViewModel ViewFurtherControlMeasureTaskViewModel { get; set; }

        [Required(ErrorMessage = "Task Assigned To is required")]
        public Guid ReassignTaskToId { get; set; }

        [Required(ErrorMessage = "Task Assigned To is required")]
        public string ReassignTaskTo { get; set; }
        public Guid TaskGuid { get; set; }
    }
}