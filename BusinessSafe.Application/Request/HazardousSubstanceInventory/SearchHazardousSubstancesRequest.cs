namespace BusinessSafe.Application.Request.HazardousSubstanceInventory
{
    public class SearchHazardousSubstancesRequest
    {
        public long CompanyId { get; set; }
        public long? SupplierId { get; set; }
        public string SubstanceNameLike { get; set; }
        public bool ShowDeleted { get; set; }
    }
}
