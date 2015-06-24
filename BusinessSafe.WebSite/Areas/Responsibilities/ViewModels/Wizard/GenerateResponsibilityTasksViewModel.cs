using System.Collections.Generic;

using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard
{
    public class GenerateResponsibilityTasksViewModel
    {
        public GenerateResponsibilityTasksViewModel()
        {
            Employees = new List<AutoCompleteViewModel>();
            SelectedSites = new List<ResponsibilityWizardSite>();
        }
        public IEnumerable<AutoCompleteViewModel> Employees;
        public List<ResponsibilityWizardSite> SelectedSites { get; set; }
        public IEnumerable<AutoCompleteViewModel> FrequencyOptions { get; set; }
    }

    public class ResponsibilityWizardSite
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ResponsibilityUncreatedTasksViewModel> Responsibilities { get; set; }
    }
}