using System;

using BusinessSafe.Application.Implementations.Documents;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.Documents.DocumentServiceTests
{
    [TestFixture]
    public class MarkDocumentAsDeletedTests
    {
        private Mock<IDocumentRepository> _documentRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<ITaskDocumentRepository> _taskDocumentRepository;
        private Mock<IRiskAssessmentDocumentRepository> _riskAssessmentDocumentRepository;
        private Mock<IAddedDocumentRepository> _addedDocumentRepository;
        private Mock<IPeninsulaLog> _log;
        
        [SetUp]
        public void SetUp()
        {
            _documentRepository = new Mock<IDocumentRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _log = new Mock<IPeninsulaLog>();
            _taskDocumentRepository = new Mock<ITaskDocumentRepository>();
            _riskAssessmentDocumentRepository = new Mock<IRiskAssessmentDocumentRepository>();
            _addedDocumentRepository = new Mock<IAddedDocumentRepository>();
        }

        [Test]
        public void Given_valid_request_When_mark_document_as_deleted_Then_should_call_correct_methods()
        {

            // Given
            var request = new MarkDocumentAsDeletedRequest
            {
                                  CompanyId = 1,
                                  DocumentId = 200,
                                  UserId = Guid.NewGuid()
                              };


            var target = CreateEmployeeService();
            var documentMock = new Mock<Document>();

            _documentRepository
                .Setup(x => x.GetByIdAndCompanyId(request.DocumentId, request.CompanyId))
                .Returns(documentMock.Object);


            _userRepository.Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId)).Returns(new UserForAuditing { Id = request.UserId });

            // When
            target.MarkDocumentAsDeleted(request);

            // Then
            _documentRepository.Verify(x => x.SaveOrUpdate(documentMock.Object));
            documentMock.Verify(x => x.MarkForDelete(It.Is<UserForAuditing>(y => y.Id == request.UserId)));
        }
        
        private DocumentService CreateEmployeeService()
        {
            return new DocumentService(_log.Object, _documentRepository.Object, _userRepository.Object
                , _taskDocumentRepository.Object, _riskAssessmentDocumentRepository.Object, _addedDocumentRepository.Object);
        }
    }
}