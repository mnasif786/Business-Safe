using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.WebSite.Areas.TaskList.ViewModels
{
    public class MarkTaskAsDeletedViewModel
    {
        [Required]
        public long CompanyId { get; set; }
        [Required]
        public long FurtherControlMeasureTaskId { get; set; }

    }
}