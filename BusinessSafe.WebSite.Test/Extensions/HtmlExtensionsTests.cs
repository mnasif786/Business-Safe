using BusinessSafe.WebSite.Extensions;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Extensions
{
    [TestFixture]
    public class HtmlExtensionsTests
    {
   
        [Test]
        public void Given_text_When_WrapWithParagraphTag_is_called_Then_adds_p_tags()
        {
            // Given
            var text = "Windows 8 is for PCs with only a mouse and keyboard, those with touchscreens, and those with both.";

            // When
            var result = HtmlExtensions.WrapWithParagraphTag(null, text, null);

            // Then
            Assert.That(result, Is.EqualTo(string.Format("<p>{0}</p>", text)));
        }

        [Test]
        public void Given_text_When_WrapWithParagraphTag_is_called_Then_adds_p_tags_and_adds_css_class()
        {
            // Given
            var text = "Windows 8 is for PCs with only a mouse and keyboard, those with touchscreens, and those with both.";
            var cssClass = "span2";

            // When
            var result = HtmlExtensions.WrapWithParagraphTag(null, text, cssClass);

            // Then
            Assert.That(result, Is.EqualTo(string.Format("<p class=\"{0}\">{1}</p>", cssClass, text)));
        }
        
        [Test]
        public void Given_text_with_line_breaks_When_BuildParagraphs_Then_seperate_paragraphs_returned()
        {
            // Given
            var lineBreak = "\n";
            var text = string.Format("Hello{0}World,{0}you are great", lineBreak);

            // Then
            var result = HtmlExtensions.BuildParagraphs(null, text);

            // When
            Assert.That(result.ToString(), Is.EqualTo(string.Format("{0}Hello{1}{0}World,{1}{0}you are great{1}", "<p>", "</p>")));
        }
    }
}
