using System.ComponentModel;

namespace BusinessSafe.WebSite.ViewModels
{
    public class RiskAssessmentSearchViewModel
    {
        public string title { get; set; }
        public long? companyId { get; set; }
        public string createdFrom { get; set; }
        public string createdTo { get; set; }
        public long? siteGroupId { get; set; }
        public long? siteId { get; set; }
        public bool showDeleted { get; set; }
        public bool showArchived { get; set; }
        public bool isGeneralRiskAssessmentTemplating { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public string OrderBy { get; set; }
       
        public RiskAssessmentSearchViewModel()
        {
            showDeleted = false;
            showArchived = false;
            isGeneralRiskAssessmentTemplating = false;
            OrderBy = "CreatedOn-desc";
        }
    }

    public class HazardousSubstanceRiskAssessmentSearchViewModel : RiskAssessmentSearchViewModel
    {
        public long hazardousSubstanceId { get; set; }
    }
}