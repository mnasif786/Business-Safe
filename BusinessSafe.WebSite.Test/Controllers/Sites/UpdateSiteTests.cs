using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
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
    public class UpdateSiteTests
    {
        private Mock<ISiteStructureViewModelFactory> _siteStructureViewModelFactory;
        private Mock<ISiteDetailsViewModelFactory> _siteDetailsViewModelFactory;
        private Mock<ISiteService> _siteService;
        private SiteDetailsViewModel _siteDetailsViewModel;
        private Mock<ISiteUpdateCompositionGateway> _siteUpdateCompositionGateway;
        private SiteDto _siteDto;

        private long _clientId;
        private long _siteStructureId;

        [SetUp]
        public void SetUp()
        {
            _siteStructureViewModelFactory = new Mock<ISiteStructureViewModelFactory>();
            _siteDetailsViewModelFactory = new Mock<ISiteDetailsViewModelFactory>();
            _siteService = new Mock<ISiteService>();
            _siteUpdateCompositionGateway = new Mock<ISiteUpdateCompositionGateway>();
            _siteService
                .Setup(ss => ss.CreateUpdate(It.IsAny<CreateUpdateSiteRequest>()))
                .Returns(10);

            _clientId = 888L;
            _siteStructureId = 111L;

            _siteDto = new SiteDto
                           {
                               Id = _siteStructureId,
                               ClientId = _clientId,
                               Name = "Hagatna"
                           };

            _siteService
                .Setup(x => x.GetByIdAndCompanyId(_siteStructureId, _clientId))
                .Returns(_siteDto);
        }

        [Test]
        public void Given_a_invalid_model_When_update_site_Then_should_return_to_view_with_same_viewmodel()
        {
            //Given
            _siteDetailsViewModel = SiteDetailsViewModelBuilder
                                            .Create()
                                            .WithName(string.Empty)
                                            .Build();


            _siteStructureViewModelFactory
                .Setup(x => x.WithSiteDetailsViewModel(_siteDetailsViewModel))
                .Returns(_siteStructureViewModelFactory.Object);

            _siteStructureViewModelFactory
                    .Setup(x => x.WithClientId(TestControllerHelpers.CompanyIdAssigned))
                    .Returns(_siteStructureViewModelFactory.Object);

            _siteStructureViewModelFactory
                    .Setup(x => x.DisplaySiteDetails())
                    .Returns(_siteStructureViewModelFactory.Object);

            _siteStructureViewModelFactory
                    .Setup(x => x.GetViewModel())
                    .Returns(new SiteStructureViewModel(_clientId, MvcHtmlString.Empty, MvcHtmlString.Empty, _siteDetailsViewModel, null, false, false, false, true));

            _siteStructureViewModelFactory.Setup(ss => ss.WithValidationError(true)).Returns(_siteStructureViewModelFactory.Object);

            var target = GetTarget();

            target.ModelState.AddModelError("Name", "Is required");

            //When
            var result = target.UpdateSite(_siteDetailsViewModel) as ViewResult;

            //Then            
            Assert.That(result.ViewName, Is.EqualTo("Index"));
            var model = result.Model as SiteStructureViewModel;
            Assert.That(model.SiteDetailsViewModel, Is.EqualTo(_siteDetailsViewModel));
        }

        [Test]
        public void Given_a_valid_model_When_update_site_Then_should_call_appropiate_methods()
        {
            //Given
            _siteDetailsViewModel = SiteDetailsViewModelBuilder
                                            .Create()
                                            .Build();



            var target = GetTarget();

            //When
            var result = target.UpdateSite(_siteDetailsViewModel) as ViewResult;

            //Then            
            _siteService.Setup(x => x.CreateUpdate(It.IsAny<CreateUpdateSiteRequest>()));

        }

        [Test]
        public void Given_a_valid_model_When_update_site_Then_viewModel_should_have_currentUsers_full_name_as_actioning_user()
        {
            //Given
            _siteDetailsViewModel = SiteDetailsViewModelBuilder
                                            .Create()
                                            .Build();



            var target = GetTarget();

            //When
            var result = target.UpdateSite(_siteDetailsViewModel) as ViewResult;

            //Then
            _siteUpdateCompositionGateway.Verify(x => x.SendEmailIfRequired(It.Is<SiteDetailsViewModel>(y => y.ActioningUserName == TestControllerHelpers.UserFullNameAssigned)));
        }

        [Test]
        public void Given_a_valid_model_When_update_site_Then_should_set_correct_route_values()
        {
            //Given
            _siteDetailsViewModel = SiteDetailsViewModelBuilder
                                            .Create()
                                            .WithSiteId(100)
                                            .Build();



            var target = GetTarget();

            //When
            var result = target.UpdateSite(_siteDetailsViewModel) as RedirectToRouteResult;

            //Then            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(result.RouteValues["controller"], Is.EqualTo("SitesStructure"));
            Assert.That(result.RouteValues.Values.Where(x => x.ToString() == "id"), Is.Not.Null);
            Assert.That(target.TempData[SitesController.UpdateSiteIdKey], Is.EqualTo(_siteDetailsViewModel.SiteId));
        }

        [Test]
        public void Given_a_valid_model_for_new_site_When_update_site_Then_should_set_correct_success_message()
        {
            //Given
            string expectedMessage = "Your new Site has been successfully created";
            _siteDetailsViewModel = SiteDetailsViewModelBuilder
                                            .Create()
                                            .WithSiteStructureId(0)
                                            .WithSiteId(100)
                                            .Build();



            var target = GetTarget();

            _siteUpdateCompositionGateway.Setup(x => x.SendEmailIfRequired(_siteDetailsViewModel)).Returns(false);

            //When
            var result = target.UpdateSite(_siteDetailsViewModel) as RedirectToRouteResult;

            //Then            
            Assert.That(result, Is.Not.Null);
            Assert.That((target.TempData[SitesController.MessageKey] as string), Is.EqualTo(expectedMessage));

        }

        [Test]
        public void Given_a_valid_model_for_editing_site_When_update_site_Then_should_set_correct_success_message()
        {
            //Given
            _siteService
                .Setup(x => x.GetByIdAndCompanyId(10, It.IsAny<long>()))
                .Returns(new SiteDto{ Name = "site"});

            const string expectedMessage = "A member of Client Services has been notified and will be in contact in due course";
            _siteDetailsViewModel = SiteDetailsViewModelBuilder
                                            .Create()
                                            .WithSiteStructureId(10)
                                            .WithSiteId(100)
                                            .Build();

            var target = GetTarget();

            _siteUpdateCompositionGateway.Setup(x => x.SendEmailIfRequired(_siteDetailsViewModel)).Returns(true);

            //When
            var result = target.UpdateSite(_siteDetailsViewModel) as RedirectToRouteResult;

            //Then            
            Assert.That(result, Is.Not.Null);
            Assert.That((target.TempData[SitesController.MessageKey] as string), Is.EqualTo(expectedMessage));
        }

        [Test]
        public void Given_that_address_info_change_is_true_Then_correct_message_is_returned()
        {
            //Given
            _siteService
                .Setup(x => x.GetByIdAndCompanyId(10, It.IsAny<long>()))
                .Returns(new SiteDto { Name = "site" });

            _siteDetailsViewModel = SiteDetailsViewModelBuilder
                                .Create().WithSiteStructureId(10)
                                .WithName(string.Empty)
                                .Build();

            const string expectedMessage = "Your changes have been successfully saved";
            var target = GetTarget();
            _siteService.Setup(ss => ss.CreateUpdate(It.IsAny<CreateUpdateSiteRequest>())).Returns(10);

            //When
            target.UpdateSite(_siteDetailsViewModel);

            //Then            
            Assert.That((target.TempData[SitesController.MessageKey] as string), Is.EqualTo(expectedMessage));
        }

        [Test]
        public void Given_view_model_with_site_structure_id_and_client_id_When_UpdateSite_Then_calls_site_service_GetByIdAndCompanyId()
        {
            // Given
            _siteDetailsViewModel = SiteDetailsViewModelBuilder
                                             .Create()
                                             .WithSiteStructureId(_siteStructureId)
                                             .WithClientId(_clientId)
                                             .Build();
            var target = GetTarget();

            // When
            target.UpdateSite(_siteDetailsViewModel);

            // Then
            _siteService.Verify(x => x.GetByIdAndCompanyId(_siteStructureId, _clientId));
        }

        [Test]
        public void Given_view_model_with_site_id_and_client_id_When_UpdateSite_Then_calls_site_service_GetByIdAndCompanyId()
        {
            // Given
            _siteService
                .Setup(x => x.GetByIdAndCompanyId(_siteStructureId, _clientId))
                .Returns(new SiteDto { Name = "site" });

            _siteDetailsViewModel = SiteDetailsViewModelBuilder
                                             .Create()
                                             .WithSiteStructureId(_siteStructureId)
                                             .WithClientId(_clientId)
                                             .Build();
            //_siteService
            //    .Setup(x => x.GetByIdAndCompanyId(_siteStructureId, _clientId))
            //    .Throws<Exception>();

            var target = GetTarget();

            // When
            var result = target.UpdateSite(_siteDetailsViewModel);
            
            // Then
            _siteUpdateCompositionGateway
                .Verify(x => x.SendEmailIfRequired(It.Is<SiteDetailsViewModel>
                    (y => y.NameBeforeUpdate.Equals("site"))));

        }
        
        private SitesController GetTarget()
        {
            var result = new SitesController(
                _siteStructureViewModelFactory.Object,
                _siteDetailsViewModelFactory.Object,
                _siteService.Object,
                _siteUpdateCompositionGateway.Object,
                null
                );
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}
