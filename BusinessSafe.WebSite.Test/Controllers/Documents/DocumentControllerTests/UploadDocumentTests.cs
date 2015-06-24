using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.ViewModels;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Documents.DocumentControllerTests
{
    [TestFixture]
    public class UploadDocumentTests : BaseUploadDocument
    {
        private Mock<HttpPostedFileBase> _uploadedFile;

        private DocumentUploadButtonViewModel _viewModel;
        private const string _uploadedFileName = "my uploaded file";

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            _documentLibraryUploader
                .Setup(x => x.Upload(It.IsAny<string>(), It.IsAny<Stream>()))
                .Returns(_returnedDocLibraryId);

            _uploadedFile = new Mock<HttpPostedFileBase>();
            _uploadedFile.Setup(x => x.FileName).Returns(_uploadedFileName);

            _viewModel = new DocumentUploadButtonViewModel()
            {
                CanEditDocumentType = true,
                DocumentOriginTypeId = 1,
                DocumentTypeId = 2,
                LastUploadedDocumentFilename = "",
                File = _uploadedFile.Object
            };
        }

        [Test]
        public void Given_UploadDocument_When_called_Then_returns_correct_view()
        {
            // Given
            const string expectedViewName = "~/Areas/Documents/Views/Document/DocumentUploadButton.cshtml";
            var target = GetTarget();

            // When
            var result =
                target.UploadDocument(_viewModel) as ViewResult;

            // Then
            Assert.That(result.ViewName, Is.EqualTo(expectedViewName));
        }

        [Test]
        public void Given_UploadDocument_When_called_Then_returns_correct_view_model()
        {
            // Given
            var target = GetTarget();

            // When
            var result =
                target.UploadDocument(_viewModel) as ViewResult;
            var model = result.Model as DocumentUploadButtonViewModel;

            // Then
            Assert.That(result.Model, Is.InstanceOf<DocumentUploadButtonViewModel>());
            Assert.That(model.CanEditDocumentType, Is.EqualTo(_viewModel.CanEditDocumentType));
            Assert.That(model.DocumentOriginTypeId, Is.EqualTo(_viewModel.DocumentOriginTypeId));
            Assert.That(model.DocumentTypeId, Is.EqualTo(_viewModel.DocumentTypeId));
            Assert.That(model.LastUploadedDocumentLibraryId, Is.EqualTo(_returnedDocLibraryId));
            Assert.That(model.DocumentUploaded, Is.True);
            Assert.That(model.LastUploadedDocumentFilename, Is.EqualTo(_uploadedFileName));
        }

        [Test]
        public void Given_UploadDocument_When_called_Then_uses_IDocumentLibraryUploader()
        {
            // Given
            var target = GetTarget();

            // When
            var result =
                target.UploadDocument(_viewModel) as ViewResult;

            // Then
            _documentLibraryUploader.Verify(x => x.Upload(It.IsAny<string>(), It.IsAny<Stream>()));
        }

        [Test]
        public void When_exception_thrown_UploadDocument_logs_exception()
        {
            // Given
            var thrownException = new Exception();
            _log.Setup(x => x.Add(It.IsAny<Exception>()));

            _documentLibraryUploader
                .Setup(x => x.Upload(It.IsAny<string>(), It.IsAny<Stream>()))
                .Throws(thrownException);

            var target = GetTargetMocked();
            target.Setup(x => x.LogExceptionInElmah(It.IsAny<Exception>()));

            // When
            var result = target.Object.UploadDocument(_viewModel) as ViewResult;

            // Then
            _log.Verify(x => x.Add(thrownException), Times.Once());
            target.Verify(x=> x.LogExceptionInElmah(thrownException),Times.Once());
        }

        [Test]
        public void When_exception_thrown_UploadDocument_passes_exception_details_to_view_model()
        {
            // Given
            var thrownException = new Exception("Exception Message");
            _log.Setup(x => x.Add(It.IsAny<Exception>()));

            _documentLibraryUploader
                .Setup(x => x.Upload(It.IsAny<string>(), It.IsAny<Stream>()))
                .Throws(thrownException);

            var targetMocked = GetTargetMocked();
            targetMocked.Setup(x => x.LogExceptionInElmah(It.IsAny<Exception>()));

            var target = TestControllerHelpers.AddUserWithDefinableAllowedSitesToController(GetTargetMocked().Object, _allowedSites);

            // When
            var result = target.UploadDocument(_viewModel) as ViewResult;

            // Then
            Assert.That(_viewModel.Error, Is.True);
            Assert.That(_viewModel.ErrorMessage, Is.EqualTo(thrownException.Message));
        }
    }
}
