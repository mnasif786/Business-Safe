using System.IO;
using System.Web;
using BusinessSafe.Infrastructure.Security;
using BusinessSafe.WebSite.DocumentLibraryService;
using BusinessSafe.WebSite.Helpers;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Helpers
{
    [TestFixture]
    public class DocumentLibraryUploaderTests
    {
        private Mock<ISaveFileStreamHelper> _saveFileStreamHelper;
        private DocumentLibraryUploader _target;
        private Mock<IImpersonator> _impersonator;
        private Mock<IDocumentLibraryService> _docService;
        private string _expectedFilename;

        [SetUp]
        public void Setup()
        {
            _saveFileStreamHelper = new Mock<ISaveFileStreamHelper>();
            _saveFileStreamHelper.Setup(x => x.Write(It.IsAny<string>(), It.IsAny<Stream>()));

            _expectedFilename = "my filename";
            _impersonator = new Mock<IImpersonator>();
            _impersonator
                .Setup(x => x.ImpersonateValidUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            _docService = new Mock<IDocumentLibraryService>();

            _docService
                .Setup(x => x.CreateDocumentFromPath(It.IsAny<CreateDocumentFromPathRequest>()))
                .Returns(new CreateDocumentFromPathResponse() { DocumentId = It.IsAny<long>() });

            _target = GetTarget();
        }

        [Test]
        public void Given_upload_When_called_Then_impersonate_user()
        {
            // Given

            // When
            _target.Upload(It.IsAny<string>(), It.IsAny<Stream>());

            // Then
            _impersonator.Verify(x => x.ImpersonateValidUser("Network.Monitor", "HQ", "WBS9CNrr1YzIFNRx8Wtx5tZ7UsIs5jgV4yBuP3nAFuM="), Times.Once());
        }

        [Test]
        public void Given_upload_When_called_Then_CreateDocumentFromPath()
        {
            // Given
            var passedCreateDocumentFromPathRequest = new CreateDocumentFromPathRequest();

            _docService
                .Setup(x => x.CreateDocumentFromPath(It.IsAny<CreateDocumentFromPathRequest>()))
                .Callback<CreateDocumentFromPathRequest>(y => passedCreateDocumentFromPathRequest = y)
                .Returns(new CreateDocumentFromPathResponse()
                         {
                             DocumentId = It.IsAny<long>()
                         });

            // When
            _target.Upload(It.IsAny<string>(), It.IsAny<Stream>());

            // Then
            _docService.Verify(x => x.CreateDocumentFromPath(It.IsAny<CreateDocumentFromPathRequest>()), Times.Once());
            Assert.That(passedCreateDocumentFromPathRequest.ApplicationId, Is.EqualTo(6));
            //Assert.That(passedCreateDocumentFromPathRequest.Filename, Is.EqualTo(_expectedFilename));
            Assert.That(passedCreateDocumentFromPathRequest.FilePath, Is.InstanceOf<string>());
        }

        [Test]
        [TestCase(1)]
        [TestCase(100)]
        [TestCase(999)]
        [TestCase(458)]
        public void Given_upload_When_called_Then_return_document_id_from_docService(long docId)
        {
            // Given
            _docService
                .Setup(x => x.CreateDocumentFromPath(It.IsAny<CreateDocumentFromPathRequest>()))
                .Returns(new CreateDocumentFromPathResponse() { DocumentId = docId });

            // When
            var result = _target.Upload(It.IsAny<string>(), It.IsAny<Stream>());

            // Then
            Assert.That(result, Is.EqualTo(docId));
        }

        [Test]
        public void Given_upload_When_called_Then_SaveFileStreamHelper_is_called()
        {
            // Given

            // When
            var result = _target.Upload(It.IsAny<string>(), It.IsAny<Stream>());

            // Then
            _saveFileStreamHelper.Verify(x => x.Write(It.IsAny<string>(), It.IsAny<Stream>()));
        }

        private Mock<HttpPostedFileBase> GetPostedFile(string myFilename)
        {

            var model = new Mock<HttpPostedFileBase>();
            model.Setup(x => x.FileName).Returns(_expectedFilename);
            model.Setup(x => x.InputStream).Returns(It.IsAny<FileStream>());
            return model;
        }

        private DocumentLibraryUploader GetTarget()
        {
            return new DocumentLibraryUploader(_impersonator.Object, _docService.Object, _saveFileStreamHelper.Object);
        }
    }
}
