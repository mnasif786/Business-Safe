using BusinessSafe.Domain.Entities;

namespace BusinessSafe.WebSite.Areas.Documents.ViewModels
{
    public class PreviouslyAddedDocumentGridRowViewModel
    {
        public long Id { get; set; }
        public long DocumentLibraryId { get; set; }
        public string Filename { get; set; }
        public string Description { get; set; }
        public string DocumentTypeName { get; set; }
        public DocumentOriginType DocumentOriginType { get; set; }
    }
}