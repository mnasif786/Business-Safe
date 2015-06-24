using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.WebSite.Areas.TaskList.ViewModels
{
    public class MarkResponsibilityTaskAsDeletedViewModel
    {
        [Required]
        public long CompanyId { get; set; }
        [Required]
        public long TaskId { get; set; }

    }
}