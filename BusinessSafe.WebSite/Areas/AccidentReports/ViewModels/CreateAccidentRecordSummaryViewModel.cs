using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.WebSite.Areas.AccidentReports.ViewModels
{
    public class CreateAccidentRecordSummaryViewModel
    {
        public long CompanyId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Reference is required")]
        public string Reference { get; set; }
        [Required(ErrorMessage = "Jurisdiction is required")]
        public long? JurisdictionId { get; set; }
    }
}