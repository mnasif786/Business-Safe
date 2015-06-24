using System.Web.Mvc;

using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using BusinessSafe.WebSite.ViewModels;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModels
{
    [TestFixture]
    [Category("Unit")]
    public class SiteStructureViewModelTests
    {
        private static MvcHtmlString _sitesHtml = new MvcHtmlString(string.Empty);
        private static MvcHtmlString _unlinkedSitesHtml = new MvcHtmlString(string.Empty);
        private static SiteDetailsViewModel _siteDetailsViewModel = new SiteDetailsViewModel();

        [Test]
        public void Given_no_unlinked_sites_html_When_has_unlinked_sites_Then_should_return_false()
        {
            // Arrange
            var viewModel = CreateSiteStructureViewModel();
            viewModel.UnlinkedSitesHtml = new MvcHtmlString(string.Empty);

            // Act
            var result = viewModel.HasUnlinkedSites;

            // Assert
            Assert.False(result);
        }

        [Test]
        public void Given_unlinked_sites_html_When_has_unlinked_sites_Then_should_return_false()
        {
            // Arrange
            _unlinkedSitesHtml = new MvcHtmlString("Any data will do");

            var viewModel = CreateSiteStructureViewModel();
            

            // Act
            var result = viewModel.HasUnlinkedSites;

            // Assert
            Assert.True(result);
        }

        private static SiteStructureViewModel CreateSiteStructureViewModel()
        {
            var result = new SiteStructureViewModel(1, _sitesHtml, _unlinkedSitesHtml, _siteDetailsViewModel, null, false, false, false, true);
            return result;
        }
    }
}
