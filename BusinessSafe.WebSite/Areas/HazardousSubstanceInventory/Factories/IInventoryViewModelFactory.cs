using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.Factories
{
    public interface IInventoryViewModelFactory
    {
        IInventoryViewModelFactory WithCompanyId(long companyId);
        InventoryViewModel GetViewModel();
        IInventoryViewModelFactory WithSupplierId(long? supplierId);
        IInventoryViewModelFactory WithSubstanceNameLike(string substanceNameLike);
        IInventoryViewModelFactory WithShowDeleted(bool showDeleted);
    }
}