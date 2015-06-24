using System;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Application.Contracts.Documents;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.WebSite.Areas.Documents.Factories;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.Documents.AddedDocumentsLibraryViewModelFactoryTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetViewModelTests
    {
        private Mock<IDocumentTypeService> _documentTypeService;
        private Mock<IDocumentService> _documentService;
        private Mock<ITaskService> _taskService;
        private Mock<ISiteGroupService> _siteGroupService;
        private Mock<ISiteService> _siteService;
        private long _companyId;
        private string _title;
        private long _documentTypeId;
        private long _siteId;
        private long _siteGroupId;

        [SetUp]
        public void Setup()
        {
            _companyId = 800;
            _title = "Test";
            _documentTypeId = 5;
            _siteId = 1;
            _siteGroupId = 235251L;
            _documentTypeService = new Mock<IDocumentTypeService>();
            _documentService = new Mock<IDocumentService>();
            _taskService = new Mock<ITaskService>();
            _siteService = new Mock<ISiteService>();
            _siteGroupService = new Mock<ISiteGroupService>();
            _siteGroupService.Setup(x => x.GetByCompanyId(It.IsAny<long>()));
        }

        [Test]
        public void When_get_view_model_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateViewModelFactory();

            var documentTypes = new List<DocumentTypeDto>();
            _documentTypeService
                .Setup(x => x.GetAll())
                .Returns(documentTypes);

            _documentService
                .Setup(x => x.Search(It.Is<SearchDocumentRequest>(y => y.CompanyId == _companyId &&
                                                                       y.TitleLike == _title &&
                                                                       y.DocumentTypeId == _documentTypeId &&
                                                                       y.SiteId == _siteId &&
                                                                       y.SiteGroupId == _siteGroupId)))
                .Returns(new List<DocumentDto>());


            _siteService
              .Setup(x => x.Search(It.IsAny<SearchSitesRequest>()))
              .Returns(new List<SiteDto>());

            //When
            target
                .WithCompanyId(_companyId)
                .WithDocumentTitle(_title)
                .WithDocumentTypeId(_documentTypeId)
                .WithSiteId(_siteId)
                .WithSiteGroupId(_siteGroupId)
                .GetViewModel();

            //Then
            _documentTypeService.VerifyAll();
            _documentService.VerifyAll();
        }

        [Test]
        public void When_get_view_model_Then_return_correct_view_model()
        {
            //Given
            var target = CreateViewModelFactory();

            var returnDocumentTypesList = new List<DocumentTypeDto>()
                                              {
                                                  new DocumentTypeDto()
                                                      {
                                                          Id = 1, Name = "Test 2"
                                                      },
                                                  new DocumentTypeDto()
                                                      {
                                                          Id = 2, Name = "Test 1"
                                                      }
                                              };
            _documentTypeService
                .Setup(x => x.GetAll())
                .Returns(returnDocumentTypesList);

            var returnDocuments = new[]
                                      {
                                          new DocumentDto()
                                              {
                                                  Id = 1,
                                                  DocumentType = returnDocumentTypesList[0],
                                                  CreatedOn = DateTime.Now
                                              },
                                          new DocumentDto()
                                              {
                                                  Id = 2,
                                                  DocumentType = returnDocumentTypesList[0],
                                                  CreatedOn = DateTime.Now
                                              },
                                          new DocumentDto()
                                              {
                                                  Id = 3,
                                                  DocumentType = returnDocumentTypesList[0],
                                                  CreatedOn = DateTime.Now
                                              }, 
                                      };
            _documentService
                .Setup(x => x.Search(It.IsAny<SearchDocumentRequest>()))
                .Returns(returnDocuments);

            _siteService
              .Setup(x => x.Search(It.IsAny<SearchSitesRequest>()))
              .Returns(new List<SiteDto>());

            //When
            var result = target
                .WithCompanyId(_companyId)
                .WithDocumentTitle(_title)
                .WithDocumentTypeId(_documentTypeId)
                .GetViewModel();

            //Then
            Assert.That(result, Is.TypeOf<AddedDocumentsLibraryViewModel>());
            Assert.That(result.CompanyId, Is.EqualTo(_companyId));
            Assert.That(result.Title, Is.EqualTo(_title));
            Assert.That(result.DocumentTypeId, Is.EqualTo(_documentTypeId));

            Assert.That(result.DocumentTypes.Count(), Is.EqualTo(returnDocumentTypesList.Count() + 1));
            Assert.That(result.DocumentTypes.Skip(1).Take(1).First().label, Is.EqualTo(returnDocumentTypesList.Last().Name));

            Assert.That(result.Documents.Count(), Is.EqualTo(returnDocuments.Count()));

        }

        [Test]
        public void When_GetViewModel_Then_get_all_site_groups()
        {
            //Given
            var target = CreateViewModelFactory();

            // When
            target
                .WithCompanyId(_companyId)
                .GetViewModel();

            // Then
            _siteGroupService.Verify(x => x.GetByCompanyId(_companyId));
        }

        [Test]
        public void When_GetViewModel_Then_map_site_groups_to_AutoCompleteViewModels()
        {
            //Given
            var siteGroups = new List<SiteGroupDto>()
                             {
                                 new SiteGroupDto() { Id = 123L, Name = "abc" },
                                 new SiteGroupDto() { Id = 456L, Name = "def" },
                                 new SiteGroupDto() { Id = 789L, Name = "ghi" },
                             };
            _siteGroupService
                .Setup(x => x.GetByCompanyId(It.IsAny<long>()))
                .Returns(siteGroups);

            var target = CreateViewModelFactory();

            // When
            var result = target
                        .WithCompanyId(_companyId)
                        .GetViewModel();

            // Then
            Assert.That(result.SiteGroups.Count(), Is.EqualTo(siteGroups.Count() + 1));
            Assert.That(result.SiteGroups.ElementAt(1).label, Is.EqualTo(siteGroups.ElementAt(0).Name));
            Assert.That(result.SiteGroups.ElementAt(2).label, Is.EqualTo(siteGroups.ElementAt(1).Name));
            Assert.That(result.SiteGroups.ElementAt(3).label, Is.EqualTo(siteGroups.ElementAt(2).Name));
            Assert.That(result.SiteGroups.ElementAt(1).value, Is.EqualTo(siteGroups.ElementAt(0).Id.ToString()));
            Assert.That(result.SiteGroups.ElementAt(2).value, Is.EqualTo(siteGroups.ElementAt(1).Id.ToString()));
            Assert.That(result.SiteGroups.ElementAt(3).value, Is.EqualTo(siteGroups.ElementAt(2).Id.ToString()));
        }

        private AddedDocumentsLibraryViewModelFactory CreateViewModelFactory()
        {
            return new AddedDocumentsLibraryViewModelFactory(_documentTypeService.Object, _documentService.Object, _taskService.Object, _siteService.Object, _siteGroupService.Object);
        }

    }
}