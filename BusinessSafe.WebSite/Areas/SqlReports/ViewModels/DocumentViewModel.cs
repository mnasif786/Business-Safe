using System.IO;

namespace BusinessSafe.WebSite.Areas.SqlReports.ViewModels
{
    public class DocumentViewModel
    {
        public string MimeType { get; set; }
        public string FileName { get; set; }
        public Stream FileStream { get; set; }
    }
}