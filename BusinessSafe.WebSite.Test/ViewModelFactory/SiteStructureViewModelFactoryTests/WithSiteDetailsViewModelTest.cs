using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.RestAPI;
using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.SiteStructureViewModelFactoryTests
{
    [TestFixture]
    [Category("Unit")]
    public class WithSiteDetailsViewModelTest
    {
        private Mock<ISiteService> _siteRequestStructureService;
        private Mock<IClientService> _clientService;
        private long _clientId;

        [SetUp]
        public void SetUp()
        {
            _clientId = 0;
            _siteRequestStructureService = new Mock<ISiteService>();
            _clientService = new Mock<IClientService>();
        }

        [Test]
        public void Given_site_details_view_model_not_got_list_of_sites_When_get_view_model_Then_should_call_appropiate_methods()
        {
            // Arrange
            var viewModelFactory = CreateSiteStructureViewModelFactory();
            var siteDetailsViewModel = new SiteDetailsViewModel {SiteId = 1};
            
            // Act
            viewModelFactory.WithSiteDetailsViewModel(siteDetailsViewModel);

            // Assert
            _siteRequestStructureService.Verify(x => x.GetByCompanyIdNotIncluding(_clientId, siteDetailsViewModel.SiteId), Times.Once());
        }

        private SiteStructureViewModelFactory CreateSiteStructureViewModelFactory()
        {
            return new SiteStructureViewModelFactory(_siteRequestStructureService.Object, new Mock<ISiteGroupService>().Object, _clientService.Object);
        }
    }
}