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
    public class UpdateSiteGroupTests
    {
        private Mock<ISiteStructureViewModelFactory> _siteStructureViewModelFactory;
        private Mock<ISiteGroupViewModelFactory> _siteGroupViewModelFactory;
        private Mock<ISiteGroupService> _siteGroupService;

        [SetUp]
        public void SetUp()
        {
            _siteStructureViewModelFactory = new Mock<ISiteStructureViewModelFactory>();
            _siteGroupViewModelFactory = new Mock<ISiteGroupViewModelFactory>();
            _siteGroupService = new Mock<ISiteGroupService>();

            _siteStructureViewModelFactory.Setup(ss => ss.WithGroupDetailsViewModel(It.IsAny<SiteGroupDetailsViewModel>())).Returns(_siteStructureViewModelFactory.Object);
            _siteStructureViewModelFactory.Setup(ss => ss.WithClientId(It.IsAny<long>())).Returns(_siteStructureViewModelFactory.Object);
            _siteStructureViewModelFactory.Setup(ss => ss.GetViewModel()).Returns(CreateSiteStructureViewModel());
            _siteStructureViewModelFactory.Setup(ss => ss.HideSiteDetails()).Returns(_siteStructureViewModelFactory.Object);
            _siteStructureViewModelFactory.Setup(ss => ss.DisplaySiteGroups()).Returns(_siteStructureViewModelFactory.Object);
        }
        
        [TestCase(10)]
        [TestCase(20)]
        public void Given_that_view_model_has_groupId_When_viewmodel_is_mapped_Then_correct_id_is_used_in_request_object(long groupId)
        {
            //Given            
            CreateUpdateSiteGroupRequest expectedSiteGroupRequest = null;
            _siteGroupService.Setup(siteService => siteService.CreateUpdate(It.IsAny<CreateUpdateSiteGroupRequest>())).Callback<CreateUpdateSiteGroupRequest>(gr => expectedSiteGroupRequest = gr);

            var target = CreateSitesController();

            var groupDetailsViewModel = SiteGroupDetailsViewModelBuilder.Create().WithGroupId(groupId).Build();

            //When
            target.UpdateGroup(groupDetailsViewModel);

            //Then
            Assert.That(expectedSiteGroupRequest.GroupId, Is.EqualTo(groupId));
        }


        [TestCase("test 1")]
        [TestCase("test name")]
        public void Given_that_view_model_has_name_When_viewmodel_is_mapped_Then_correct_name_is_used_in_request_object(string name)
        {
            //Given                       
            CreateUpdateSiteGroupRequest expectedSiteGroupRequest = null;
            _siteGroupService.Setup(siteService => siteService.CreateUpdate(It.IsAny<CreateUpdateSiteGroupRequest>())).Callback<CreateUpdateSiteGroupRequest>(gr => expectedSiteGroupRequest = gr);

            var target = CreateSitesController();

            var groupDetailsViewModel = SiteGroupDetailsViewModelBuilder.Create().WithName(name).Build();

            //When
            target.UpdateGroup(groupDetailsViewModel);

            //Then
            Assert.That(expectedSiteGroupRequest.Name, Is.EqualTo(name));
        }

        [TestCase(2)]
        [TestCase(5)]
        public void Given_that_view_model_has_linked_to_site_id_When_viewmodel_is_mapped_Then_correct_linked_to_site_id_is_used_in_request_object(long linkToSiteId)
        {
            //Given                                 
            CreateUpdateSiteGroupRequest expectedSiteGroupRequest = null;
            _siteGroupService.Setup(siteService => siteService.CreateUpdate(It.IsAny<CreateUpdateSiteGroupRequest>())).Callback<CreateUpdateSiteGroupRequest>(gr => expectedSiteGroupRequest = gr);

            var target = CreateSitesController();

            var groupDetailsViewModel = SiteGroupDetailsViewModelBuilder.Create().WithLinkToSiteId(linkToSiteId).Build();

            //When
            target.UpdateGroup(groupDetailsViewModel);

            //Then
            Assert.That(expectedSiteGroupRequest.LinkToSiteId, Is.EqualTo(linkToSiteId));
        }

        [TestCase(2)]
        [TestCase(5)]
        public void Given_that_view_model_has_linked_to_group_id_When_viewmodel_is_mapped_Then_correct_linked_to_group_id_is_used_in_request_object(long linkToGroupId)
        {
            //Given                                 
            CreateUpdateSiteGroupRequest expectedSiteGroupRequest = null;
            _siteGroupService.Setup(siteService => siteService.CreateUpdate(It.IsAny<CreateUpdateSiteGroupRequest>())).Callback<CreateUpdateSiteGroupRequest>(gr => expectedSiteGroupRequest = gr);

            var target = CreateSitesController();

            var groupDetailsViewModel = SiteGroupDetailsViewModelBuilder.Create().WithLinkToGroupId(linkToGroupId).Build();

            //When
            target.UpdateGroup(groupDetailsViewModel);

            //Then
            Assert.That(expectedSiteGroupRequest.LinkToGroupId, Is.EqualTo(linkToGroupId));
        }

        [Test]
        public void Given_that_view_model_not_valid_Then_index_is_returned()
        {
            //Given                                 
            _siteGroupService.Setup(siteService => siteService.CreateUpdate(It.IsAny<CreateUpdateSiteGroupRequest>()));

            var target = CreateSitesController();
            
            target.ModelState.AddModelError("Name", "Some error");
            var groupDetailsViewModel = SiteGroupDetailsViewModelBuilder.Create().Build();

            _siteStructureViewModelFactory.Setup(ss => ss.WithValidationError(true)).Returns(_siteStructureViewModelFactory.Object);

            //When
            var result = target.UpdateGroup(groupDetailsViewModel) as ViewResult;

            //Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void Given_that_view_model_not_valid_Then_index_is_returned2()
        {
            //Given
            const long clientId = 10;                   
            
            var groupDetailsViewModel = SiteGroupDetailsViewModelBuilder.Create().WithClientId(clientId).Build();

            _siteGroupService.Setup(siteService => siteService.CreateUpdate(It.IsAny<CreateUpdateSiteGroupRequest>()));
            
            _siteStructureViewModelFactory.Setup(ss => ss.WithGroupDetailsViewModel(groupDetailsViewModel)).Returns(_siteStructureViewModelFactory.Object);
            _siteStructureViewModelFactory.Setup(ss => ss.WithClientId(clientId)).Returns(_siteStructureViewModelFactory.Object);
            _siteStructureViewModelFactory.Setup(ss => ss.HideSiteDetails()).Returns(_siteStructureViewModelFactory.Object);
            _siteStructureViewModelFactory.Setup(ss => ss.DisplaySiteGroups()).Returns(_siteStructureViewModelFactory.Object);
            _siteStructureViewModelFactory.Setup(ss => ss.WithValidationError(true)).Returns(_siteStructureViewModelFactory.Object);

            var expectedViewModel = CreateSiteStructureViewModel();
            _siteStructureViewModelFactory.Setup(ss => ss.GetViewModel()).Returns(expectedViewModel);

            var target = CreateSitesController();

            target.ModelState.AddModelError("Name", "Some error");
            

            //When
            var result = target.UpdateGroup(groupDetailsViewModel) as ViewResult;

            //Then
            Assert.That(result.Model, Is.EqualTo(expectedViewModel));
        }

        [Test]
        public void Given_a_view_model_for_new_site_Then_index_is_returned_with_message_in_temp_data()
        {
            //Given
            const long clientId = 10;
            const string expectedMessage = "Your new Site Group has been successfully created";

            _siteGroupService.Setup(siteService => siteService.CreateUpdate(It.IsAny<CreateUpdateSiteGroupRequest>()));

            var groupDetailsViewModel = SiteGroupDetailsViewModelBuilder
                                                .Create()
                                                .WithClientId(clientId)
                                                .Build();

            _siteStructureViewModelFactory.Setup(ss => ss.WithGroupDetailsViewModel(groupDetailsViewModel)).Returns(_siteStructureViewModelFactory.Object);
            _siteStructureViewModelFactory.Setup(ss => ss.WithClientId(clientId)).Returns(_siteStructureViewModelFactory.Object);
            _siteStructureViewModelFactory.Setup(ss => ss.GetViewModel()).Returns(CreateSiteStructureViewModel());

            var target = CreateSitesController();

            //When
            target.UpdateGroup(groupDetailsViewModel);

            //Then
            Assert.That(target.TempData["Message"], Is.EqualTo(expectedMessage));
        }

        [Test]
        public void Given_a_view_model_for_editing_site_Then_index_is_returned_with_message_in_temp_data()
        {
            //Given
            const long clientId = 10;
            const string expectedMessage = "Your changes have been successfully saved";

            _siteGroupService.Setup(siteService => siteService.CreateUpdate(It.IsAny<CreateUpdateSiteGroupRequest>()));

            var groupDetailsViewModel = SiteGroupDetailsViewModelBuilder
                                                .Create()
                                                .WithClientId(clientId)
                                                .WithGroupId(1)
                                                .Build();

            _siteStructureViewModelFactory.Setup(ss => ss.WithGroupDetailsViewModel(groupDetailsViewModel)).Returns(_siteStructureViewModelFactory.Object);
            _siteStructureViewModelFactory.Setup(ss => ss.WithClientId(clientId)).Returns(_siteStructureViewModelFactory.Object);
            _siteStructureViewModelFactory.Setup(ss => ss.GetViewModel()).Returns(CreateSiteStructureViewModel());

            var target = CreateSitesController();

            //When
            target.UpdateGroup(groupDetailsViewModel);

            //Then
            Assert.That(target.TempData["Message"], Is.EqualTo(expectedMessage));
        }

        [Test]
        public void Given_that_view_model_valid_Then_it_is_redirected_to_index()
        {
            //Given
            const long clientId = 10;


            _siteGroupService.Setup(siteService => siteService.CreateUpdate(It.IsAny<CreateUpdateSiteGroupRequest>()));

            var groupDetailsViewModel = SiteGroupDetailsViewModelBuilder.Create().WithClientId(clientId).Build();

            _siteStructureViewModelFactory.Setup(ss => ss.WithGroupDetailsViewModel(groupDetailsViewModel)).Returns(_siteStructureViewModelFactory.Object);
            _siteStructureViewModelFactory.Setup(ss => ss.WithClientId(clientId)).Returns(_siteStructureViewModelFactory.Object);
            _siteStructureViewModelFactory.Setup(ss => ss.GetViewModel()).Returns(CreateSiteStructureViewModel());

            var target = CreateSitesController();

            //When
            var result = (RedirectToRouteResult)target.UpdateGroup(groupDetailsViewModel);

            //Then
            Assert.That(result.RouteValues["controller"], Is.EqualTo("SitesStructure"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        private static SiteStructureViewModel CreateSiteStructureViewModel()
        {
            var result = new SiteStructureViewModel(1, new MvcHtmlString(string.Empty), new MvcHtmlString(string.Empty), new SiteDetailsViewModel(), null, false, false, false, false);
            return result;
        }

        private SiteGroupController CreateSitesController()
        {
            var result = new SiteGroupController(_siteGroupViewModelFactory.Object, _siteGroupService.Object, _siteStructureViewModelFactory.Object);
            return TestControllerHelpers.AddUserToController(result);

        }
    }
}
