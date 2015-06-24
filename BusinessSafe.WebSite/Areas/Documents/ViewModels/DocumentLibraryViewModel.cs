using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.Documents.ViewModels
{
    public class DocumentLibraryViewModel
    {
        public long CompanyId { get; set; }
        public string DocumentGroup { get; set; }
        public long DocumentTypeId { get; set; }
        public IEnumerable<SelectListItem> DocumentTypes { get; set; }
        public long DocumentSubTypeId { get; set; }
        public IEnumerable<SelectListItem> DocumentSubTypes { get; set; }
        public string Title { get; set; }
        public string Keywords { get; set; }
        public IEnumerable<DocumentViewModel> Documents { get; set; }
    }
}