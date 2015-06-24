namespace BusinessSafe.Application.Request.HazardousSubstanceInventory
{
    public class CheckForDuplicatedSubstanceRequest
    {
        public long CompanyId { get; set; }
        public string NewSubstanceName { get; set; }
    }
}
