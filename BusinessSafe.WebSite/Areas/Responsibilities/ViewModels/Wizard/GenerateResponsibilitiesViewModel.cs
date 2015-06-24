using System.Collections;
using System.Collections.Generic;

using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard
{
    public class GenerateResponsibilitiesViewModel
    {
        public GenerateResponsibilitiesViewModel()
        {
            Employees = new List<AutoCompleteViewModel>();
        }
        public IEnumerable<StatutoryResponsibilityViewModel> ResponsibilityTemplates;
        public IEnumerable<LookupDto> Sites { get; set; }

        public IEnumerable<AutoCompleteViewModel> Employees { get; set; }
        public IEnumerable<AutoCompleteViewModel> FrequencyOptions { get; set; }
    }
}