using BusinessSafe.WebSite.Extensions;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Extensions.StringExtensions
{
    [TestFixture]
    public class TruncateWithEllipsisTests
    {
        const string ellipsis = "\u2026";

        [SetUp]
        public void Setup()
        {
        }

        [TestCase(50, 50)]
        [TestCase(1, 50)]
        public void Given_a_string_up_to_and_equal_in_length_to_maxChars_When_TruncateWithEllipsis_Then_return_whole_string(int totalChars, int maxChars)
        {
            // Given
            var stringToTruncate = new string('a', totalChars);

            // When
            var truncatedString = stringToTruncate.TruncateWithEllipsis(maxChars);

            // Then
            Assert.That(truncatedString, Is.EqualTo(stringToTruncate));
        }

        [TestCase(51, 50)]
        public void Given_a_string_longer_than_maxChars_When_TruncateWithEllipsis_Then_returned_string_should_end_with_ellipsis(int totalChars, int maxChars)
        {
            // Given
            var stringToTruncate = new string('a', totalChars);

            // When
            var truncatedString = stringToTruncate.TruncateWithEllipsis(maxChars);

            // Then
            Assert.That(truncatedString.EndsWith(ellipsis));
        }

        [TestCase("abcdefghijklmnopqrstuvwxyz")]
        [TestCase("wibblybibbly")]
        public void Given_a_string_longer_than_maxChars_When_TruncateWithEllipsis_Then_returned_string_should_be_truncated_to_maxChars(string stringToTruncate)
        {
            // Given
            const int maxChars = 10;

            // When
            var truncatedString = stringToTruncate.TruncateWithEllipsis(maxChars);

            // Then
            Assert.That(truncatedString, Is.EqualTo(stringToTruncate.Substring(0, maxChars) + ellipsis));
        }

        [TestCase("abcdefghi klmnopqrstuvwxyz", "abcdefghi")]
        [TestCase("wibblybib ly", "wibblybib")]
        public void Given_a_string_longer_than_maxChars_When_TruncateWithEllipsis_Then_should_not_be_spaces_between_end_of_string_and_ellipsis(string stringToTruncate, string expected)
        {
            // Given
            const int maxChars = 10;

            // When
            var truncatedString = stringToTruncate.TruncateWithEllipsis(maxChars);

            // Then
            Assert.That(truncatedString, Is.EqualTo(expected + ellipsis));
        }
    }
}
