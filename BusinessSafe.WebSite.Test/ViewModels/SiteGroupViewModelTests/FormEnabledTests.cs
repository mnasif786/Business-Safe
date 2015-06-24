using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using BusinessSafe.WebSite.ViewModels;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModels.SiteGroupViewModelTests
{
    [TestFixture]
    [Category("Unit")]
    public class FormEnabledTests
    {
        [Test]
        public void Given_that_group_id_is_zero_Then_Form_is_not_enabled()
        {
            //Given
            var target = new SiteGroupDetailsViewModel {GroupId = 0};

            //When
            var result = target.FormEnabled;

            //Then
            Assert.That(result, Is.False);
        }

        [Test]
        public void Given_that_group_id_is_greater_than_zero_Then_Form_is_enabled()
        {
            //Given
            var target = new SiteGroupDetailsViewModel { GroupId = 10 };

            //When
            var result = target.FormEnabled;

            //Then
            Assert.That(result, Is.True);
        }
    }
}
