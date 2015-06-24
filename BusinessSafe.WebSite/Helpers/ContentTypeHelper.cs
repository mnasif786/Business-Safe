namespace BusinessSafe.WebSite.Helpers
{
    public class ContentTypeHelper
    {
        public static string GetContentTypeFromExtension(string extension)
        {
            string contentType;

            switch (extension.ToLower())
            {
                case ".txt":
                    contentType = "text/plain";
                    break;
                case ".html":
                case ".htm":
                    contentType = "text/html";
                    break;
                case ".docx":
                    contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case ".dotx":
                    contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.template";
                    break;
                case ".xlsx":
                    contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case ".ppsx":
                    contentType = "application/vnd.openxmlformats-officedocument.presentationml.slideshow";
                    break;
                case ".pptx":
                    contentType = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                    break;
                case ".xps":
                    contentType = "application/vnd.ms-xpsdocument";
                    break;
                case ".pdf":
                    contentType = "application/pdf";
                    break;
                case ".doc":
                case ".rtf":
                case ".dot":
                    contentType = "application/msword";
                    break;
                case ".xls":
                case ".xlt":
                case ".csv":
                    contentType = "application/vnd.ms-excel";
                    break;
                case ".mpg":
                case ".peg":
                case ".mpe":
                    contentType = "video/mpeg";
                    break;
                case ".qt":
                case ".mov":
                    contentType = "video/quicktime";
                    break;
                case ".avi":
                case ".wmv":
                case ".wmf":
                    contentType = "video/x-msvideo";
                    break;
                case ".wav":
                    contentType = "audio/x-wav";
                    break;
                case ".mp3":
                    contentType = "audio/mp3";
                    break;
                case ".vsd":
                case ".vst":
                case ".vsw":
                    contentType = "application/vnd.visio";
                    break;
                case ".ppt":
                    contentType = "application/ms-ppt";
                    break;
                case ".ail":
                case ".msg":
                case ".eml":
                case ".email":
                    contentType = "application/base64";
                    break;
                case ".tif":
                case ".tiff":
                    contentType = "image/tiff";
                    break;
                case ".png":
                    contentType = "image/png";
                    break;
                case ".zip":
                    contentType = "application/zip";
                    break;
                default:
                    contentType = "application/x-unknown";
                    break;
            }

            return contentType;
        }
    }
}