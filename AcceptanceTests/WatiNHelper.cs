using WatiN.Core;

namespace BusinessSafe.AcceptanceTests
{
    public static class WatiNHelper
    {
        public static bool IsDisplayed(Element element)
        {
            if (string.Equals(element.Style.Display, "none"))
            {
                return false;
            }
            if (element.Parent != null)
            {
                return IsDisplayed(element.Parent);
            }
            return true;
        }
    }
}
