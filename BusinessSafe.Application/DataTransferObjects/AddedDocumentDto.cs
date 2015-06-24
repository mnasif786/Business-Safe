namespace BusinessSafe.Application.DataTransferObjects
{
    public class AddedDocumentDto : DocumentDto
    {
        public long SiteId { get; set; }
        public string SiteName { get; set; }
    }
}