using System.Web.Mvc;
using System.Collections.Generic;
using BusinessSafe.WebSite.Areas.Documents.Controllers;
using BusinessSafe.WebSite.Areas.Documents.Factories;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Documents.AddedDocumentsController
{
    [TestFixture]
    [Category("Unit")]
    public class IndexTests
    {
        private Mock<IAddedDocumentsLibraryViewModelFactory> _viewModelFactory;
        private long _documentTypeId;
        private long _siteId;
        private long _siteGroupId;
        private string _title;

        [SetUp]
        public void SetUp()
        {
            _documentTypeId = 24634462L;
            _siteId = 1L;
            _siteGroupId = 543651L;
            _title = "test";

            _viewModelFactory = new Mock<IAddedDocumentsLibraryViewModelFactory>();
            
            _viewModelFactory
                .Setup(x => x.WithCompanyId(TestControllerHelpers.CompanyIdAssigned))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.WithDocumentTypeId(_documentTypeId))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.WithDocumentTitle(_title))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.WithSiteId(_siteId))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.WithSiteGroupId(_siteGroupId))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.WithAllowedSiteIds(It.IsAny<IList<long>>()))
                .Returns(_viewModelFactory.Object);
        }

        [Test]
        public void Given_get_Then_should_return_correct_view()
        {
            //Given
            var controller = CreateAddedDocumentsLibraryController();

            //Get
            var result = controller.Index( _documentTypeId, _siteId, _siteGroupId, _title) as ViewResult;
            
            //Then
            Assert.That(result.ViewName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Given_get_Then_should_return_correct_view_model()
        {
            //Given
            var controller = CreateAddedDocumentsLibraryController();


            var returnedViewModel = new AddedDocumentsLibraryViewModel();
            _viewModelFactory.Setup(x => x.GetViewModel()).Returns(returnedViewModel);

            //Get
            var result = controller.Index(_documentTypeId, _siteId, _siteGroupId, _title) as ViewResult;
            
            //Then
            Assert.That(result.Model, Is.InstanceOf<AddedDocumentsLibraryViewModel>());
        }


        [Test]
        public void Given_get_Then_should_call_correct_methods()
        {
            // Given
            var controller = CreateAddedDocumentsLibraryController();
            
            _viewModelFactory.Setup(x => x.GetViewModel());
            
            // When
            controller.Index(_documentTypeId, _siteId, _siteGroupId, _title); 

            // Then
            _viewModelFactory.VerifyAll();
        }
        

        private AddedDocumentsLibraryController CreateAddedDocumentsLibraryController()
        {
            return TestControllerHelpers.AddUserToController(new AddedDocumentsLibraryController(null, _viewModelFactory.Object));
        }
    }
}