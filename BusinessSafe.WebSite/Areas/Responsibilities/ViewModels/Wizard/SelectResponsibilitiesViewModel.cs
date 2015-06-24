using System.Collections.Generic;

using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard
{
    public class SelectResponsibilitiesViewModel
    {
        public List<LookupDto> Sites { get; set; }
        public List<LookupDto> Employees { get; set; }
        public IEnumerable<StatutoryResponsibilityViewModel> Responsibilities { get; set; }
        public IEnumerable<StatutoryResponsibilityViewModel> SelectedResponsibilities { get; set; }
    }
}