using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BusinessSafe.WebSite.ViewModels
{
    public class CopyRiskAssessmentForMultipleSitesViewModel
    {
        [Required]
        public long RiskAssessmentId { get; set; }
        public string Reference { get; set; }
        [Required(ErrorMessage = "Please enter a title")]
        public string Title { get; set; }
        public List<long> SiteIds { get; set; }

        public CopyRiskAssessmentForMultipleSitesViewModel()
        {
            SiteIds = new List<long>();
        }
    }
}