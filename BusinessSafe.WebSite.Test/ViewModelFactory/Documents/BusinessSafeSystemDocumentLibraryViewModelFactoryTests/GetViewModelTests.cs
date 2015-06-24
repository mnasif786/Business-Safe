using System;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Application.Contracts.Documents;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.Factories;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.ClientDocumentService;
using BusinessSafe.WebSite.DocumentSubTypeService;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.WebsiteMoqs;

using Moq;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.Documents.BusinessSafeSystemDocumentLibraryViewModelFactoryTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetViewModelTests
    {
        private long _companyId;
        private string _title;
        private long _documentTypeId;
        private Mock<IClientDocumentService> _clientDocumentService;
        private Mock<DocumentTypeService.IDocumentTypeService> _clientDocumentTypeService;
        private Mock<IDocHandlerDocumentTypeService> _documentTypeService;
        private Mock<IDocumentSubTypeService> _subDocumentTypeService;
        private ICacheHelper _cacheHelper;
        private Mock<ISiteService> _siteService;

        [SetUp]
        public void Setup()
        {
            _companyId = 800;
            _title = "Test";
            _documentTypeId = 5;
            _documentTypeService = new Mock<IDocHandlerDocumentTypeService>();
            _subDocumentTypeService = new Mock<IDocumentSubTypeService>();
            _clientDocumentService = new Mock<IClientDocumentService>();
            _clientDocumentTypeService = new Mock<BusinessSafe.WebSite.DocumentTypeService.IDocumentTypeService>();
            _cacheHelper = new FakeCacheHelper();
            _siteService = new Mock<ISiteService>();
            _siteService.Setup(x => x.GetAll(It.IsAny<long>())).Returns(new List<SiteDto>());
        }

        [Test]
        public void When_get_view_model_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateViewModelFactory();

            var documentTypes = new List<DocHandlerDocumentTypeDto>();
            _documentTypeService
                .Setup(x => x.GetForDocumentGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem))
                .Returns(documentTypes);

            var documents = new ClientDocumentDto[]{};
            _clientDocumentService
                .Setup(x => x.Search(It.Is<SearchClientDocumentsRequest>(y => y.ClientId == _companyId && 
                                                                              y.TitleLike == _title && 
                                                                              y.DocumentTypeId == _documentTypeId)))
                .Returns(documents);

            //When
            target
                .WithCompanyId(_companyId)
                .WithDocHandlerDocumentTypeGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem)
                .WithDocumentTypeId(_documentTypeId)
                .WithDocumentSubTypeId(It.IsAny<long>())
                .WithDocumentTitle(_title)
                .WithKeywords(It.IsAny<string>())
                .GetViewModel();

            //Then
            _documentTypeService.VerifyAll();
            _clientDocumentService.VerifyAll();
        }

        [Test]
        public void Given_siteId_When_get_view_model_Then_should_call_site_service_to_get_peninsula_site_id_for_request_to_client_documentation()
        {
            //Given
            var target = CreateViewModelFactory();

            var documentTypes = new List<DocHandlerDocumentTypeDto>();
            _documentTypeService
                .Setup(x => x.GetForDocumentGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem))
                .Returns(documentTypes);

            var documents = new ClientDocumentDto[] { };
            _clientDocumentService
                .Setup(x => x.Search(It.Is<SearchClientDocumentsRequest>(y => y.ClientId == _companyId &&
                                                                              y.TitleLike == _title &&
                                                                              y.DocumentTypeId == _documentTypeId)))
                .Returns(documents);

            const int siteId = 500;
            const int peninsulaSiteId = 1000;

            _siteService
                .Setup(x => x.GetByIdAndCompanyId(siteId, _companyId))
                .Returns(new SiteDto {SiteId = peninsulaSiteId});

            //When
            target
                .WithCompanyId(_companyId)
                .WithDocHandlerDocumentTypeGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem)
                .WithDocumentTypeId(_documentTypeId)
                .WithDocumentSubTypeId(It.IsAny<long>())
                .WithDocumentTitle(_title)
                .WithKeywords(It.IsAny<string>())
                .WithSiteId(siteId)
                .GetViewModel();

            //Then
            _siteService.Verify(x => x.Search(It.IsAny<SearchSitesRequest>()));
        }

        [Test]
        public void GetViewModel_retrieves_docType_ids_from_dochandler_and_returns_correct_documenttypes_for_doctype_ddl()
        {
            // Given
            var target = CreateViewModelFactory();
            var documentTypes = new List<DocHandlerDocumentTypeDto>()
                                    {
                                        new DocHandlerDocumentTypeDto { Id = 1 },
                                        new DocHandlerDocumentTypeDto { Id = 2 },
                                        new DocHandlerDocumentTypeDto { Id = 3 }
                                    };
            _documentTypeService
                .Setup(x => x.GetForDocumentGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem))
                .Returns(documentTypes);

            _clientDocumentTypeService.Setup(x => x.GetByIds(It.IsAny<long[]>())).Returns(new DocumentTypeService.DocumentTypeDto[3]);

            // When
            var model = target
                .WithCompanyId(_companyId)
                .WithDocumentTitle(_title)
                .WithDocHandlerDocumentTypeGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem)
                .GetViewModel();

            // Test
            _documentTypeService.Verify(x => x.GetForDocumentGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem), Times.Once());

            Assert.That(model.DocumentTypes.Count(), Is.EqualTo(4));
        }

        [Test]
        public void GetViewModel_retrieves_docType_ids_from_dochandler_service_and_passes_to_clientdocument_webservice()
        {
            // Given
            var target = CreateViewModelFactory();
            var passedSearchClientDocumentsRequest = new SearchClientDocumentsRequest();
            var documentTypes = new List<DocHandlerDocumentTypeDto>()
                                    {
                                        new DocHandlerDocumentTypeDto { Id = 1 },
                                        new DocHandlerDocumentTypeDto { Id = 2 },
                                        new DocHandlerDocumentTypeDto { Id = 3 }
                                    };
            _documentTypeService
                .Setup(x => x.GetForDocumentGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem))
                .Returns(documentTypes);

            _clientDocumentService
                .Setup(x => x.Search(It.IsAny<SearchClientDocumentsRequest>())).Returns(new ClientDocumentDto[0])
                .Callback<SearchClientDocumentsRequest>(y => passedSearchClientDocumentsRequest = y);

            // When
            target
                .WithCompanyId(_companyId)
                .WithDocumentTitle(_title)
                .WithDocHandlerDocumentTypeGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem)
                .GetViewModel();

            // Test
            _documentTypeService.Verify(x => x.GetForDocumentGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem), Times.Once());
            _clientDocumentService.Verify(x => x.Search(It.IsAny<SearchClientDocumentsRequest>()), Times.Once());
            Assert.That(passedSearchClientDocumentsRequest.DocumentTypeIds, Is.EqualTo(new long[] { 1,2,3 }));
        }

        [Test]
        public void When_get_view_model_Then_return_correct_view_model()
        {
            //Given
            var target = CreateViewModelFactory();

            var returnDocumentTypesList = new List<DocHandlerDocumentTypeDto>()
                                              {
                                                  new DocHandlerDocumentTypeDto()
                                                      {
                                                          Id = 1
                                                      },
                                                  new DocHandlerDocumentTypeDto()
                                                      {
                                                          Id = 2
                                                      }
                                              };
            _documentTypeService
                .Setup(x => x.GetForDocumentGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem))
                .Returns(returnDocumentTypesList);


            var peninsulaSiteId = 34587435;
            var siteId = 678L;

            _siteService
                .Setup(x => x.Search(It.IsAny<SearchSitesRequest>()))
                .Returns(new SiteDto[] {new SiteDto {Id = siteId, SiteId = peninsulaSiteId}});

            var documentType = new ClientDocumentService.DocumentTypeDto()
                                   {
                                       Id = 1,
                                       Title = "DocumentType"
                                   };

            var returnDocuments = new[]
                                      {
                                          new ClientDocumentDto() 
                                              {
                                                  Id = 1,
                                                  CreatedOn = DateTime.Now,
                                                  Title = "Title1",
                                                  Description = "Test Description",
                                                  DocumentType = documentType,
                                                  SiteId = peninsulaSiteId
                                              },
                                          new ClientDocumentDto()
                                              {
                                                  Id = 2,
                                                  CreatedOn = DateTime.Now,
                                                  Title = "Title2",
                                                  DocumentType = documentType,
                                                  SiteId = peninsulaSiteId
                                              },
                                          new ClientDocumentDto()
                                              {
                                                  Id = 3,
                                                  CreatedOn = DateTime.Now,
                                                  Title = "Title3",
                                                  DocumentType = documentType,
                                                  SiteId = peninsulaSiteId
                                              }, 
                                      };
            _clientDocumentService
                .Setup(x => x.Search(It.IsAny<SearchClientDocumentsRequest>()))
                .Returns(returnDocuments);

            //When
            var result = target
                .WithCompanyId(_companyId)
                .WithDocHandlerDocumentTypeGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem)
                .WithDocumentTitle(_title)
                .WithDocumentTypeId(_documentTypeId)
                .WithAllowedSites(new List<long>{siteId})
                .GetViewModel();

            //Then
            Assert.That(result, Is.TypeOf<BusinessSafeSystemDocumentsLibraryViewModel>());
            Assert.That(result.CompanyId, Is.EqualTo(_companyId));
            Assert.That(result.Title, Is.EqualTo(_title));
            Assert.That(result.Documents.Count(), Is.EqualTo(returnDocuments.Count()));

            var firstViewModel = result.Documents.First();
            var firstClientDocumentDto = returnDocuments.First();

            Assert.That(firstViewModel.Id, Is.EqualTo(firstClientDocumentDto.Id));
            Assert.That(firstViewModel.Title, Is.EqualTo(firstClientDocumentDto.Title));
            Assert.That(firstViewModel.DocumentType, Is.EqualTo(firstClientDocumentDto.DocumentType.Title));
            Assert.That(firstViewModel.Description, Is.EqualTo(firstClientDocumentDto.Description));
            Assert.That(result.Documents.Last().Title, Is.EqualTo(returnDocuments.Last().Title));
        }

        [Test]
        public void Given_2_sites_with_same_when_get_view_model_Then_return_correct_view_model()
        {
            //Given
            var target = CreateViewModelFactory();

            var returnDocumentTypesList = new List<DocHandlerDocumentTypeDto>()
                                              {
                                                  new DocHandlerDocumentTypeDto()
                                                      {
                                                          Id = 1
                                                      },
                                                  new DocHandlerDocumentTypeDto()
                                                      {
                                                          Id = 2
                                                      }
                                              };
            _documentTypeService
                .Setup(x => x.GetForDocumentGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem))
                .Returns(returnDocumentTypesList);


            var peninsulaSiteId = 34587435;
            var siteId = 678L;

            _siteService
                .Setup(x => x.Search(It.IsAny<SearchSitesRequest>()))
                .Returns(() => new SiteDto[] { 
                    new SiteDto() { Id = siteId, SiteId = peninsulaSiteId,Name = "Test"} 
                    ,new SiteDto() {Id=siteId,SiteId = peninsulaSiteId,Name="Test"}, 
                });

            var documentType = new ClientDocumentService.DocumentTypeDto()
            {
                Id = 1,
                Title = "DocumentType"
            };

            var returnDocuments = new[]{
                                          new ClientDocumentDto() 
                                              {
                                                  Id = 1,
                                                  CreatedOn = DateTime.Now,
                                                  Title = "Title1",
                                                  Description = "Test Description",
                                                  DocumentType = documentType,
                                                  SiteId = peninsulaSiteId
                                              }
                                      };
            _clientDocumentService
                .Setup(x => x.Search(It.IsAny<SearchClientDocumentsRequest>()))
                .Returns(returnDocuments);

            //When
            var result = target
                .WithCompanyId(_companyId)
                .WithDocHandlerDocumentTypeGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem)
                .WithDocumentTitle(_title)
                .WithDocumentTypeId(_documentTypeId)
                .WithAllowedSites(new List<long> { siteId })
                .GetViewModel();

            //Then
            Assert.That(result.Sites.Count(), Is.EqualTo(3));
          
        }


        [Test]
        public void Given_view_model_with_document_type_ids_when_get_view_model_Then_pass_document_type_ids_to_request()
        {
            //Given
            var target = CreateViewModelFactory();

            var returnDocumentTypesList = new List<DocHandlerDocumentTypeDto>()
                                              {
                                                  new DocHandlerDocumentTypeDto()
                                                      {
                                                          Id = 1
                                                      },
                                                  new DocHandlerDocumentTypeDto()
                                                      {
                                                          Id = 2
                                                      }
                                              };
            _documentTypeService
                .Setup(x => x.GetForDocumentGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem))
                .Returns(returnDocumentTypesList);


            var peninsulaSiteId = 34587435;
            var siteId = 678L;

            _siteService
                .Setup(x => x.Search(It.IsAny<SearchSitesRequest>()))
                .Returns(() => new SiteDto[] { 
                    new SiteDto() { Id = siteId, SiteId = peninsulaSiteId,Name = "Test"} 
                    ,new SiteDto() {Id=siteId,SiteId = peninsulaSiteId,Name="Test"}, 
                });

            var documentTypeEvaluationReport = new ClientDocumentService.DocumentTypeDto()
            {
                Id = 1,
                Title = "Site Evaluation Reports"
            };

            var documentTypeBBSManagementReviews = new ClientDocumentService.DocumentTypeDto()
            {
                Id = 2,
                Title = "Management Reviews"
            };

            var documentTypeEvaluationReportSafeDoc = new ClientDocumentService.DocumentTypeDto()
            {
                Id = 3,
                Title = "Site Evaluation Reports safe docs"
            };

            var documentTypeHandsBookSafeDoc = new ClientDocumentService.DocumentTypeDto()
            {
                Id = 4,
                Title = "Site Evaluation Reports safe docs"
            };

            var docTypeIdsToSerachFor = new List<long> {1, 2};

            var returnDocuments = new[]{
                                          new ClientDocumentDto() 
                                              {
                                                  Id = 1,
                                                  CreatedOn = DateTime.Now,
                                                  Title = "Title1",
                                                  Description = "Test Description",
                                                  DocumentType = documentTypeEvaluationReport,
                                                  SiteId = peninsulaSiteId
                                              },

                                               new ClientDocumentDto() 
                                              {
                                                  Id = 2,
                                                  CreatedOn = DateTime.Now,
                                                  Title = "Title2",
                                                  Description = "Test Description",
                                                  DocumentType = documentTypeBBSManagementReviews,
                                                  SiteId = peninsulaSiteId
                                              },

                                              new ClientDocumentDto() 
                                              {
                                                  Id = 3,
                                                  CreatedOn = DateTime.Now,
                                                  Title = "Title3",
                                                  Description = "Test Description",
                                                  DocumentType = documentTypeHandsBookSafeDoc,
                                                  SiteId = peninsulaSiteId
                                              },

                                              new ClientDocumentDto() 
                                              {
                                                  Id = 4,
                                                  CreatedOn = DateTime.Now,
                                                  Title = "Title4",
                                                  Description = "Test Description",
                                                  DocumentType = documentTypeEvaluationReportSafeDoc,
                                                  SiteId = peninsulaSiteId
                                              }
                                      };

            var searchClientDocumentsRequest = new SearchClientDocumentsRequest();
            _clientDocumentService
                .Setup(x => x.Search(It.IsAny<SearchClientDocumentsRequest>()))
                .Callback<SearchClientDocumentsRequest>(y => searchClientDocumentsRequest= y)
                .Returns(returnDocuments);

            //When
            var result = target
                .WithCompanyId(_companyId)
                .WithDocHandlerDocumentTypeGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem)
                .WithDocumentTitle(_title)
                .WithDocumentTypeIds(docTypeIdsToSerachFor)
                .WithAllowedSites(new List<long> { siteId })
                .GetViewModel();

            //Then
            Assert.That(searchClientDocumentsRequest.DocumentTypeIds.Count(), Is.EqualTo(2));
        }

        private DocumentLibraryViewModelFactory CreateViewModelFactory()
        {
            return new DocumentLibraryViewModelFactory(_clientDocumentService.Object, _documentTypeService.Object, _clientDocumentTypeService.Object, _subDocumentTypeService.Object, _siteService.Object, _cacheHelper);
        }
    }
}