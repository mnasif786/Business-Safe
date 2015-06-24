using System.Collections.Generic;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.Documents;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.Controllers;
using BusinessSafe.WebSite.Areas.Documents.Factories;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.ClientDocumentService;
using BusinessSafe.WebSite.WebsiteMoqs;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Documents.BusinessSafeSystemDocumentsLibrary
{
    [TestFixture]
    [Category("Unit")]
    public class IndexTests
    {
        private const long _companyId = 1;
        private Mock<IDocumentLibraryViewModelFactory> _viewModelFactory;
        private readonly FakeCacheHelper _cacheHelper = new FakeCacheHelper();
        private const long _documentTypeId = 2;
        private const string _title = "test";
        private DocHandlerDocumentTypeGroup _docHandlerDocumentTypeGroup;
        private const long _siteId = 0;
        private List<long> _allowedSites;

        [SetUp]
        public void SetUp()
        {
            _allowedSites = new List<long>() { 123, 456, 789 };

            _docHandlerDocumentTypeGroup = DocHandlerDocumentTypeGroup.BusinessSafeSystem;
        
            _viewModelFactory = new Mock<IDocumentLibraryViewModelFactory>();

            _viewModelFactory
                .Setup(x => x.WithCompanyId(_companyId))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.WithDocumentTypeId(_documentTypeId))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.WithDocHandlerDocumentTypeGroup(_docHandlerDocumentTypeGroup))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.WithDocumentTitle(_title))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.WithSiteId(_siteId))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.WithAllowedSites(It.IsAny<List<long>>()))
                .Returns(_viewModelFactory.Object);        
        }

        [Test]
        public void Given_get_Then_should_return_correct_view()
        {
            //Given
            var controller = CreateControllerWithUserAndFactory(_viewModelFactory.Object);

            //Get
            var result = controller.Index(_companyId, _documentTypeId,_title) as ViewResult;
            
            //Then
            Assert.That(result.ViewName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Given_get_Then_should_return_correct_view_model()
        {
            //Given
            var controller = CreateControllerWithUserAndFactory(_viewModelFactory.Object);

            var returnedViewModel = new BusinessSafeSystemDocumentsLibraryViewModel();
            _viewModelFactory.Setup(x => x.GetViewModel()).Returns(returnedViewModel);

            //Get
            var result = controller.Index(_companyId, _documentTypeId, _title) as ViewResult;

            //Then
            Assert.That(result.Model, Is.InstanceOf<BusinessSafeSystemDocumentsLibraryViewModel>());
        }

        [Test]
        public void Given_get_Then_should_call_correct_methods()
        {
            // Given
            _viewModelFactory.Setup(x => x.GetViewModel());
            var controller = CreateControllerWithUserAndFactory(_viewModelFactory.Object);

            // When
            controller.Index(_companyId, _documentTypeId, _title);

            // Then
            _viewModelFactory.VerifyAll();
        }

        // stinky test, but required to ensure allowed sites are used in site retrieval
        [Test]
        public void Given_get_Then_viewmodel_should_return_sites_collection_dropdown()
        {
            // Given
            var docHandler = new Mock<IDocHandlerDocumentTypeService>();
            var clientDocumentService = new Mock<IClientDocumentService>();
            var siteService = new Mock<ISiteService>();
            var sites = new List<SiteDto>()
                            {
                                new SiteDto{ Id = 1, Name = "Test"}
                            };

            docHandler.Setup(x => x.GetForDocumentGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem)).Returns(new DocHandlerDocumentTypeDto[0]);
            var passedSearchSitesRequest = new SearchSitesRequest();
            siteService
                .Setup(x => x.Search(It.IsAny<SearchSitesRequest>()))
                .Callback<SearchSitesRequest>(y => passedSearchSitesRequest = y)
                .Returns(sites);
            clientDocumentService.Setup(x => x.Search(It.IsAny<SearchClientDocumentsRequest>())).Returns(new ClientDocumentDto[0]);

            var factory = new DocumentLibraryViewModelFactory(clientDocumentService.Object, docHandler.Object, null, null, siteService.Object, _cacheHelper);
            var controller = CreateControllerWithUserAndFactory(factory);

            // When
            var result = controller.Index(_companyId, _documentTypeId, _title) as ViewResult;
            var model = result.Model as BusinessSafeSystemDocumentsLibraryViewModel;

            // Then
            siteService.Verify(x => x.Search(It.IsAny<SearchSitesRequest>()), Times.Once());
            Assert.That(model.Sites, Is.Not.Null);
            Assert.That(passedSearchSitesRequest.CompanyId, Is.EqualTo(_companyId));
            Assert.That(passedSearchSitesRequest.AllowedSiteIds, Is.EqualTo(_allowedSites));
        }

        private BusinessSafeSystemDocumentsLibraryController CreateController()
        {
            return new BusinessSafeSystemDocumentsLibraryController(_viewModelFactory.Object);
        }

        private BusinessSafeSystemDocumentsLibraryController CreateControllerWithUserAndFactory(IDocumentLibraryViewModelFactory viewModelFactory)
        {
            var controller = new BusinessSafeSystemDocumentsLibraryController(viewModelFactory);
            return TestControllerHelpers.AddUserWithDefinableAllowedSitesToController(controller, _allowedSites);
        }
    }
}