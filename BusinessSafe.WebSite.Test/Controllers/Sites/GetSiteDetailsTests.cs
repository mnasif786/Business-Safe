using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.WebSite.Areas.Sites.Controllers;
using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using BusinessSafe.WebSite.Contracts;
using Moq;
using NUnit.Framework;
using BusinessSafe.WebSite.ServiceCompositionGateways;

namespace BusinessSafe.WebSite.Tests.Controllers.Sites
{
    [TestFixture]
    [Category("Unit")]
    public class GetSiteDetailsTests
    {
        private Mock<ISiteStructureViewModelFactory> siteStructureViewModelFactory;
        private Mock<ISiteDetailsViewModelFactory> siteDetailsViewModelFactory;
        private Mock<ISiteService> siteService;
        private Mock<ISiteUpdateCompositionGateway> _siteUpdateCompositionGateway;

        private const int _clientId = 1234;
        private const int _siteId = 5678;
        private const long _id = 235L;

        [SetUp]
        public void SetUp()
        {
            siteStructureViewModelFactory = new Mock<ISiteStructureViewModelFactory>();
            siteDetailsViewModelFactory = new Mock<ISiteDetailsViewModelFactory>();
            siteService = new Mock<ISiteService>();
            _siteUpdateCompositionGateway = new Mock<ISiteUpdateCompositionGateway>();

            siteDetailsViewModelFactory
                                      .Setup(x => x.WithSiteId(_siteId))
                                      .Returns(siteDetailsViewModelFactory.Object);

            siteDetailsViewModelFactory
                                     .Setup(x => x.WithClientId(_clientId))
                                     .Returns(siteDetailsViewModelFactory.Object);

            siteDetailsViewModelFactory
                                     .Setup(x => x.WithId(_id))
                                     .Returns(siteDetailsViewModelFactory.Object);

        }
        [Test]
        public void Given_a_supplied_site_id_When_get_Then_should_return_correct_view()
        {
            //Given
            const string viewNameExpected = "_SiteDetails";
            var target = CreateSitesController();



            siteDetailsViewModelFactory
                    .Setup(x => x.GetViewModel())
                    .Returns(new SiteDetailsViewModel());


            //When
            var result = target.GetLinkedSiteDetails(_clientId, _id);

            //Then            
            Assert.That(result.ViewName, Is.EqualTo(viewNameExpected));
        }

        [Test]
        public void Given_a_supplied_site_id_When_get_Then_should_return_correct_view_model()
        {
            //Given            
            var target = CreateSitesController();



            siteDetailsViewModelFactory
                    .Setup(x => x.GetViewModel())
                    .Returns(new SiteDetailsViewModel());


            //When
            var result = target.GetLinkedSiteDetails(_clientId, _id);

            //Then
            Assert.That(result.Model, Is.TypeOf<SiteDetailsViewModel>());
        }

        private SitesController CreateSitesController()
        {
            return new SitesController(siteStructureViewModelFactory.Object
                , siteDetailsViewModelFactory.Object
                , siteService.Object
                , _siteUpdateCompositionGateway.Object
                ,null);
        }
    }

}