using System.IO;

namespace BusinessSafe.WebSite.Helpers
{
    public interface ISaveFileStreamHelper
    {
        void Write(string filePath, Stream stream);
    }
}