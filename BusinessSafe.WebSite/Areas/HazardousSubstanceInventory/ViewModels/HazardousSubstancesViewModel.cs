using System.Collections.Generic;
using System.Web.Mvc;

using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.ViewModels
{
    public class HazardousSubstancesViewModel
    {
        public long SubstanceId { get; set; }
        public IEnumerable<SelectListItem> Substances { get; set; }
        public long SupplierId { get; set; }
        public IEnumerable<SelectListItem> Suppliers { get; set; }
        public IEnumerable<HazardousSubstanceDto> HazardousSubstances;
    }
}