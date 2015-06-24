using System.IO;

namespace BusinessSafe.WebSite.Helpers
{
    public class SaveFileStreamHelper : ISaveFileStreamHelper
    {
        public void Write(string filePath, Stream stream)
        {
            using (var fs = System.IO.File.Create(filePath))
            {
                SaveFile(stream, fs);
            }
        }

        private void SaveFile(Stream stream, FileStream fs)
        {
            var buffer = new byte[4096];
            int bytesRead;
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                fs.Write(buffer, 0, bytesRead);
            }
        }
    }
}