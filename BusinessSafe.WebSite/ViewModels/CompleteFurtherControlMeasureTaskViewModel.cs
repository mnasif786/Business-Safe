using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.WebSite.ViewModels
{
    public class CompleteFurtherControlMeasureTaskViewModel
    {
        public long FurtherControlMeasureTaskId { get; set; }
        public long CompanyId { get; set; }
        public ViewFurtherControlMeasureTaskViewModel ViewFurtherControlMeasureTaskViewModel { get; set; }

        [StringLength(250)]
        public string CompletedComments { get; set; }
    }
}