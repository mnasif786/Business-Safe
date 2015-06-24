using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Sites.Controllers;
using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using BusinessSafe.WebSite.Contracts;
using BusinessSafe.WebSite.Tests.Builder;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Sites
{
    [TestFixture]
    [Category("Unit")]
    public class DeleteSiteGroupTests
    {
        private Mock<ISiteStructureViewModelFactory> siteStructureViewModelFactory;
        private Mock<ISiteGroupViewModelFactory> siteGroupViewModelFactory;
        private Mock<ISiteGroupService> siteGroupService;

        private DeleteSiteGroupViewModel _deleteSiteGroupViewModel;

        [SetUp]
        public void SetUp()
        {
            siteStructureViewModelFactory = new Mock<ISiteStructureViewModelFactory>();
            siteGroupViewModelFactory = new Mock<ISiteGroupViewModelFactory>();
            siteGroupService = new Mock<ISiteGroupService>();
        }

        [Test]
        public void Given_a_valid_delete_site_group_view_model_When_delete_site_group_Then_should_return_correct_redirect_to_action_result()
        {
            //Given
            var target = CreateSitesController();


            _deleteSiteGroupViewModel = DeleteSiteGroupViewModelBuilder
                                                .Create()
                                                .Build();


            //When
            var result = target.DeleteSiteGroup(_deleteSiteGroupViewModel) as RedirectToRouteResult;

            //Then            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues["controller"], Is.EqualTo("SitesStructure"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        public void Given_a_valid_delete_site_group_view_model_When_delete_site_group_Then_should_return_success_message()
        {
            //Given
            const string expectedMessage = "Your site group has been successfully deleted";

            var target = CreateSitesController();

            _deleteSiteGroupViewModel = DeleteSiteGroupViewModelBuilder
                                                         .Create()
                                                         .Build();


            //When
            var result = target.DeleteSiteGroup(_deleteSiteGroupViewModel) as RedirectToRouteResult;

            //Then            
            Assert.That(target.TempData[SitesController.MessageKey], Is.Not.Null);
            Assert.That((target.TempData[SitesController.MessageKey] as string), Is.EqualTo(expectedMessage));
        }

        [Test]
        public void Given_a_delete_site_group_view_model_with_no_site_group_id_When_delete_site_group_Then_should_return_index_with_message_informing_user_could_not_delink()
        {
            //Given
            var target = CreateSitesController();

            _deleteSiteGroupViewModel = DeleteSiteGroupViewModelBuilder
                                                         .Create()
                                                         .WithSiteGroupId(0)
                                                         .Build();
            
            target.ModelState.AddModelError("SiteGroupId", "Site Group Id Validation Issue");

            //When
            var result = target.DeleteSiteGroup(_deleteSiteGroupViewModel) as RedirectToRouteResult;

            //Then            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues["controller"], Is.EqualTo("SitesStructure"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(target.TempData[SiteGroupController.DeleteSiteGroupValidationMessageKey], Is.Not.Null);
            Assert.That((target.TempData[SiteGroupController.DeleteSiteGroupValidationMessageKey] as string).Length, Is.GreaterThan(0));
        }

        [Test]
        public void Given_a_valid_delete_site_group_view_model_When_delete_site_group_Then_should_call_site_group_service_to_delete()
        {
            //Given
            var target = CreateSitesController();


            _deleteSiteGroupViewModel = DeleteSiteGroupViewModelBuilder
                                                         .Create()
                                                         .Build();


            //When
            target.DeleteSiteGroup(_deleteSiteGroupViewModel);

            //Then            
            siteGroupService.Verify(x => x.Delete(It.IsAny<DeleteSiteGroupRequest>()), Times.Once());

        }

        private SiteGroupController CreateSitesController()
        {
            var result = new SiteGroupController(siteGroupViewModelFactory.Object, siteGroupService.Object, siteStructureViewModelFactory.Object);
            return TestControllerHelpers.AddUserToController(result);

        }
    }
}