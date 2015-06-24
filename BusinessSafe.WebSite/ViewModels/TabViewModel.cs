using System.Web;

namespace BusinessSafe.WebSite.ViewModels
{
    public abstract class TabViewModel
    {
        public long CompanyId { get; set; }
        public long Id { get; set; }
        public bool IsReadOnly { get; set; }
    }

    public abstract class TabViewModel<T> : TabViewModel
    {
        public T CurrentTab { get; set; }
        public HtmlString IsTabActive(string tab)
        {
            if (CurrentTab.ToString() == tab)
                return new HtmlString(" active ");
            return new HtmlString("disable");
        }
    }
}