using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Implementations.Documents;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.Documents.AddedDocumentServiceTests
{
    [TestFixture]
    public class AddAddedDocumentTest
    {

        private Mock<IPeninsulaLog> _log;
        private Mock<IDocumentRepository> _documentRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<ISiteStructureElementRepository> _siteRepository;
        private Mock<IDocumentTypeRepository> _documentTypeRepository;

        private AddedDocumentsService _target;

        [SetUp]
        public void Setup()
        {
            _log = new Mock<IPeninsulaLog>();
            _log.Setup(x => x.Add(It.IsAny<object>()));

            _documentRepository = new Mock<IDocumentRepository>();
            _documentRepository.Setup(x => x.SaveOrUpdate(It.IsAny<AddedDocument>()));

            _userRepository = new Mock<IUserForAuditingRepository>();
            _userRepository.Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>())).Returns(new UserForAuditing());

            _siteRepository = new Mock<ISiteStructureElementRepository>();
            _siteRepository.Setup(x => x.GetById(It.IsAny<long>())).Returns(new SiteGroup());

            _documentTypeRepository = new Mock<IDocumentTypeRepository>();
            _documentTypeRepository.Setup(x => x.GetById(It.IsAny<long>())).Returns(new DocumentType());

            _target = new AddedDocumentsService(_log.Object, _documentRepository.Object, _userRepository.Object, _siteRepository.Object, _documentTypeRepository.Object);
        }

        [Test]
        public void Receives_List_Of_CreateDocumentRequests_And_Calls_Repo_To_Save_Accordingly()
        {
            // Given
            var userId = Guid.NewGuid();
            var companyId = 1234;
            var createDocumentRequests = new List<CreateDocumentRequest>();
            var passedRequest = new AddedDocument();
            var createDocumentRequest = new CreateDocumentRequest()
                                            {
                                                Description = "description",
                                                DocumentLibraryId = 1234,
                                                DocumentType = DocumentTypeEnum.GRADocumentType,
                                                Filename = "helloworld.txt",
                                                SiteId = 123,
                                                Title = "title"
                                            };

            _documentRepository.Setup(x => x.SaveOrUpdate(It.IsAny<AddedDocument>())).Callback<Document>(y => passedRequest = y as AddedDocument);

            createDocumentRequests.Add(createDocumentRequest);

            // When
            _target.Add(createDocumentRequests, userId, companyId);

            // Then
            _log.Verify(x => x.Add(It.IsAny<object>()), Times.Once());
            _siteRepository.Verify(x => x.GetById(123), Times.Once());
            _documentTypeRepository.Verify(x => x.GetById(1), Times.Once());
            _userRepository.Verify(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()));
            _documentRepository.Verify(x => x.SaveOrUpdate(It.IsAny<AddedDocument>()), Times.Exactly(createDocumentRequests.Count()));

            Assert.That(passedRequest, Is.Not.Null);
            Assert.That(passedRequest.Description, Is.EqualTo(createDocumentRequest.Description));
            Assert.That(passedRequest.DocumentLibraryId, Is.EqualTo(createDocumentRequest.DocumentLibraryId));
            Assert.That(passedRequest.DocumentType, Is.Not.Null);
            Assert.That(passedRequest.Filename, Is.EqualTo(createDocumentRequest.Filename));
            Assert.That(passedRequest.Site, Is.Not.Null);
            Assert.That(passedRequest.Title, Is.EqualTo(createDocumentRequest.Title));
            Assert.That(passedRequest.ClientId, Is.EqualTo(companyId));
        }
    }
}
