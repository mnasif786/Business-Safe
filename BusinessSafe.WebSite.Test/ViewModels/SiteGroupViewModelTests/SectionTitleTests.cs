using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using BusinessSafe.WebSite.ViewModels;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModels.SiteGroupViewModelTests
{
    [TestFixture]
    [Category("Unit")]
    public class SectionTitleTests
    {
        [TestCase(10)]
        [TestCase(20)]
        public void Given_that_group_id_is_greater_that_zero_Then_correct_text_for_edit_is_used(long groupId)
        {
            //Given
            var target = new SiteGroupDetailsViewModel {GroupId = groupId};

            //When
            var result = target.SectionTitle;

            //Then
            Assert.That(result, Is.EqualTo("Edit Site Group"));
        }

        [Test]
        public void Given_that_group_id_is_zero_Then_correct_text_for_edit_is_used()
        {
            //Given
            var target = new SiteGroupDetailsViewModel { GroupId = 0 };

            //When
            var result = target.SectionTitle;

            //Then
            Assert.That(result, Is.EqualTo("Add Site Group"));
        }
    }
}
