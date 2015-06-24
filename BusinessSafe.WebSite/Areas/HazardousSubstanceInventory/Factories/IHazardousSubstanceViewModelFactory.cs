using BusinessSafe.Application.Request.HazardousSubstanceInventory;
using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.Factories
{
    public interface  IHazardousSubstanceViewModelFactory
    {
        IHazardousSubstanceViewModelFactory WithCompanyId(long companyId);
        IHazardousSubstanceViewModelFactory WithHazardousSubstanceId(long hazardousSubstanceId);
        AddEditHazardousSubstanceViewModel GetViewModel();
        AddEditHazardousSubstanceViewModel GetViewModel(AddHazardousSubstanceRequest model);
    }
}