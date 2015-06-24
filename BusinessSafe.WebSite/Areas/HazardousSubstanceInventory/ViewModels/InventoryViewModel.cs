using System.Collections.Generic;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.ViewModels
{
    public class InventoryViewModel
    {
        public long CompanyId { get; set; }
        public string SubstanceNameLike { get; set; }
        public IEnumerable<AutoCompleteViewModel> Suppliers { get; set; }
        public long? SupplierId { get; set; }
        public IEnumerable<InventorySubstanceViewModel> Substances { get; set; }
        public bool ShowDeleted { get; set; }
        public string ShowDeletedButtonText { get; set; }
    }
}