using System.IO;
using System.Web;

namespace BusinessSafe.WebSite.Helpers
{
    public interface IDocumentLibraryUploader
    {
        long Upload(string fileName, Stream stream);
    }
}