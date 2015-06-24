using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Sites.Controllers;
using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using BusinessSafe.WebSite.Contracts;
using BusinessSafe.WebSite.Tests.Builder;
using Moq;
using NUnit.Framework;
using BusinessSafe.WebSite.ServiceCompositionGateways;

namespace BusinessSafe.WebSite.Tests.Controllers.Sites
{
    
    [TestFixture]
    [Category("Unit")]
    public class DelinkSiteAddressTests
    {
        private Mock<ISiteStructureViewModelFactory> siteStructureViewModelFactory;
        private Mock<ISiteDetailsViewModelFactory> siteDetailsViewModelFactory;
        private Mock<ISiteService> siteService;
        private Mock<ISiteUpdateCompositionGateway> _siteUpdateCompositionGateway;

        private DelinkSiteViewModel _delinkSiteViewModel;

        [SetUp]
        public void SetUp()
        {
            siteStructureViewModelFactory = new Mock<ISiteStructureViewModelFactory>();
            siteDetailsViewModelFactory = new Mock<ISiteDetailsViewModelFactory>();
            siteService = new Mock<ISiteService>();
            _siteUpdateCompositionGateway = new Mock<ISiteUpdateCompositionGateway>();
            
            siteService = new Mock<ISiteService>();
        }

        [Test]
        public void Given_a_valid_delink_site_address_view_model_When_delink_site_Then_should_return_correct_redirect_to_action_result()
        {
            //Given
            var target = CreateSitesController();

            
            _delinkSiteViewModel = DelinkSiteViewModelBuilder
                                                .Create()
                                                .Build();


            //When
            var result = target.DelinkSite(_delinkSiteViewModel) as RedirectToRouteResult;

            //Then            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues["controller"], Is.EqualTo("SitesStructure"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        public void Given_a_valid_delink_site_address_view_model_When_delink_site_Then_should_return_success_message()
        {
            //Given
            var target = CreateSitesController();


            _delinkSiteViewModel = DelinkSiteViewModelBuilder
                                                .Create()
                                                .Build();


            //When
            var result = target.DelinkSite(_delinkSiteViewModel) as RedirectToRouteResult;

            //Then            
            Assert.That(target.TempData[SitesController.MessageKey], Is.Not.Null);
            Assert.That((target.TempData[SitesController.MessageKey]as string).Length, Is.GreaterThan(0));
        }

        [Test]
        public void Given_a_delink_site_address_view_model_with_no_site_address_id_When_delink_site_Then_should_return_index_with_message_informing_user_could_not_delink()
        {
            //Given
            var target = CreateSitesController();


            _delinkSiteViewModel = DelinkSiteViewModelBuilder
                                                .Create()
                                                .WithSiteId(0)
                                                .Build();
            target.ModelState.AddModelError("SiteId", "Site Id Validation Issue");

            //When
            var result = target.DelinkSite(_delinkSiteViewModel) as RedirectToRouteResult;

            //Then            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues["controller"], Is.EqualTo("SitesStructure"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(target.TempData[SitesController.DelinkSiteAddressValidationMessageKey], Is.Not.Null);
            Assert.That((target.TempData[SitesController.DelinkSiteAddressValidationMessageKey] as string).Length, Is.GreaterThan(0));
        }

        [Test]
        public void Given_a_valid_delink_site_address_view_model_When_delink_site_Then_should_call_site_service_to_delink()
        {
            //Given
            var target = CreateSitesController();


            _delinkSiteViewModel = DelinkSiteViewModelBuilder
                                                .Create()
                                                .Build();


            //When
            target.DelinkSite(_delinkSiteViewModel);

            //Then            
            siteService.Verify(x => x.DelinkSite(It.IsAny<DelinkSiteRequest>()),Times.Once());
            
        }

        private SitesController CreateSitesController()
        {
            return new SitesController(siteStructureViewModelFactory.Object, siteDetailsViewModelFactory.Object, siteService.Object, _siteUpdateCompositionGateway.Object,null);
        }
    }
}