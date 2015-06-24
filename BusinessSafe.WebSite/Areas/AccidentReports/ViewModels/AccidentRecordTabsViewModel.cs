using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.AccidentReports.ViewModels
{
    public class AccidentRecordTabsViewModel : TabViewModel<AccidentRecordTabs>
    {
        public bool NextStepsVisible { get; set; }
    }
}