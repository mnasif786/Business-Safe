using BusinessSafe.WebSite.Tests.Builder;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModels
{
    [TestFixture]
    [Category("Unit")]
    public class SiteDetailsViewModelTests
    {
        [Test]
        public void Given_no_site_details_When_check_command_buttons_enabled_Then_should_return_false()
        {
            // Arrange
            var viewModel = SiteDetailsViewModelBuilder
                                    .Create()
                                    .WithSiteId(0)
                                    .Build(); 
            
            // Act
            var result = viewModel.FormEnabled;

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void Given_site_details_When_check_command_buttons_enabled_Then_should_return_true()
        {
            // Arrange
            var viewModel = SiteDetailsViewModelBuilder
                                    .Create()
                                    .WithSiteId(1)
                                    .Build(); 
            
            // Act
            var result = viewModel.FormEnabled;

            // Assert
            Assert.That(result, Is.True);
        }

    }
}