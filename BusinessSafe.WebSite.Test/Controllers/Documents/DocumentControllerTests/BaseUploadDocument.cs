using System.Collections.Generic;
using System.Web;

using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.Documents;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Infrastructure.Security;
using BusinessSafe.WebSite.Areas.Documents.Controllers;
using BusinessSafe.WebSite.DocumentLibraryService;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.StreamingClientDocumentService;
using BusinessSafe.WebSite.StreamingDocumentLibraryService;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Documents.DocumentControllerTests
{
    [TestFixture]
    public class BaseUploadDocument
    {
        protected DocumentController _target;
        protected Mock<ILookupService> _lookupService;
        protected Mock<IDocumentLibraryService> _docLibraryService;
        protected Mock<IStreamingDocumentLibraryService> _streamingDocLibraryService;
        protected Mock<IDocumentLibraryUploader> _documentLibraryUploader;
        protected Mock<ISiteService> _siteService;
        protected Mock<IDocumentService> _documentService;
        protected Mock<IStreamingClientDocumentService> _streamingClientDocService;
        protected Mock<IPeninsulaLog> _log;
        protected List<long> _allowedSites;
        protected const long _returnedDocLibraryId = 1234;

        [SetUp]
        public virtual void Setup()
        {
            _lookupService = new Mock<ILookupService>();
            _lookupService.Setup(x => x.GetDocumentTypes()).Returns(new List<LookupDto>());
            
            _documentLibraryUploader = new Mock<IDocumentLibraryUploader>();

            _docLibraryService = new Mock<IDocumentLibraryService>();
            _streamingDocLibraryService = new Mock<IStreamingDocumentLibraryService>();

            _siteService = new Mock<ISiteService>();
            _documentService = new Mock<IDocumentService>();
            _streamingClientDocService = new Mock<IStreamingClientDocumentService>();
            _log = new Mock<IPeninsulaLog>();

            _allowedSites = new List<long>() { 123, 456, 789 };

            _target = GetTarget();
        }

        protected DocumentController GetTarget()
        {
            var controller = new DocumentController(
                _lookupService.Object,
                _docLibraryService.Object,
                _streamingDocLibraryService.Object,
                _documentLibraryUploader.Object,
                _siteService.Object,
                _documentService.Object,
                _streamingClientDocService.Object,
                _log.Object);
            return TestControllerHelpers.AddUserWithDefinableAllowedSitesToController(controller, _allowedSites);
        }

        protected Mock<DocumentController> GetTargetMocked()
        {
            object[] args = {
                                _lookupService.Object,
                                _docLibraryService.Object,
                                _streamingDocLibraryService.Object,
                                _documentLibraryUploader.Object,
                                _siteService.Object,
                                _documentService.Object,
                                _streamingClientDocService.Object,
                                _log.Object
                            };

            var controller = new Mock<DocumentController>(args);

            return controller;
        }
    }
}
