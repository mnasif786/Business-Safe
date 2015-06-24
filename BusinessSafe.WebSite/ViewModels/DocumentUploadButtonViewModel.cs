using System.Web;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;

namespace BusinessSafe.WebSite.ViewModels
{
    public class DocumentUploadButtonViewModel
    {
        public HttpPostedFileBase File { get; set; }

        public bool DocumentUploaded { get; set; }
        public string LastUploadedDocumentFilename { get; set; }
        public long LastUploadedDocumentLibraryId { get; set; }
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }
        public int DocumentOriginTypeId { get; set; }
        public bool CanEditDocumentType { get; set; }
        public AttachDocumentReturnView ReturnView { get; set; }
        public int DocumentTypeId { get; set; }
    }
}