namespace BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.ViewModels
{
    public class HazardousSubstanceSummaryViewModel
    {
        public string HazardSymbolStandard { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string[] Hazards { get; set; }
        public string[] RiskPhrases { get; set; }
        public string[] SafetyPhrases { get; set; }
        public string[] AdditionalInformationRecords { get; set; }
    }
}