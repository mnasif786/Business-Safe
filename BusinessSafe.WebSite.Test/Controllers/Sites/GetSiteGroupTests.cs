using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.WebSite.Areas.Sites.Controllers;
using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using BusinessSafe.WebSite.Contracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Sites
{
    [TestFixture]
    [Category("Unit")]
    public class GetSiteGroupTests
    {
        private Mock<ISiteStructureViewModelFactory> siteStructureViewModelFactory;
        private Mock<ISiteGroupViewModelFactory> siteGroupViewModelFactory;
        private Mock<ISiteGroupService> siteGroupService;
        private const int _clientId = 1234;
        private const int _siteGroupId = 5678;

        [SetUp]
        public void SetUp()
        {
            siteStructureViewModelFactory = new Mock<ISiteStructureViewModelFactory>();
            siteGroupViewModelFactory = new Mock<ISiteGroupViewModelFactory>();
            siteGroupService = new Mock<ISiteGroupService>();

            siteGroupViewModelFactory
                          .Setup(x => x.WithSiteGroupId(_siteGroupId))
                          .Returns(siteGroupViewModelFactory.Object);

            siteGroupViewModelFactory
                                     .Setup(x => x.WithClientId(_clientId))
                                     .Returns(siteGroupViewModelFactory.Object);
        }

        [Test]
        public void Given_a_supplied_site_group_id_When_get_site_group_Then_should_return_correct_view()
        {
            //Given
            const string viewNameExpected = "_SiteGroup";
            var target = CreateSitesController();

            siteGroupViewModelFactory
                    .Setup(x => x.GetViewModel())
                    .Returns(new SiteGroupDetailsViewModel());


            //When
            var result = target.GetSiteGroup(_clientId, _siteGroupId);

            //Then            
            Assert.That(result.ViewName, Is.EqualTo(viewNameExpected));
        }

        [Test]
        public void Given_a_supplied_site_group_id_When_get_Then_should_return_correct_view_model()
        {
            //Given            
            var target = CreateSitesController();


            var siteDetailsViewModel = new SiteGroupDetailsViewModel();
            siteGroupViewModelFactory
                    .Setup(x => x.GetViewModel())
                    .Returns(siteDetailsViewModel);


            //When
            var result = target.GetSiteGroup(_clientId, _siteGroupId);

            //Then
            Assert.That(result.Model, Is.TypeOf<SiteGroupDetailsViewModel>());
            Assert.That(result.Model, Is.SameAs(siteDetailsViewModel));
        }

        private SiteGroupController CreateSitesController()
        {
            var result = new SiteGroupController(siteGroupViewModelFactory.Object, siteGroupService.Object, siteStructureViewModelFactory.Object);
            return TestControllerHelpers.AddUserToController(result);

        }
    }
}