using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;

namespace BusinessSafe.WebSite.Areas.AccidentReports.Factories
{
    public interface IInjuredPersonViewModelFactory
    {
        IInjuredPersonViewModelFactory WithCompanyId(long companyId);
        IInjuredPersonViewModelFactory WithAccidentRecordId(long accidentRecordId);
        InjuredPersonViewModel GetViewModel();
        InjuredPersonViewModel GetViewModel(InjuredPersonViewModel viewModel);
    }
}