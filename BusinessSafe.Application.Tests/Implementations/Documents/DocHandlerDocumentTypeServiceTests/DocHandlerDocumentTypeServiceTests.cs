using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Implementations.Documents;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.Documents.DocHandlerDocumentTypeServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class DocHandlerDocumentTypeServiceTests
    {
        private Mock<IDocHandlerDocumentTypeRepository> _docTypeRepo;
        private DocHandlerDocumentTypeService _target;
        [SetUp]
        public void Setup()
        {
            _docTypeRepo = new Mock<IDocHandlerDocumentTypeRepository>();
            _docTypeRepo.Setup(x => x.GetByDocHandlerDocumentTypeGroup(It.IsAny<DocHandlerDocumentTypeGroup>())).Returns(new List<DocHandlerDocumentType>());
            _docTypeRepo.Setup(x => x.GetById(It.IsAny<long>()));

            _target = new DocHandlerDocumentTypeService(_docTypeRepo.Object);
        }

        [Test]
        public void Given_document_group_BusinessSafeSystem_When_GetIdsForDocumentGroup_Then_should_return_correct_result()
        {
            // Given
            var returnedDocTypes = new List<DocHandlerDocumentType>()
                                       {
                                           new DocHandlerDocumentType() { Id = 125 },
                                           new DocHandlerDocumentType() { Id = 131 },
                                           new DocHandlerDocumentType() { Id = 132 },
                                           new DocHandlerDocumentType() { Id = 127 },
                                           new DocHandlerDocumentType() { Id = 128 }
                                       };
            _docTypeRepo
                .Setup(x => x.GetByDocHandlerDocumentTypeGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem))
                .Returns(returnedDocTypes); 

            //When
            var result = _target.GetForDocumentGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem).Select(x => x.Id).ToList();

            //Then
             _docTypeRepo
                .Verify(x => x.GetByDocHandlerDocumentTypeGroup(DocHandlerDocumentTypeGroup.BusinessSafeSystem), Times.Once());
            Assert.True(result.Contains(125));
            Assert.True(result.Contains(131));
            Assert.True(result.Contains(132));
            Assert.True(result.Contains(127));
            Assert.True(result.Contains(128));
        }

        [Test]
        public void Given_document_group_ReferenceLibrary_When_GetIdsForDocumentGroup_Then_should_return_correct_result()
        {
            // Given
            var returnedDocTypes = new List<DocHandlerDocumentType>()
                                       {
                                           new DocHandlerDocumentType() { Id = 124 },
                                           new DocHandlerDocumentType() { Id = 126 },
                                           new DocHandlerDocumentType() { Id = 129 },
                                           new DocHandlerDocumentType() { Id = 130 },
                                           new DocHandlerDocumentType() { Id = 133 }
                                       };
            _docTypeRepo
                .Setup(x => x.GetByDocHandlerDocumentTypeGroup(DocHandlerDocumentTypeGroup.ReferenceLibrary))
                .Returns(returnedDocTypes); 

            //When
            var result = _target.GetForDocumentGroup(DocHandlerDocumentTypeGroup.ReferenceLibrary).Select(x => x.Id).ToList();

            //Then
            Assert.True(result.Contains(124));
            Assert.True(result.Contains(126));
            Assert.True(result.Contains(129));
            Assert.True(result.Contains(130));
            Assert.True(result.Contains(133));
        }
    }
}