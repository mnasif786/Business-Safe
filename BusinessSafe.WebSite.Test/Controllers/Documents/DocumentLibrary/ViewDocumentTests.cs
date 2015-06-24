using System.IO;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.Documents;
using BusinessSafe.Application.Custom_Exceptions;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.StreamingClientDocumentService;
using BusinessSafe.WebSite.StreamingDocumentLibraryService;
using BusinessSafe.WebSite.Tests.Controllers.Documents.DocumentControllerTests;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Documents.DocumentLibrary
{
    [TestFixture]
    public class ViewDocumentTests : BaseUploadDocument
    {
        private readonly string _encryptedDocId1 = EncryptionHelper.Encrypt("documentId=1");

        [Test]
        public void When_Document_Is_Requested_Then_Return_FileContentResult_For_Viewing_Document()
        {
            // Given
            var metadata = new DocumentDto()
                               {
                                   Extension = "txt",
                                   Filename = "Test"
                               };
            Stream stream = new MemoryStream();
            var file = new GetStreamedDocumentByIdResponse()
                           {
                               MetaData = metadata,
                               Content = stream
                           };
            _streamingDocLibraryService
                .Setup(x => x.GetStreamedDocumentById(It.IsAny<GetStreamedDocumentByIdRequest>()))
                .Returns(file);

            // When
            var result = _target.DownloadDocument(_encryptedDocId1);

            // Then
            Assert.That(result, Is.InstanceOf<FileStreamResult>());
        }

        [Test]
        public void When_Document_Is_Requested_Then_FileBytes_Is_Populated()
        {
            // Given
            var metadata = new DocumentDto()
            {
                Extension = "txt",
                Filename = "Test"
            };
            Stream stream = new MemoryStream();
            var file = new GetStreamedDocumentByIdResponse()
            {
                MetaData = metadata,
                Content = stream
            };
            _streamingDocLibraryService
                .Setup(x => x.GetStreamedDocumentById(It.IsAny<GetStreamedDocumentByIdRequest>()))
                .Returns(file);

            // When
            var result = _target.DownloadDocument(_encryptedDocId1);

            // Then
            Assert.That(result.FileStream, Is.Not.Null);
        }

        [Test]
        public void When_Document_Is_requested_Then_Should_Call_Correct_Methods()
        {
            // Given
            var metadata = new DocumentDto()
            {
                Extension = "txt",
                Filename = "Test"
            };

            Stream stream = new MemoryStream();

            var file = new GetStreamedDocumentByIdResponse()
            {
                MetaData = metadata,
                Content = stream
            };

            _streamingDocLibraryService
                .Setup(x => x.GetStreamedDocumentById(It.IsAny<GetStreamedDocumentByIdRequest>()))
                .Returns(file);

            // When
            _target.DownloadDocument(_encryptedDocId1);

            // Then
            _streamingDocLibraryService.VerifyAll();
            _documentService.Verify(x => x.ValidateDocumentForCompany(It.IsAny<long>(), It.IsAny<long>()));
        }

        [Test]
        public void When_Document_Is_Requested_Where_The_ClientId_Is_Different_From_Users_ClientId_Then_Should_Throw_Exception()
        {
            // Given
            var response = new GetStreamedClientDocumentByIdResponse()
                               {
                                   MetaData = new ClientDocumentDto()
                                   {
                                       ClientId = 999
                                   }
                               };

            _streamingClientDocService.Setup(x => x.GetById(It.IsAny<GetStreamedClientDocumentByIdRequest>())).Returns(response);

            // Then
            Assert.Throws<InvalidDocumentForCompanyException>(() => _target.DownloadClientDocument(_encryptedDocId1));
        }
    }
}