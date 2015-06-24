namespace BusinessSafe.WebSite.Areas.Company.ViewModels
{
    public class SaveCompanyDefaultViewModel
    {
        public long CompanyDefaultId { get; set; }
        public string CompanyDefaultValue { get; set; }
        public string CompanyDefaultType { get; set; }
        public bool RunMatchCheck { get; set; }
        public long CompanyId { get; set; }
        public long? RiskAssessmentId { get; set; }
        public int[] RiskAssessmentTypeApplicable { get; set; }
        
    }
}