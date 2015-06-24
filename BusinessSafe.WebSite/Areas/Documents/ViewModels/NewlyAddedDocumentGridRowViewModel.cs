using System.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.Documents.ViewModels
{
    public class NewlyAddedDocumentGridRowViewModel
    {
        public SelectList DocumentTypes { get; set; }
        public SelectList Sites { get; set; }

        public long SiteId { get; set; }
        public string Title { get; set; }
        public long DocumentLibraryId { get; set; }
        public string Filename { get; set; }
        public string Description { get; set; }
        public long DocumentTypeId { get; set; }
        public int DocumentOriginTypeId { get; set; }
        public bool CanEditDocumentType { get; set; }
    }
}