using System;
using System.Configuration;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace BusinessSafe.WebSite.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString IsEnabled(bool isEnabled)
        {
            if (!isEnabled)
                return new MvcHtmlString("disabled='disabled'");
            return MvcHtmlString.Empty;
        }

        public static object CreateDisabledHtmlAttribute(bool isEnabled)
        {
            if (!isEnabled)
                return new
                           {
                               disabled = "disabled"
                           };
            return null;
        }

        public static MvcHtmlString HideDisplayHtmlAttribute(bool displaySiteDetails)
        {
            if (displaySiteDetails)
                return new MvcHtmlString("display: block;");

            return new MvcHtmlString("display: none;");
        }

        public static bool IsReadOnly(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ViewData.IsReadOnly;
        }

        public static bool IsDebug(this HtmlHelper htmlHelper)
        {
            return HttpContext.Current.IsDebuggingEnabled;
        }

        public static HtmlString BuildParagraphs(this HtmlHelper helper, string message)
        {
            var builder = new StringBuilder();

            var paragraphs = message.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var paragraph in paragraphs)
            {
                builder.Append(helper.WrapWithParagraphTag(paragraph));
            }

            return new HtmlString(builder.ToString());
        }

        public static string WrapWithParagraphTag(this HtmlHelper helper, string text, string cssClass = null)
        {
            var paragraph = new TagBuilder("p");

            if (cssClass != null)
            {
                paragraph.AddCssClass(cssClass);
            }
            paragraph.SetInnerText(text);

            return paragraph.ToString();
        }

        public static bool ShouldTrackAnalytics(this HtmlHelper htmlHelper)
        {
            var environment = ConfigurationManager.AppSettings["config_file"];
            if (environment == null || environment.ToLower() != "live")
                return false;
            return true;
        }
    }
}