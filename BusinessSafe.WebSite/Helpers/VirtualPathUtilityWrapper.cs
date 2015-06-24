using System.Web;

namespace BusinessSafe.WebSite.Helpers
{
    public class VirtualPathUtilityWrapper : IVirtualPathUtilityWrapper
    {
        public string ToAbsolute(string virtualPath)
        {
            return VirtualPathUtility.ToAbsolute(virtualPath);
        }
    }
}